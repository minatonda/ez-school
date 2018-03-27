using System;

namespace Api.Common.Base {
    public class BaseExceptionAdapter {

        public static BaseExceptionVM ToBaseExceptionViewModel(BaseException exception) {
            return new BaseExceptionVM() {
                Code = exception.Code,
                Infos = exception.Infos
            };
        }

    }

}