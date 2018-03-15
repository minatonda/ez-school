using System;
using System.Collections.Generic;

namespace Api.Common.Base {
    public class BaseUnauthorizedExceptionVM : BaseExceptionVM {
        
        public BaseRole Role { get; set; }

    }

}