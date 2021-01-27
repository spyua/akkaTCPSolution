using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcpWindowsForm.Config;

namespace TcpWindowsForm
{
    public partial class Form1 : Form
    {
        private AppSetting _appSetting;

        public Form1(AppSetting appSetting)
        {
            _appSetting = appSetting;
            InitializeComponent(); 
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            LocalCntLabel.Text = "Local IP:" + _appSetting.LocalIp + " Port:" + _appSetting.LocalPort;
            RemoteCntLabel.Text = "Remote IP:" + _appSetting.RemoteIp + " Port:" + _appSetting.RemotePort;
        }
    }
}
