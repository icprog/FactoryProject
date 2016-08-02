using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ROSO.BLL;
using ROSO.Model;


namespace ROSO.WinApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //模拟获取设备规格信息列表
            //List<Model.DeviceSpec> list = BLL.BL_DeviceSpec.GetDeviceSpecList();
            //comboBox1.DataSource = list;
            //comboBox1.DisplayMember = "DeviceSpecValue";
            //comboBox1.ValueMember = "DeviceSpecValue";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //模拟写设备状态信息
            Model.DeviceState ds = new Model.DeviceState();
            ds.DAQTime = DateTime.Now;
            ds.DeviceID = 3;
            ds.OperatingState = 0;
            ds.OperatorID = 3;
            ds.VatID = 2;
            ds.FaultMessage = string.Empty;
            BLL.BL_DeviceState.AddDeviceState(ds);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //模拟添加管理员用户
            Model.ROSOUser user = new Model.ROSOUser();
            user.Account = "jerry";
            user.Password = "123456";
            user.Role = "管理员";
            user.LastLoginTime = DateTime.Now;
            if (!BLL.BL_User.UserIsExist(user.Account))
            {
                BLL.BL_User.AddUser(user);
            }
            else
            {
                MessageBox.Show("帐号已经存在！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //模拟获取指定设备指定日期的状态列表
            List<Model.DeviceState> list = new List<Model.DeviceState>();
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = DateTime.Now.AddDays(1).Date;
            list= BLL.BL_DeviceState.GetDeviceStateList(3, 1, startDate, endDate);
            dataGridView1.DataSource = list;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<RDJ> RDJList = BL_GetRDJList.GetRDJList(BL_DeviceInfo.GetDeviceInfoList(DeviceType.ROSO_RDJ), 502, 10000, 100, BL_DeviceTemplate.GetDeviceTemplateList(DeviceType.ROSO_RDJ));
            Console.WriteLine("RDJ:");
            foreach (RDJ rdj in RDJList)
            {
                Console.WriteLine(rdj.SBBH);
                Console.WriteLine(rdj.SBZDXX);
                for (int i = 0; i < rdj.PLCZT.Length; i++)
                {
                    Console.Write(rdj.PLCZT[i] + ":");
                }
                Console.WriteLine(rdj.KH);
            }
            Console.WriteLine("DXJ:");
            List<DXJ> DXJList = BL_GetDXJList.GetDXJList(BL_DeviceInfo.GetDeviceInfoList(DeviceType.ROSO_DXJ), 502, 10000, 100, BL_DeviceTemplate.GetDeviceTemplateList(DeviceType.ROSO_DXJ));
            foreach (DXJ dxj in DXJList)
            {
                Console.WriteLine(dxj.SBBH);
                Console.WriteLine(dxj.SBZDXX); 
                for (int i = 0; i < dxj.PLCZT.Length; i++)
                {
                    Console.Write(dxj.PLCZT[i] + ":");
                }
            }
            Console.WriteLine("BZJ:");
            List<BZJ> BZJList = BL_GetBZJList.GetBZJList(BL_DeviceInfo.GetDeviceInfoList(DeviceType.ROSO_BZJ), 502, 10000, 100, BL_DeviceTemplate.GetDeviceTemplateList(DeviceType.ROSO_BZJ));
            foreach (BZJ bzj in BZJList)
            {
                Console.WriteLine(bzj.SBBH);
                Console.WriteLine(bzj.SBZDXX);
                for (int i = 0; i < bzj.PLCZT.Length; i++)
                {
                    Console.Write(bzj.PLCZT[i] + ":");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<DeviceInfo> devicelist = BL_DeviceInfo.GetDeviceInfoList(DeviceType.ROSO_RDJ);
            List<RDJ> list= BL_GetRDJList.GetRDJList(devicelist, 502, 10000, 200, BL_DeviceTemplate.GetDeviceTemplateList(DeviceType.ROSO_RDJ));


        }

 
    }
}
