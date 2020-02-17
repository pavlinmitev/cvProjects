using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.Data.Models
{
  public  class User
    {
        public User()
        {
            this.Cards = new List<Card>();
        }
        public int Id { get; set; }
        [StringLength(20,MinimumLength =3)]
        public string Username { get; set; }
        [RegularExpression("^[A-Z][a-z]+ [A-Z][a-z]+$")]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Range(3,103)]
        [Required]
        public int Age { get; set; }
        public ICollection<Card> Cards { get; set; }
    }
}
