using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VaporStore.Data.Models.enumers;

namespace VaporStore.Datasets.Dto
{
  public  class CardDto
    {
        [RegularExpression("^[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}$")]
        public string Number { get; set; }
        [RegularExpression("^[0-9]{3}$")]
        public string CVC { get; set; }
        public CardType Type { get; set; }
    }
}
