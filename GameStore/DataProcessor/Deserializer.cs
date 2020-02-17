namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.enumers;
    using VaporStore.Datasets.Dto;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

    public static class Deserializer
	{
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
            var games = JsonConvert.DeserializeObject<List<GameDto>>(jsonString);
            StringBuilder sb = new StringBuilder();
            var gList = new List<Game>();
            foreach(var game in games)
            {
                if(!IsValid(game) || game.Tags.Count == 0)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }
                var newGame = new Game
                {
                    Name = game.Name,
                    Price = game.Price,
                    ReleaseDate = DateTime.ParseExact(game.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)

                };
                var developer = GetDev(context,game.Developer);
                var genre = GetGenre(context, game.Genre);
                newGame.Developer = developer;
                newGame.Genre = genre;
                foreach (var Ctag in game.Tags)
                {
                    var tag = GetTag(context, Ctag);
                    newGame.GameTags.Add(new GameTag { Game = newGame, Tag = tag });
                }
                
                gList.Add(newGame);
                sb.AppendLine($"Added {newGame.Name} ({newGame.Genre.Name}) with {newGame.GameTags.Count} tags");
            }
            context.Games.AddRange(gList);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
		}

        private static Developer GetDev(VaporStoreDbContext context,string name)
        {
            var dev = context.Developers.FirstOrDefault(x => x.Name == name);
            if (dev == null)
            {
                dev = new Developer
                {
                    Name = name
                };
                context.Developers.Add(dev);
                context.SaveChanges();
            }
            
            return dev;
        }
        private static Genre GetGenre(VaporStoreDbContext context, string name)
        {
            var genre = context.Genres.FirstOrDefault(x => x.Name == name);
            if (genre == null)
            {
                genre = new Genre
                {
                    Name = name
                };
                context.Genres.Add(genre);
                context.SaveChanges();
            }
         
            return genre;
        }
        private static Tag GetTag(VaporStoreDbContext context, string name)
        {
            var tag = context.Tags.FirstOrDefault(x => x.Name == name);
            if (tag == null)
            {
                tag = new Tag
                {
                    Name = name
                };
                context.Tags.Add(tag);
                context.SaveChanges();
            }
            
            return tag;
        }
        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
            StringBuilder sb = new StringBuilder();
            var users = JsonConvert.DeserializeObject<List<UserDto>>(jsonString);
            var userList = new List<User>();
            foreach(var user in users)
            {
               
                if (IsValid(user))
                {
                    var nUser = new User();
                    nUser.FullName = user.FullName;
                    nUser.Username = user.UserName;
                    nUser.Email = user.Email;
                    nUser.Age = user.Age;
                    var cardList = new List<Card>();
                    foreach (var card in user.Cards)
                    {
                       
                        if (IsValid(card))
                        {
                            var purchases = context.Purchases.Where(x => x.Card.Number == card.Number).ToList();
                            cardList.Add(new Card { Number = card.Number, Cvc = card.CVC, Type = card.Type, User = nUser,Purchases=purchases });
                            
                            
                        }
                        if (cardList.Count > 0)
                        {
                            nUser.Cards = cardList;
                        }
                        else
                        {
                            sb.AppendLine("Invalid Data");
                            continue;
                        }
                    }
                    if (nUser.Cards.Count > 0)
                    {
                        userList.Add(nUser);
                        sb.AppendLine($"Imported {nUser.Username} with {nUser.Cards.Count} cards");
                    }
                   
                    
                }
                else
                {
                     sb.AppendLine("Invalid Data");
                            continue;
                }
               
                
            }
            context.Users.AddRange(userList);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(PurchaseDto[]), new XmlRootAttribute("Purchases"));
            var purchases = (PurchaseDto[])serializer.Deserialize(new StringReader(xmlString));
            var pList = new List<Purchase>();
            foreach(var purchase in purchases)
            {
                if (!IsValid(purchase))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }
                var cPurchase = new Purchase
                {
                    Date = DateTime.ParseExact(purchase.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    ProductKey = purchase.Key,
                    Type = (PurchaseType)Enum.Parse(typeof(PurchaseType), purchase.Type)
                };
                var Card = context.Cards.FirstOrDefault(x => x.Number == purchase.Card);
                var Game = context.Games.FirstOrDefault(x => x.Name == purchase.Title);
                cPurchase.Card = Card;
                cPurchase.Game = Game;
                sb.AppendLine($"Imported {cPurchase.Game.Name} for {cPurchase.Card.User.Username}");
                pList.Add(cPurchase);


            }
            context.Purchases.AddRange(pList);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }
        private static bool IsValid(object e)
        {
            var vc = new ValidationContext(e);
            var vr = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(e, vc, vr, true);
            return isValid;
        }
	}
}