using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Repository.Abstract;
using ProductApi.ViewModels;

namespace ProductApi.Controllers
{
    [EnableCors]

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IValidator<Product> _validator;
        private readonly IMapper _mapper;

        public ProductController(IRepository<Product> productRepository, IValidator<Product> validator, IMapper mapper)
        {
            _productRepository = productRepository;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("products/getProducts")] // Yeni route değeri
        public IActionResult GetAllProducts()
        {
            var result = _productRepository.GetAll();
            var mappedResult = _mapper.Map<List<ProductViewModel>>(result);

            if (result is null)
            {
                return BadRequest(result);
            }

            return Ok(mappedResult);

        }

        [HttpPost]
        [Authorize(Policy = "SellerOnly")]
        [Route("products/addProduct")]
        public IActionResult AddProduct([FromBody] ProductViewModel obj)
        {
            Product mappedObj = _mapper.Map<Product>(obj);
            ValidationResult result = _validator.Validate(mappedObj);



            if (!result.IsValid)
            {
                return BadRequest(result);

            }


            _productRepository.Add(mappedObj);
            return Ok(mappedObj);

        }

        [HttpPut]
        [Authorize(Policy = "SellerOnly")]
        [Route("products/editProduct")]
        public IActionResult UpdateProduct([FromBody] ProductViewModel obj)
        {
            Product mappedProduct = _mapper.Map<Product>(obj);
            ValidationResult result = _validator.Validate(mappedProduct);


            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            _productRepository.Update(mappedProduct);
            return Ok(mappedProduct);

        }

        [HttpDelete]
        [Authorize(Policy = "SellerOnly")]
        [Route("products/deleteProduct")]
        public IActionResult DeleteProduct(int id)
        {

            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetById(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _productRepository.Delete(product);
            return Ok(product);

        }

        [HttpGet]
        [Route("products/getProduct")]
        public IActionResult GetProduct(int id)
        {

            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetById(p => p.Id == id);
            ProductViewModel mappedProductVVM = _mapper.Map<ProductViewModel>(product);


            if (product == null)
            {
                return NotFound();
            }

            return Ok(mappedProductVVM);

        }
    }
}
