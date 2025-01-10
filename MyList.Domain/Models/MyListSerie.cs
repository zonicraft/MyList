using MyList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyList.Domain.Models
{
    public class MyListSerie : Entity
    {
        public MyListSerie(int serieListId, int serieUserId)
        {

            SerieListId = serieListId;
            SerieUserId = serieUserId;
            Status = MyListSerieStatus.NORMAL;
        }



        public MyListSerieStatus Status { get; set; }

        public int SerieListId { get; set; }

        public SerieList SerieList { get; set; }

        public int SerieUserId { get; set; }

        public SerieUser SerieUser { get; set; }
    }
    public enum MyListSerieStatus { NORMAL, CHECKED }
}
