using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyModBus;
using ROSO.Model;

namespace ROSO.BLL
{
    class BL_GetRSJList:BL_DeviceList
    {
        /// <summary>
        /// 错误信息列表
        /// </summary>
        //private static Dictionary<int, string> errorString;

        /// <summary>
        /// 错误IP列表
        /// </summary>
        //private static List<String> ErrorIPS = new List<string>();

        /// <summary>
        /// 返回错误信息列表
        /// </summary>
        /// <returns></returns>
        private static Dictionary<int, string> GetErrorStringDic()
        {
            Dictionary<int, string> errorString;
            errorString = new Dictionary<int, string>();
            errorString.Add(0, "急停报警");
            errorString.Add(1, "气压不足报警");
            errorString.Add(2, "PLC电池电量低报警");
            errorString.Add(3, "PLC CAN总线故障");
            errorString.Add(4, "PLC 硬件故障");
            errorString.Add(5, "PLC 扩展模块故障");
            errorString.Add(6, "排水阀故障");
            errorString.Add(7, "泄气阀故障");
            errorString.Add(8, "左换向故障");
            errorString.Add(9, "右换向故障");
            errorString.Add(10, "泵变频器报警");
            errorString.Add(11, "染缸超温");
            errorString.Add(12, "染缸过压");
            errorString.Add(13, "染缸门未关闭");
            errorString.Add(14, "启动时染缸门未锁定");
            for (int j = 15; j < 32; j++)
            {
                errorString.Add(j, "备用");
            }
            return errorString;
        }

        /// <summary>
        /// 获取绕带机对象列表
        /// </summary>
        /// <param name="deviceInfoList">设备基本信息列表</param>
        /// <param name="port">端口号</param>
        /// <param name="startAddress">读取数据的开始地址</param>
        /// <param name="numRegister">需要读取的寄存器数量</param>
        /// <param name="deviceTemplateList">RDJ数据模板</param>
        /// <param name="isHour">是否整点，把设备状态写入数据库，true表示写入，默认值为false</param>
        /// <returns></returns>
        public static List<RSJ> GetRSJList(List<DeviceInfo> deviceInfoList, int port, short startAddress, short numRegister, List<DeviceTemplate> deviceTemplateList,bool isHour=false)
        {
            //1.获取错误状态解析词典
            Dictionary<int, string> errorStringDic=GetErrorStringDic();
            
            //2.创建连接对象列表
            List<ConnectData> ConnectDataList = GetConnetData(deviceInfoList, port, startAddress, numRegister);

            //3.错误ip列表
            List<String> ErrorIPS;
            //4.获取设备源数据
            List<byte[]> ResultDataList = Read(ConnectDataList, out ErrorIPS);
            //5.创建绕带机对象列表
            List<RSJ> RSJList = new List<RSJ>();

            //6.判断是否有数据，有则处理。
            if (ResultDataList.Count > 0)
            {
                //循环遍历数据列表，获取每台设备的数据
                foreach (byte[] resultData in ResultDataList)
                {
                    //创建RDJ实例对象
                    RSJ rsj = RSJConvertData(resultData, deviceTemplateList);
                    //根据控制字改变设备运行状态
                    rsj = SetKZZ(rsj);                  
                    //添加到列表中
                    RSJList.Add(rsj);
                }
            }
            //7.判断是否有出现读取错误的设备，有则处理
            if (ErrorIPS.Count > 0)
            {
                //遍历错误对象列表
                for (int i = 0; i < ErrorIPS.Count; i++)
                {
                    DeviceInfo deviceInfo = BL_DeviceInfo.GetDeviceInfoByIP(ErrorIPS[i]);
                    RSJ errorRSJ = new RSJ();                    
                    errorRSJ.RGS = new List<RG>();
                    //如果染色机链接不上，则添加5个钢对象，全部设置为9网络无法链接
                    for(int n=0;n<5;n++)
                    {
                        RG r = new RG();
                        r.RGBH = (short)(n + 1);
                        r.SBYXZT = (short)9;
                        errorRSJ.RGS.Add(r);
                    }
                    errorRSJ.SBBH = deviceInfo.DeviceID;                    
                    errorRSJ.SBZDXX = "网络故障";
                    //添加到RDJ对象列表的后面
                    RSJList.Add(errorRSJ);
                }
            }
            //8.将状态添加到数据库
            foreach (RSJ rsj in RSJList)
            {
                List<DeviceState> errorList = GetDeviceState(rsj, errorStringDic);
                //调用胡老师的状态判断函数
                foreach (DeviceState errords in errorList)
                {
                    //如果为真，表明调用整点数据直接写到数据库
                    if (isHour)
                        BL_DeviceState.AddDeviceState(errords, DateTime.Now);
                    else
                        BL_DeviceState.AddDeviceState(errords);
                }
            }
            return RSJList;
        }

