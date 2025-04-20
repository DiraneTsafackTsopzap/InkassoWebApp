namespace backend.Models
{
    public class OperationResult
    {
        public bool IsSucces { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }

        public OperationResult()

        {
            IsSucces = true;
            IsError = false;
            Message = string.Empty;

        }

        public OperationResult(string message)

        {
            IsSucces = false;
            IsError = true;
            Message = message;

        }
    }
}
