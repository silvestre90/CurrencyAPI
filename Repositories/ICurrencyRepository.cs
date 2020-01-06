using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITest.Models;
using WebAPITest.Models.NBPApiModels;

namespace WebAPITest.Repositories
{
    public interface ICurrencyRepository
    {
        ICollection<Currency> GetAllCurrencies();
        void AddRequest(Request request);
        RequestType GetRequestType(int id);
        bool CurrencyExists(string code);

        void AddNewInternalRequest(string requestURI);
        void AddNewExternalRequest(string requestURI);
        void AddNewRequest(string requestURI, int requestTypeId);
        object GetRatesForValidCurrencies(List<SingleRate> nbpRates);
    }
}
