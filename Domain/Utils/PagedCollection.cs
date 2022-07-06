using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utils
{
    public class PagedCollection<TEntity>
    {
        #region Properties

        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public virtual int PageSize { get; set; } 

        [DataMember]
        public int TotalItems { get; set; }

        [DataMember]
        public List<TEntity>? Items { get; set; }

        //[DataMember]
        //public string Filter { get; set; }

        [DataMember]
        public Dictionary<string, bool>? Order { get; set; }



        //public int CurrentPage { get; set; }
        //public int TotalPages { get; set; }
        //public bool HasPrevious => CurrentPage > 1;
        //public bool HasNext => CurrentPage < TotalPages;

        //public PagedCollection(int pageIndex, int pageSize, int totalItems, List<TEntity> items)
        //{
        //    PageIndex = pageIndex;
        //    PageSize = pageSize;
        //    TotalItems = totalItems;
        //    Items = items;
        //    TotalPages = (int)Math.Ceiling(TotalItems / (double)pageSize);
        //    //AddRange(items);
        //}

        #endregion


    }
}
