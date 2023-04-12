
using NFC.Core.Enums;

namespace NFC.Core.Shared
{
    public class ResponseResult
    {
        /// <summary>
        /// Empty Ctor
        /// For The Default Use Of ResponseResult
        /// </summary>
        public ResponseResult()
        {
        }

        /// <summary>
        /// Ctor Take IsSuccess
        /// ApiStatusCode = NULL , ReturnData = NULL , ErrorMessage = NULL
        /// </summary>
        /// <param name="isSucceeded"></param>
        public ResponseResult(bool isSucceeded)
        {
            IsSucceeded = isSucceeded;
            ApiStatusCode = null;
            ErrorMessage = null;
            ReturnData = null;
        }

        /// <summary>
        /// Ctor Take ReturnDate And StatusCode
        /// IsSuccess = True , ErrorMessage = ""
        /// </summary>
        /// <param name="returnData"></param>
        /// <param name="apiStatusCode"></param>

        public ResponseResult(object returnData, ApiStatusCodeEnum? apiStatusCode)
        {
            IsSucceeded = true;
            ApiStatusCode = apiStatusCode.HasValue ? (int)apiStatusCode : null;
            ErrorMessage = "";
            ReturnData = returnData;
        }

        /// <summary>
        /// Ctor Take ErrorMessage And StatusCode
        /// IsSuccess = False , ReturnDate = NULL
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="apiStatusCode"></param>
        public ResponseResult(string errorMessage, ApiStatusCodeEnum? apiStatusCode)
        {
            IsSucceeded = false;
            ApiStatusCode = apiStatusCode.HasValue ? (int)apiStatusCode : null;
            ErrorMessage = errorMessage;
            ReturnData = null;
        }

        public bool IsSucceeded { get; set; }
        public int? ApiStatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public object ReturnData { get; set; }

    }
}