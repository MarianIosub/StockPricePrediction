namespace ServiceLayer.Models
{
    public class ApiResponse<T>
    {
        public bool Succeed { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }

        public static ApiResponse<T> Fail(string errorMessage)
        {
            return new ApiResponse<T> {Succeed = false, Error = errorMessage};
        }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T> {Succeed = true, Data = data};
        }
    }
}