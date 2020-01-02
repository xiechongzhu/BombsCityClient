using BombsCityClient.DataStruct;
using BombsCityClient.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BombsCityClient.ParkingCamera
{
    class ParkingCameraManager
    {
        Timer uploadTimer = new Timer(30000);
        private Dictionary<Tuple<String, UInt32>, ParkingCameraController> Controllers = new Dictionary<Tuple<string, uint>, ParkingCameraController>();
        private ParkingDataHttpClient FlowCountHttpClient = new ParkingDataHttpClient();
        private static ParkingCameraManager __instance = new ParkingCameraManager();

        public static ParkingCameraManager GetInstance()
        {
            return __instance;
        }

        protected ParkingCameraManager()
        {
            uploadTimer.Elapsed += new ElapsedEventHandler(UploadTimerTimeout);
        }

        public void Start()
        {
            Controllers.Clear();
            ParkingCameraCfg cfg = GlobalConfig.GetInstance().parkingCameraCfg;
            ParkingCameraController controller = new ParkingCameraController(cfg.IpAddress, cfg.Port, cfg.UserName, cfg.Password);
            Controllers.Add(new Tuple<String, UInt32>(cfg.IpAddress, cfg.Port), controller);
            controller.Login();
            uploadTimer.Start();
        }

        public void Stop()
        {
            uploadTimer.Stop();
            foreach (ParkingCameraController controller in Controllers.Values)
            {
                controller.Logout();
            }
        }

        protected void UploadTimerTimeout(object sender, ElapsedEventArgs e)
        {
            ParkingDataUpload dataUpload = new ParkingDataUpload();
            int carIn = 0;
            int carOut = 0;
            foreach (ParkingCameraController controller in Controllers.Values)
            {
                carIn += controller.GetCarInCount();
                carOut += controller.GetCarOutCount();
            }

            dataUpload.time = DateTime.Now.ToString("yyyy-MM-dd HH");
            dataUpload.used = carIn - carOut;
            if(dataUpload.used < 0)
            {
                dataUpload.used = 0;
            }
            dataUpload.parkid = "";
            dataUpload.resourceid = GlobalConfig.GetInstance().ResourceCode;
            dataUpload.pname = "两弹城停车场";
            dataUpload.total = GlobalConfig.GetInstance().parkingCameraCfg.ParkingTotal;
            FlowCountHttpClient.UploadParkingData(dataUpload);
        }

        public void DisconnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            String strIpAdd = Marshal.PtrToStringAnsi(pchDVRIP);
            Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, String.Format("与车辆监控摄像头断开连接,IP={0},PORT={1}", strIpAdd, nDVRPort));
            if (Controllers.ContainsKey(new Tuple<String, UInt32>(strIpAdd, (UInt32)nDVRPort)))
            {
                Controllers[new Tuple<String, UInt32>(strIpAdd, (UInt32)nDVRPort)].StartAutoLogin();
            }
        }
    }
}
