using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Extensions
{
    public static class ListStringExtensions
    {

        public static List<int> CommaSepStringToIntList(this List<int> list, string item)
        {

            
            foreach (var num in item.Split

            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                list.Add(Int32.Parse(num));
            }

            return list; 
        }

        public static bool CommaSepStringToIntListValidator(this List<int> list, string item)
        {

            try
            {
                CommaSepStringToIntList(list, item);
                return true; 
            }
            catch(FormatException ex){
                return false; 
            }

        }


    }
}
