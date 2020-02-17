using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VaporStore.Data.Models.enumers;

namespace VaporStore.Data.Models
{
   public class Card
    {
        public Card()
        {
            this.Purchases = new List<Purchase>();
        }
        public int Id { get; set; }
        [RegularExpression("^[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}$")]
        public string Number { get; set; }
        [RegularExpression("^[0-9]{3}$")]
        public String Cvc { get; set; }
        public CardType Type { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}
