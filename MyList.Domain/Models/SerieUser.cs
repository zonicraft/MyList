using System;
using System.Collections.Generic;
using System.Text;
using MyList.Domain.SeedWork;

namespace MyList.Domain.Models
{
    public class SerieUser : Entity
    {
        public int SerieId { get; set; }

        public Serie Serie { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<MyListSerie> MyListSeries { get; set; }

        public SerieUser()
        {

        }
        public SerieUser(int userId, int filmeId)
        {
            UserId = userId;
            SerieId = filmeId;

        }
    }
}
