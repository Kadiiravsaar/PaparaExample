using AutoMapper;
using RestfulApiExample.Core.DTOs;
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
		}
	}
}
