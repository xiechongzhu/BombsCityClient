using BombsCityClient.DataStruct;
using NetSDKCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BombsCityClient.FlowCamera
{
    public class FlowCameraController
    {
        private String ipAddress;
        private UInt32 port;
        private String userName;
        private String password;
        private IntPtr m_LoginID;
        private IntPtr m_PlayID;
        private IntPtr m_AttactID;
        private NET_DEVICEINFO_Ex m_DevicInfo = new NET_DEVICEINFO_Ex();
        private Timer autoLoginTimer = new Timer();
        //private Timer clearTimer = new Timer();
        private fVideoStatSumCallBack m_VideoStatSumCallBack;
        public FlowCount flowCount { get; set; }

        public FlowCameraController(String ipAddress, UInt32 port, String userName, String password)
        {
            this.ipAddress = ipAddress;
            this.port = port;
            this.userName = userName;
            this.password = password;
            this.m_LoginID = IntPtr.Zero;
            this.m_PlayID = IntPtr.Zero;
            this.m_AttactID = IntPtr.Zero;  
            autoLoginTimer.Interval = 10000;
            autoLoginTimer.Elapsed += new ElapsedEventHandler(AutoLoginTimeout);
            //clearTimer.Interval = 5000;
            //clearTimer.Elapsed += new ElapsedEventHandler(ClearTimeout);
            m_VideoStatSumCallBack = new fVideoStatSumCallBack(VideoStatSumCallBack);

            flowCount = new FlowCount
            {
                Detained = 0,
                Entered = 0
            };
        }

        protected delegate void LoginDelegate();
        protected void InternalLogin()
        {
            Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("开始连接人流量统计摄像头,IP={0},PORT={1}", ipAddress, port));
            m_LoginID = NETClient.Login(ipAddress, (ushort)port, userName, password, EM_LOGIN_SPAC_CAP_TYPE.TCP, IntPtr.Zero, ref m_DevicInfo);
            if (m_LoginID == IntPtr.Zero)
            {
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, String.Format("连接人流量统计摄像头失败,IP={0},PORT={1},错误原因:{2}",
                    ipAddress, port, NETClient.GetLastError()));
                autoLoginTimer.Start();
                return;
            }
            Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("连接人流量统计摄像头成功,IP={0},PORT={1}", ipAddress, port));
            m_PlayID = NETClient.RealPlay(m_LoginID, 0, IntPtr.Zero);
            if (m_PlayID == IntPtr.Zero)
            {
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, String.Format("启动人流量统计摄像头监视失败,IP={0},PORT={1},错误原因:{2}",
                ipAddress, port, NETClient.GetLastError()));
                NETClient.Logout(m_LoginID);
                m_LoginID = IntPtr.Zero;
                autoLoginTimer.Start();
                return;
            }
            Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("启动人流量统计摄像头监视成功,IP={0},PORT={1}", ipAddress, port));
            NET_IN_ATTACH_VIDEOSTAT_SUM inParam = new NET_IN_ATTACH_VIDEOSTAT_SUM();
            inParam.dwSize = (uint)Marshal.SizeOf(typeof(NET_IN_ATTACH_VIDEOSTAT_SUM));
            inParam.nChannel = 0;
            inParam.cbVideoStatSum = m_VideoStatSumCallBack;
            NET_OUT_ATTACH_VIDEOSTAT_SUM outParam = new NET_OUT_ATTACH_VIDEOSTAT_SUM();
            outParam.dwSize = (uint)Marshal.SizeOf(typeof(NET_OUT_ATTACH_VIDEOSTAT_SUM));
            m_AttactID = NETClient.AttachVideoStatSummary(m_LoginID, ref inParam, ref outParam, 5000);
            if(m_AttactID == IntPtr.Zero)
            {
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, String.Format("订阅人流量统计摄像头统计数据失败,IP={0},PORT={1},错误原因:{2}",
                    ipAddress, port, NETClient.GetLastError()));
                NETClient.StopRealPlay(m_PlayID);
                m_PlayID = IntPtr.Zero;
                NETClient.Logout(m_LoginID);
                m_LoginID = IntPtr.Zero;
                autoLoginTimer.Start();
                return;
            }
            Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("订阅人流量统计摄像头统计数据成功,IP={0},PORT={1}", ipAddress, port));
            //clearTimer.Start();
            autoLoginTimer.Stop();
        }

        public void Login()
        {
            if (m_LoginID == IntPtr.Zero)
            {
                LoginDelegate loginDelegate = new LoginDelegate(InternalLogin);
                loginDelegate.BeginInvoke(LoginCallBack, null);
            }
        }

        protected void LoginCallBack(IAsyncResult result)
        {
            AsyncResult asyncResult = (AsyncResult)result;
            LoginDelegate loginDelegate = (LoginDelegate)asyncResult.AsyncDelegate;
            loginDelegate.EndInvoke(result);
        }

        public void Logout()
        {
            NETClient.StopRealPlay(m_PlayID);
            m_PlayID = IntPtr.Zero;
            NETClient.DetachVideoStatSummary(m_AttactID);
            m_AttactID = IntPtr.Zero;
            autoLoginTimer.Stop();
            //clearTimer.Stop();
            
            if (NETClient.Logout(m_LoginID))
            {
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("与人流量统计摄像头断开连接成功,IP={0},PORT={1}", ipAddress, port));
                m_LoginID = IntPtr.Zero;
            }
            else
            {
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, String.Format("与人流量统计摄像头断开连接失败,IP={0},PORT={1},错误原因:{2}",
                    ipAddress, port, NETClient.GetLastError()));
            }
        }

        public void StartAutoLogin()
        {
            m_LoginID = IntPtr.Zero;
            m_PlayID = IntPtr.Zero;
            m_AttactID = IntPtr.Zero;
            Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("开始自动连接人流量统计摄像头,IP={0},PORT={1}", ipAddress, port));
            autoLoginTimer.Start();
            //clearTimer.Stop();
        }

        protected void AutoLoginTimeout(object source, ElapsedEventArgs e)
        {
            Login();
        }

        /*protected void ClearTimeout(object source, ElapsedEventArgs e)
        {
            ClearFlowCount();
        }*/

        protected void VideoStatSumCallBack(IntPtr lAttachHandle, IntPtr pBuf, uint dwBufLen, IntPtr dwUser)
        {
            if (lAttachHandle == m_AttactID)
            {
                NET_VIDEOSTAT_SUMMARY info = (NET_VIDEOSTAT_SUMMARY)Marshal.PtrToStructure(pBuf, typeof(NET_VIDEOSTAT_SUMMARY));
                flowCount.Detained = info.stuEnteredSubtotal.nToday - info.stuExitedSubtotal.nToday;
                flowCount.Entered = info.stuEnteredSubtotal.nToday;
                flowCount.Leaved = info.stuExitedSubtotal.nToday;
            }
        }

        /*protected void ClearFlowCount()
        {
            NET_CTRL_CLEAR_SECTION_STAT_INFO info = new NET_CTRL_CLEAR_SECTION_STAT_INFO();
            info.dwSize = (uint)Marshal.SizeOf(typeof(NET_CTRL_CLEAR_SECTION_STAT_INFO));
            info.nChannel = 0;
            IntPtr inPtr = IntPtr.Zero;
            inPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NET_CTRL_CLEAR_SECTION_STAT_INFO)));
            Marshal.StructureToPtr(info, inPtr, true);
            if(!NETClient.ControlDevice(m_LoginID, EM_CtrlType.CLEAR_SECTION_STAT, inPtr, 5000))
            {
                flowCount.Entered = 0;
            }
        }*/
    }
}
