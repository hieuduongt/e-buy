namespace Shared.Common
{

    public class EBuyResponse
    {
        public int Code { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public bool IsSuccess { get; set; }

        public static EBuyResponse Failed(List<string> messages)
        {
            return new EBuyResponse()
            {
                Code = 500,
                Messages = messages,
                IsSuccess = false
            };
        }

        public static EBuyResponse Success()
        {
            return new EBuyResponse()
            {
                Code = 200,
                IsSuccess = true
            };
        }
    }

    public class EBuyResponse<T> : EBuyResponse where T : class
    {
        public T? Result { get; set; }

        public static new EBuyResponse<T> Failed(List<string> messages)
        {
            return new EBuyResponse<T>()
            {
                Code = 500,
                Messages = messages,
                IsSuccess = false,
                Result = null
            };
        }

        public static EBuyResponse<T> Success(T result)
        {
            return new EBuyResponse<T>()
            {
                Code = 200,
                IsSuccess = true,
                Result = result
            };
        }
    }

    public class Paginated<T> where T : class
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}
