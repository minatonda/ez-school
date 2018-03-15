using System;

namespace Api.Common.Base {
    public class BaseException : Exception {

        public BaseExceptionCode Code { get; set; }

    }

}