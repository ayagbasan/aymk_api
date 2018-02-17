using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aymk_engine.Model.Custom
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public dynamic Data { get; set; }
        public Response()
        {
            this.IsSuccess = true;
            this.Message = string.Empty;
        }

        public Response(dynamic data)
        {
            this.IsSuccess = true;
            this.Message = string.Empty;
            this.Data = data;
        }

        public Response(string errorMessage)
        {
            this.Message = errorMessage;
            this.IsSuccess = false;
        }
    }


}
