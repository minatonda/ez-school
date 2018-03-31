using System;
using System.Collections.Generic;

namespace Api.Common.Base {
    public class BaseExceptionFieldInfo {

        public string Value { get; set; }
        public string ValueType { get; set; } = BaseExceptionFieldInfoValueType.TEXT;
        public string Code { get; set; }
        public string Field { get; set; }
        public List<BaseExceptionFieldInfo> References { get; set; }

    }

    public class BaseExceptionFieldInfoValueType {
        public static readonly string TEXT = "TEXT";
        public static readonly string CURRENCY = "CURRENCY";
        public static readonly string DATE = "DATE";
        public static readonly string TIME = "TIME";
        public static readonly string DATETIME = "DATETIME";
    }

}