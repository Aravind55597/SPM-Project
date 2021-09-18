﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class Quiz
    {
        public int Id { get; set; }


        public string Name { get; set; }

        public string Description { get; set; }


        public DateTime CreationTimeStamp { get; set; }


        public DateTime UpdateTimeStamp { get; set; }

        
        public int Score { get; set; }

        public Chapter Chapter { get; set; }


        public QuizQuestion Questions { get; set; }
    }





}
