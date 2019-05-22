using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListWebAPICore.Models
{
    public class TodoTaskModel
    {
        [Required]
        [MaxLength(512)]
        public string Description { get; set; }
        public bool Status { get; set; }

    }
}
