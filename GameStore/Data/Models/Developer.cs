﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.Data.Models
{
   public class Developer
    {
        public Developer()
        {
            this.Games = new List<Game>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
