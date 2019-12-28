using BombsCityClient.DataStruct;
using BombsCityClient.HttpClient;
using NetSDKCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BombsCityClient.FlowCamera
{
    class FlowCameraManager
    {
        private Timer uploadTimer = new Timer(10000);
        private Dictionary<Tuple<String, UInt32>, FlowCameraController> Controllers = new Dictionary<Tuple<string, uint>, FlowCameraController>();
        private FlowCountHttpClient FlowCountHttpClient = new FlowCountHttpClient();
        private static FlowCameraManager __instance = new FlowCameraManager();

        public static FlowCameraManager GetInstance()
        {
            return __instance;
        }

        protected FlowCameraManager()
        {
            uploadTimer.Elapsed += new ElapsedEventHandler(UploadTimeout);
        }

        public void Start()
        {
            Controllers.Clear();
            List<FlowCamCfg> flowCamCfgs = GlobalConfig.GetInstance().FlowCamCfgList;
            foreach(FlowCamCfg cfg in flowCamCfgs)
            {
                FlowCameraController controller = new FlowCameraController(cfg.IpAddress, cfg.Port, cfg.UserName, cfg.Password);
                Controllers.Add(new Tuple<String, UInt32>(cfg.IpAddress, cfg.Port), controller);
                controller.Login();
            }
            uploadTimer.Start();
        }

        public void Stop()
        {
            uploadTimer.Stop();
            foreach (FlowCameraController controller in Controllers.Values)
            {
                controller.Logout();
            }
        }

        protected void UploadTimeout(Object sender, ElapsedEventArgs args)
        {
            List<FlowCount> flowCounts = new List<FlowCount>();
            foreach(FlowCameraController controller in Controllers.Values)
            {
                if(controller.flowCount != null)
                {
                    flowCounts.Add(controller.flowCount);
                }
            }
            UploadFlowCounts(flowCounts);
        }

        protected void UploadFlowCounts(List<FlowCount> flowCounts)
        {
            if(flowCounts.Count == 0)
            {
                return;
            }
            FlowCountHttpClient.UploadFlowCounts(flowCounts);
        }

        public void DisconnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            String strIpAdd = Marshal.PtrToStringAnsi(pchDVRIP);
            Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, String.Format("与人流量统计摄像头断开连接,IP={0},PORT={1}", strIpAdd, nDVRPort));
            if (Controllers.ContainsKey(new Tuple<String, UInt32>(strIpAdd, (UInt32)nDVRPort)))
            {
                Controllers[new Tuple<String, UInt32>(strIpAdd, (UInt32)nDVRPort)].StartAutoLogin();
            }
        }
    }
}
