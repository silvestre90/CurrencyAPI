using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPITest.Models;
using WebAPITest.Models.NBPApiModels;

namespace WebAPITest.Services
{
    public class NBPApiClient
    {
        private  static HttpClient _httpClient = new HttpClient();
        public NBPApiClient()
        {
            
        }

        public async Task<CurrencyDetails> GetCurrencyCourse(string currencyCode)
        {
            
            string uri = ApplicationHelper.GetApiClientURL(currencyCode);
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            CurrencyDetails currencyCourse = JsonConvert.DeserializeObject<CurrencyDetails>(content);
            return currencyCourse;
            
        }

        public async Task<List<SingleRate>> GetAllCourses()
        {
            string uri = NBPApiCalls.CoursesAllCurrencies;
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            CourseTable[] tables = JsonConvert.DeserializeObject<CourseTable[]>(content);

            List<SingleRate> rates = tables[0].rates;
            return rates;
        }
    }
}
