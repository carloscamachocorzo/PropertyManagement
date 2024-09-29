namespace Million.PropertyManagement.Common
{
    public sealed class RequestResult<T>
    {
        public bool IsSuccessful { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<string> Messages { get; set; }
        public T Result { get; set; }

        // Constructor privado
        private RequestResult(bool isSuccessful, bool isError, string errorMessage, IEnumerable<string> messages, T result)
        {
            IsSuccessful = isSuccessful;
            IsError = isError;
            ErrorMessage = errorMessage;
            Messages = messages;
            Result = result;
        }
        // Método estático para crear una respuesta exitosa
        public static RequestResult<T> CreateSuccessful(T result, IEnumerable<string> messages = null)
        {
            return new RequestResult<T>(isSuccessful: true, isError: false, errorMessage: null, messages: messages, result: result);
        }

        // Método estático para crear una respuesta no exitosa pero sin error
        public static RequestResult<T> CreateUnsuccessful(IEnumerable<string> messages)
        {
            return new RequestResult<T>(isSuccessful: false, isError: false, errorMessage: null, messages: messages, result: default(T));
        }
        // Método estático para crear una respuesta con error
        public static RequestResult<T> CreateError(string errorMessage)
        {
            return new RequestResult<T>(isSuccessful: false, isError: true, errorMessage: errorMessage, messages: null, result: default(T));
        }
    }

}
