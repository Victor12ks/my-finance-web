namespace MyFinanceWeb.Domain.Core
{
    public class Response<TResponse>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public TResponse? Data { get; set; }

        public Response(Exception ex)
        {
            Message = ex.Message;
            Success = false;
        }
        public Response()
        {
            
        }
        public Response(TResponse data, string message = "")
        {
            Data = data;
            this.Message = message;
        }
        public Response(TResponse data)
        {
            Data = data;
        }
        public Response(string message)
        {
            this.Message = message;
            this.Success = false;
        }
    }
}
