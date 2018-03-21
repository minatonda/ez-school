using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common {

    public interface IBaseModel {
        DateTime? Ativo { get; set; }
    }

}