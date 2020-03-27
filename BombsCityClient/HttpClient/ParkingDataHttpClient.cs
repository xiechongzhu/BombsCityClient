using BombsCityClient.DataStruct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace BombsCityClient.HttpClient
{
    class ParkingDataHttpClient
    {
        protected delegate void UploadParkingDataDelegate(ParkingDataUpload data);

        public void UploadParkingData(ParkingDataUpload data)
        {
            UploadParkingDataDelegate uploadFlowCountsDelegate = new UploadParkingDataDelegate(InternalUploadParkingData);
            uploadFlowCountsDelegate.BeginInvoke(data, UploadParkingDataCallBack, null);
        }

        protected void UploadParkingDataCallBack(IAsyncResult result)
        {
            AsyncResult asyncResult = (AsyncResult)result;
            UploadParkingDataDelegate loginDelegate = (UploadParkingDataDelegate)asyncResult.AsyncDelegate;
            loginDelegate.EndInvoke(result);
        }

        protected void InternalUploadParkingData(ParkingDataUpload data)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                String json = js.Serialize(data);
                String url = GlobalConfig.GetInstance().ParkingUrl;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                string strContent = String.Format("clustertag={0}&data=[{1}]",  GlobalConfig.GetInstance().ClusterTag, json);
                using (StreamWriter stOut = new StreamWriter(httpWebRequest.GetRequestStream(), Encoding.ASCII))
                {
                    stOut.Write(strContent);
                    stOut.Close();
                }
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
                String responseContent = streamReader.ReadToEnd();
                streamReader.Close();
                httpWebResponse.Close();

                FlowCountResponse response = js.Deserialize<FlowCountResponse>(responseContent);
                if (response.code != 0)
                {
                    Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, String.Format("推送停车场数据失败,错误:{0}", response.message));
                }
                else
                {
                    Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("推送停车场数据成功,停车位总数={0},已使用停车位数={1}", data.total, data.used));
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, String.Format("推送停车场数据失败,错误:{0}", e.Message));
            }
        }
    }
}
