using Domain.CursoDomain;
using Domain.EnderecoDomain;

namespace Api.EnderecoApi {

    public class EnderecoAdapter {

        public static EnderecoVM ToViewModel(Endereco model, bool deep) {
            var vm = new EnderecoVM();
            vm.ID = model.ID;
            vm.Rua = model.Rua;
            vm.Complemento = model.Complemento;
            vm.Numero = model.Numero;
            vm.Bairro = model.Bairro;
            vm.Cidade = model.Cidade;
            vm.Estado = model.Estado;
            vm.Lat = model.Lat;
            vm.Lon = model.Lon;

            return vm;
        }

        public static Endereco ToModel(EnderecoVM vm, bool deep) {
            var model = new Endereco();
            model.ID = vm.ID;
            model.Rua = vm.Rua;
            model.Complemento = vm.Complemento;
            model.Numero = vm.Numero;
            model.Bairro = vm.Bairro;
            model.Cidade = vm.Cidade;
            model.Estado = vm.Estado;
            model.Lat = vm.Lat;
            model.Lon = vm.Lon;
            
            return model;
        }

    }
}