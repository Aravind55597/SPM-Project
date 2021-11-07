using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Reflection;
using LinqKit;

namespace SPM_Project.DataTableModels.DataTableRequest
{
    public class DTRequestHandler<T>
    {


        public DTRequestHandler(DTParameterModel dTParameterModel)
        {
            Draw = dTParameterModel.Draw;
            Start = dTParameterModel.Start;
            Length = dTParameterModel.Start;
            SortColumn = dTParameterModel.Columns[dTParameterModel.Order[0].Column].Data;
            SortColumnDirection = dTParameterModel.Order[0].Dir;
            SearchValue = dTParameterModel.Search.Value;
            PageSize = dTParameterModel.Length;
            FilterList = dTParameterModel.Filter;
            //number of records to be skipped
            Skip = dTParameterModel.Start;
            RecordsTotal = 0;
            RecordsFiltered = 0;
        }

        public int Draw { get; set; }

        public int Start { get; set; }


        public int Length { get; set; }


        public string SortColumn { get; set; }


        public string SortColumnDirection { get; set; }


        public string SearchValue { get; set; }

        public int PageSize { get; set; }


        public List<DTFilter> FilterList { get; set; }


        public int Skip { get; set; }


        public int RecordsTotal { get; set; }


        public int RecordsFiltered { get; set; }

                
        //DATATABLE-FUNCTIONS--------------------------------------------------------------------------------------------------------
       
        
        //ok
        
        public IQueryable<T> TableSorter( IQueryable<T> queryable)
        {
            if (!(string.IsNullOrEmpty(SortColumn) && string.IsNullOrEmpty(SortColumnDirection)))
            {
                queryable = queryable.OrderBy(SortColumn + " " + SortColumnDirection);
            }

            return queryable;
        }


        //ok
        public virtual IQueryable<T> TableFilterer(IQueryable<T> queryable)
        {

            if (FilterList != null)
            {
                foreach (DTFilter filter in FilterList)
                {
                    if (filter.Column != null && filter.Value != null)
                    {
                        //queryable= queryable.Where("@0 = @1", filter.Column, filter.Value);
                        //queryable = queryable.Where(q=>q.);
                        queryable = queryable.Where($"{filter.Column}=\"{filter.Value}\"");
                    }

                }
            }


            return queryable; 
        }

        //ok
        public virtual IQueryable<T> TablePager(IQueryable<T> queryable)
        {

            queryable = queryable.Skip(Skip).Take(PageSize); 


            return queryable;
        }

        //count record 
        public virtual void RecordsCounter(IQueryable<T> queryable)
        {

            RecordsTotal = queryable.Count(); 

            
        }

        //count fitlered record 
        public virtual void FilteredRecordsCounter(IQueryable<T> queryable)
        {

            RecordsFiltered = queryable.Count();

        }



    }
}
