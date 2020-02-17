﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.Data.Models
{
  public  class Genre
    {
        public Genre()
        {
            this.Games = new List<Game>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
