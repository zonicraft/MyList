using MyList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyList.Domain.Models
{
    public class FilmeUser : Entity
    {
        public int FilmeId { get; set; }

        public Filme Filme { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<MyListFilme> MyListFilmes { get; set; }

        public FilmeUser()
        {

        }

        public FilmeUser(int userId, int filmeId)
        {
            UserId = userId;
            FilmeId = filmeId;

        }
    }
}
