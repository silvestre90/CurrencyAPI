namespace WebAPITest.Models
{
    public class RequestType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public enum RequestTypeTranslation
    {
        InternalRequest = 1,
        ExternalRequest = 2,
    }
}