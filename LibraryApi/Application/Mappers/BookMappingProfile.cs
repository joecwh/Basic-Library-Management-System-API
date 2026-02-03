using AutoMapper;
using LibraryApi.Application.Dtos;
using LibraryApi.Domain.Entities;

namespace LibraryApi.Application.Mappers
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookResponseModel>()
               .ForMember(
                   dest => dest.Categories,
                   opt => opt.MapFrom(src =>
                       src.BookCategories
                          .Select(bc => bc.Category.CategoryName)
                          .ToList()
                   )
               );
        }
    }
}
