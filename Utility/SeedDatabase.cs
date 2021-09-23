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


            //start user id -> 15
           
            //add to current class g2 course 1 to 4--------------------------------------------------------------------------------------------------------------------
            for (int i = 15; i < 25; i++)
            {
                var user = dbContext.LMSUser.FirstOrDefault(u=>u.Id==i);

                var enrollment = new ClassEnrollmentRecord()
                {
                    CompletionStatus = false,
                    Approved = true,
                    CourseClass= dbContext.CourseClass.FirstOrDefault(c => c.Id == 26)
                };

                user.Enrollments = new List<ClassEnrollmentRecord>() {



                    enrollment



                };
                    
 
                    
              

            }

            for (int i = 25; i < 35; i++)
            {
                var user = dbContext.LMSUser.FirstOrDefault(u => u.Id == i);

                var enrollment = new ClassEnrollmentRecord()
                {
                    CompletionStatus = false,
                    Approved = true,
                    CourseClass = dbContext.CourseClass.FirstOrDefault(c => c.Id == 28)
                };


                user.Enrollments = new List<ClassEnrollmentRecord>() {



                    enrollment



                };
            }

            for (int i = 35; i < 45; i++)
            {
                var user = dbContext.LMSUser.FirstOrDefault(u => u.Id == i);

                var enrollment = new ClassEnrollmentRecord()
                {
                    CompletionStatus = false,
                    Approved = true,
                    CourseClass = dbContext.CourseClass.FirstOrDefault(c => c.Id == 30)
                };

                user.Enrollments = new List<ClassEnrollmentRecord>() {



                    enrollment



                };
            }

            for (int i = 45; i < 55; i++)
            {
                var user = dbContext.LMSUser.FirstOrDefault(u => u.Id == i);

                var enrollment = new ClassEnrollmentRecord()
                {
                    CompletionStatus = false,
                    Approved = true,
                    CourseClass = dbContext.CourseClass.FirstOrDefault(c => c.Id == 32)
                };


                user.Enrollments = new List<ClassEnrollmentRecord>() {



                    enrollment



                };
            }


            //add to current class g2 course 5 to 8--------------------------------------------------------------------------------------------------------------------

            for (int i = 55; i < 63; i++)
            {
                var user = dbContext.LMSUser.FirstOrDefault(u => u.Id == i);

                var enrollment = new ClassEnrollmentRecord()
                {
                    CompletionStatus = false,
                    Approved = true,
                    CourseClass = dbContext.CourseClass.FirstOrDefault(c => c.Id == 34)
                };


                user.Enrollments = new List<ClassEnrollmentRecord>() {



                    enrollment



                };

            }

            for (int i = 63; i < 71; i++)
            {
                var user = dbContext.LMSUser.FirstOrDefault(u => u.Id == i);

                var enrollment = new ClassEnrollmentRecord()
                {
                    CompletionStatus = false,
                    Approved = true,
                    CourseClass = dbContext.CourseClass.FirstOrDefault(c => c.Id == 36)
                };


                user.Enrollments = new List<ClassEnrollmentRecord>() {



                    enrollment



                };
            }

            for (int i = 71; i < 79; i++)
            {
                var user = dbContext.LMSUser.FirstOrDefault(u => u.Id == i);

                var enrollment = new ClassEnrollmentRecord()
                {
                    CompletionStatus = false,
                    Approved = true,
                    CourseClass = dbContext.CourseClass.FirstOrDefault(c => c.Id == 38)
                };


                user.Enrollments = new List<ClassEnrollmentRecord>() {



                    enrollment



                };
            }

            for (int i = 79; i < 89; i++)
            {
                var user = dbContext.LMSUser.FirstOrDefault(u => u.Id == i);

                var enrollment = new ClassEnrollmentRecord()
                {
                    CompletionStatus = false,
                    Approved = true,
                    CourseClass = dbContext.CourseClass.FirstOrDefault(c => c.Id == 40)
                };


                user.Enrollments = new List<ClassEnrollmentRecord>() {



                    enrollment



                };
            }



            //NOT CREATED-----------------------------------------------------------------------------------------

            //pdf,word,docs for resources


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

            var imageUrl = "https://inmagazine.ca/wp-content/uploads/2020/04/Hey-Can-I-Ask-You-A-Question.jpg";


            var create = DateTime.Now;

            var questionOptionmMCQ = new List<string>()
            {
                "Answer A" , "Answer B" , "Answer C" , "Answer D"
            };

            var tfStrings = new List<string>()
            {
                "true","false"
            };


            //create list of ungraded quiz questions
            List<QuizQuestion> CreateListQuestionsUngraded()
            {
                List<QuizQuestion> questions1 = new List<QuizQuestion>();

                //multiseldect mcq
                for (int z = 0; z < 3; z++)
                {
                    Random rnd = new Random();
                    var ans = rnd.Next(0, 3);
                    questions1.Add(new McqQuestion()
                    {
                        ImageUrl = imageUrl,
                        Question = Faker.Lorem.Sentence(),
                        Option1 = questionOptionmMCQ[0],
                        Option2 = questionOptionmMCQ[1],
                        Option3 = questionOptionmMCQ[2],
                        Option4 = questionOptionmMCQ[3],
                        IsMultiSelect = true,
                        Answer = $"{rnd.Next(0, 3).ToString()},{rnd.Next(0, 3).ToString()},{rnd.Next(0, 3).ToString()}",
                        QuestionType = "McqQuestion"
                    }); ;
                }

                //sngle select mcq
                for (int y = 0; y < 3; y++)
                {
                    Random rnd = new Random();
                    var ans = rnd.Next(0, 3);
                    questions1.Add(new McqQuestion()
                    {
                        ImageUrl = imageUrl,
                        Question = Faker.Lorem.Sentence(),
                        Option1 = questionOptionmMCQ[0],
                        Option2 = questionOptionmMCQ[1],
                        Option3 = questionOptionmMCQ[2],
                        Option4 = questionOptionmMCQ[3],
                        Answer = rnd.Next(0, 3).ToString(),
                        QuestionType = "McqQuestion"
                    }); ;
                }

                //true false
                for (int g = 0; g < 3; g++)
                {
                    Random rnd = new Random();
                    var ans = rnd.Next(0, 1);
                    questions1.Add(new TFQuestion()
                    {
                        ImageUrl = imageUrl,
                        Question = Faker.Lorem.Sentence(),
                        TrueOption = questionOptionmMCQ[0],
                        FalseOption = questionOptionmMCQ[1],
                        Answer = tfStrings[rnd.Next(0, 1)],
                        QuestionType = "TFQuestion"
                    }); ;
                }

                return questions1;
            }

            //create list of ungraded questions 

            List<QuizQuestion> CreateListQuestionsGraded()
            {
                List<QuizQuestion> questions = new List<QuizQuestion>();

                //multiseldect mcq
                for (int z = 0; z < 3; z++)
                {
                    Random rnd = new Random();
                    var ans = rnd.Next(0, 3);
                    questions.Add(new McqQuestion()
                    {
                        ImageUrl = imageUrl,
                        Question = Faker.Lorem.Sentence(),
                        Option1 = questionOptionmMCQ[0],
                        Option2 = questionOptionmMCQ[1],
                        Option3 = questionOptionmMCQ[2],
                        Option4 = questionOptionmMCQ[3],
                        IsMultiSelect = true,
                        Answer = $"{rnd.Next(0, 3).ToString()},{rnd.Next(0, 3).ToString()},{rnd.Next(0, 3).ToString()}",
                        QuestionType = "McqQuestion",
                        Marks = 4
                    }); ;
                }

                //sngle select mcq
                for (int y = 0; y < 3; y++)
                {
                    Random rnd = new Random();
                    var ans = rnd.Next(0, 3);
                    questions.Add(new McqQuestion()
                    {
                        ImageUrl = imageUrl,
                        Question = Faker.Lorem.Sentence(),
                        Option1 = questionOptionmMCQ[0],
                        Option2 = questionOptionmMCQ[1],
                        Option3 = questionOptionmMCQ[2],
                        Option4 = questionOptionmMCQ[3],
                        Answer = rnd.Next(0, 3).ToString(),
                        QuestionType = "McqQuestion",
                        Marks = 2
                    }); ;
                }

                //true false
                for (int g = 0; g < 3; g++)
                {
                    Random rnd = new Random();
                    var ans = rnd.Next(0, 1);
                    questions.Add(new TFQuestion()
                    {
                        ImageUrl = imageUrl,
                        Question = Faker.Lorem.Sentence(),
                        TrueOption = questionOptionmMCQ[0],
                        FalseOption = questionOptionmMCQ[1],
                        Answer = tfStrings[rnd.Next(0, 1)],
                        QuestionType = "TFQuestion",
                        Marks = 1
                    }); ;
                }

                return questions;

            }

            //create list of chapters (10)
            List<Chapter> CreateChapters(string courseName, List<QuizQuestion> ungradedQuizzes)
            {
                List<Chapter> chapters = new List<Chapter>();

                //CREATE 10 CHAPTERS PER COURSE
                for (int n = 0; n < 10; n++)
                {
                    var chapterName = courseName + " Chapter " + n.ToString();
                    chapters.Add(

                        new Chapter()
                        {
                            Name = chapterName,
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

                            },
                            Quizzes = new List<Quiz>()
                            {
                                new Quiz()
                                {
                                    Name=chapterName+" Quiz 1",
                                    Description= Faker.Lorem.Paragraph(),
                                    CreationTimeStamp=create ,
                                    TimeLimit=20,
                                    Questions=ungradedQuizzes
                                },
                                new Quiz()
                                {
                                    Name=chapterName+" Quiz 2",
                                    Description= Faker.Lorem.Paragraph(),
                                    CreationTimeStamp=create ,
                                    Questions=ungradedQuizzes
                                }
                            }
                        }

                        );
                }


                return chapters; 
            }


            //create list of ungraded quiz questions
            var ungradedQuestions = CreateListQuestionsUngraded();


            //create list of graded quiz questions 
            var gradedQuestions = CreateListQuestionsGraded();



            //DATA CREATION------------------------------------------------------------------------------------------------

            var transaction = dbContext.Database.BeginTransaction();

            //CREATE LMSUSERS----------------------------------------------------

            void CreateLMSUsers()
            {

                LMSUser lmsUser = new LMSUser(); 
                for (int i = 0; i < 100; i++)
                {
                    lmsUser = new LMSUser()
                    {
                    };

                    dbContext.Add(lmsUser);

                }
                dbContext.SaveChanges();
            }


            //CREATE COURSES 1 to 4 (NO PREREQUISITES)-------------------------------------------------------------------------------------------------------

            //g1->FUTURE CLASSES : EMPTY
            //g2->ONGOING CLASSES : TRAINERS ,LEARNERS,  CHAPTERS , RESOURCES (JUST VIDEO FOR NOW) , QUIZ QUESTIONS , QUIZ MODEL ANSWERS

            void CreateCourses1to4()
            {

                for (int i = 1; i < 5; i++)
                {



                    //COURSE NAME
                    var name = "Course " + i.ToString();

                    //CHAPTER LIST
                    List<Chapter> chapters = new List<Chapter>();

                    //CREATE 10 CHAPTERS PER COURSE
                    for (int n = 0; n < 10; n++)
                    {
                        var chapterName = name + " Chapter " + n.ToString();
                        chapters.Add(

                            new Chapter()
                            {
                                Name = chapterName,
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

                                },
                                Quizzes = new List<Quiz>()
                                {
                                new Quiz()
                                {
                                    Name=chapterName+" Quiz 1",
                                    Description= Faker.Lorem.Paragraph(),
                                    CreationTimeStamp=create ,
                                    TimeLimit=20,
                                    Questions= ungradedQuestions
                                },
                                new Quiz()
                                {
                                    Name=chapterName+" Quiz 2",
                                    Description= Faker.Lorem.Paragraph(),
                                    CreationTimeStamp=create ,
                                    Questions= ungradedQuestions
                                }
                                }
                            }

                            );
                    }

                    var course = new Course()
                    {
                        Name = name,
                        CreationTimestamp = create,
                        Description = Faker.Lorem.Sentence(),
                        CourseClass = new List<CourseClass>()
                    {
                       new CourseClass(){
                           Name=name + " G1"   ,
                           CreationTimeStamp=create,
                           StartRegistration =create.AddMinutes(1),
                           EndRegistration = create.AddMonths(3),
                           StartClass =create.AddMonths(3).AddDays(1) ,
                           EndClass = create.AddMonths(5),
                           Slots= 30,
                       },

                        new CourseClass(){
                           Name=name + " G2"   ,
                           CreationTimeStamp=create,
                           StartRegistration =create.AddMinutes(1),
                           EndRegistration = create.AddMinutes(4),
                           StartClass=create.AddHours(1) ,
                           EndClass=create.AddMonths(1),
                           Slots= 30,
                           Chapters=chapters,
                           ClassTrainer = dbContext.LMSUser.FirstOrDefault(u=>u.Id==i),
                       },

                    }

                    };

                    dbContext.Add(course);
                }


            }



            //ADD GRADED QUIZZES TO COURSES FROM 1 to 4 (NO PREREQUISITES)-------------------------------------------------------------------------------------------------------

            void AddGradedQuiz1to4()
            {
                //ADD GRADED QUIZ TO  G2 CLASSES
                for (int i = 26; i < 33; i += 2)
                {
                    var courseClass = dbContext.CourseClass.FirstOrDefault(c => c.Id == i);

                    courseClass.GradedQuiz = new Quiz
                    {
                        Name = "Class" + " Graded Quiz",
                        Description = Faker.Lorem.Sentence(),
                        CreationTimeStamp = create,
                        IsGraded = true,
                        Questions = gradedQuestions,
                        TimeLimit = 20
                    };

                }

            }




            //CREATE COURSES 5 to 8 (HAVE PREREQUISITES)-------------------------------------------------------------------------------------------------------

            //g1->FUTURE CLASSES : EMPTY
            //g2->ONGOING CLASSES : TRAINERS ,LEARNERS,  CHAPTERS , RESOURCES (JUST VIDEO FOR NOW) , QUIZ QUESTIONS , QUIZ MODEL ANSWERS


            void CreateCourses5to8()
            {
                for (int i = 1; i < 5; i++)
                {
                    //QUIZ QUSTION LIST
                    List<QuizQuestion> questions3 = new List<QuizQuestion>();


                    //COURSE NAME
                    var name = "Course " + $"{i + 4}";

                    //CHAPTER LIST
                    List<Chapter> chapters = new List<Chapter>();

                    //CREATE 10 CHAPTERS PER COURSE CLASS
                    for (int n = 0; n < 10; n++)
                    {
                        var chapterName = name + " Chapter " + n.ToString();
                        chapters.Add(

                            new Chapter()
                            {
                                Name = chapterName,
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

                                },
                                Quizzes = new List<Quiz>()
                                {
                                new Quiz()
                                {
                                    Name=chapterName+" Quiz 1",
                                    Description= Faker.Lorem.Paragraph(),
                                    CreationTimeStamp=create ,
                                    TimeLimit=20,
                                    Questions= ungradedQuestions
                                },
                                new Quiz()
                                {
                                    Name=chapterName+" Quiz 2",
                                    Description= Faker.Lorem.Paragraph(),
                                    CreationTimeStamp=create ,
                                    Questions= ungradedQuestions
                                }
                                }
                            }

                            );
                    }

                    var course = new Course()
                    {
                        Name = name,
                        CreationTimestamp = create,
                        Description = Faker.Lorem.Sentence(),
                        CourseClass = new List<CourseClass>()
                    {
                        new CourseClass() {
                            Name = name + " G1",
                            CreationTimeStamp = create,
                            StartRegistration = create.AddMinutes(1),
                            EndRegistration = create.AddMonths(3),
                            StartClass = create.AddMonths(3).AddDays(1),
                            EndClass = create.AddMonths(5),
                            Slots = 30,
                        },

                        new CourseClass() {
                            Name = name + " G2",
                            CreationTimeStamp = create,
                            StartRegistration = create.AddMinutes(1),
                            EndRegistration = create.AddMinutes(4),
                            StartClass = create.AddHours(1),
                            EndClass = create.AddMonths(1),
                            Slots = 30,
                            Chapters = chapters,
                            ClassTrainer = dbContext.LMSUser.FirstOrDefault(u => u.Id == i),
                            GradedQuiz = new Quiz()
                            {
                                Name = "Class" + " Graded Quiz",
                                Description = Faker.Lorem.Sentence(),
                                CreationTimeStamp = create,
                                IsGraded = true,
                                Questions = gradedQuestions,
                                TimeLimit = 20
                            }
                        },

                    },
                        PreRequisites = new List<Course>()
                    {
                        dbContext.Course.FirstOrDefault(c=>c.Name==$"Course {i}")

                    }

                    };

                    dbContext.Add(course);
                }

                dbContext.SaveChanges();
            };



            dbContext.SaveChanges();

            transaction.Commit();
        }
    }
}