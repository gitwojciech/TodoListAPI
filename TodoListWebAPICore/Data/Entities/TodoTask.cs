﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListWebAPICore.Data.Entities
{
    public class TodoTask
    {
        public long Id { get; set; } 
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
