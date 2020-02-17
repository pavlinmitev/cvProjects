using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.Datasets.Dto
{
   public class GameDto
    {
        [Required]
        public string Name { get; set; }
        [Range(typeof(decimal),"0.00", "79228162514264337593543950335")]
        public decimal Price { get; set; }
        [Required]
        public string ReleaseDate { get; set; }
        [Required]
        public string Developer { get; set; }
        [Required]
        public string Genre { get; set; }
        public List<string> Tags { get; set; }
        
    }
}
