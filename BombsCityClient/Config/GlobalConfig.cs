﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BombsCityClient
{
    [Serializable]
    public class FlowCamCfg
    {
        public String IpAddress { get; set; }
        public UInt32 Port { get; set; }
        public String Description { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
    }

    public class ParkingCameraCfg
    {
        public String IpAddress { get; set; }
        public UInt32 Port { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public Int32 ParkingTotal { get; set; }
        public Int32 ParkingUsed { get; set; }
        public Int32 LaneIn { get; set; }
        public Int32 LaneOut { get; set; }
    }

    public class GlobalConfig
    {
        private static GlobalConfig __instance = new GlobalConfig();
        private static String ConfigFile = "config.xml";
        public List<FlowCamCfg> FlowCamCfgList { get; set; }
        public String ResourceCode { get; set; }
        public int ClusterTag { get; set; }
        public String RtPeopleUrl { get; set; }
        public String ParkingUrl { get; set; }
        public ParkingCameraCfg parkingCameraCfg { get; set; }
        public String ZytfUrl { get; set; }

        private GlobalConfig()
        {
            FlowCamCfgList = new List<FlowCamCfg>();
            parkingCameraCfg = new ParkingCameraCfg();
        }

        public static GlobalConfig GetInstance()
        {
            return __instance;
        }

        public static bool LoadConfig()
        {
            StreamReader file = new StreamReader(ConfigFile);
            GlobalConfig __config = new GlobalConfig();
            try
            {
                XmlSerializer reader = new XmlSerializer(typeof(GlobalConfig));
                __config = (GlobalConfig)reader.Deserialize(file);
                __instance.FlowCamCfgList = __config.FlowCamCfgList;
                __instance.ResourceCode = __config.ResourceCode;
                __instance.ClusterTag = __config.ClusterTag;
                __instance.RtPeopleUrl = __config.RtPeopleUrl;
                __instance.ParkingUrl = __config.ParkingUrl;
                __instance.parkingCameraCfg = __config.parkingCameraCfg;
                __instance.ZytfUrl = __config.ZytfUrl;
                file.Close();
            }
            catch(Exception)
            {
                file.Close();
                return false;
            }
            return true;
        }

        public static bool SaveConfig()
        {
            FileStream file = File.Create(ConfigFile);
            try
            {
                XmlSerializer wirter = new XmlSerializer(typeof(GlobalConfig));
                wirter.Serialize(file, __instance);
            }catch(Exception)
            {
                file.Close();
                return false;
            }
            file.Close();
            return true;
        }
    }
}
