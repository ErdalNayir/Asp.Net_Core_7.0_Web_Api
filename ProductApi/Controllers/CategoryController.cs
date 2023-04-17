using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Repository.Abstract;
using ProductApi.ViewModels;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IValidator<Category> _validator;
        private readonly IMapper _mapper;

        public CategoryController(IMapper mapper, IRepository<Category> categoryRepository, IValidator<Category> validator)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("categories/getCategories")] // Yeni route değeri
        public IActionResult GetAllCategories()
        {
            var result = _categoryRepository.GetAll();
            var mappedResult = _mapper.Map<List<CategoryViewModel>>(result);

            if (result is null)
            {
                return BadRequest(result);
            }

            return Ok(mappedResult);

        }

        [HttpPost]
        //[Authorize(Policy = "AdminPolicy")]
        [Route("categories/addCategory")]
        public IActionResult AddCategory([FromBody] CategoryViewModel obj)
        {

            Category mappedObj = _mapper.Map<Category>(obj);
            ValidationResult result = _validator.Validate(mappedObj);



            if (!result.IsValid)
            {
                return BadRequest(result);

            }

            _categoryRepository.Add(mappedObj);
            return Ok(mappedObj);

        }

        [HttpPut]
        //[Authorize(Policy = "AdminPolicy")]
        [Route("categories/editCategory")]
        public IActionResult UpdateCategory([FromBody] CategoryViewModel obj)
        {

            Category mappedProduct = _mapper.Map<Category>(obj);
            ValidationResult result = _validator.Validate(mappedProduct);


            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            _categoryRepository.Update(mappedProduct);
            return Ok(mappedProduct);

        }

        [HttpDelete]
        //[Authorize(Policy = "AdminPolicy")]
        [Route("categories/deleteCategory")]
        public IActionResult DeleteCategory(int id)
        {

            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(p => p.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            _categoryRepository.Delete(category);
            return Ok(category);

        }

        [HttpGet]
        [Route("categories/getCategory")]
        public IActionResult GetCategory(int id)
        {

            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(p => p.CategoryId == id);
            CategoryViewModel mappedProductVVM = _mapper.Map<CategoryViewModel>(category);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(mappedProductVVM);

        }
    }
}
