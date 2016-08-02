using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyModBus;
using ROSO.Model;
using System.IO;

namespace ROSO.BLL
{
    /// <summary>
    /// 获取设备对象列表的基类
    /// </summary>
    public class BL_DeviceList 
    {
        /// <summary>
        /// 连接对象
        /// </summary>
        protected static ModBusTCPIPWrapper wrapper = new ModBusTCPIPWrapper();      
       

        /// <summary>
        /// 读取设备源数据
        /// </summary>
        /// <param name="ConnectDataList">连接对象列表</param>
        /// <returns>源数据列表</returns>
        protected static List<byte[]> Read(List<ConnectData> ConnectDataList,out List<String> ErrorIPS)
        {
            //创建存储源数据的列表
            List<byte[]> ResultDataList = new List<byte[]>();

            ErrorIPS = new List<string>();
            //清空错误IP列表
            ErrorIPS.Clear();
            //遍历连接对象列表
            foreach (ConnectData connectData in ConnectDataList)
            {
                byte[] deviceByte = wrapper.Receive(connectData);
                //如果返回数据为空表示读取失败
                if (deviceByte == null)
                {
                    //记录读取失败的设备IP
                    ErrorIPS.Add(connectData.IP);
                    //继续下一次循环
                    continue;
                }
                //跟踪显示
                StreamWriter sw = new StreamWriter("data.txt", true);
                sw.WriteLine(connectData.IP);
                int i = 0;
                foreach (byte bt in deviceByte)
                {
                    if (i % 20 == 0) sw.WriteLine();
                    sw.Write("{0,4}",bt);
                    i++;
                }
                sw.WriteLine();
                sw.Close();
                //如果读取数据成功，就将数据添加的数据列表中
                ResultDataList.Add(deviceByte);
            }
           
            return ResultDataList;
        }

        /// <summary>
        /// 获取连接数据对象列表
        /// </summary>
        /// <param name="deviceInfoList">设备基本信息列表</param>
        /// <param name="port">端口号</param>
        /// <param name="startAddress">读取数据的开始地址</param>
        /// <param name="numRegister">需要读取的寄存器数量</param>
        /// <returns>连接对象列表
        /// </returns>
        protected static List<ConnectData> GetConnetData(List<DeviceInfo> deviceInfoList, int port, short startAddress, short numRegister)
        {
            List<ConnectData> ConnectDataList = new List<ConnectData>();
            foreach (DeviceInfo deviceInfo in deviceInfoList)
            {
                ConnectData connectData = new ConnectData(deviceInfo.DeviceIP, port, startAddress, numRegister);
                ConnectDataList.Add(connectData);
                
            }
            return ConnectDataList;
        }


        /// <summary>
        /// 返回设备错误的详细信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected static string GetFaultMessage(string p,Dictionary<int,string> errorStringDic)
        {
            //Dictionary<int, string> ErrorStringDic = GetErrorStringDic();
            string errorsString = "";
            for (int i = 0; i < errorStringDic.Count; i++)
            {
                if (p[i] == '1')
                {
                    errorsString += errorStringDic[i] + "|";
                }
            }
            return errorsString;
        }
    }
    
}
