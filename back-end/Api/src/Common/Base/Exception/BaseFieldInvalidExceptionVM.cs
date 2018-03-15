using System;
using System.Collections.Generic;

namespace Api.Common.Base {
    public class BaseFieldInvalidExceptionVM : BaseExceptionVM {

        public List<BaseFieldInvalidExceptionFieldInfo> Infos { get; set; }

    }

}