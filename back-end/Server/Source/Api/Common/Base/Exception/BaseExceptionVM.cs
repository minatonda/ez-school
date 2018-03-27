using System;
using System.Collections.Generic;

namespace Api.Common.Base {
    public class BaseExceptionVM {

        public string Code { get; set; }
        public List<BaseExceptionFieldInfo> Infos { get; set; }


    }

}