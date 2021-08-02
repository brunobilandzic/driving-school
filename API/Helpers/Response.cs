namespace API.Helpers
{
    public class Response<T>
    {
        public Response()
        {
        }

        public Response(T response)
        {
            Data = response;
        }

        private T Data { get; set; }
    }
}