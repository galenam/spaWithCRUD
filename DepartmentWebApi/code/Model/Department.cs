using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace code.Model
{
    public partial class Department
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
