using System;
using System.Linq;
using WebAPITest.Models;
using WebAPITest.Models.NBPApiModels;

namespace WebAPITest.Services
{
    public class ApplicationHelper
    {

        public static string GetApiClientURL(string currencyCode)
        {
            string jsonFormat = "/?format=json";
            string uri = $"{NBPApiCalls.SingleCurrencyBaseURI}{currencyCode}{jsonFormat}";
            return uri;

        }
        
        public static Calculation GetCalculation(long amount, CurrencyDetails firstCurrencyDetails, CurrencyDetails secondCurrencyDetails)
        {
            Calculation calculation = new Calculation();
            calculation.From = firstCurrencyDetails.code;
            calculation.To = secondCurrencyDetails.code;
            calculation.InitialAmount = amount;
            calculation.FinalAmount = CalculateFinalAmount(amount, firstCurrencyDetails, secondCurrencyDetails);
            return calculation;
        }

        private static decimal CalculateFinalAmount(long amount, CurrencyDetails firstCurrencyDetails, CurrencyDetails secondCurrencyDetails)
        {
            double firstCoursePLN = firstCurrencyDetails.rates.FirstOrDefault().mid;
            double secondCoursePLN = secondCurrencyDetails.rates.FirstOrDefault().mid;

            decimal result = (decimal)(amount * (firstCoursePLN / secondCoursePLN));
            return Math.Round(result, 2);

        }
    }
}
