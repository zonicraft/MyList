using System;
using System.Threading.Tasks;
using MyList.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IFilmeRepository FilmeRepository { get; }

    ICategoriaRepository CategoriaRepository { get; }

    IReviewRepository ReviewRepository { get; }

    ISerieRepository SerieRepository { get; }

    IUserRepository UserRepository { get; }

    IFilmeUserRepository FilmeUserRepository { get; }

    IFilmeListRepository FilmeListRepository { get; }

    ISerieUserRepository SerieUserRepository { get; }

    ISerieListRepository SerieListRepository { get; }

    IMyListFilmeRepository MyListFilmeRepository { get; }

    IMyListSerieRepository MyListSerieRepository { get; }




    Task SaveAsync();
}