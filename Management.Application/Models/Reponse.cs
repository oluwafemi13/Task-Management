using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Models
{
    public class Reponse
    {
        public object Data { get; set; }
        public int ResponseCode { get; set; }
        public bool IsSuccess { get; set; }
        public string ReasonPhrase { get; set; }

    }
}
