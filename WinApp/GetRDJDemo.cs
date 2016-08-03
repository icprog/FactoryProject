using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ROSO.Model;
using ROSO.BLL;

namespace ROSO.WinApp
{
    public partial class GetRDJDemo : Form
    {
        public GetRDJDemo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<DeviceInfo> devicelist = BL_DeviceInfo.GetDeviceInfoList(DeviceType.ROSO_RDJ);
            List<RDJ> list = BL_GetRDJList.GetRDJList(devicelist, 502, 10000, 200, BL_DeviceTemplate.GetDeviceTemplateList(DeviceType.ROSO_RDJ));
        }
    }
}
