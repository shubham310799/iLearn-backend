namespace iLearn.Common
{
    public static class Constants
    {
        public static string UserRole_User = "User";
        public static string UserRole_Instructor = "Instructor";
    }

    public static class ErrorCodes
    {
        public static ErrorInfo UserAlreadyExist = new ErrorInfo(errorCode : "4376557f-f770-46b3-a717-57caedd7b470", errorMsg: "Given Email already exists");
        public static ErrorInfo UserNotFound = new ErrorInfo(errorCode: "ee454718-b103-469a-b7a9-c54a8ebd7bd1", errorMsg: "Email or Password is incorrect");
        public static ErrorInfo SomethingWentWrong = new ErrorInfo(errorCode: "8772926d-e871-4b75-951c-fbea084ed5fc", errorMsg: "Something Went Wrong");
        public static ErrorInfo DbError = new ErrorInfo(errorCode: "3b9efd49-513b-42d8-a879-3fa4522f1382", errorMsg:"Database Error");
        public static ErrorInfo UserIsNotAuthenticated = new ErrorInfo(errorCode: "b0c189a9-8972-4a1a-9377-97ed4faeeed3", errorMsg: "Unauthenticated User");
        public static ErrorInfo UnauthorizedUser = new ErrorInfo(errorCode: "3022e757-6d3a-4627-af76-b38f7fac1fda", errorMsg:"You are not authorized to access this");
        public static ErrorInfo RequestValidationFailed = new ErrorInfo(errorCode: "35bbac74-b1cd-464a-9d99-15e4bd89bf1a", errorMsg: "Bad Request");
    }

    public class ErrorInfo
    {
        public ErrorInfo(string errorCode, string errorMsg)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMsg;
        }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
