namespace WebAPITest.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string RequestDate { get; set; }
        public string RequestTime { get; set; }
        public RequestType RequestType { get; set; }
        public string RequestURL { get; set; }
    }
}
