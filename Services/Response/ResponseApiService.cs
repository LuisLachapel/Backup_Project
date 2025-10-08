using Entity;

namespace Services.Response
{
    public static class ResponseApiService
    {
        public static ResponseModel Response(int statusCode, object? value = null, string? message = null)
        {
            bool success = false;
            if(statusCode >= 200 && statusCode < 300)
            {
                success = true;
            }

            var response = new ResponseModel
            {
                StatusCode = statusCode,
                Value = value,
                Message = message,
                Success = success
            };
            return response;
        }
    }
}
