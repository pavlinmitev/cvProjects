using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VaporStore.Data.Models;

namespace VaporStore.Datasets.Dto
{
   public class UserDto
    {
        [RegularExpression("^[A-Z][a-z]+ [A-Z][a-z]+$")]
        [Required]
        public string FullName { get; set; }
        [StringLength(20, MinimumLength = 3)]
        public string UserName { get; set; }
   [Required]
        public string Email { get; set; }
        [Range(3, 103)]
        [Required]
        public int Age { get; set; }
        public List<CardDto> Cards { get; set; }
 

    }
}
