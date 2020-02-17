namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models.enumers;
    using VaporStore.Datasets.Dto;
    using VaporStore.Datasets.exportDtos;

    public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
            var games = context.Games.Where(x => genreNames.Contains(x.Genre.Name)&& x.Purchases.Count>0);
            var GenreDtos = new List<GenreDto>();
            foreach(var g in genreNames)
            {
                var currentGames = games.Where(x => x.Genre.Name == g);


                // var fake = currentGames.Select(x => new { id = x.Id, genre = x.Genre.Name });
                var genreDto = new GenreDto
                {
                    Id = context.Genres.FirstOrDefault(x => x.Name == g).Id,
                    Genre = g,
                    Games = currentGames.
                    Select(x => new GameExportDto { Developer = x.Developer.Name, Id = x.Id, Players = x.Purchases.Count, Title = x.Name,
                        Tags =string.Join(", ",(x.GameTags.Select(y => y.Tag.Name))) }).OrderByDescending(x => x.Players).ThenBy(x => x.Id).ToList(),
                    
                    };
                genreDto.TotalPlayers = genreDto.Games.Sum(z => z.Players);
                    GenreDtos.Add(genreDto);

               
             
            }
            GenreDtos = GenreDtos.OrderByDescending(x => x.TotalPlayers).ThenBy(x => x.Id).ToList();
           
            var r= JsonConvert.SerializeObject(GenreDtos, Newtonsoft.Json.Formatting.Indented,
     new JsonSerializerSettings()
     {
         ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
     }
 );
            return r;
		}

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
            var storeTypeValue = Enum.Parse<PurchaseType>(storeType);
            var purchases = context.Users
                .Select(u => new ExportUserDto
                {
                    UserName = u.Username,
                    Purchases = u.Cards
                        .SelectMany(c => c.Purchases)
                        .Where(p => p.Type == storeTypeValue)
                        .Select(p => new ExportPurchaseDto
                        {
                            Card = p.Card.Number,
                            Cvc = p.Card.Cvc,
                            Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                            Game = new PurchaseGameDto
                            {
                                Title = p.Game.Name,
                                Genre = p.Game.Genre.Name,
                                Price = p.Game.Price,
                            }
                        })
                        .OrderBy(p => p.Date)
                        .ToArray(),
                    TotalSpent = u.Cards
                        .SelectMany(c => c.Purchases)
                        .Where(p => p.Type == storeTypeValue)
                        .Sum(p => p.Game.Price)
                })
                .Where(u => u.Purchases.Any())
                .OrderByDescending(u => u.TotalSpent)
                .ThenBy(u => u.UserName)
                .ToArray();

            var serializer = new XmlSerializer(typeof(ExportUserDto[]), new XmlRootAttribute("Users"));

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            serializer.Serialize(new StringWriter(sb), purchases, namespaces);

            var result = sb.ToString();
            return result;
        }
	}
}