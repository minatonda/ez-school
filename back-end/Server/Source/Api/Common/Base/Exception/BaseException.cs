using System;
using System.Collections.Generic;
using System.Net;

namespace Api.Common.Base {
    public class BaseException : Exception {

        public HttpStatusCode HttpStatusCode = HttpStatusCode.BadRequest;
        public string Code { get; set; }
        public List<BaseExceptionFieldInfo> Infos { get; set; }


    }

}