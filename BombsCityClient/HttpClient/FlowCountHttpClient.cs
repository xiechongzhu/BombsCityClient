using BombsCityClient.DataStruct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace BombsCityClient.HttpClient
{
    public class FlowCountHttpClient
    {
        protected String AES_KEY = "js7ksl3nhnfivl4m";
        protected String AES_IV = "3859345501849051";

        protected delegate void UploadFlowCountsDelegate(List<FlowCount> flowCounts);

        public void UploadFlowCounts(List<FlowCount> flowCounts)
        {
            UploadFlowCountsDelegate uploadFlowCountsDelegate = new UploadFlowCountsDelegate(InternalUploadFlowCounts);
            uploadFlowCountsDelegate.BeginInvoke(flowCounts, UploadFlowCountsCallBack, null);
            UploadFlowCountsDelegate ZytfUploadFlowCountsDelegate = new UploadFlowCountsDelegate(ZytfInternalUploadFlowCounts);
            ZytfUploadFlowCountsDelegate.BeginInvoke(flowCounts, UploadFlowCountsCallBack, null);
        }

        protected void UploadFlowCountsCallBack(IAsyncResult result)
        {
            AsyncResult asyncResult = (AsyncResult)result;
            UploadFlowCountsDelegate loginDelegate = (UploadFlowCountsDelegate)asyncResult.AsyncDelegate;
            loginDelegate.EndInvoke(result);
        }

        protected void InternalUploadFlowCounts(List<FlowCount> flowCounts)
        {
            FlowUploadInfo flowUploadInfo = new FlowUploadInfo();
            FlowInfo flowInfo = new FlowInfo
            {
                time = DateTime.Now.ToString("yyyy-MM-dd HH"),
                resourcecode = GlobalConfig.GetInstance().ResourceCode,
                rtnumber = 0,
                realpeopleInto = 0
            };
            foreach (FlowCount flowCount in flowCounts)
            {
                flowInfo.rtnumber += flowCount.Detained;
                flowInfo.realpeopleInto += flowCount.Entered;
            }
            flowUploadInfo.data.Add(flowInfo);

            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                String json = js.Serialize(flowUploadInfo.data);
                String url = GlobalConfig.GetInstance().RtPeopleUrl;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                string strContent = String.Format("clustertag={0}&data={1}", GlobalConfig.GetInstance().ClusterTag, json);
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

                DataStruct.HttpResponse response = js.Deserialize<DataStruct.HttpResponse>(responseContent);
                if(response.code != 0)
                {
                    Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, String.Format("推送人流量数据失败,错误:{0}", response.message));
                }
                else
                {
                    Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("推送人流量数据成功,实时人数={0},进入人数={1}", flowInfo.rtnumber, flowInfo.realpeopleInto));
                }
            }catch(Exception e)
            {
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, String.Format("推送人流量数据失败,错误:{0}", e.Message));
            }
        }

        protected void ZytfInternalUploadFlowCounts(List<FlowCount> flowCounts)
        {
            int entered = 0;
            int leaved = 0;
            foreach (FlowCount flowCount in flowCounts)
            {
                entered += flowCount.Entered;
                leaved += flowCount.Leaved;
            }
            ZytfFlowUploadInfo zytfFlowUploadInfo = new ZytfFlowUploadInfo
            {
                resourceType = "scenery",
                resourceCode = "A51072549065",
                total = entered.ToString(),
                realOutNumber = leaved.ToString(),
                pushTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                sourceType = "1"
            };
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                String json = js.Serialize(zytfFlowUploadInfo);
                String url = GlobalConfig.GetInstance().ZytfUrl;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                String strContent = String.Format("data={0}", AESEncrypt(json, AES_KEY, AES_IV));
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

                DataStruct.HttpResponse response = js.Deserialize<DataStruct.HttpResponse>(responseContent);
                if (response.code != 0)
                {
                    Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, String.Format("推送智游天府人流量数据失败,错误:{0}", response.message));
                }
                else
                {
                    Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_INFO, String.Format("推送智游天府人流量数据成功,入园={0},出园={1}", entered, leaved));
                }
            }
            catch(Exception e)
            {
                Logger.GetInstance().Log(Logger.LOG_LEVEL.LOG_ERROR, String.Format("推送智游天府人流量数据失败,错误:{0}", e.Message));
            }
        }

        public static string ToHexString(byte[] bytes)

        {
            string hexString = string.Empty;

            if (bytes != null)

            {

                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)

                {

                    strB.Append(bytes[i].ToString("X2"));

                }

                hexString = strB.ToString();

            }
            return hexString;

        }

        public string AESEncrypt(String Data, String Key, String Vector)
        {
            Byte[] plainBytes = Encoding.UTF8.GetBytes(Data);

            Byte[] bKey = new Byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);
            Byte[] bVector = new Byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(bVector.Length)), bVector, bVector.Length);

            Byte[] Cryptograph = null; // 加密后的密文

            Rijndael Aes = Rijndael.Create();
            Aes.Mode = CipherMode.CBC;
            Aes.Padding = PaddingMode.ISO10126;
            try
            {
                // 开辟一块内存流
                using (MemoryStream Memory = new MemoryStream())
                {
                    // 把内存流对象包装成加密流对象
                    using (CryptoStream Encryptor = new CryptoStream(Memory,
                     Aes.CreateEncryptor(bKey, bVector),
                     CryptoStreamMode.Write))
                    {
                        // 明文数据写入加密流
                        Encryptor.Write(plainBytes, 0, plainBytes.Length);
                        Encryptor.FlushFinalBlock();

                        Cryptograph = Memory.ToArray();
                    }
                }
            }
            catch
            {
                Cryptograph = null;
            }

            return ToHexString(Cryptograph);
        }
    }
}
