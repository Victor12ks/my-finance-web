namespace MyFinanceWeb.Domain.Core
{
    public class Response<TResponse>
    {
        public bool Success { get; set; }
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
        public Response(TResponse data, string message = "", bool sucess = true)
        {
            this.Data = data;
            this.Message = message;
            this.Success = sucess;
        }
        
        public Response(string message)
        {
            this.Message = message;
            this.Success = false;
        }
    }
}
