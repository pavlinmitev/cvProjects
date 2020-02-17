using System;
using System.Collections.Generic;
using System.Text;
using VaporStore.Datasets.exportDtos;

namespace VaporStore.Datasets.Dto
{
   public class FinalGenreDto
    {
        public List<GenreDto> genreDtos { get; set; }
        public int TotalPlayers { get; set; }
    }
}
