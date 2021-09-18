using Microsoft.AspNetCore.Mvc.ModelBinding;
using SPM_Project.DataTableModels.DataTableRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.DataTableModels.ModelBinder
{
    public class DataTableModelBinder
    {

        public class DTModelBinder : IModelBinder
        {
            public Task BindModelAsync(ModelBindingContext bindingContext)
            {

                var request = bindingContext.ActionContext.HttpContext.Request.Form;


                var test = request["columns[1][name]"];
                // Retrieve request data
                var draw = Convert.ToInt32(request["draw"]);
                var start = Convert.ToInt32(request["start"]);
                var length = Convert.ToInt32(request["length"]);
                // Search
                var search = new DTSearch
                {
                    Value = request["search[value]"],
                    Regex = Convert.ToBoolean(request["search[regex]"])
                };
                // Order
                var o = 0;
                var order = new List<DTOrder>();
                while (!string.IsNullOrEmpty(request["order[" + o + "][column]"]))
                {
                    order.Add(new DTOrder
                    {
                        Column = Convert.ToInt32(request["order[" + o + "][column]"]),
                        Dir = request["order[" + o + "][dir]"]
                    });
                    o++;
                }
                // Columns

                var c = 0;
                var columns = new List<DTColumn>();
                while (!string.IsNullOrEmpty(request["columns[" + c + "][name]"]))
                {
                    columns.Add(new DTColumn
                    {
                        Data = request["columns[" + c + "][data]"],
                        Name = request["columns[" + c + "][name]"],
                        Orderable = Convert.ToBoolean(request["columns[" + c + "][orderable]"]),
                        Searchable = Convert.ToBoolean(request["columns[" + c + "][searchable]"]),
                        Search = new DTSearch
                        {
                            Value = request["columns[" + c + "][search][value]"],
                            Regex = Convert.ToBoolean(request["columns[" + c + "][search][regex]"])
                        }
                    });
                    c++;
                }

                var result = new DTParameterModel
                {
                    Draw = draw,
                    Start = start,
                    Length = length,
                    Search = search,
                    Order = order,
                    Columns = columns
                };

                bindingContext.Result = ModelBindingResult.Success(result);

                return Task.CompletedTask;
            }

        }



















    }
}
