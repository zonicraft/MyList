using MyList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyList.Domain.Models
{
    public class Review:Entity
    {
        public string Titulo { get; set; }

        public string Rating { get; set; }

        public string Comentario { get; set; }

        public Filme  Filme { get; set; }

        public int FilmeID { get; set; }

        public Serie Serie { get; set; }

        public int SerieID { get; set; }
    }
}
