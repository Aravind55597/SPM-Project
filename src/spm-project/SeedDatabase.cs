using Microsoft.EntityFrameworkCore;
using SPM_Project.Data;
using SPM_Project.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPM_Project.Utility
{
    public class SeedDatabase
    {
        public static void Initialize(ApplicationDbContext dbContext)
        {
            //USERS CREATED------------------------------------------------------------------------------\

            //14 senior engineers
            //60 enginneers
            //lmsusers of id from 1 to 100 have been created
            //1 TO 14 are senior engineers
            //15-89 are junior engineers
            //90 to 100 are administrators 


            //COURSES ETC. CREATED-----------------------------------------------------------------------------------------


            //each course have 2 classes
            //each class have 10 chapters
            //each chapter have a video for now

            //COURSE 1 TO 4 (No prerequisites )

            //g1->FUTURE CLASSES : EMPTY
            //g2->ONGOING CLASSES : TRAINERS ,LEARNERS,  CHAPTERS , RESOURCES (JUST VIDEO FOR NOW) , QUIZ QUESTIONS , QUIZ MODEL ANSWERS


            //COURSE 5 TO 8 (have prerequisites)

            //g1->FUTURE CLASSES : EMPTY
            //g2->ONGOING CLASSES : TRAINERS ,LEARNERS,  CHAPTERS , RESOURCES (JUST VIDEO FOR NOW) , QUIZ QUESTIONS , QUIZ MODEL ANSWERS

            //EACH CLASS HAVE MAX 30


            //so LMSuser 1 (Trainer) will train course1 g2 & course 5 g2 (Requires course 1 as prerequisite)

            //LMSuser 15 (Leaner) will be in course1 g2


            //the rest will be free flow 

            //plan add learners to  course 1 ,2 ,3 , 4  g2

            //course 1 ,2 , 3, 4 g2 classes 
            //ids are 26 , 28 , 30 , 32 
            //each class will have 10 learners 

            //DATA------------------------------------------------------------------------------------------------

            //var links = new List<string>() {
            //    "https://www.youtube.com/watch?v=r-X9coYTOV4&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs",
            //    "https://www.youtube.com/watch?v=r3MFuAdkHEM&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=2",
            //    "https://www.youtube.com/watch?v=-HpIzM11xJI&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=3",
            //    "https://www.youtube.com/watch?v=PqjwxoUa9Z4&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=4",
            //    "https://www.youtube.com/watch?v=KAgU0PDcsH4&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=5",
            //    "https://www.youtube.com/watch?v=NZXq4HPJwT8&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=6",
            //    "https://www.youtube.com/watch?v=YlqWTID1-4s&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=7",
            //    "https://www.youtube.com/watch?v=E5AbNQVhmU8&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=8",
            //    "https://www.youtube.com/watch?v=p5iXLloIfqs&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=9",
            //    "https://www.youtube.com/watch?v=1XACmWnK0MU&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=11"
            //    };

            //var imageUrl = "https://inmagazine.ca/wp-content/uploads/2020/04/Hey-Can-I-Ask-You-A-Question.jpg";


            //List<Chapter> CreateChapters(string courseName)
            //{
            //    List<Chapter> chapters = new List<Chapter>();

            //    //CREATE 10 CHAPTERS PER COURSE
            //    for (int n = 0; n < 10; n++)
            //    {
            //        var chapterName = courseName + " Chapter " + n.ToString();
            //        chapters.Add(

            //            new Chapter()
            //            {
            //                Name = chapterName,
            //                Description = Faker.Lorem.Sentence(),
            //                //CreationTimeStamp=DateTime.Now,
            //                //UpdateTimeStamp=DateTime.Now,
            //                Resources = new List<Resource>()
            //                {
            //                    new Resource()
            //                    {
            //                        ContentUrl=links[n],
            //                        Content = ContentType.Video,
            //                    },

            //                },
            //                Quizzes = new List<Quiz>()
            //                {
            //                    new Quiz()
            //                    {
            //                        Name=chapterName+" Quiz 1",
            //                        Description= Faker.Lorem.Paragraph(),
            //                        TimeLimit=20,
            //                    },
            //                    new Quiz()
            //                    {
            //                        Name=chapterName+" Quiz 2",
            //                        Description= Faker.Lorem.Paragraph(),
            //                    }
            //                }
            //            }

            //            );
            //    }


            //    return chapters;
            //}


            //var courses = dbContext.CourseClass.Include(cc=>cc.Course).Where(cc => cc.Chapters.Count < 1).ToList();

            //foreach (var item in courses)
            //{
            //    var chapList =  CreateChapters(item.Course.Name);
            //    item.Chapters = chapList; 
            //}


            //dbContext.SaveChanges(); 

            //var tf = dbContext.TFQuestion.ToList();

            //foreach (var item in tf)
            //{
            //    item.TrueOption = "lorem Option True";
            //    item.FalseOption = "lorem Option False";

            //}

            //dbContext.SaveChanges();

            //var test = dbContext.Quiz.Find(586); 
        }
    }
}