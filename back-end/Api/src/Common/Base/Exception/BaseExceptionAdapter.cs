using System;

namespace Api.Common.Base {
    public class BaseExceptionAdapter {

        public static BaseExceptionVM ToBaseExceptionViewModel(BaseException exception) {
            return new BaseExceptionVM() {
                Code = exception.Code
            };
        }

        public static BaseFieldInvalidExceptionVM ToBaseFieldInvalidExceptionViewModel(BaseFieldInvalidException exception) {
            return new BaseFieldInvalidExceptionVM() {
                Code = exception.Code,
                Infos = exception.Infos
            };
        }

        public static BaseUnauthorizedExceptionVM ToBaseUnauthorizedException(BaseUnauthorizedException exception) {
            return new BaseUnauthorizedExceptionVM() {
                Code = exception.Code,
                Role = exception.Role
            };
        }

        public static BaseUnauthorizedInstituicaoExceptionVM ToBaseUnauthorizedInstituicaoException(BaseUnauthorizedInstituicaoException exception) {
            return new BaseUnauthorizedInstituicaoExceptionVM() {
                Code = exception.Code,
                Role = exception.Role,
                IdInstituicao = exception.IdInstituicao
            };
        }

    }

}