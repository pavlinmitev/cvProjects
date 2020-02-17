using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
   
{
    public class Game
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string Duration { get; set; }

        public int Counter { get; set; }

        public int GuessedNumber { get; set; }
    }
}
