using System;

namespace Api.Common.Base {
    public static class BaseExceptionCode {

        public static readonly string UNAUTHORIZED = "UNAUTHORIZED";
        public static readonly string UNAUTHORIZED_INSTITUICAO = "UNAUTHORIZED_INSTITUICAO";

        public static readonly string RESOURCE_REFUSED = "RESOURCE_REFUSED";

        public static readonly string FIELD_INVALID = "FIELD_INVALID";
        public static readonly string FIELD_REQUIRED = "FIELD_REQUIRED";

        public static readonly string FIELD_LESS_THAN = "FIELD_LESS_THAN";
        public static readonly string FIELD_LESS_THAN_OR_EQUAL = "FIELD_LESS_THAN_OR_EQUAL";
        public static readonly string FIELD_GREATER_THAN = "FIELD_GREATER_THAN";
        public static readonly string FIELD_GREATER_THAN_OR_EQUAL = "FIELD_GREATER_THAN_OR_EQUAL";

        public static readonly string FIELD_LIST_LESS_THAN = "FIELD_LIST_LESS_THAN";
        public static readonly string FIELD_LIST_LESS_THAN_OR_EQUAL = "FIELD_LIST_LESS_THAN_OR_EQUAL";
        public static readonly string FIELD_LIST_GREATER_THAN = "FIELD_LIST_GREATER_THAN";
        public static readonly string FIELD_LIST_GREATER_THAN_OR_EQUAL = "FIELD_LIST_GREATER_THAN_OR_EQUAL";

        public static readonly string FIELD_HOUR_INVALID = "FIELD_HOUR_INVALID";
        public static readonly string FIELD_HOUR_LESS_THAN = "FIELD_HOUR_LESS_THAN";
        public static readonly string FIELD_HOUR_LESS_THAN_OR_EQUAL = "FIELD_HOUR_LESS_THAN_OR_EQUAL";
        public static readonly string FIELD_HOUR_GREATER_THAN = "FIELD_HOUR_GREATER_THAN";
        public static readonly string FIELD_HOUR_GREATER_THAN_OR_EQUAL = "FIELD_HOUR_GREATER_THAN_OR_EQUAL";

        public static readonly string FIELD_DATE_INVALID = "FIELD_DATE_INVALID";
        public static readonly string FIELD_DATE_LESS_THAN = "FIELD_DATE_LESS_THAN";
        public static readonly string FIELD_DATE_LESS_THAN_OR_EQUAL = "FIELD_DATE_LESS_THAN_OR_EQUAL";
        public static readonly string FIELD_DATE_GREATER_THAN = "FIELD_DATE_GREATER_THAN";
        public static readonly string FIELD_DATE_GREATER_THAN_OR_EQUAL = "FIELD_DATE_GREATER_THAN_OR_EQUAL";

        public static readonly string USER_WRONG_USERNAME = "USER_WRONG_USERNAME";
        public static readonly string USER_WRONG_PASSWORD = "USER_WRONG_PASSWORD";

        public static readonly string REGISTER_WITH_SAME_VALUE_EXISTS = "REGISTER_WITH_SAME_VALUE_EXISTS";

    }

}