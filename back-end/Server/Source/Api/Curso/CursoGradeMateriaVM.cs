using Api.Common.Base;
using Api.MateriaApi;

namespace Api.CursoApi {

    public class CursoGradeMateriaVM : BaseVM<long> {

        public CursoGradeMateriaVM() {

        }

        public string NomeExibicao { get; set; }
        public string Descricao { get; set; }
        public string Tags { get; set; }
        public string Grupo { get; set; }
        public long NumeroAulas { get; set; }
        public MateriaVM Materia { get; set; }

    }
}