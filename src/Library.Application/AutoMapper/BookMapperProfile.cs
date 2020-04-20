using AutoMapper;
using Library.Application.UseCases.Storages.Dtos;
using Library.Domain.AggregateModels.StorageAggregate;

namespace Library.Application.AutoMapper
{
    public class BookMapperProfile : Profile
    {
        public BookMapperProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(x => x.Title, dest => dest.MapFrom(src => src.BookInformation.Title))
                .ForMember(x => x.Author, dest => dest.MapFrom(src => src.BookInformation.Author))
                .ForMember(x => x.Subject, dest => dest.MapFrom(src => src.BookInformation.Subject))
                .ForMember(x => x.Isbn, dest => dest.MapFrom(src => src.BookInformation.Isbn))
                .ForMember(x => x.InStock, dest => dest.MapFrom(src => src.InStock));
        }
    }
}
