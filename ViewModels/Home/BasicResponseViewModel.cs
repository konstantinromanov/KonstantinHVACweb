namespace KonstantinHVACweb.ViewModels.Home
{
    public class BasicResponseViewModel
    {
        public string Message { get; set; }
        public bool Error { get; set; }

        public BasicResponseViewModel(string message, bool error)
        {
            Message = message;
            Error = error;
        }
    }

    public class DataResponseModel : BasicResponseViewModel
    {
        public dynamic Data { get; set; }

        public DataResponseModel(string message, bool error) : base(message, error)
        {
        }

        public DataResponseModel(string message, bool error, dynamic data) : base(message, error)
        {
            Data = data;
        }
    }
}
