using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BombsCityClient
{
    public class Logger
    {
        public enum LOG_LEVEL
        {
            LOG_INFO,
            LOG_ERROR
        }

        protected StreamWriter logWriter;

        private Logger()
        {
            String strLogDir = AppDomain.CurrentDomain.BaseDirectory + "Log";
            Directory.CreateDirectory(strLogDir);
            logWriter = new StreamWriter(strLogDir + "\\" + "App.log", true);
        }

        private MainForm mainForm;
        private static Logger __instance = new Logger();
        public static Logger GetInstance()
        {
            return __instance;
        }

        public void SetMainForm(MainForm _mainForm)
        {
            mainForm = _mainForm;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Log(LOG_LEVEL level, String msg)
        {
            DateTime dateTime = DateTime.Now;
            mainForm.Log(dateTime, level, msg);
            String strLevel;
            switch(level)
            {
                case LOG_LEVEL.LOG_INFO:
                    strLevel = "信息";
                    break;
                case LOG_LEVEL.LOG_ERROR:
                    strLevel = "错误";
                    break;
                default:
                    return;
            }
            String strLog = String.Format("[{0}][{1}]{2}", dateTime.ToString("G"), strLevel, msg);
            logWriter.WriteLine(strLog);
            logWriter.Flush();
        }
    }
}
