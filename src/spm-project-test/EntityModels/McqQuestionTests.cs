using Xunit;
using SPM_Project.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels.Tests
{
    public class McqQuestionTests : IDisposable
    {


        private McqQuestion _testQuestionMulti ;
        private McqQuestion _testQuestionSingle;
        private McqQuestion _testQuestionMultiGraded;
        private McqQuestion _testQuestionSingleGraded;

        private List<int> _expectedMultiAnswer;
        private List<int> _expectedSingleAnswer;


        private McqQuestion CreateTestQuestion(bool isGraded ,bool isMulti)
        {
          
            var answer = isMulti ? "1,2,3" : "1";

            var question = new McqQuestion()
            {
                ImageUrl = "https://cdn.pixabay.com/photo/2015/04/23/22/00/tree-736885__480.jpg",
                Question = "Lorem Question", 
                QuestionType = "McqQuestion",
                Answer= answer,
                Marks = 5,
                Option1="Lorem Option 1",
                Option2 = "Lorem Option 2",
                Option3 = "Lorem Option 3",
                Option4 = "Lorem Option 4",
                IsMultiSelect = isMulti,
            };


            return question; 
            //typeof(Course).GetProperty(nameof(courseClass.Course.Id)).SetValue(courseClass.Course, 1);
        }

        //setup--------------------------------------------------
        public McqQuestionTests()
        {
            _testQuestionMulti = CreateTestQuestion(false,true);
            _testQuestionSingle= CreateTestQuestion(false, false);
            _testQuestionMultiGraded= CreateTestQuestion(true, true);
            _testQuestionSingleGraded = CreateTestQuestion(true,false);


            _expectedMultiAnswer = new List<int>() {1,2,3};

            _expectedSingleAnswer = new List<int> {1}; 
        }

        //setup--------------------------------------------------


        //tear down-----------------------------------------------------------------------------
        public void Dispose()
        {
            _testQuestionMulti = null;
            _testQuestionSingle = null;
            _testQuestionMultiGraded = null;
            _testQuestionSingleGraded = null;
        }


        [Fact()]
        //returns a list of integers when parsing from string to int is valid 
        public void GetAnswerTest()
        {
            //MultiAnswersValid_ReturnListOfIntegers
            Assert.Equal(_expectedMultiAnswer, _testQuestionMulti.GetAnswer());
            //MultiAnswersValid_ReturnListOfIntegers
            Assert.Equal(_expectedSingleAnswer, _testQuestionSingle.GetAnswer());

            //MultiAnswersNotValid_ReturnFormatException
            _testQuestionMulti.Answer = "a,b,c";
            Func<List<int>> actionMulti = (() => _testQuestionMulti.GetAnswer());
            Assert.Throws<FormatException>(actionMulti);

            //SingleAnswerNotValid_ReturnFormatException
            _testQuestionSingle.Answer = "a";
            Func<List<int>> actionSingle = (() => _testQuestionSingle.GetAnswer());
            Assert.Throws<FormatException>(actionSingle);

            //return 1 answer when multiple answers are present for a question that accepts 1 answer only
            _testQuestionSingle.Answer = "1";
            Assert.Equal(_expectedSingleAnswer, _testQuestionSingle.GetAnswer());


        }




        [Fact()]
        //returns a list of integers when parsing from string to int is valid 
        public void SetAnswerTest()
        {
            //Set list of answers _ ReturnListOfIntegers
            var ans1 = new List<int>() { 1, 2 };
            _testQuestionMulti.SetAnswer(ans1); 
            //when directly assessed 
            Assert.Equal("1,2", _testQuestionMulti.Answer);
            //when not directly assessed; via method instead
            Assert.Equal(ans1, _testQuestionMulti.GetAnswer());


            //set list of int with 1 int _ return 1 int 
            var ans2 = new List<int>() {1};
            _testQuestionSingle.SetAnswer(ans2);
            //when directly assessed 
            Assert.Equal("1", _testQuestionSingle.Answer);
            //when not directly assessed; via method instead
            Assert.Equal(ans2, _testQuestionSingle.GetAnswer());


            //set list of int with many int _ return 1 int
            var ans3 = new List<int>() {1,2,3};
            var expected = new List<int>() { 1};
            _testQuestionSingle.SetAnswer(ans3);
            //when directly assessed 
            Assert.Equal("1", _testQuestionSingle.Answer);
            //when not directly assessed; via method instead
            Assert.Equal(expected, _testQuestionSingle.GetAnswer());

        }








    }
}