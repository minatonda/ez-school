using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Interface;

namespace Service.Data.ViewModels {
    public class ShortVM {

        public long ID { get; set; }
        public string Label { get; set; }

    }
    
}