namespace LoginAndVegitable.Utilities
{
    public class ResponseApi<T>
    {
        public T? Value { get; set; }
        public string? msg { get; set; }
        public bool? status { get; set; }
    }
}
