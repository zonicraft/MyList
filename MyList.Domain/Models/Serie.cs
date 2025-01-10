using MyList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyList.Domain.Models
{
    public class Serie : Entity
    {
        public string Titulo { get; set; }

        public string Realizador { get; set; }

        public string Produtora { get; set; }

        public string Pg_rating { get; set; }

        public string Num_temporadas { get; set; }

        public string Sinopse { get; set; }

        public byte[] Cartaz { get; set; }

        public int CategoriaID { get; set; }

        public string Data_inicio { get; set; }

        public Categoria Categoria{ get; set; }

        public List<Review> Reviews { get; set; }

        public List<SerieUser> SerieUsers { get; set; }

        public Serie()
        {

        }

        public Serie(string name, int categoryId)
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
