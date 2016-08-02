using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Configuration;
using System.Threading;

namespace MyModBus
{
    class SocketWrapper
    {
        private static int TimeOut = 100;
        private string IP;
        private int Port;

        public ILog Logger;
        private Socket socket = null;

        public SocketWrapper(string ip,int port)
        {
            this.IP = ip;
            this.Port = port;
        }
        public SocketWrapper(string ip, int port, ILog logger)
        {
            this.IP = ip;
            this.Port = port;
            this.Logger = logger;
        }

        public void Connect()
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, TimeOut);

            IAsyncResult connResult = socket.BeginConnect(IP, Port, null, null);
            connResult.AsyncWaitHandle.WaitOne(100, true);  
            if (!connResult.IsCompleted)
            {
                socket.Close();
                throw new SocketException();

            }
        }

        public void closeConnect()
        {
            this.socket.Close();
        }

        public byte[] Read(int length)
        {
            byte[] data = new byte[length];
            this.socket.Receive(data);
            return data;
        }

        public bool Write(byte[] data)
        {
            if (this.socket.Send(data)>0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 打印日志
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        public void Log(string type, byte[] data)
        {
            if (this.Logger != null)
            {
                StringBuilder logText = new StringBuilder(type);
                foreach (byte item in data)
                {
                    logText.Append(item.ToString() + " ");
                }
                this.Logger.Write(logText.ToString());
            }
        }

        #region IDisposable 成员
        public void Dispose()
        {
            if (this.socket != null)
            {
                this.socket.Close();
            }
        }
        #endregion
    }
}
