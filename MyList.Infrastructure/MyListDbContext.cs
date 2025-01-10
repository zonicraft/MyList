using MyList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyList.Infrastructure
{
    public class MyListDbContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FilmeUser> FilmeUsers { get; set; }
        public DbSet<FilmeList> FilmeLists { get; set; }
        public DbSet<SerieUser> SerieUsers { get; set; }
        public DbSet<SerieList> SerieLists { get; set; }
        public DbSet<MyListFilme> MyListFilmes { get; set; }
        public DbSet<MyListSerie> MyListSeries { get; set; }



        public string DbPath { get; private set; }

        public MyListDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}MyListDB.db";
            //C:\Users\Ricardo\AppData\Local\Packages\45c15f2c-f7e0-43f5-b886-13e159e39154_f7jej9wr3mfpr\LocalState
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlite($"Data Source ={DbPath}");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Filme
            modelBuilder.Entity<Filme>()
                .HasIndex(x => x.Titulo).IsUnique();

            modelBuilder.Entity<Filme>()
                .Property(x => x.Titulo).IsRequired()
                .HasMaxLength(256);

            //Serie
            modelBuilder.Entity<Serie>()
                .HasIndex(m => m.Titulo).IsUnique();

            modelBuilder.Entity<Serie>()
                .Property(m => m.Titulo).IsRequired()
                .HasMaxLength(256);

            //Categoria
            modelBuilder.Entity<Categoria>()
              .HasIndex(x => x.Nome).IsUnique();

            modelBuilder.Entity<Categoria>()
                .Property(x => x.Nome).IsRequired()
                .HasMaxLength(256);

            //Foreign Key (Categoria ID) ----> Filme
            modelBuilder.Entity<Filme>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Filmes)
                .HasForeignKey(p => p.CategoriaID)
                .OnDelete(DeleteBehavior.Restrict);

            //Foreign Key (Categoria ID) ------> Serie
            modelBuilder.Entity<Serie>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Series)
                .HasForeignKey(p => p.CategoriaID)
                .OnDelete(DeleteBehavior.Restrict);

            //Foreign Key (FilmeID) ---->Review
            modelBuilder.Entity<Review>()
               .HasOne(p => p.Filme)
               .WithMany(c => c.Reviews)
               .HasForeignKey(p => p.FilmeID)
               .OnDelete(DeleteBehavior.Restrict);

            //User

            modelBuilder.Entity<User>().HasIndex(x => x.Username).IsUnique();
            modelBuilder.Entity<User>().Property(x => x.Username)
                .IsRequired().HasMaxLength(256);
            modelBuilder.Entity<User>().Property(x => x.Password)
                .IsRequired().HasMaxLength(256);
            modelBuilder.Entity<User>().Property(x => x.IsAdmin)
                .IsRequired();


            // Filme User

            modelBuilder.Entity<FilmeUser>()
                .HasIndex(x => new { x.FilmeId, x.UserId })
                .IsUnique();
            modelBuilder.Entity<FilmeUser>()
                .HasOne(p => p.Filme)
                .WithMany(p => p.FilmeUsers)
                .HasForeignKey(p => p.FilmeId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<FilmeUser>()
                .HasOne(p => p.User)
                .WithMany(u => u.FilmeUsers)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            // Serie User

            modelBuilder.Entity<SerieUser>()
                .HasIndex(x => new { x.SerieId, x.UserId })
                .IsUnique();
            modelBuilder.Entity<SerieUser>()
                .HasOne(p => p.Serie)
                .WithMany(p => p.SerieUsers)
                .HasForeignKey(p => p.SerieId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SerieUser>()
                .HasOne(p => p.User)
                .WithMany(u => u.SerieUsers)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            //Filme List
            modelBuilder.Entity<FilmeList>()
                .HasIndex(x => new { x.Nome, x.UserId })
                .IsUnique();
            modelBuilder.Entity<FilmeList>().Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(256);
            modelBuilder.Entity<FilmeList>().Property(x => x.Color)
                .HasMaxLength(9);
            modelBuilder.Entity<FilmeList>().Property(x => x.Status)
                .IsRequired();
            modelBuilder.Entity<FilmeList>().Property(x => x.Data_criacao)
                .IsRequired();

            //Admin injection
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = "admin", IsAdmin = true });

            //Serie List
            modelBuilder.Entity<SerieList>()
                .HasIndex(x => new { x.Nome, x.UserId })
                .IsUnique();
            modelBuilder.Entity<SerieList>().Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(256);
            modelBuilder.Entity<SerieList>().Property(x => x.Color)
                .HasMaxLength(9);
            modelBuilder.Entity<SerieList>().Property(x => x.Status)
                .IsRequired();
            modelBuilder.Entity<SerieList>().Property(x => x.Data_criacao)
                .IsRequired();

            //My List Filme
            modelBuilder.Entity<Domain.Models.MyListFilme>()
                .HasIndex(x => new { x.FilmeListId, x.FilmeUserId })
                .IsUnique();
            modelBuilder.Entity<MyListFilme>()
               .HasOne(x => x.FilmeList)
               .WithMany(x => x.MyListFilmes)
               .HasForeignKey(x => x.FilmeListId)
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MyListFilme>()
               .HasOne(x => x.FilmeUser)
               .WithMany(x => x.MyListFilmes)
               .HasForeignKey(x => x.FilmeUserId)
               .OnDelete(DeleteBehavior.Restrict);


            //My List Serie
            modelBuilder.Entity<Domain.Models.MyListSerie>()
                .HasIndex(x => new { x.SerieListId, x.SerieUserId })
                .IsUnique();
            modelBuilder.Entity<MyListSerie>()
               .HasOne(x => x.SerieList)
               .WithMany(x => x.MyListSeries)
               .HasForeignKey(x => x.SerieListId)
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MyListSerie>()
               .HasOne(x => x.SerieUser)
               .WithMany(x => x.MyListSeries)
               .HasForeignKey(x => x.SerieUserId)
               .OnDelete(DeleteBehavior.Restrict);

        }

    }
}

