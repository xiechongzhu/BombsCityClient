using NetSDKCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BombsCityClient.ParkingCamera
{
    class ParkingCameraController
    {
        private String ipAddress;
        private UInt32 port;
        private String userName;
        private String password;
        private IntPtr m_LoginID;
        private IntPtr m_PlayID;
        private IntPtr m_EventID;
        private static fAnalyzerDataCallBack m_AnalyzerDataCallBack;
        private NET_DEVICEINFO_Ex m_DevicInfo = new NET_DEVICEINFO_Ex();
        private Timer autoLoginTimer = new Timer();
        private int m_inCountTotal;
        private int m_outCountTotal;

        public int GetCarInCountTotal()
        {
            return m_inCountTotal;
        }

        public int GetCarOutCountTotal()
        {
            return m_outCountTotal;
        }

        public ParkingCameraController(String ipAddress, UInt32 port, String userName, String password)
        {
            this.ipAddress = ipAddress;
            this.port = port;
            this.userName = userName;
            this.password = password;
            this.m_LoginID = IntPtr.Zero;
            this.m_PlayID = IntPtr.Zero;
            this.m_EventID = IntPtr.Zero;
            autoLoginTimer.Interval = 10000;
            autoLoginTimer.Elapsed += new ElapsedEventHandler(AutoLoginTimeout);
            m_AnalyzerDataCallBack = new fAnalyzerDataCallBack(AnalyzerDataCallBack);
            m_inCountTotal = m_outCountTotal = 0;
        }

        protected delegate void LoginDelegate();
        protected void InternalLogin()
        {
            Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("开始连接车辆监控摄像头,IP={0},PORT={1}", ipAddress, port));
            m_LoginID = NETClient.Login(ipAddress, (ushort)port, userName, password, EM_LOGIN_SPAC_CAP_TYPE.TCP, IntPtr.Zero, ref m_DevicInfo);
            if (m_LoginID == IntPtr.Zero)
            {
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, String.Format("连接车辆监控摄像头失败,IP={0},PORT={1},错误原因:{2}",
                    ipAddress, port, NETClient.GetLastError()));
                autoLoginTimer.Start();
                return;
            }
            Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("连接车辆监控摄像头成功,IP={0},PORT={1}", ipAddress, port));
            m_PlayID = NETClient.RealPlay(m_LoginID, 0, IntPtr.Zero);
            if (m_PlayID == IntPtr.Zero)
            {
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, String.Format("启动车辆监控摄像头监视失败,IP={0},PORT={1},错误原因:{2}",
                ipAddress, port, NETClient.GetLastError()));
                NETClient.Logout(m_LoginID);
                m_LoginID = IntPtr.Zero;
                autoLoginTimer.Start();
                return;
            }
            Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("启动车辆监控摄像头监视成功,IP={0},PORT={1}", ipAddress, port));
            m_EventID = NETClient.RealLoadPicture(m_LoginID, 0, (uint)EM_EVENT_IVS_TYPE.ALL, true, m_AnalyzerDataCallBack, m_LoginID, IntPtr.Zero);
            if (IntPtr.Zero == m_EventID)
            {
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, String.Format("订阅车辆监控摄像头统计数据失败,IP={0},PORT={1},错误原因:{2}",
                ipAddress, port, NETClient.GetLastError()));
                NETClient.StopRealPlay(m_PlayID);
                m_PlayID = IntPtr.Zero;
                NETClient.Logout(m_LoginID);
                m_LoginID = IntPtr.Zero;
                autoLoginTimer.Start();
                return;
            }
            Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("订阅车辆监控摄像头统计数据成功,IP={0},PORT={1}", ipAddress, port));
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
            NETClient.StopLoadPic(m_EventID);
            m_EventID = IntPtr.Zero;
            autoLoginTimer.Stop();

            if (NETClient.Logout(m_LoginID))
            {
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("与车辆监控摄像头断开连接成功,IP={0},PORT={1}", ipAddress, port));
                m_LoginID = IntPtr.Zero;
            }
            else
            {
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, String.Format("与车辆监控摄像头断开连接失败,IP={0},PORT={1},错误原因:{2}",
                    ipAddress, port, NETClient.GetLastError()));
            }
        }

        public void StartAutoLogin()
        {
            m_LoginID = IntPtr.Zero;
            m_PlayID = IntPtr.Zero;
            m_EventID = IntPtr.Zero;
            Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("开始自动连接车辆监控摄像头,IP={0},PORT={1}", ipAddress, port));
            autoLoginTimer.Start();
        }

        protected void AutoLoginTimeout(object source, ElapsedEventArgs e)
        {
            Login();
        }

        private int AnalyzerDataCallBack(IntPtr lAnalyzerHandle, uint dwEventType, IntPtr pEventInfo, IntPtr pBuffer, uint dwBufSize, 
            IntPtr dwUser, int nSequence, IntPtr reserved)
        {
            EM_EVENT_IVS_TYPE type = (EM_EVENT_IVS_TYPE)dwEventType;
            switch (type)
            {
                case EM_EVENT_IVS_TYPE.TRAFFICJUNCTION:
                    NET_DEV_EVENT_TRAFFICJUNCTION_INFO info = (NET_DEV_EVENT_TRAFFICJUNCTION_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFICJUNCTION_INFO));
                    if (GlobalConfig.GetInstance().parkingCameraCfg.LaneIn == info.nLane)
                    {
                        Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("检测到车辆进入停车场,车牌号:{0}", info.stTrafficCar.szPlateNumber));
                        m_inCountTotal++;
                    }
                    else if(GlobalConfig.GetInstance().parkingCameraCfg.LaneOut == info.nLane)
                    {
                        Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("检测到车辆离开停车场,车牌号:{0}", info.stTrafficCar.szPlateNumber));
                        m_outCountTotal++;
                    }
                    break;
                default:
                    break;
            }
            return 0;
        }
    }
}
