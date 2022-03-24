namespace PublicApi
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool Succeed { get; set; }
        public string Message { get; set; }

        public static ApiResponse<T> Fail(string errorMessage) => 
            new ApiResponse<T> { Succeed = false, Message = errorMessage };
            
        public static ApiResponse<T> Success(T data) => 
            new ApiResponse<T> { Succeed = true, Data = data };
    }
}