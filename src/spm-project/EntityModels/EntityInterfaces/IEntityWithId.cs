using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public interface IEntityWithId
    {

        public int Id { get;  }


        public DateTime CreationTimestamp { get; set; }

        public DateTime UpdateTimestamp { get; set; }


    }
}
