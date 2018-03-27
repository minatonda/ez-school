using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Common.Base;

namespace Api.Common {
    public class TreeViewVM<TID> : BaseVM<TID> {

        public List<TreeViewVM<TID>> Children { get; set; }
        public bool ChildrenRequisite { get; set; }

    }

}