        /// <summary>
        /// 将RSJ的源数据转换
        /// </summary>
        /// <param name="resultData">源数据</param>
        /// <param name="project">项目名称</param>
        /// <param name="startAddress">数据起始地址</param>
        /// <param name="length">数据长度</param>
        /// <returns></returns>
        private static RSJ RSJConvertData(byte[] resultData, List<DeviceTemplate> deviceTemplateList)
        {
            RSJ rsj = new RSJ();
            #region 初始化染缸对象
            DeviceTemplate dvTemplate= BL_DeviceTemplate.GetDeviceTemplate(deviceTemplateList, "染缸数量");
           int gangNum = rsj.RGSL = (short)ReadData.GetData(resultData, dvTemplate.Address, dvTemplate.Length);
           List<RG> listRG =new List<RG>();
            for (int i = 0; i < gangNum; i++)
            {
                RG rg = new RG();
                rg.RGBH = (short)(i + 1);
                listRG.Add(rg);
            }
            #endregion

            #region 染色机数据封装
            foreach (DeviceTemplate deviceTamplate in deviceTemplateList)
            {
                
                switch (deviceTamplate.Project)
                {
                    case "设备型号":
                        rsj.SBXH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "设备诊断信息":                       
                        rsj.PLCZT = ReadData.GetIntArrayData(resultData, deviceTamplate.Address + 10, deviceTamplate.Length - 20, 1);
                         break;
                    case "设备规格":
                        rsj.SBGG = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "设备编号":
                        rsj.SBBH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "染缸数量":
                        rsj.RGSL = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "操作人编号":
                        rsj.CZRBH = ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    #region
                    //case "设备运行状态1":
                    //    rsj.RG[1].SBYXZT = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "设备运行状态2":
                    //    rsj.RG[2].SBYXZT = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "设备运行状态3":
                    //    rsj.RG[3].SBYXZT = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "设备运行状态4":
                    //    rsj.RG[4].SBYXZT = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "设备运行状态5":
                    //    rsj.RG[5].SBYXZT = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                   
                    //case "运行时间1":
                    //    rsj.RG[1].YXSJ = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "运行时间2":
                    //    rsj.RG[2].YXSJ = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "运行时间3":
                    //    rsj.RG[3].YXSJ = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "运行时间4":
                    //    rsj.RG[4].YXSJ = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "运行时间5":
                    //    rsj.RG[5].YXSJ = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "工艺段号1":
                    //    rsj.RG[1].GYDH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "工艺段号2":
                    //    rsj.RG[2].GYDH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "工艺段号3":
                    //    rsj.RG[3].GYDH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "工艺段号4":
                    //    rsj.RG[4].GYDH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "工艺段号5":
                    //    rsj.RG[5].GYDH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "工艺步号1":
                    //    rsj.RG[1].GYBH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "工艺步号2":
                    //    rsj.RG[2].GYBH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "工艺步号3":
                    //    rsj.RG[3].GYBH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "工艺步号4":
                    //    rsj.RG[4].GYBH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "工艺步号5":
                    //    rsj.RG[5].GYBH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "工艺名称1":
                    //    rsj.RG[1].GYMC = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "工艺名称2":
                    //    rsj.RG[2].GYMC = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "工艺名称3":
                    //    rsj.RG[3].GYMC = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "工艺名称4":
                    //    rsj.RG[4].GYMC = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "工艺名称5":
                    //    rsj.RG[5].GYMC = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "泵速度1":
                    //    rsj.RG[1].BSD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "泵速度2":
                    //    rsj.RG[2].BSD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "泵速度3":
                    //    rsj.RG[3].BSD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "泵速度4":
                    //    rsj.RG[4].BSD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "泵速度5":
                    //    rsj.RG[5].BSD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "温度1":
                    //    rsj.RG[1].WD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "温度2":
                    //    rsj.RG[2].WD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "温度3":
                    //    rsj.RG[3].WD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "温度4":
                    //    rsj.RG[4].WD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "温度5":
                    //    rsj.RG[5].WD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "压力1":
                    //    rsj.RG[1].YL = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "压力2":
                    //    rsj.RG[2].YL = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "压力3":
                    //    rsj.RG[3].YL = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "压力4":
                    //    rsj.RG[4].YL = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "压力5":
                    //    rsj.RG[5].YL = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "控制字11":
                    //    rsj.RG[1].KZZ1 = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length, 2);
                    //    break;
                    //case "控制字12":
                    //    rsj.RG[2].KZZ1 = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length, 2);
                    //    break;
                    //case "控制字13":
                    //    rsj.RG[3].KZZ1 = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length, 2);
                    //    break;
                    //case "控制字14":
                    //    rsj.RG[4].KZZ1 = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length, 2);
                    //    break;
                    //case "控制字15":
                    //    rsj.RG[5].KZZ1 = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length, 2);
                    //    break;
                    //case "控制字21":
                    //    rsj.RG[1].KZZ2 = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length, 2);
                    //    break;
                    //case "控制字22":
                    //    rsj.RG[2].KZZ2 = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length, 2);
                    //    break;
                    //case "控制字23":
                    //    rsj.RG[3].KZZ2 = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length, 2);
                    //    break;
                    //case "控制字24":
                    //    rsj.RG[4].KZZ2 = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length, 2);
                    //    break;
                    //case "控制字25":
                    //    rsj.RG[5].KZZ2 = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length, 2);
                    //    break;
                    //case "控制字31":
                    //    rsj.RG[1].KZZ3 = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length, 2);
                    //    break;
                    //case "控制字32":
                    //    rsj.RG[2].KZZ3 = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length, 2);
                    //    break;
                    //case "控制字33":
                    //    rsj.RG[3].KZZ3 = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length, 2);
                    //    break;
                    //case "控制字34":
                    //    rsj.RG[4].KZZ3 = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length, 2);
                    //    break;
                    //case "控制字35":
                    //    rsj.RG[5].KZZ3 = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length, 2);
                    //    break;
                    
                    //case "卷筒编号1":
                    //    rsj.RG[1].JTBH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "卷筒编号2":
                    //    rsj.RG[2].JTBH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "卷筒编号3":
                    //    rsj.RG[3].JTBH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "卷筒编号4":
                    //    rsj.RG[4].JTBH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    //case "卷筒编号5":
                    //    rsj.RG[5].JTBH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                    //    break;
                    #endregion
                    default:
                        break;
                }
            }
            #endregion

            #region 染缸数据封装
            
            DeviceTemplate dvtRGYXZT = BL_DeviceTemplate.GetDeviceTemplate(deviceTemplateList, "设备运行状态1");
            DeviceTemplate dvtRGZDXX = BL_DeviceTemplate.GetDeviceTemplate(deviceTemplateList, "设备诊断信息");
            //"设备诊断信息"

            DeviceTemplate dvtRGYXSJ = BL_DeviceTemplate.GetDeviceTemplate(deviceTemplateList, "运行时间1");

            DeviceTemplate dvtRGGYDH = BL_DeviceTemplate.GetDeviceTemplate(deviceTemplateList, "工艺段号1");
            DeviceTemplate dvtRGGYBH = BL_DeviceTemplate.GetDeviceTemplate(deviceTemplateList, "工艺步号1");
            DeviceTemplate dvtRGGYMC = BL_DeviceTemplate.GetDeviceTemplate(deviceTemplateList, "工艺名称1");
            DeviceTemplate dvtRGBSD = BL_DeviceTemplate.GetDeviceTemplate(deviceTemplateList, "泵速度1");
            DeviceTemplate dvtRGWD = BL_DeviceTemplate.GetDeviceTemplate(deviceTemplateList, "温度1");
            DeviceTemplate dvtRGYL = BL_DeviceTemplate.GetDeviceTemplate(deviceTemplateList, "压力1");
            DeviceTemplate dvtRGKZZ1 = BL_DeviceTemplate.GetDeviceTemplate(deviceTemplateList, "控制字11");
            DeviceTemplate dvtRGKZZ2 = BL_DeviceTemplate.GetDeviceTemplate(deviceTemplateList, "控制字21");
            DeviceTemplate dvtRGKZZ3 = BL_DeviceTemplate.GetDeviceTemplate(deviceTemplateList, "控制字31");
            DeviceTemplate dvtRGJTBH = BL_DeviceTemplate.GetDeviceTemplate(deviceTemplateList, "卷筒编号1");
           
            

            for (int i = 0; i < listRG.Count; i++)
            {
                listRG[i].SBYXZT = (short)ReadData.GetData(resultData, dvtRGYXZT.Address, dvtRGYXZT.Length + i*2);
                listRG[i].SBZDXX = ReadData.GetStringData(resultData, dvtRGZDXX.Address, dvtRGZDXX.Length + i * 4,2);

                listRG[i].YXSJ = (short)ReadData.GetData(resultData, dvtRGYXSJ.Address, dvtRGYXSJ.Length + i * 2);
                //染色机诊断信息
                listRG[i].GYDH = (short)ReadData.GetData(resultData, dvtRGGYDH.Address, dvtRGGYDH.Length + i * 2);
                listRG[i].GYBH = (short)ReadData.GetData(resultData, dvtRGGYBH.Address, dvtRGGYBH.Length + i * 2);
                listRG[i].GYMC = (short)ReadData.GetData(resultData, dvtRGGYMC.Address, dvtRGGYMC.Length + i * 4);
                listRG[i].BSD = (short)ReadData.GetData(resultData, dvtRGBSD.Address, dvtRGBSD.Length + i * 2);
                listRG[i].WD = (short)ReadData.GetData(resultData, dvtRGWD.Address, dvtRGWD.Length + i * 2);
                listRG[i].YL = (short)ReadData.GetData(resultData, dvtRGYL.Address, dvtRGYL.Length + i * 2);
                listRG[i].KZZ1 = ReadData.GetStringData(resultData, dvtRGKZZ1.Address, dvtRGKZZ1.Length + i * 2);
                listRG[i].KZZ2 = ReadData.GetStringData(resultData, dvtRGKZZ2.Address, dvtRGKZZ1.Length + i * 2);
                listRG[i].KZZ3 = ReadData.GetStringData(resultData, dvtRGKZZ3.Address, dvtRGKZZ1.Length + i * 2);
                listRG[i].JTBH = ReadData.GetData(resultData, dvtRGJTBH.Address, dvtRGJTBH.Length + i * 2);
               }

            rsj.RGS = listRG;
            #endregion
            
            return rsj;
        }

