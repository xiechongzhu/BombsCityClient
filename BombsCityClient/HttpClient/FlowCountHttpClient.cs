using BombsCityClient.DataStruct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BombsCityClient.HttpClient
{
    public class FlowCountHttpClient
    {
        protected delegate void UploadFlowCountsDelegate(List<FlowCount> flowCounts);

        public void UploadFlowCounts(List<FlowCount> flowCounts)
        {
            UploadFlowCountsDelegate uploadFlowCountsDelegate = new UploadFlowCountsDelegate(InternalUploadFlowCounts);
            uploadFlowCountsDelegate.BeginInvoke(flowCounts, UploadFlowCountsCallBack, null);
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
            FlowInfo flowInfo = new FlowInfo();
            flowInfo.time = DateTime.Now.ToString("yyyy-MM-dd HH");
            flowInfo.resourcecode = GlobalConfig.GetInstance().ResourceCode;
            flowInfo.rtnumber = 0;
            flowInfo.realpeopleInto = 0;
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
                String url = String.Format("{0}?clustertag={1}&data={2}", GlobalConfig.GetInstance().RtPeopleUrl, GlobalConfig.GetInstance().ClusterTag, json);
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "POST";
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
                String responseContent = streamReader.ReadToEnd();
                streamReader.Close();
                httpWebResponse.Close();

                FlowCountResponse response = js.Deserialize<FlowCountResponse>(responseContent);
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
    }
}
