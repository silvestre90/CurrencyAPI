using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPITest.Models;
using WebAPITest.Repositories;
using Microsoft.AspNetCore.Http.Extensions;
using WebAPITest.Services;

namespace WebAPITest.Controllers
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {

        private ICurrencyRepository _repository;
        private NBPApiClient _apiClient;
        private const string ServerError = "500 - Error on the server site";

        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            _repository = currencyRepository;
            _apiClient = new NBPApiClient();
        }
        // GET api/currencies
        
        [HttpGet]
        public IActionResult GetCurrencies()
        {
            try
            {
                var result = _repository.GetAllCurrencies();
                _repository.AddNewInternalRequest(GetRequestUrl());
                return Ok(result);
            }
            catch (System.Exception)
            {

                return StatusCode(500);
            }
            
        }

        private string GetRequestUrl()
        {
            return UriHelper.GetDisplayUrl(Request);
        }

        // GET api/currencies/actualcourses
        [HttpGet]
        [Route("actualcourses")]
        public async Task<IActionResult> GetCurrentCourses()
        {
            try
            {
                _repository.AddNewInternalRequest(GetRequestUrl());
                var nbpRates = await _apiClient.GetAllCourses();
                _repository.AddNewExternalRequest(NBPApiCalls.CoursesAllCurrencies);
                var result = _repository.GetRatesForValidCurrencies(nbpRates);

                return Ok(result);
            }
            catch (System.Exception)
            {

                return StatusCode(500);
            }
            
        }

        // GET api/currencies/1000/USD/EUR
        [HttpGet("{amount}/{firstCurrency}/{secondCurrency}")]
        public async Task<IActionResult> CalculateAmount(long amount, string firstCurrency, string secondCurrency)

        {
            _repository.AddNewInternalRequest(GetRequestUrl());
            if (!(_repository.CurrencyExists(firstCurrency) && _repository.CurrencyExists(secondCurrency)))
            {
                return NotFound();
            }

            if(amount < 0)
            {
                return BadRequest();
            }

            try
            {
                var firstCurrencyDetails = await _apiClient.GetCurrencyCourse(firstCurrency);
                _repository.AddNewExternalRequest(ApplicationHelper.GetApiClientURL(firstCurrency));
                var secondCurrencyDetails = await _apiClient.GetCurrencyCourse(secondCurrency);
                _repository.AddNewExternalRequest(ApplicationHelper.GetApiClientURL(secondCurrency));

                Calculation calculation = ApplicationHelper.GetCalculation(amount, firstCurrencyDetails, secondCurrencyDetails);
                return Ok(calculation);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
            
        }

    }
}
