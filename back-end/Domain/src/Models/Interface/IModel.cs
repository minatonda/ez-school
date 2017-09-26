using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Interface {

    public interface IBaseModel {
        bool Ativo { get; set; }
    }

}