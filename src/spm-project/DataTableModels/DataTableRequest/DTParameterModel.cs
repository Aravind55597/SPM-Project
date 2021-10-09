using Microsoft.AspNetCore.Mvc;
using SPM_Project.DataTableModels.DataTableRequest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.DataTableModels
{

    public class DTParameterModel
    {
        /// <summary>
        /// Draw counter. This is used by DataTables to ensure that the Ajax returns from 
        /// server-side processing requests are drawn in sequence by DataTables 
        /// </summary>
        [Required]
        public int Draw { get; set; }

        /// <summary>
        /// Paging first record indicator. This is the start point in the current data set 
        /// (0 index based - i.e. 0 is the first record)
        /// </summary>
        [Required]
        public int Start { get; set; }

        /// <summary>
        /// Number of records that the table can display in the current draw. It is expected
        /// that the number of records returned will be equal to this number, unless the 
        /// server has fewer records to return. Note that this can be -1 to indicate that 
        /// all records should be returned (although that negates any benefits of 
        /// server-side processing!)
        /// </summary>
        [Required]
        public int Length { get; set; }

        /// <summary>
        /// Global Search for the table
        /// </summary>
        [Required]
        public DTSearch Search { get; set; }

        /// <summary>
        /// Collection of all column indexes and their sort directions
        /// </summary>
        [Required]
        public List<DTOrder> Order { get; set; }

        /// <summary>
        /// Collection of all columns in the table
        /// </summary>
        [Required]
        public List<DTColumn> Columns { get; set; }

       
    }
}
