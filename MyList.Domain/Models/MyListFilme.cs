using MyList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyList.Domain.Models
{
    public class MyListFilme : Entity
    {
        public MyListFilme(int filmeListId, int filmeUserId)
        {
           
            FilmeListId = filmeListId;
            FilmeUserId = filmeUserId;
            Status = MyListFilmeStatus.NORMAL;
        }

      

        public MyListFilmeStatus Status { get; set; }

        public int FilmeListId { get; set; }

        public FilmeList FilmeList { get; set; }

        public int FilmeUserId { get; set; }

        public FilmeUser FilmeUser { get; set; }
    }
    public enum MyListFilmeStatus { NORMAL, CHECKED }
}
