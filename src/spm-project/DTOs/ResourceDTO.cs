using SPM_Project.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.DTOs
{
    public class ResourceDTO
    {


        public ResourceDTO()
        {
            //for model binding
        }

        public ResourceDTO(Resource domain)
        {
            //for model binding
            Id = domain.Id;
            ContentUrl = domain.ContentUrl;
            DownloadableContentUrl = domain.DownloadableContentUrl;
            Content = domain.Content.ToString();

            if (domain.Chapter!=null)
            {
                ChapterId = domain.Chapter.Id;
            }

        }



        public int Id { get; private set; }

        public string ContentUrl { get; set; }

        public string DownloadableContentUrl { get; set; }

        public string Content { get; set; }

        public int ChapterId { get; set; }






    }
}
