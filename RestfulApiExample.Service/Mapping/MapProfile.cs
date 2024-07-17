using AutoMapper;
using RestfulApiExample.Core.DTOs;
using RestfulApiExample.Core.DTOs.Author;
using RestfulApiExample.Core.DTOs.Book;
using RestfulApiExample.Core.DTOs.Genre;
using RestfulApiExample.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Service.Mapping
{
	public class MapProfile : Profile
	{
		public MapProfile()
		{
			CreateMap<Product, ProductDto>().ReverseMap();

			CreateMap<Product, UpdateProductDto>().ReverseMap();

			CreateMap<Genre, GenreDto>().ReverseMap();
			CreateMap<Genre, CreateGenreDto>().ReverseMap();


			CreateMap<Book, BookDto>().ReverseMap();
			CreateMap<Book, CreateBookDto>().ReverseMap();
			CreateMap<Book, UpdateBookDto>().ReverseMap();

			CreateMap<Author, AuthorDto>().ReverseMap();
			CreateMap<Author, CreateAuthorDto>().ReverseMap();
			CreateMap<Author, UpdateAuthorDto>().ReverseMap();
		}

	}
}
