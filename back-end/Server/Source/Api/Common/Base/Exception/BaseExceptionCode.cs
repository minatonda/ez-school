using System;

namespace Api.Common.Base {
    public static class BaseExceptionCode {

        public static readonly string UNAUTHORIZED = "UNAUTHORIZED";
        public static readonly string UNAUTHORIZED_INSTITUICAO = "UNAUTHORIZED_INSTITUICAO";
        public static readonly string RESOURCE_REFUSED = "RESOURCE_REFUSED";
        public static readonly string FIELD_INVALID = "FIELD_INVALID";
        public static readonly string FIELD_REQUIRED = "FIELD_REQUIRED";
        public static readonly string USER_WRONG_USERNAME = "USER_WRONG_USERNAME";
        public static readonly string USER_WRONG_PASSWORD = "USER_WRONG_PASSWORD";
        public static readonly string REGISTER_WITH_SAME_VALUE_EXISTS = "REGISTER_WITH_SAME_VALUE_EXISTS";

    }

}