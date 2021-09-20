using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class Resource
    {
        public int Id { get; set; }

        public int ContentUrl { get; set; }


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
        PowerPoint
    }

}
