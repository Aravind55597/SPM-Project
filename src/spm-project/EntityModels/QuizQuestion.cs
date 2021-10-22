using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPM_Project.EntityModels
{
    public class QuizQuestion : IEntityWithId
    {
        private int _Marks;

        public int Id { get; private set; }

        [Url(ErrorMessage = "Invalid URL!")]
        public string ImageUrl { get; set; }

        public string Question { get; set; }

        [Required(ErrorMessage = "Please provide a discriminator")]
        //this is discriminator ; auto set by efcore
        public string QuestionType { get; set; }

        //either array of integers for MCQ
        //true false
        //ans (serialise object and dump it in)
        /// <summary>
        /// eg.
        /// [
        /// 1,
        /// 2,
        /// 4
        /// ]
        /// </summary>
        ///

        //can't be set to private as ORM would not work otherwise
        public string Answer { get; set; }

        public int Marks
        {
            get
            {
                return _Marks;
            }
            set
            {
                _Marks = value;
                if (!IsGraded)
                {
                    _Marks = 0;
                }
            }
        }

        public List<UserAnswer> UserAnswers { get; set; }

        public bool IsGraded { get; set; }


        public Quiz Quiz { get; set; }

        //retrieve discriminators to check
        [NotMapped]
        public static List<string> Discriminators
        {
            //throw  FormatException if parse fails
            get
            {
                var dList = new List<string>()
                {
                   "TFQuestion","McqQuestion"
                };
                return dList;
            }
        }
    }

    public class TFQuestion : QuizQuestion
    {
        public string TrueOption { get; set; }

        public string FalseOption { get; set; }

        public bool GetAnswer()
        {
            return bool.Parse(Answer);
        }

        public void SetAnswer(bool ans)
        {
            Answer = ans.ToString();
        }
    }

    public class McqQuestion : QuizQuestion
    {
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

        //check if multiselect or not
        public bool IsMultiSelect { get; set; }

        //get answer
        public List<int> GetAnswer()
        {
            //throw  FormatException if parse fails
            List<int> ans = new List<int>();
            foreach (var num in Answer.Split

             (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                ans.Add(Int32.Parse(num));
            }

            if (!IsMultiSelect)
            {
                return (List<int>)ans.GetRange(0, 1);
            }
            return ans;
        }

        //set answer
        public void SetAnswer(List<int> ans)
        {
            //throw  FormatException if parse fails
            string stringAns = "";

            for (int i = 0; i < ans.Count; i++)
            {
                stringAns += ans[i].ToString();
                if (!IsMultiSelect)
                {
                    break;
                }
                if (i != ans.Count - 1)
                {
                    stringAns += ",";
                }
            }

            Answer = stringAns;
        }
    }
}