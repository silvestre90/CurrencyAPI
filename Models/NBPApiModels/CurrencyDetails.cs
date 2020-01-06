using System.Collections.Generic;

namespace WebAPITest.Models.NBPApiModels
{

    public class CurrencyDetails
        {
            public string table { get; set; }
            public string currency { get; set; }
            public string code { get; set; }

        
        public virtual IEnumerable<Rate> rates { get; set; }
        }
    
}
