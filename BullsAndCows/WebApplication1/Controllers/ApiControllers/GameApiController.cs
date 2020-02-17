using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Data;

namespace WebApplication1.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameApiController : ControllerBase
    {
        private DataContext DataContext;

        public GameApiController(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        [Route("play")]
      //  [HttpPost]
        public ActionResult Play()
        {
            var data = Request.Query;

            var number =int.Parse(data["Numbers"].ToString());
            var date = data["Date"].ToString();
            var counter = int.Parse(data["Counter"].ToString());
            var duration = data["Duration"];

            Game currentGame = new Game()
            {
                Counter=counter,
                Date=date,
                GuessedNumber=number,
                Duration=duration
            };

            DataContext.Games.Add(currentGame);
            DataContext.SaveChanges();

            return Ok("Succesfully Added");
        }
    }
}