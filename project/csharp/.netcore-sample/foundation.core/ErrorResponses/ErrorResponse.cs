namespace Edu.Ntu.Foundation.Core.ErrorResponses
{
    public class ErrorResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public object Parameters { get; set; }
    }
}