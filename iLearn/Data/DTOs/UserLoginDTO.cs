using iLearn.Common;

namespace iLearn.Data.DTOs
{
    public class UserLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public static GlobalResponse<string> ValidateRequest(UserLoginDTO user)
        {
            var res = new GlobalResponse<string>();
            try
            {
                if (user == null ||
                string.IsNullOrEmpty(user.Email) ||
                string.IsNullOrEmpty(user.Password))
                {
                    res = new GlobalResponse<string>()
                    {
                        ErrorCode = ErrorCodes.RequestValidationFailed.ErrorCode,
                        Message = ErrorCodes.RequestValidationFailed.ErrorMessage
                    };
                }
            }
            catch(Exception ex)
            {
                res = new GlobalResponse<string>()
                {
                    ErrorCode = ErrorCodes.SomethingWentWrong.ErrorCode,
                    Message = ErrorCodes.SomethingWentWrong.ErrorMessage
                };
            }
            return res;
        }
    }
}
