namespace Warehouse.App
{
    public class CommandError
    {
        public CommandError()
        {
        }

        public CommandError(string errorMessage, string errorCode = null)
        {
            Code = errorCode;
            Message = errorMessage;
        }

        public string Code { get; set; }
        public string Message { get; set; }
    }
}