using MyList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyList.Domain.Models
{
    public class Filme:Entity
    {
        public string Titulo { get; set; }

        public string Realizador { get; set; }

        public string Produtora { get; set; }

        public string Ano { get; set; }

        public string Pg_rating { get; set; }

        public string Sinopse { get; set; }

        public byte[] Cartaz { get; set; }

        public int CategoriaID { get; set; }

        public Categoria Categoria { get; set; }

        public List<Review>Reviews { get; set; }

        public List<FilmeUser> FilmeUsers { get; set; }

        public Filme()
        {

        }

        public Filme(string name, int categoryId)
        {
            Titulo = name;
            CategoriaID = categoryId;
        }

        public override string ToString()
        {
            return Titulo;
        }
    }
}
