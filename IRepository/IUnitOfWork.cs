using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZdravaHranaWebShop.Entities;

namespace ZdravaHranaWebShop.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Adresa> Adrese { get; }
        IGenericRepository<Korpa> Korpe { get; }
        IGenericRepository<NacinPlacanja> NaciniPlacanja { get; }
        IGenericRepository<Porudzbina> Porudzbine { get; }
        IGenericRepository<Proizvod> Proizvodi { get; }
        IGenericRepository<Proizvodjac> Proizvodjaci { get; }
        IGenericRepository<StavkaKorpe> StavkeKorpe { get; }
        IGenericRepository<TipProizvoda> TipoviProizvoda { get; }

        IGenericRepository<User> Users { get; }

        Task Save();

    }
}
