using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombsCityClient.DataStruct
{
    public class FlowInfo
    {
        public String time { get; set; }
        public String resourcecode { get; set; }
        public int rtnumber { get; set; }
        public int realpeopleInto { get; set; }
    }

    public class FlowUploadInfo
    {
        public FlowUploadInfo()
        {
            data = new List<FlowInfo>();
        }
        public List<FlowInfo> data { get; set; }
    }

    public class ZytfFlowUploadInfo
    {
        public String resourceType { get; set; }
        public String resourceCode { get; set; }
        public String total { get; set; }
        public String realOutNumber { get; set; }
        public String pushTime { get; set; }
        public String sourceType { get; set; }
    }
}
