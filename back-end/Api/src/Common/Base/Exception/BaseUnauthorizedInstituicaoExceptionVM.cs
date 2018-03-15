using System;
using System.Collections.Generic;

namespace Api.Common.Base {
    public class BaseUnauthorizedInstituicaoExceptionVM : BaseUnauthorizedExceptionVM {
        
        public long IdInstituicao { get; set; }

    }

}