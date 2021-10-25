using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class Resource : IEntityWithId
    {
        public int Id { get; private set;  }

        public string ContentUrl { get; set; }


        public ContentType Content { get; set; }


        public DateTime CreationTimestamp { get; set; }

        public DateTime UpdateTimestamp { get; set; }


        public Chapter Chapter { get; set; }
    }


    public enum ContentType
    {
        PDF, 
        Word,
        Excel,
        Video,
        PowerPoint,
        Hyperlink
    }


}
