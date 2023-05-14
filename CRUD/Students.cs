﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRUD
{
    internal class Students
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [StringLength(10)]
        public string Attitude { get; set; }
    }
}