        /// <summary>
        /// 把染色机对象转换成设备状态
        /// </summary>
        /// <param name="rsj">染色机对象</param>
        /// <param name="num">缸号</param>
        /// <param name="SBYXZT">设备运行状态</param>
        /// <param name="errorString">错误词典</param>
        /// <returns></returns>
        private static List<DeviceState> GetDeviceState(RSJ rsj,Dictionary<int, string> errorString)
        {
            List<DeviceState> dsList = new List<DeviceState>();
            foreach(RG rg in rsj.RGS)
            {
            DeviceState ds=new DeviceState();
            ds.DAQTime = DateTime.Now;
            ds.DeviceID = rsj.SBBH;
            ds.VatID = (short)rg.RGBH;
            if (rg.SBYXZT == 9)
            {
                rsj.SBZDXX = "网络错误";
                //ds.OperatorID = (short)0;
            }
            else
            {
                ds.OperatorID = (short)rsj.CZRBH;
                ds.OperatingState = rg.SBYXZT;
                ds.FaultMessage = GetFaultMessage(rsj.SBZDXX, errorString, rg.RGBH);
                rsj.SBZDXX = ds.FaultMessage;
                dsList.Add(ds);
            }
            }
            return dsList;
        }

        /// <summary>
        /// 返回设备错误的详细信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected static string GetFaultMessage(string p, Dictionary<int, string> errorStringDic, int vatID)
        {
            //Dictionary<int, string> ErrorStringDic = GetErrorStringDic();
            string errorsString = "";
            for (int i = vatID * 32; i < 32; i++)
            {
                if (p[i] == '1')
                {
                    errorsString += errorStringDic[i] + "|";
                }
            }
            return errorsString;
        }

        /// <summary>
        /// 根据控制字改变设备运行状态
        /// </summary>
        /// <param name="rsj"></param>
        /// <returns></returns>
        private static RSJ SetKZZ(RSJ rsj)
        {
            foreach (RG rg in rsj.RGS)
            {
                if (rg.KZZ1[10] == '1')
                    rg.SBYXZT = 11;
                if (rg.KZZ2[12] == '1')
                    rg.SBYXZT = 12;
                else if (rg.KZZ2[13] == '1')
                    rg.SBYXZT = 13;
                else if (rg.KZZ2[15] == '1')
                    rg.SBYXZT = 14;
                else if (rg.KZZ2[16] == '1')
                    rg.SBYXZT = 15;
                if (rg.KZZ3[6] == '1')
                    rg.SBYXZT = 16;
                else if (rg.KZZ3[7] == '1')
                    rg.SBYXZT = 17;
                else if (rg.KZZ3[8] == '1')
                    rg.SBYXZT = 18;
            }
            return rsj;
        }
    }

}
