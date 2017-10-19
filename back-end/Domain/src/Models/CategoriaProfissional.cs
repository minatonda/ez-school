using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Interface;

namespace Domain.Models {
    public class CategoriaProfissional : IBaseModel
    {
        public CategoriaProfissional() 
        {

        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get ; set; }
        public string Nome { get ; set; }
        public string Descricao { get; set; }
        public bool Ativo { get ; set; } = true;
    }

}