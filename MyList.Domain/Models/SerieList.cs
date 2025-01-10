using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyList.Domain.SeedWork;

namespace MyList.Domain.Models
{
    public class SerieList : Entity
    {
        public string Nome { get; set; }

        public string Color { get; set; }

        public SerieListStatus Status { get; set; }

        public DateTime Data_criacao { get; set; }

        public int UserId { get; set; }

        public User user { get; }

        public List<MyListSerie> MyListSeries { get; set; }

        public SerieList()
        {

        }

        public MyListSerie AddSerie(int serieUserId)
        {
            var existingProduct = MyListSeries
                .SingleOrDefault(x => x.SerieUserId == serieUserId);

            if (existingProduct == null)
            {
                var slp = new MyListSerie(Id, serieUserId);
                MyListSeries.Add(slp);
            }


            return existingProduct;
        }
    }
    public enum SerieListStatus
    {
        Active,
        Archived
    }
}
