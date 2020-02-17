using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.Datasets.exportDtos
{
   public class GenreDto
    {
        public int Id { get; set; }
        public string Genre { get; set; }
        public List<GameExportDto> Games { get; set; }
        public int TotalPlayers { get; set; }
    }
}
