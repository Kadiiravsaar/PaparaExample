﻿using AutoMapper;
using RestfulApiExample.Core.DTOs;
using RestfulApiExample.Core.Models;
using RestfulApiExample.Core.Repositories;
using RestfulApiExample.Core.Services;
using RestfulApiExample.Core.UnitOfWorks;
using RestfulApiExample.Core.Utilities;
using RestfulApiExample.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiExample.Service.Services
{
	// Ürünlere özel servis arayüzü, genel servis arayüzünü genişletir
	public class ProductService : Service<Product>, IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;

		public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork)
		{
			_mapper = mapper;
			_productRepository = productRepository;
		}

		// Sayfalı ürünleri getirir => DTO döner
		public async Task<CustomResponseDto<PagedResultDto<ProductDto>>> GetPagedProductsAsync(int page, int pageSize)
		{
			var pagedProducts = await _productRepository.GetPagedProductsAsync(page, pageSize );
			var pagedProductDtos = new PagedResultDto<ProductDto>
			{
				TotalCount = pagedProducts.TotalCount,
				Items = _mapper.Map<List<ProductDto>>(pagedProducts.Items)
			};

			return CustomResponseDto<PagedResultDto<ProductDto>>.Success(200, pagedProductDtos);
		}


		public async Task<CustomResponseDto<List<ProductDto>>> GetProductsByName(string name)
		{
			var products = await _productRepository.GetProductsByName(name);
			var productDtos = _mapper.Map<List<ProductDto>>(products);
			return CustomResponseDto<List<ProductDto>>.Success(200,productDtos);
		}


		// Belirli bir alan ve sıraya göre ürünleri sıralar => DTO döner
		public async Task<CustomResponseDto<List<ProductDto>>> SortProducts(string field, SortOrder order)
		{
			if (string.IsNullOrEmpty(field))
			{
				throw new NotFoundException("Field Required");

			}
			var products = await _productRepository.SortProducts(field, order);
			var productDtos = _mapper.Map<List<ProductDto>>(products);
			return CustomResponseDto<List<ProductDto>>.Success(200,productDtos);
		}
	}
}
