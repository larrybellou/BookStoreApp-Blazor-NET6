using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Models.Book;

namespace BookStoreApp.API.Configurations
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<AuthorReadOnlyDTO, Author>().ReverseMap();
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();

            CreateMap<BookCreateDTO, Book>().ReverseMap();
            CreateMap<Book, BookReadOnlyDTO>()
                .ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author.Firstname} {map.Author.Lastname}"));
            CreateMap<Book, BookDetailDTO>()
                .ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author.Firstname} {map.Author.Lastname}"));
            CreateMap<Book, BookUpdateDTO>().ReverseMap();
        }
    }
}
