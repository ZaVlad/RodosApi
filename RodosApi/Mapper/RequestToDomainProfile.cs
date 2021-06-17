using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RodosApi.Contract;
using RodosApi.Contract.V1.Request;
using RodosApi.Contract.V1.Request.Queries;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Mapper
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<TypeOfDoorToCreate, TypeOfDoor>();
            CreateMap<PaginationQuery, PaginationFilter>();
            CreateMap<GetAllHingesQuery, GetAllHingesFilter>();
            CreateMap<DoorHandleQuery, DoorHandleFilter>();
            CreateMap<DoorQuery, DoorFilter>();
            CreateMap<OrderFilterQuery, OrderFilter>();

            CreateMap<CategorySortingQuery, CategorySorting>();
            CreateMap<CoatingSortingQuery, CoatingSorting>();
            CreateMap<CollectionSortingQuery, CollectionSorting>();
            CreateMap<ColorSortingQuery, ColorSorting>();
            CreateMap<CountrySortingQuery, CountrySorting>();
            CreateMap<DoorModelSortingQuery, DoorModelSorting>();
            CreateMap<FurnitureTypeSortingQuery, FurnitureTypeSorting>();
            CreateMap<MakerFilterQuery, MakerFilter>();
            CreateMap<MakerSortingQuery, MakerSorting>();
            CreateMap<MaterialSortingQuery, MaterialSorting>();
            CreateMap<TypeOfDoorSortingQuery, TypeOfDoorSorting>();
            CreateMap<TypeOfHingesSortingQuery, TypeOfHingesSorting>();
            CreateMap<HingesSortingQuery, HingesSorting>();
            CreateMap<DoorHandlesSortingQuery, DoorHandlesSorting>();
            CreateMap<DoorSortingQuery, DoorSorting>();
            CreateMap<ClientSortingQuery, ClientSorting>();
            CreateMap<OrderSortingQuery, OrderSorting>();

            CreateMap<CoatingToCreate, Coating>();
            CreateMap<CollectionToCreate, Collection>();
            CreateMap<DoorModelToCreate, DoorModel>();
            CreateMap<ColorToCreate, Color>();
            CreateMap<CountryToCreate, Country>();
            CreateMap<MakerToCreate, Maker>().ForMember(s => s.CountryId, opt => opt.MapFrom(s => s.CountryId));
            CreateMap<FurnitureTypeToCreate, FurnitureType>();
            CreateMap<MaterialToCreate, Material>();
            CreateMap<TypeOfHingesToCreate, TypeOfHinge>();
            CreateMap<CategoryToCreate, Category>();
            CreateMap<HingeToCreate, Hinges>();
            CreateMap<DoorHandleToCreate, DoorHandle>();
            CreateMap<DoorToCreate, Door>();
            CreateMap<ClientToCreate, Client>();
            CreateMap<OrderToCreate, Order>();
        }
    }
}
