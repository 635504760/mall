using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallPartner.DataObject
{
    public class Page<T>
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
        public IList<T> List { get; set; }

        public Page()
        {
            List = new List<T>();
        }

        public Page(IList<T> list, int pageNum, int pageSize, long totalCount)
        {
            List = list;
            PageNum = pageNum;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
