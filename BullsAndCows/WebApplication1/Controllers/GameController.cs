using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Data;

namespace WebApplication1.Controllers
{
    public class GameController : Controller
    {
      private  DataContext Context;
        public GameController(DataContext context)
        {
            Context = context;
        }
        public IActionResult GameHistory()
        {
            var games = Context.Games.ToList();
            return View(games);
        }
    }
}