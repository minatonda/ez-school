using System;
using System.Collections.Generic;
using Api.Common.Base;

namespace Api.InstituicaoApi {
    public class InstituicaoCursoPeriodoVM : BaseVM<long> {

        public InstituicaoCursoPeriodoVM() {
            this.DiaSemana = new List<DayOfWeek>();
        }

        public string Inicio { get; set; }
        public string Fim { get; set; }
        public string PausaInicio { get; set; }
        public string PausaFim { get; set; }
        public long Quebras { get; set; }
        public List<DayOfWeek> DiaSemana { get; set; }

    }
}