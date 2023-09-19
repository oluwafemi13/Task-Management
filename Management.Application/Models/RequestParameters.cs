using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Models
{
    public class RequestParameters
    {
        private int _pageSize = 20; 

        const int MaxPageSize = 50;
        public int _pageIndex { get; set; } = 1;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value < MaxPageSize) ? value : MaxPageSize;
            }
        }
    }
}
