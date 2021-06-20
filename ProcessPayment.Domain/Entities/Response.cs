namespace ProcessPayment.Domain.Entities
{
    public class Response<T>
    {
        public string State { get; set; }
        public T Data { get; set; }
    }
}
