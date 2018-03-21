using System;
using System.Collections.Generic;

namespace Api.Common.Base {
    public class BaseUnauthorizedException : BaseException {

        public BaseUnauthorizedException(BaseRole role) {
            this.Code = BaseExceptionCode.UNAUTHORIZED;
            this.Role = role;
        }

        public BaseUnauthorizedException() {
            this.Code = BaseExceptionCode.UNAUTHORIZED;
        }

        public BaseRole Role { get; set; }

    }

}