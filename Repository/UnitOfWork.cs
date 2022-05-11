using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZdravaHranaWebShop.Entities;
using ZdravaHranaWebShop.IRepository;

namespace ZdravaHranaWebShop.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        private IGenericRepository<Adresa> _adrese;
        private IGenericRepository<Korpa> _korpe;
        private IGenericRepository<NacinPlacanja> _naciniPlacanja;
        private IGenericRepository<Porudzbina> _porudzbine;
        private IGenericRepository<Proizvod> _proizvodi;
        private IGenericRepository<Proizvodjac> _proizvodjaci;
        private IGenericRepository<StavkaKorpe> _stavkeKorpe;
        private IGenericRepository<TipProizvoda> _tipoviProizvoda;
        private IGenericRepository<User> _user;

        public IGenericRepository<Adresa> Adrese => _adrese ??= new GenericRepository<Adresa>(_context);

        public IGenericRepository<Korpa> Korpe => _korpe ??= new GenericRepository<Korpa>(_context);

        public IGenericRepository<NacinPlacanja> NaciniPlacanja => _naciniPlacanja ??= new GenericRepository<NacinPlacanja>(_context);

        public IGenericRepository<Porudzbina> Porudzbine => _porudzbine ??= new GenericRepository<Porudzbina>(_context);

        public IGenericRepository<Proizvod> Proizvodi => _proizvodi ??= new GenericRepository<Proizvod>(_context);

        public IGenericRepository<Proizvodjac> Proizvodjaci => _proizvodjaci ??= new GenericRepository<Proizvodjac>(_context);

        public IGenericRepository<StavkaKorpe> StavkeKorpe => _stavkeKorpe ??= new GenericRepository<StavkaKorpe>(_context);

        public IGenericRepository<TipProizvoda> TipoviProizvoda => _tipoviProizvoda ??= new GenericRepository<TipProizvoda>(_context);

        public IGenericRepository<User> Users => _user ??= new GenericRepository<User>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
