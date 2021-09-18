using Newtonsoft.Json;
using SPM_Project.DataTableModels.DataTableDataInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.DataTableModels.DataTableResponse
{
    public class DTResponse
    {
        [JsonProperty(PropertyName = "draw")]
        public int Draw { get; set; }

        [JsonProperty(PropertyName = "recordsTotal")]
        public int RecordsTotal { get; set; }

        [JsonProperty(PropertyName = "recordsFiltered")]
        public int RecordsFiltered { get; set; }

        [JsonProperty(PropertyName = "data")]
        public List<IDTData> Data { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }



    }
}
