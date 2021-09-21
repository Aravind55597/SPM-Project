using SPM_Project.Data;
using SPM_Project.EntityModels;
using SPM_Project.PracticeDatatableModelAndController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Utility
{
    public class SeedDatabase
    {

        public static void Initialize(ApplicationDbContext dbContext)
        {

            //USERS TO BE CREATED------------------------------------------------------------------------------\

                //14 senior engineers
                //60 enginneers
                //lmsusers of id from 1 to 100 have been created 
                //1 TO 14 are senior engineers 
                //15-100 are junior engineers 

            //CREATED-----------------------------------------------------------------------------------------

                //1-14 are seniors 
                //15 to 100 are juniors
                //1 to 15 are courses
                //each course have 2 classes
                //each class have 10 chapters 
                //each chapter have a video for now 

            //NOT CREATED-----------------------------------------------------------------------------------------

                //pdf,word,docs for resources 
                //prerequisites 


            //DATA------------------------------------------------------------------------------------------------

                var links = new List<string>() {
                "https://www.youtube.com/watch?v=r-X9coYTOV4&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs",
                "https://www.youtube.com/watch?v=r3MFuAdkHEM&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=2",
                "https://www.youtube.com/watch?v=-HpIzM11xJI&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=3",
                "https://www.youtube.com/watch?v=PqjwxoUa9Z4&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=4",
                "https://www.youtube.com/watch?v=KAgU0PDcsH4&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=5",
                "https://www.youtube.com/watch?v=NZXq4HPJwT8&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=6",
                "https://www.youtube.com/watch?v=YlqWTID1-4s&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=7",
                "https://www.youtube.com/watch?v=E5AbNQVhmU8&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=8",
                "https://www.youtube.com/watch?v=p5iXLloIfqs&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=9",
                "https://www.youtube.com/watch?v=1XACmWnK0MU&list=PLah6faXAgguOeMUIxS22ZU4w5nDvCl5gs&index=11"
                };

            //DATA CREATION------------------------------------------------------------------------------------------------

                var transaction = dbContext.Database.BeginTransaction();

            //CREATE LMSUSERS----------------------------------------------------

                //LMSUser lmsUser; 

                //for (int i = 0; i <100 ; i++)
                //{
                //    lmsUser = new LMSUser()
                //    {

                //    };



                //    dbContext.Add(lmsUser);

                //}

            dbContext.SaveChanges();

           

            //create courses , classes , chapters ,quiz , 


            //CREATE COURSES 1 to 8 (NO PREREQUISITES)-------------------------------------------------------------------------------------------------------


                    //CREATED FOR : To preassign students, To preassign Trainers 
            var create = DateTime.Now;

            for (int i = 1; i < 8; i++)
            {
                var name = Faker.Company.CatchPhrase();

                List<Chapter> chapters = new List<Chapter>();

                for (int n = 0; n < 10; n++)
                {
                    chapters.Add(
                        
                        new Chapter()
                        {
                            Name= Faker.Lorem.GetFirstWord(),
                            Description = Faker.Lorem.Sentence(),
                            CreationTimeStamp = create,
                            Resources = new List<Resource>()
                            {
                                new Resource()
                                {
                                    ContentUrl=links[n],
                                    Content = ContentType.Video,
                                    CreationTimestamp=create
                                },

                            }
                        }

                        );
                }
               
                var course = new Course()
                {
                    Name = name,
                    CreationTimestamp=create,
                    Description = Faker.Lorem.Sentence(),
                    CourseClass = new List<CourseClass>()
                    {
                        //g1 is for ongoing classes 
                        //can add quizzes etc.
                       new CourseClass(){ 
                           Name=name + " G1"   ,
                           CreationTimeStamp=create,
                           StartRegistration =create.AddMinutes(1),
                           EndRegistration = create.AddMinutes(4),
                           StartClass =create.AddHours(1) ,
                           EndClass = create.AddMonths(4),
                           ClassTrainer = dbContext.LMSUser.FirstOrDefault(u=>u.Id==i),
                           MaxStudents= 30,
                           Chapters=chapters
                       },

                       //g2 is for future classes to register
                        new CourseClass(){
                           Name=name + " G2"   ,
                           CreationTimeStamp=create,
                           StartRegistration =create.AddMinutes(1),
                           EndRegistration = create.AddMonths(3),
                           StartClass=create.AddMonths(2).AddHours(1) ,
                           EndClass=create.AddMonths(5),
                           ClassTrainer = dbContext.LMSUser.FirstOrDefault(u=>u.Id==i),
                           MaxStudents= 30,
                           Chapters=chapters
                       }
                    }



                };


            }



            //CREATE COURSES 8 to 10 (HAVE PREREQUISITES)-------------------------------------------------------------------------------------------------------








            //CREATE COURSES 8 to 10 (HAVE QUIZZES AND ANSWERS)-------------------------------------------------------------------------------------------------------






            transaction.Commit();
        }





    }
}
