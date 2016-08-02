using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace MyModBus
{
    public enum FunctionCode : byte
    {
        /// <summary>
        /// Read Multiple Registers
        /// </summary>
        Read = 3,

        /// <summary>
        /// Write Multiple Registers
        /// </summary>
        Write = 16
    }

    public class ModBusTCPIPWrapper : IDisposable
    {
        private SocketWrapper socketWrapper;
        public ILog Logger { get; set; }

        /// <summary>
        /// 读取p2c中的数据
        /// </summary>
        /// <param name="startAddresss">读取数据的起始位置</param>
        /// <param name="numrRegister">读取寄存器的数量</param>
        /// <returns>返回p2c中的数据</returns>
        public byte[] Receive(ConnectData connectData)
        {
            socketWrapper = getSocket(connectData.IP, connectData.port);

            //设置循环次数
            int loopNum = getLoopNum(connectData.numRegister);

            //存储返回的数据
            byte[] result = new Byte[0];//每次读取的是一个寄存器（两个字节），乘以循环次数就是数据的最大数量

            //分段读取数据
            for (int i = 0; i < loopNum; i++)
            {
                //组拼请求头信息
                connectData.startAddress += (short)(i * 100);//每次循环后计算下一次读取的起始位置
                List<byte> sendData;
                try
                {
                    sendData = requestHead(FunctionCode.Read, connectData.startAddress, 100);
                }
                catch (Exception)
                {
                    //this.socketWrapper.Logger.Write("连接错误，错误设备IP:" + connectData.IP);
                    Console.WriteLine("连接错误，错误设备IP:" + connectData.IP);
                    return null;
                }

                //发送读取数据的请求信息
                if (this.socketWrapper.Write(sendData.ToArray()))
                {
                    this.socketWrapper.Logger.Write("发送请求失败，失败设备IP:" + connectData.IP);
                    return null;
                }

                //打印日志
                this.socketWrapper.Log("Send:", sendData.ToArray());

                //防止连续读写引起前台UI线程阻塞
                Application.DoEvents();

                //读取Response的信息
                byte[] receiveData = this.socketWrapper.Read(255);//缓冲区中的数据总量不超过256byte，一次读256byte，防止残余数据影响下次读取

                //获取事务标识
                short identifier = (short)((((short)receiveData[0]) << 8) + receiveData[1]);

                //对比事务标识
                if (identifier != CurrentDataIndex) //请求的数据标识与返回的标识不一致，则丢掉数据包
                {
                    return null;
                }
                byte length = receiveData[8];//最后一个字节，记录寄存器中数据的Byte数

                result = getReadData(receiveData, result, length);
            }

            this.socketWrapper.closeConnect();

            //打印日志
            if (result.Length >= 200) //如果有数据则打印 没有则不打印
            {
                this.socketWrapper.Log("Receive:", result);
            }

            return result;
        }

        /// <summary>
        /// 获取socket连接
        /// </summary>
        /// <param name="ip">设备ip地址</param>
        /// <param name="port">设备端口号</param>
        /// <param name="logger"></param>
        /// <returns></returns>
        private SocketWrapper getSocket(string ip, int port)
        {
            return new SocketWrapper(ip, port, this.Logger);
        }

        /// <summary>
        /// 向p2c中写入数据
        /// </summary>
        /// <param name="data">要写入的数据</param>
        /// <param name="startAddresss">写入的起始地址</param>
        public void Send(byte[] data,short startAddresss)
        {
            //[0]:填充0，清掉剩余的寄存器
            if (data.Length < 60)
            {
                var input = data;
                data = new Byte[60];
                Array.Copy(input, data, input.Length);
            }

            var numRegister = (short)(data.Length / 2);
            List<byte> values = requestHead(FunctionCode.Write, startAddresss, numRegister, data);

            //[3].写数据
            this.socketWrapper.Write(values.ToArray());

            //[4].关闭连接
            this.socketWrapper.closeConnect();

            //[5].防止连续读写引起前台UI线程阻塞
            Application.DoEvents();

            //[6].读取Response: 写完后会返回12个byte的结果
            byte[] responseHeader = this.socketWrapper.Read(12);
        }

        /// <summary>
        /// 连接分段返回的数据
        /// </summary>
        /// <param name="sourceData">源数据</param>
        /// <param name="targetData">汇总数据</param>
        /// <param name="length">源数据中需要提取的数据长度</param>
        /// <returns></returns>
        private byte[] getReadData(byte[] sourceData, byte[] targetData, byte length)
        {
            byte[] source_temp = new byte[length];
            Array.Copy(sourceData, 9, source_temp, 0, length);
            
            byte[] target_temp = new byte[targetData.Length];
            Array.Copy(targetData, target_temp, targetData.Length);

            targetData = new byte[length + target_temp.Length];
            Array.Copy(target_temp, targetData, target_temp.Length);
            Array.Copy(source_temp, 0, targetData, target_temp.Length, source_temp.Length);

            return targetData;
        }

        /// <summary>
        /// 获取读取循环次数
        /// </summary>
        /// <param name="numrRegister"></param>
        /// <returns></returns>
        private int getLoopNum(short numrRegister)
        {
            int loopNum = 1;
            if (numrRegister >= 100)
            {
                if (numrRegister % 100 == 0)
                {
                    loopNum = numrRegister / 100;
                }
                else
                {
                    loopNum = numrRegister / 100 + 1;
                }
            }
            return loopNum;
        }

        /// <summary>
        /// 请求的头信息组拼
        /// </summary>
        /// <param name="type">请求的类型</param>
        /// <param name="StartingAddress">数据处理的起始位置</param>
        /// <param name="numRegister">需要处理的寄存器的数量</param>
        /// <param name="data">可选值，如果是写入操作需要传入写入的数据</param>
        /// <returns>返回组拼好的请求头信息</returns>
        private List<byte> requestHead(FunctionCode type, short StartingAddress, short numRegister, byte[] data = null)
        {
            socketWrapper.Connect();
            List<byte> sendData = new List<byte>(255);

            sendData.AddRange(ValueHelper.Instance.GetBytes(this.NextDataIndex()));//1~2.(Transaction Identifier)
            sendData.AddRange(new Byte[] { 0, 0 });//3~4:Protocol Identifier,0 = MODBUS protocol
            sendData.AddRange(ValueHelper.Instance.GetBytes((short)6));//5~6:后续的Byte数量（针对读请求，后续为6个byte）
            sendData.Add(0);//7:单元标识
            sendData.Add((byte)type);//8.功能码 3标示读取,16表示写入
            sendData.AddRange(ValueHelper.Instance.GetBytes(StartingAddress));//9~10.起始地址
            sendData.AddRange(ValueHelper.Instance.GetBytes(numRegister));//11~12.需要读取的寄存器数量
            if (type == FunctionCode.Write)
            {
                sendData.Add((byte)data.Length);//13.数据的Byte数量
                sendData.AddRange(data);
            }

            return sendData;
        }

        #region 事务标识
        /// <summary>
        /// 数据事务标识
        /// </summary>
        private short dataIndex = 0;

        protected short CurrentDataIndex
        {
            get { return this.dataIndex; }
        }

        protected short NextDataIndex()
        {
            return ++this.dataIndex;
        }
        #endregion

        #region IDisposable 成员
        public void Dispose()
        {
            socketWrapper.Dispose();
        }
        #endregion
    }
}
