using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZdravaHranaWebShop.Entities;
using ZdravaHranaWebShop.Models;

namespace ZdravaHranaWebShop.Configurations
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<Adresa, AdresaDTO>().ReverseMap();
            CreateMap<Adresa, CreateAdresaDTO>().ReverseMap();
            CreateMap<Korpa, KorpaDTO>().ReverseMap();
            CreateMap<NacinPlacanja, NacinPlacanjaDTO>().ReverseMap();
            CreateMap<NacinPlacanja, CreateNacinPlacanjaDTO>().ReverseMap();
            CreateMap<Porudzbina, PorudzbinaDTO>().ReverseMap();
            CreateMap<Porudzbina, CreatePorudzbinaDTO>().ReverseMap();
            CreateMap<Proizvod, ProizvodDTO>().ReverseMap();
            CreateMap<Proizvod, CreateProizvodDTO>().ReverseMap();
            CreateMap<Proizvodjac, ProizvodjacDTO>().ReverseMap();
            CreateMap<Proizvodjac, CreateProizvodjacDTO>().ReverseMap();
            CreateMap<StavkaKorpe, StavkaKorpeDTO>().ReverseMap();
            CreateMap<StavkaKorpe, CreateStavkaKorpeDTO>().ReverseMap();
            CreateMap<TipProizvoda, TipProizvodaDTO>().ReverseMap();
            CreateMap<TipProizvoda, CreateTipProizvodaDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
