namespace RestAPI101_Back.Services
{
    public class ServiceResponse<T>
    {
        private T? _value;

        public T Value => _value!;

        public ServiceResponse(T? value)
        {
            this._value = value;
        }
    }

    public class ServiceErrorResponse<T> : ServiceResponse<T>
    {
        public string errorMessage;

        public ServiceErrorResponse(string errorMessage) : base(default)
        {
            this.errorMessage = errorMessage;
        }
    }
}
