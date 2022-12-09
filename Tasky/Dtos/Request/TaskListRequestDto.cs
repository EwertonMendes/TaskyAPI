﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Tasky.Dtos.Request
{
    public class TaskListRequestDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }

        public bool Checked { get; set; }
    }
}
