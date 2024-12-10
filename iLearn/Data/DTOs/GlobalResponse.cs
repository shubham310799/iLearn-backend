using iLearn.Common;
using System.Text.Json.Serialization;

namespace iLearn.Data.DTOs
{
    public class GlobalResponse<T>
    {
        public GlobalResponse()
        {
            
        }
        GlobalResponse(ErrorInfo errorInfo)
        {
            ErrorCode = errorInfo.ErrorCode;
            Message = errorInfo.ErrorMessage;
        }
        [JsonPropertyName("error_code")]
        public string ErrorCode { get; set; } = string.Empty;
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("has_error")]
        public bool HasError
        {
            get
            {
                return !string.IsNullOrEmpty(ErrorCode);
            }
        }
        [JsonPropertyName("data")]
        public T? Data { get; set; }
    }
}
