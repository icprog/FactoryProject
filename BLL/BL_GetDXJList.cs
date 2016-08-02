using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyModBus;
using ROSO.Model;

namespace ROSO.BLL
{
    public class BL_GetDXJList:BL_DeviceList
    {
       
        /// <summary>
        /// 返回错误信息列表
        /// </summary>
        /// <returns></returns>
        private static Dictionary<int, string> GetErrorStringDic()
        {
            Dictionary<int, string> errorString = new Dictionary<int, string>();
            errorString.Add(0, "急停报警");
            errorString.Add(1, "气压不足报警");
            errorString.Add(2, "PLC电池电量低报警");
            errorString.Add(3, "PLC CAN总线故障");
            errorString.Add(4, "PLC 硬件故障");
            errorString.Add(5, "PLC 扩展模块故障");
            errorString.Add(6, "反转驱动报警");
            errorString.Add(7, "第一级驱动故障");
            errorString.Add(8, "第二级驱动故障");
            errorString.Add(9, "第三级驱动故障");
            errorString.Add(10, "第四级驱动故障");
            errorString.Add(11, "第五级驱动故障");
            errorString.Add(12, "循环驱动故障");
            errorString.Add(13, "鲜风驱动故障");
            errorString.Add(14, "出口驱动故障");
            errorString.Add(15, "上限位");
            errorString.Add(16, "下限位");
            errorString.Add(17, "打结报警");
            errorString.Add(18, "炉内中上部温度高");
            errorString.Add(19, "炉内左上部温度高");
            errorString.Add(20, "炉内右上部温度高");
            errorString.Add(21, "炉内中下部温度高");
            errorString.Add(22, "炉内左下部温度高");
            errorString.Add(23, "炉内右下部温度高");
            errorString.Add(24, "加热部温度高");
            errorString.Add(25, "出口温度高");
            errorString.Add(26, "炉内通道1温度高");
            errorString.Add(27, "炉内通道2温度高");
            errorString.Add(28, "交换部温度高");
            errorString.Add(29, "集油器1故障");
            errorString.Add(30, "集油器2故障");
            errorString.Add(31, "自动灭火器故障");
            errorString.Add(32, "加热管损坏过多,负载不平衡");
            errorString.Add(33, "10K加热组三相不平衡");
            errorString.Add(34, "20K-1加热组三相不平衡");
            errorString.Add(35, "20K-2加热组三相不平衡");
            errorString.Add(36, "30K加热组三相不平衡");
            errorString.Add(37, "40K加热组三相不平衡");
            return errorString;
        }


        /// <summary>
        /// 获取绕带机对象列表
        /// </summary>
        /// <param name="deviceInfoList">设备基本信息列表</param>
        /// <param name="port">端口号</param>
        /// <param name="startAddress">读取数据的开始地址</param>
        /// <param name="numRegister">需要读取的寄存器数量</param>
        /// <param name="deviceTemplateList">DXJ数据模板</param>
        /// <param name="isHour">是否整点，把设备状态写入数据库，true表示写入，默认值为false</param>
        /// <returns></returns>
        public static List<DXJ> GetDXJList(List<DeviceInfo> deviceInfoList, int port, short startAddress, short numRegister, List<DeviceTemplate> deviceTemplateList,bool isHour=false)
        {
            //获取错误状态解析词典
            Dictionary<int, string> errorStringDic = GetErrorStringDic();

            //创建连接对象列表
            List<ConnectData> ConnectDataList = GetConnetData(deviceInfoList, port, startAddress, numRegister);
            //错误ip列表
            List<String> ErrorIPS;
            //获取设备源数据
            List<byte[]> ResultDataList = Read(ConnectDataList, out ErrorIPS);
            //创建绕带机对象列表
            List<DXJ> DXJList = new List<DXJ>();
            //判断是否有数据，有则处理。
            if (ResultDataList.Count > 0)
            {
                //循环遍历数据列表，获取每台设备的数据
                foreach (byte[] resultData in ResultDataList)
                {
                    //创建RDJ实例对象
                    DXJ dxj = DXJConvertData(resultData, deviceTemplateList);
                    //添加到列表中
                    DXJList.Add(dxj);
                }
            }
            //判断是否有出现读取错误的设备，有则处理
            if (ErrorIPS.Count > 0)
            {
                //遍历错误对象列表
                for (int i = 0; i < ErrorIPS.Count; i++)
                {
                    DeviceInfo deviceInfo = BL_DeviceInfo.GetDeviceInfoByIP(ErrorIPS[i]);
                    DXJ errorDXJ = new DXJ();
                    errorDXJ.SBBH = deviceInfo.DeviceID;
                    errorDXJ.SBYXZT = (short)9;
                    errorDXJ.SBZDXX = "网络故障";
                    //添加到RDJ对象列表的后面
                    DXJList.Add(errorDXJ);
                }
            }
             //生成设备状态对象添加到数据库
            foreach (DXJ dxj in DXJList)
            {
                //创建设备状态对象
                DeviceState ds = GetDeviceState(dxj, errorStringDic);
                //调用胡老师的状态判断函数
                if (isHour)                
                    BL_DeviceState.AddDeviceState(ds, DateTime.Now);   
                else
                    BL_DeviceState.AddDeviceState(ds);
                
            }

            return DXJList;
        }
        
