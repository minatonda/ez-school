using System;
using System.Collections.Generic;

namespace Api.Common.Base {
    public class BaseUnauthorizedInstituicaoException : BaseUnauthorizedException {

        public BaseUnauthorizedInstituicaoException(BaseRole role, long IdInstituicao) : base(role) {
            this.Code = BaseExceptionCode.UNAUTHORIZED_INSTITUICAO;
            this.IdInstituicao = IdInstituicao;
        }

        public long IdInstituicao { get; set; }

    }

}