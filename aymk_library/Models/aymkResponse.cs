using aymk_library.Library.Util;
using System;

namespace aymk_library.Library.Models
{

    public class aymkResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public dynamic Data { get; set; }


        public aymkResponse()
        {
            this.IsSuccess = true;
        }

        public aymkResponse(dynamic data)
        {
            this.IsSuccess = true;
            this.Data = data;
        }

        public aymkResponse(bool isSuccess, string message)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
        }

        public aymkResponse(bool isSuccess, string message, string detail)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
            this.Detail = detail;
        }

        public aymkResponse(bool isSuccess, string message, string detail,dynamic data)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
            this.Detail = detail;
            this.Data = data;
        }


        public aymkResponse(aymkError error, string errDetail="")
        {
            this.IsSuccess = false;
            this.Message = error.GetDescription();
            this.Detail = errDetail;           
        }

        public aymkResponse(aymkError error, string entityName,string errDetail = "")
        {
            this.IsSuccess = false;
            this.Message = error.GetDescription();
            this.Detail = string.Format("{0}{1}{2}", entityName, errDetail == "" ? "" : Environment.NewLine, errDetail);
        }

        public aymkResponse(aymkError error, string entityName, Exception ex)
        {
            this.IsSuccess = false;
            this.Message = error.GetDescription();          
            this.Detail = string.Format("{0}{1}{2}", entityName, Environment.NewLine, ex.ToString());
        }

    }
}
