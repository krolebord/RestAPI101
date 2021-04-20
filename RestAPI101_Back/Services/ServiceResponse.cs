namespace RestAPI101_Back.Services
{
    public class ServiceResponse<T> where T : class
    {
        public T value;

        public ServiceResponse(T value)
        {
            this.value = value;
        }
    }

    public class ServiceErrorResponse<T> : ServiceResponse<T> where T : class
    {
        public string errorMessage;

        public ServiceErrorResponse(string errorMessage) : base(null)
        {
            this.errorMessage = errorMessage;
        }
    }
}