        /// <summary>
        /// 将DXJ的源数据转换
        /// </summary>
        /// <param name="resultData">源数据</param>
        /// <param name="project">项目名称</param>
        /// <param name="startAddress">数据起始地址</param>
        /// <param name="length">数据长度</param>
        /// <returns></returns>
        private static DXJ DXJConvertData(byte[] resultData, List<DeviceTemplate> deviceTemplateList)
        {
            DXJ dxj = new DXJ();
            foreach (DeviceTemplate deviceTamplate in deviceTemplateList)
            {
                switch (deviceTamplate.Project)
                {
                    case "设备型号":
                        dxj.SBXH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "设备规格":
                        dxj.SBGG = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "设备编号":
                        dxj.SBBH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "备用":
                        dxj.BY = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "设备运行状态":
                        dxj.SBYXZT = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "设备诊断信息":
                        dxj.SBZDXX = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length - 108, 2);
                        dxj.PLCZT = ReadData.GetIntArrayData(resultData, deviceTamplate.Address + 10, deviceTamplate.Length - 20, 1);
                        break;
                    case "反转马达速度":
                        dxj.FZMDSD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "第一级速度":
                        dxj.SD1 = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "第二级速度":
                        dxj.SD2 = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "第三级速度":
                        dxj.SD3 = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "第四级速度":
                        dxj.SD4 = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "第五级速度":
                        dxj.SD5 = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "循环马达速度":
                        dxj.XHMDSD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "出口马达速度":
                        dxj.CKMDSD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "鲜风马达速度":
                        dxj.XFMDSD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "上左温度":
                        dxj.ULWD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "上中温度":
                        dxj.UMWD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "上右温度":
                        dxj.URWD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "下左温度":
                        dxj.DLWD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "下中温度":
                        dxj.DMWD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "下右温度":
                        dxj.DRWD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "炉内温度1":
                        dxj.LNWD1 = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "炉内温度2":
                        dxj.LNWD2 = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "加热部温度":
                        dxj.JRBWD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "出口温度":
                        dxj.CKWD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "交换部温度":
                        dxj.JHBWD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "加热功率":
                        dxj.JRGL = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    default:
                        break;
                }
            }
            return dxj;
        }
           

        /// <summary>
        /// 返回设备状态对象
        /// </summary>
        /// <param name="dxj">设备对象</param>
        /// <param name="errorStringDic">错误解析表</param>
        /// <returns></returns>
        private static DeviceState GetDeviceState(DXJ dxj,Dictionary<int, string> errorStringDic)
        {
            DeviceState ds = new DeviceState();
            ds.DAQTime = DateTime.Now;
            ds.DeviceID = dxj.SBBH;
            
            ds.VatID = 0;
            ds.OperatingState = dxj.SBYXZT;
            ds.FaultMessage = GetFaultMessage(dxj.SBZDXX,errorStringDic);
            dxj.SBZDXX = ds.FaultMessage;
            return ds;
        }



    }
}
