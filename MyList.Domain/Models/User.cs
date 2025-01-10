using MyList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MyList.Domain.Models
{
    public class User : Entity
    {
        public string Username { get; set; }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                var data = Encoding.UTF8.GetBytes(value);
                var hashData = new SHA1Managed().ComputeHash(data);

                _password = BitConverter.ToString(hashData).Replace("-", "").ToUpper();

            }
        }

        public bool IsAdmin {get;set;}

        public List<FilmeUser> FilmeUsers { get; set; }

        public List<SerieUser> SerieUsers { get; set; }

        public List<FilmeList> FilmeLists { get; set; }

        public List<SerieList> SerieLists { get; set; }

        

        internal FilmeUser AddFilme(Filme filme)
        {
            var existingProduct = FilmeUsers.SingleOrDefault(p =>
                p.FilmeId == filme.Id);

            if (existingProduct == null)
            {
                existingProduct = new FilmeUser(Id, filme.Id);
                FilmeUsers.Add(existingProduct);
            }
            
            return existingProduct;
        }

        internal SerieUser AddSerie(Serie serie)
        {
            var existingProduct = SerieUsers.SingleOrDefault(p =>
                p.SerieId == serie.Id);

            if (existingProduct == null)
            {
                existingProduct = new SerieUser(Id, serie.Id);
                SerieUsers.Add(existingProduct);
            }

            return existingProduct;
        }
    }
}
