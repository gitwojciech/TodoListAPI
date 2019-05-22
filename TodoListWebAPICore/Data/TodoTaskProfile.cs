using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TodoListWebAPICore.Data.Entities;
using TodoListWebAPICore.Models;

namespace TodoListWebAPICore.Data
{
    public class TodoTaskProfile: Profile
    {

        public TodoTaskProfile()
        {
            this.CreateMap<TodoTask, TodoTaskModel>()
                .ReverseMap();
                
        }

    }
}
