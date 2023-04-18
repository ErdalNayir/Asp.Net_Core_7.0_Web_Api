using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Commands;
using ProductApi.Models;
using ProductApi.Repository.Abstract;
using ProductApi.ViewModels;

namespace ProductApi.Controllers
{
    [EnableCors]
    
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IRepository<Seller> _sellerRepository;
        private readonly IValidator<Seller> _validator;
        private readonly IValidator<SellerRegisterViewModel> _registerValidator;
        private readonly IValidator<SellerLoginViewModel> _loginValidator;
        private readonly IMapper _mapper;

        public SellerController(IRepository<Seller> sellerRepository, IValidator<Seller> validator, IValidator<SellerRegisterViewModel> registerValidator, IValidator<SellerLoginViewModel> loginValidator, IMapper mapper)
        {
            _sellerRepository = sellerRepository;
            _validator = validator;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("sellers/getSellers")] // Yeni route değeri
        public IActionResult GetAllSellers()
        {
            var result = _sellerRepository.GetAll();
            var mappedResult = _mapper.Map<List<SellerViewModel>>(result);

            if (result is null)
            {
                return BadRequest(result);
            }

            return Ok(mappedResult);

        }

        [HttpPost]
        [Route("sellers/register")]
        public IActionResult AddSeller([FromBody] SellerRegisterViewModel obj)
        {

            //Doğrulama işlemleri FluentValidator ile yapılıyor
            ValidationResult result = _registerValidator.Validate(obj);
           

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            //Email Kontrolü
            var isRegisteredEmail = _sellerRepository.GetByEmail(p=>p.email == obj.email);

            if (isRegisteredEmail != null)
            {
                return BadRequest("Bu email zaten var");
            }


            //hash user password and return new model 
            var hashedModel = SellerHashHelperCommand.HashPasswordReturnModel(obj);
            
            //map SellerRegisterViewModel to Seller
            Seller mappedObj = _mapper.Map<Seller>(hashedModel);

            //SaveUser to database
            _sellerRepository.Add(mappedObj);

            //Generate Jwt Token
            var token = JwtHelper.GetJwtToken(mappedObj);

            return Ok(token);

        }

        [HttpPost]
        [Route("sellers/login")]
        public IActionResult LoginSeller([FromBody] SellerLoginViewModel obj)
        {

            //Doğrulama işlemleri FluentValidator ile yapılıyor
            ValidationResult result = _loginValidator.Validate(obj);


            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            //Email Kontrolü
            var isRegisteredEmail = _sellerRepository.GetByEmail(p => p.email == obj.email);

            if (isRegisteredEmail == null)
            {
                return BadRequest("Bu emaile sahip kullanıcı bulunamamaktadır");
            }

            //compare password
            var passRes = SellerHashHelperCommand.VerifyPassword(obj, isRegisteredEmail);

            if (!passRes)
            {
                return BadRequest("Bu şifre yanlış");
            }

            //Generate Jwt Token
            var token = JwtHelper.GetJwtToken(isRegisteredEmail);

            return Ok(token);

        }

        [HttpPut]
        [Authorize(Policy = "SellerOnly")]
        [Route("sellers/editSeller")]
        public IActionResult UpdateSeller([FromBody] SellerViewModel obj)
        {

            Seller mappedProduct = _mapper.Map<Seller>(obj);
            ValidationResult result = _validator.Validate(mappedProduct);


            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            _sellerRepository.Update(mappedProduct);
            return Ok(mappedProduct);

        }

        [HttpDelete]
        [Authorize(Policy = "SellerOnly")]
        [Route("sellers/deleteSeller")]
        public IActionResult DeleteSeller(int id)
        {

            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var seller = _sellerRepository.GetById(p => p.SellerId == id);
            if (seller == null)
            {
                return NotFound();
            }

            _sellerRepository.Delete(seller);
            return Ok(seller);

        }

        [HttpGet]
        [Route("sellers/getSeller")]
        public IActionResult GetSeller(int id)
        {

            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var seller = _sellerRepository.GetById(p => p.SellerId == id);
            SellerViewModel mappedProductVVM = _mapper.Map<SellerViewModel>(seller);

            if (seller == null)
            {
                return NotFound();
            }

            return Ok(mappedProductVVM);

        }
    }
}
