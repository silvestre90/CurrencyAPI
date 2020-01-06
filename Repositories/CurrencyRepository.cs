using System;
using System.Collections.Generic;
using System.Linq;
using WebAPITest.Data;
using WebAPITest.Models;
using WebAPITest.Models.NBPApiModels;

namespace WebAPITest.Repositories
{
    public class CurrencyRepository: ICurrencyRepository
    {
        private CurrencyAppContext _context;

        public CurrencyRepository(CurrencyAppContext context)
        {
            _context = context;
        }

        public void AddNewExternalRequest(string requestURI)
        {
            int requestTypeId = (int)RequestTypeTranslation.ExternalRequest;
            AddNewRequest(requestURI, requestTypeId);
        }

        public void AddNewInternalRequest(string requestURI)
        {
            int requestTypeId = (int)RequestTypeTranslation.InternalRequest;
            AddNewRequest(requestURI, requestTypeId);
        }

        public void AddNewRequest(string requestURI, int requestTypeId)
        {
            RequestType requestType = GetRequestType(requestTypeId);
            Request request = new Request();

            request.RequestType = requestType;
            request.RequestURL = requestURI;
            request.RequestDate = DateTime.Now.ToShortDateString();
            request.RequestTime = DateTime.Now.ToShortTimeString();

            AddRequest(request);
        }

        public void AddRequest(Request request)
        {
            _context.Requests.Add(request);
            _context.SaveChanges();
        }

        public bool CurrencyExists(string code)
        {
            return _context.Currencies.Where(c => c.CurrencyCode == code).ToList().Count == 1;
        }

        public ICollection<Currency> GetAllCurrencies()
        {
            return _context.Currencies.ToList();
        }

        public object GetRatesForValidCurrencies(List<SingleRate> nbpRates)
        {
            var currenciesInDb = GetAllCurrencies();
            var result = from r in nbpRates
                         join c in currenciesInDb on r.code equals c.CurrencyCode
                         select new { r.code, r.mid };

            return result;
        }

        public RequestType GetRequestType(int id)
        {
            return _context.RequestTypes.FirstOrDefault(r => r.Id == id);
        }
    }
}
