using Api.Common.Base;
using Api.MateriaApi;

namespace Api.CursoApi {

    public class CursoGradeMateriaVM : BaseVM<long> {

        public CursoGradeMateriaVM() {

        }

        public string Descricao { get; set; }
        public MateriaVM Materia { get; set; }

    }
}