using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RodosApi.Contract.V1.Response;
using RodosApi.Domain;

namespace RodosApi.Mapper
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<TypeOfDoor, TypeOfDoorResponse>();
            CreateMap<Coating, CoatingResponse>();
            CreateMap<Collection, CollectionResponse>();
            CreateMap<DoorModel, DoorModelResponse>();
            CreateMap<Color, ColorResponse>();
            CreateMap<Client, ClientResponse>();
            CreateMap<Country, CountryResponse>();
            CreateMap<Maker, MakerResponse>()
                .ForMember(s => s.CountryResponse,opt => opt.MapFrom(n => n.Country));
            CreateMap<FurnitureType, FurnitureTypeResponse>();
            CreateMap<Material, MaterialResponse>();
            CreateMap<TypeOfHinge, TypeOfHingeResponse>();
            CreateMap<Category, CategoryResponse>();
            CreateMap<Hinges, HingesResponse>()
                .ForMember(s => s.MakerResponse, opt => opt.MapFrom(n => n.Maker))
                .ForMember(c => c.MaterialResponse, opt => opt.MapFrom(n => n.Material))
                .ForMember(k => k.FurnitureTypeResponse, opt => opt.MapFrom(j => j.FurnitureType))
                .ForMember(v => v.CategoryResponse, opt => opt.MapFrom(j => j.Category))
                .ForMember(v => v.TypeOfHingeResponse, opt => opt.MapFrom(j => j.TypeOfHinge));

            CreateMap<DoorHandle, DoorHandleResponse>().
                ForMember(s => s.MakerResponse, opt => opt.MapFrom(n => n.Maker))
                .ForMember(c => c.MaterialResponse, opt => opt.MapFrom(n => n.Material))
                .ForMember(k => k.FurnitureTypeResponse, opt => opt.MapFrom(j => j.FurnitureType))
                .ForMember(v => v.CategoryResponse, opt => opt.MapFrom(j => j.Category))
                .ForMember(f => f.ColorResponse, opt => opt.MapFrom(c => c.Color));

            CreateMap<Door, DoorResponse>()
                .ForMember(s => s.CategoryResponse, opt => opt.MapFrom(c => c.Category))
                .ForMember(s => s.CoatingResponse, opt => opt.MapFrom(c => c.Coating))
                .ForMember(s => s.CollectionResponse, opt => opt.MapFrom(c => c.Collection))
                .ForMember(s => s.ColorResponse, opt => opt.MapFrom(c => c.Color))
                .ForMember(s => s.DoorHandleResponse, opt => opt.MapFrom(c => c.DoorHandle))
                .ForMember(s => s.DoorModelResponse, opt => opt.MapFrom(c => c.DoorModel))
                .ForMember(s => s.HingesResponse, opt => opt.MapFrom(c => c.Hinges))
                .ForMember(s => s.MakerResponse, opt => opt.MapFrom(c => c.Maker))
                .ForMember(s => s.TypeOfDoorResponse, opt => opt.MapFrom(c => c.TypeOfDoor));
                

                
                
                
                
                
            
            
                
        }
    }
}
