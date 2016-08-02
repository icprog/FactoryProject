using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ROSO.Model;
using MyModBus;

namespace ROSO.BLL
{
    public class BL_GetRDJList:BL_DeviceList
    {
        /// <summary>
        /// 设置错误信息列表
        /// </summary>
        /// <returns></returns>
        private static Dictionary<int, string> GetErrorStringDic()
        {
            Dictionary<int, string>  errorStringDic = new Dictionary<int, string>();
            errorStringDic.Add(0, "急停报警");
            errorStringDic.Add(1, "气压不足报警");
            errorStringDic.Add(2, "PLC电池电量低报警");
            errorStringDic.Add(3, "PLC CAN总线故障");
            errorStringDic.Add(4, "PLC 硬件故障");
            errorStringDic.Add(5, "PLC 扩展模块故障");
            errorStringDic.Add(6, "缓冲器上限位");
            errorStringDic.Add(7, "缓冲器下限位");
            errorStringDic.Add(8, "左限位报警");
            errorStringDic.Add(9, "右限位报警");
            errorStringDic.Add(10, "卷带变频器报警");
            errorStringDic.Add(11, "横杠伺服报警");
            errorStringDic.Add(12, "进料变频器故障");
            errorStringDic.Add(13, "布带打结报警");
            return errorStringDic;
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
        public static List<RDJ> GetRDJList(List<DeviceInfo> deviceInfoList, int port, short startAddress, short numRegister, List<DeviceTemplate> deviceTemplateList,bool isHour=false)
        {
            //获取错误状态解析词典
            Dictionary<int, string> errorStringDic=GetErrorStringDic();
            //创建连接对象列表
            List<ConnectData> ConnectDataList = GetConnetData(deviceInfoList, port, startAddress, numRegister);
            //错误ip列表
            List<String> ErrorIPS;
            //获取设备源数据
            List<byte[]> ResultDataList = Read(ConnectDataList,out ErrorIPS);

            //创建绕带机对象列表
            List<RDJ> RDJList = new List<RDJ>();
            //判断是否有数据，有则处理。
            if (ResultDataList.Count > 0)
            {
                //循环遍历数据列表，获取每台设备的数据
                foreach (byte[] resultData in ResultDataList)
                {
                    //创建RDJ实例对象
                    RDJ rdj = RDJConvertData(resultData, deviceTemplateList);
                    //添加到列表中
                    RDJList.Add(rdj);
                }
            }
            //判断是否有出现读取错误的设备，有则处理
            if (ErrorIPS.Count > 0)
            {
                //遍历错误对象列表
                for (int i = 0; i < ErrorIPS.Count; i++)
                {
                    
                    DeviceInfo deviceInfo = BL_DeviceInfo.GetDeviceInfoByIP(ErrorIPS[i]);
                    RDJ errorRDJ = new RDJ();
                    errorRDJ.SBBH = deviceInfo.DeviceID;
                    errorRDJ.SBYXZT = (short)9;
                    errorRDJ.SBZDXX = "网络故障";
                    //添加到RDJ对象列表的后面
                    RDJList.Add(errorRDJ);
                }
            }

            //生成设备状态对象添加到数据库
            foreach (RDJ rdj in RDJList)
            {
                //创建设备状态对象
                DeviceState ds = GetDeviceState(rdj, errorStringDic);
                //调用胡老师的状态判断函数
                if (isHour)                
                    BL_DeviceState.AddDeviceState(ds, DateTime.Now);   
                else
                    BL_DeviceState.AddDeviceState(ds);
                
            }

            return RDJList;
        }

        /// <summary>
        /// 将RDJ的源数据转换
        /// </summary>
        /// <param name="resultData">源数据</param>
        /// <param name="project">项目名称</param>
        /// <param name="startAddress">数据起始地址</param>
        /// <param name="length">数据长度</param>
        /// <returns></returns>
        private static RDJ RDJConvertData(byte[] resultData, List<DeviceTemplate> deviceTemplateList)
        {
            RDJ rdj = new RDJ();
            foreach (DeviceTemplate deviceTamplate in deviceTemplateList)
            {
                switch (deviceTamplate.Project)
                {
                    case "设备型号":
                        rdj.SBXH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "设备规格":
                        rdj.SBGG = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "设备编号":
                        rdj.SBBH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "备用":
                        rdj.BY = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "设备运行状态":
                        rdj.SBYXZT = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "设备诊断信息":
                        rdj.SBZDXX = ReadData.GetStringData(resultData, deviceTamplate.Address, deviceTamplate.Length - 108, 2);
                        rdj.PLCZT = ReadData.GetIntArrayData(resultData, deviceTamplate.Address + 10, deviceTamplate.Length - 20, 1);
                        break;
                    case "布带速度":
                        rdj.BDSD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "横杆速度":
                        rdj.HGSD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "卷筒速度":
                        rdj.JTSD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "进料电机速度":
                        rdj.JLDJSD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "布带长度":
                        rdj.BLCD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "张力值":
                        rdj.ZLZ = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "缓冲器高度":
                        rdj.HCQGD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "操作人编号":
                        rdj.CZRBH = ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "卷筒编号":
                        rdj.JTBH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "款号":
                        rdj.KH = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    case "最终长度":
                        rdj.ZZCD = (short)ReadData.GetData(resultData, deviceTamplate.Address, deviceTamplate.Length);
                        break;
                    default:
                        break;
                }
            }
            return rdj;
        }

        /// <summary>
        /// 返回设备状态对象
        /// </summary>
        /// <param name="dxj">设备对象</param>
        /// <param name="errorStringDic">错误解析表</param>
        /// <returns></returns>
        private static DeviceState GetDeviceState(RDJ rdj, Dictionary<int, string> errorStringDic)
        {
            DeviceState ds = new DeviceState();
            ds.DAQTime = DateTime.Now;
            ds.DeviceID = rdj.SBBH;
            ds.OperatorID = (short)rdj.CZRBH;
            ds.VatID = 0;
            ds.OperatingState = rdj.SBYXZT;
            ds.FaultMessage = GetFaultMessage(rdj.SBZDXX,errorStringDic);
            rdj.SBZDXX = ds.FaultMessage;
            return ds;
        }

       
    }
}

