using System;
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

    public class GlobalConfig
    {
        private static GlobalConfig __instance = new GlobalConfig();
        private static String ConfigFile = "config.xml";
        public List<FlowCamCfg> FlowCamCfgList { get; set; }
        public String ResourceCode { get; set; }
        public int ClusterTag { get; set; }
        public String RtPeopleUrl { get; set; }
        public String ParkingUrl { get; set; }

        private GlobalConfig()
        {
            FlowCamCfgList = new List<FlowCamCfg>();
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
