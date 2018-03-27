using System;
using System.Collections.Generic;

namespace Api.Common.Base {
    public class BaseExceptionFieldInfo {

        public string Value { get; set; }
        public string Code { get; set; }
        public string Field { get; set; }
        public List<BaseExceptionFieldInfo> References { get; set; }

    }

}