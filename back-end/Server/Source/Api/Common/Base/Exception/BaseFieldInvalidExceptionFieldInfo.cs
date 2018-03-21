using System;
using System.Collections.Generic;

namespace Api.Common.Base {
    public class BaseFieldInvalidExceptionFieldInfo {

        public BaseFieldInvalidExceptionCode Code { get; set; }

        public BaseField Field { get; set; }

        public List<BaseFieldInvalidExceptionFieldInfo> References { get; set; }

    }

    public enum BaseFieldInvalidExceptionCode {

    }

    public enum BaseField {

    }


}