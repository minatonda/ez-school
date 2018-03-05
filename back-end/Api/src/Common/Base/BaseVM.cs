using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Common.Base {
    public class BaseVM <TID> {

        public TID ID { get; set; }
        public string Label { get; set; }

    }
    
}