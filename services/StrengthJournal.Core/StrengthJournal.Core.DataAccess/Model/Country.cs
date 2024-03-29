﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthJournal.Core.DataAccess.Model
{
    public class Country
    {
        [Key]
        [MaxLength(2)]
        public string Code { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
