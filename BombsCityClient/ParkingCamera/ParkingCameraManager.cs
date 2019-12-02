using BombsCityClient.DataStruct;
using BombsCityClient.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BombsCityClient.ParkingCamera
{
    class ParkingCameraManager
    {
        private ParkingDataHttpClient httpClient = new ParkingDataHttpClient();
        Timer uploadTimer = new Timer(10000);
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
            uploadTimer.Start();
        }

        public void Stop()
        {
            uploadTimer.Stop();
        }

        protected void UploadTimerTimeout(object sender, ElapsedEventArgs e)
        {
            Random random = new Random();
            ParkingDataUpload dataUpload = new ParkingDataUpload
            {
                time = DateTime.Now.ToString("yyyy-MM-dd HH"),
                used = random.Next(1, 100),
                parkid = "",
                resourceid = GlobalConfig.GetInstance().ResourceCode,
                pname = "两弹城停车场",
                total = 200
            };
            httpClient.UploadParkingData(dataUpload);
        }

        public void DisconnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            
        }
    }
}
