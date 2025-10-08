

namespace Entity
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; }
        public dynamic? Value { get; set; }
    }
}
