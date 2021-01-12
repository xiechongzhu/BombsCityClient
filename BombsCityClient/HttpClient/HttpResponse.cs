using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombsCityClient.DataStruct
{
    class HttpResponse
    {
        public int code { get; set; }
        public String message { get; set; }
        public Int64 responseTime { get; set; }
    }
}
