using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infraestructure
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
        public List<TEntity> Items { get; set; }

        //[DataMember]
        //public string Filter { get; set; }

        [DataMember]
        public Dictionary<string, bool> Order { get; set; }

        #endregion
    }
}
