namespace Common.Application
{
    public class OperationResult<TData>
    {
        public OperationResult()
        {
            IsSuccessed = Status == OperationResultStatus.Success;
        }
        public const string SuccessMessage = "عملیات با موفقیت انجام شد";
        public const string ErrorMessage = "عملیات با شکست مواجه شد";

        public string Message { get; set; }
        public string Title { get; set; } = null;

        public OperationResultStatus Status { get; set; }
        public bool IsSuccessed { get; private set; }
        public TData Data { get; set; }


        public static OperationResult<TData> Success(TData data)
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Success,
                Message = SuccessMessage,
                Data = data,
                IsSuccessed = true
            };
        }
        public static OperationResult<TData> Success(TData data, string message)
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Success,
                Message = message,
                Data = data,
                IsSuccessed = true
            };
        }
        public static OperationResult<TData> NotFound()
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.NotFound,
                Title = "NotFound",
                Data = default(TData),
                IsSuccessed = false
            };
        }
        public static OperationResult<TData> EmptyList()
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.NotFound,
                Title = "NotFound",
                Data = default(TData),
                IsSuccessed = false
            };
        }
        public static OperationResult<TData> Error(string message = ErrorMessage)
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Error,
                Title = "مشکلی در عملیات رخ داده",
                Data = default(TData),
                Message = message,
                IsSuccessed = false
            };
        }


    }
    public class OperationResult
    {
        public const string SuccessMessage = "عملیات با موفقیت انجام شد";
        public const string ErrorMessage = "عملیات با شکست مواجه شد";
        public const string NotFoundMessage = "اطلاعات یافت نشد";
        public string Message { get; set; }
        public string Title { get; set; } = null;
        public OperationResultStatus Status { get; set; }
        public bool IsSuccessed { get; private set; }
        public static OperationResult Error()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Error,
                Message = ErrorMessage,
                IsSuccessed = false
            };
        }
        public static OperationResult NotFound(string message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.NotFound,
                Message = message,
                IsSuccessed = false
            };
        }
        public static OperationResult NotFound()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.NotFound,
                Message = NotFoundMessage,
                IsSuccessed = false
            };
        }
        public static OperationResult Error(string message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Error,
                Message = message,
                IsSuccessed = false
            };
        }
        public static OperationResult Error(string message, OperationResultStatus status)
        {
            return new OperationResult()
            {
                Status = status,
                Message = message,
                IsSuccessed = status == OperationResultStatus.Success
            };
        }
        public static OperationResult Success()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Success,
                Message = SuccessMessage,
                IsSuccessed = true
            };
        }
        public static OperationResult Success(string message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Success,
                Message = message,
                IsSuccessed = true
            };
        }


    }


    public enum OperationResultStatus
    {
        Error = 10,
        Success = 200,
        NotFound = 404
    }
}