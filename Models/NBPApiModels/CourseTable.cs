using System.Collections.Generic;

namespace WebAPITest.Models.NBPApiModels
{
    public class CourseTable
    {
        public string table { get; set; }
        public string no { get; set; }
        public string effectiveDate { get; set; }
        public List<SingleRate> rates { get; set; }
    }
}
