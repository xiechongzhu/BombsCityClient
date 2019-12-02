using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombsCityClient
{
    public partial class CamAddForm : Form
    {
        private FlowCamCfg flowCamCfg = new FlowCamCfg();

        public CamAddForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if(textBoxAddr.Text.Length == 0)
            {
                MessageBox.Show("摄像头IP地址不能为空", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBoxPort.Text.Length == 0)
            {
                MessageBox.Show("摄像头端口不能为空", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBoxUserName.Text.Length == 0)
            {
                MessageBox.Show("摄像头用户名不能为空", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBoxPassword.Text.Length == 0)
            {
                MessageBox.Show("摄像头密码不能为空", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                flowCamCfg.IpAddress = textBoxAddr.Text;
                flowCamCfg.Port = UInt32.Parse(textBoxPort.Text);
                flowCamCfg.Description = textBoxDescription.Text;
                flowCamCfg.UserName = textBoxUserName.Text;
                flowCamCfg.Password = textBoxPassword.Text;
            }
            catch(Exception)
            {
                MessageBox.Show("摄像头端口输入格式不正确", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        public FlowCamCfg GetFlowCamCfg()
        {
            return flowCamCfg;
        }
    }
}
