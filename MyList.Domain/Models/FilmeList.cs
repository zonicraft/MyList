using MyList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MyList.Domain.Models
{
    public class FilmeList : Entity
    {
        public string Nome { get; set; }

        public string Color { get; set; }

        public FilmeListStatus  Status { get; set; }

        public DateTime Data_criacao { get; set; }

        public int UserId { get; set; }

        public User user { get; }

        public List<MyListFilme> MyListFilmes { get; set; }

        public FilmeList()
        {

        }


        public MyListFilme AddFilme(int filmeUserId)
        {
            var existingProduct = MyListFilmes
                .SingleOrDefault(x => x.FilmeUserId == filmeUserId);

            if (existingProduct == null)
            {
                var slp = new MyListFilme(Id, filmeUserId);
                MyListFilmes.Add(slp);
            }
         
          
            return existingProduct;
        }
    }

    public enum FilmeListStatus
    {
        Active,
        Archived
    }
}
