﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthJournal.Core.DataAccess.Model
{
    [Index(nameof(ExternalId))]
    public class User
    {
        public Guid Id { get; set; }
        [MaxLength(255)]
        public string ExternalId { get; set; }
        [MaxLength(320)]
        public string Email { get; set; }
        [DefaultValue("false")]
        public bool ConsentCEM { get; set; }
        public WeightUnit PreferredWeightUnit { get; set; }
        public Country? UserCountry { get; set; }
    }
}
