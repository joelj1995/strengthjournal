﻿using System.ComponentModel.DataAnnotations;

namespace StrengthJournal.MVC.Models
{
    public class SignUpModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Password2 { get; set; }
        public bool ConsentCEM { get; set; } = true;
        public string? Error { get; set; }
        [Required]
        public string CountryCode { get; set; }
        public IEnumerable<CountryDto> CountryList { get; set; } = new List<CountryDto>();
        public string SelectedCountryCode { get; set; } = "CA";
    }

    public class CountryDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
