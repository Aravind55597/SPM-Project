using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class Resources
    {
        public int Id { get; set; }

        public int ContentUrl { get; set; }


        public Content ContentType { get; set; }


        public DateTime CreationTimestamp { get; set; }

        public DateTime UpdateTimestamp { get; set; }
    }

    public enum Content
    {
        PDF, 
        Word,
        Excel,
        Video
    }

}
