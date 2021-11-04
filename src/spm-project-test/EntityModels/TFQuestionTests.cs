using Xunit;
using SPM_Project.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels.Tests
{
    public class TFQuestionTests : IDisposable
    {

        private TFQuestion _testQuestionSingle;

        private TFQuestion _testQuestionSingleGraded;


        private TFQuestion CreateTestQuestion(bool isGraded)
        {
            var marks = isGraded ? 0 : 5;

            var question = new TFQuestion()
            {
                ImageUrl = "https://cdn.pixabay.com/photo/2015/04/23/22/00/tree-736885__480.jpg",
                Question = "Lorem Question",
                QuestionType = "McqQuestion",
                Answer = "",
                Marks = marks,
                TrueOption = "Lorem Option True",
                FalseOption = "Lorem Option False",

            };


            return question;
            //typeof(Course).GetProperty(nameof(courseClass.Course.Id)).SetValue(courseClass.Course, 1);
        }

        //setup--------------------------------------------------
        public TFQuestionTests()
        {
            //_controller = new LearnerController();
            _testQuestionSingle = CreateTestQuestion(false);
            _testQuestionSingleGraded = CreateTestQuestion(true);

        }

        //setup--------------------------------------------------


        //tear down-----------------------------------------------------------------------------
        public void Dispose()
        {
            //_controller = null;
            _testQuestionSingle = null;
            _testQuestionSingleGraded = null;
        }


        [Fact()]
        //returns a list of integers when parsing from string to int is valid 
        public void GetAnswerTest()
        {
            //string true is added as answer_Return true 
            _testQuestionSingle.Answer = "true";
            Assert.True(_testQuestionSingle.GetAnswer());


            //string true is added as answer_Return true 
            _testQuestionSingleGraded.Answer = "true";
            Assert.True(_testQuestionSingleGraded.GetAnswer());

            //TODO ASSERT FOR EXCEPTION WHEN PARSING FAILS 
        }


        [Fact()]
        //returns a list of integers when parsing from string to int is valid 
        public void SetAnswerTest()
        {
            //string true is added as answer_Return true 
            _testQuestionSingle.SetAnswer(true);
            Assert.True(bool.Parse(_testQuestionSingle.Answer));
            Assert.True(_testQuestionSingle.GetAnswer());


            //string true is added as answer_Return true 
            _testQuestionSingleGraded.SetAnswer(true);
            Assert.True(bool.Parse(_testQuestionSingleGraded.Answer));
            Assert.True(_testQuestionSingleGraded.GetAnswer());

        }






    }
}