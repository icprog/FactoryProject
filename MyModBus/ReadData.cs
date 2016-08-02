using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyModBus
{
    public class ReadData
    {
        
        /// <summary>
        /// 正向运算得到数据
        /// </summary>
        /// <param name="data">要提取的字节数组</param>
        /// <param name="start">开始索引的下标</param>
        /// <param name="length">索引的长度，长度范围在1，2，4个字节</param>
        /// <returns></returns>
        public static int GetData(byte[] data, int start, int length)
        {
            byte[] dt = new byte[length];
            int getData;
            Array.Copy(data, start, dt, 0, length);
            Array.Reverse(dt);
            if (length == 1)
            {
                getData = Convert.ToInt16(dt);
                return getData;
            }
            else
            {
                if (length == 2)
                {
                    //short data1=
                    getData = BitConverter.ToInt16(dt, 0);
                }
                else
                    getData = BitConverter.ToInt32(dt, 0);
                return getData;
            }

        }
        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="data">源字节数组</param>
        /// <param name="start">开始索引的下标</param>
        /// <param name="length">索引长度</param>
        /// <param name="Type">type为2，表示转换为二进制字符串，type为其他数就按ascll转换</param>
        /// <returns></returns>
        public static string GetStringData(byte[] data, int start, int length, int Type = 0)
        {
            if (Type == 2)
            {
                int i = 0;
                StringBuilder sb = new StringBuilder();
                while (i < length)
                {
                    sb.Append(Convert.ToString(data[start + i], 2).PadLeft(8, '0'));
                    i++;
                }
                return sb.ToString();
            }
            else
            {
                int i = 0;
                StringBuilder sb = new StringBuilder();
                while (i < length)
                {
                    if (data[start + i] == 0)
                    {
                        i++;
                        continue;
                    }
                    sb.Append((char)data[start + i]);
                    i++;
                }
                return sb.ToString();
            }
        }
        public static short[] GetIntArrayData(byte[] data, int start, int length, int Type = 0)
        {
            short[] wData = new short[length / 2];
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < (length / 2); i++)
            {
                wData[i] = (short)GetData(data, start + i * 2, 2);
            }
            return wData;
        }
    }
}
