using MyList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyList.Domain.Models
{
    public class Categoria:Entity
    {
        public string Nome { get; set; }

        public List<Filme>Filmes { get; set; }

        public List<Serie>Series { get; set; }

        public Categoria()
        {
            //Construtor por definição
        }

        public Categoria(string n)
        {
            Nome = n;
        }

        public override string ToString()
        {
            return Nome;
        }

    }
}
