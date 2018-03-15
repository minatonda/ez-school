using System;
using System.Collections.Generic;

namespace Api.Common.Base {
    public class BaseFieldInvalidException : BaseException {

        public BaseFieldInvalidException(List<BaseFieldInvalidExceptionFieldInfo> infos) {
            this.Code = BaseExceptionCode.INVALID_FIELD;
            this.Infos = infos;
        }

        public List<BaseFieldInvalidExceptionFieldInfo> Infos { get; set; }

    }

}