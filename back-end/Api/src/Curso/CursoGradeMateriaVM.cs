using Api.Common.ViewModels;
using Api.MateriaApi;

namespace Api.CursoApi {

    public class CursoGradeMateriaVM : SelectVM {

        public CursoGradeMateriaVM() {

        }

        public string Descricao { get; set; }
        public MateriaVM Materia { get; set; }

    }
}