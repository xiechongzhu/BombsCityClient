using BombsCityClient.FlowCamera;
using BombsCityClient.ParkingCamera;
using NetSDKCS;
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
    public partial class MainForm : Form
    {
        private FlowCameraManager flowCameraManager = FlowCameraManager.GetInstance();
        private ParkingCameraManager parkingCameraManager = ParkingCameraManager.GetInstance();

        public MainForm()
        {
            InitializeComponent();
            Logger.GetInstance().SetMainForm(this);
        }

        delegate void LogDelegate(DateTime time, Logger.LOG_LEVEL level, String msg);
        public void Log(DateTime time, Logger.LOG_LEVEL level, String msg)
        {
            if (InvokeRequired)
            {
                Invoke(new LogDelegate(Log), time, level, msg);
            }
            else
            {
                ListViewItem item = new ListViewItem
                {
                    Text = time.ToString("G")
                };
                String strLevel;
                switch (level)
                {
                    case Logger.LOG_LEVEL.LOG_INFO:
                        strLevel = "信息";
                        break;
                    case Logger.LOG_LEVEL.LOG_ERROR:
                        strLevel = "错误";
                        break;
                    default:
                        return;
                }
                item.SubItems.Add(strLevel);
                item.SubItems.Add(msg);
                listViewLog.Items.Add(item);
                listViewLog.EnsureVisible(listViewLog.Items.Count - 1);
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            CfgForm cfgForm = new CfgForm();
            cfgForm.ShowDialog();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, "开始运行");
            if(!GlobalConfig.LoadConfig())
            {
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, "读取配置文件失败");
                return;
            }
            else
            {
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, "读取配置文件成功");
            }

            flowCameraManager.Start();
            parkingCameraManager.Start();

            btnSetting.Enabled = false;
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            flowCameraManager.Stop();
            parkingCameraManager.Stop();
            btnSetting.Enabled = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, "停止运行");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if(!NETClient.Init(new fDisConnectCallBack(DisconnectCallBack), IntPtr.Zero, null))
            {
                MessageBox.Show("初始化摄像头失败!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, "初始化摄像头失败!");
            }
            else
            {
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, "初始化摄像头成功!");
            }
        }

        protected void DisconnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            flowCameraManager.DisconnectCallBack(lLoginID, pchDVRIP, nDVRPort, dwUser);
            parkingCameraManager.DisconnectCallBack(lLoginID, pchDVRIP, nDVRPort, dwUser);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020;
            if ((int)m.WParam == SC_MINIMIZE && m.Msg == WM_SYSCOMMAND)
            {
                this.Hide();
                return; //提前返回。拦下最小化消息。
            }
            base.WndProc(ref m); // 这一步不能忘!
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }
    }
}
