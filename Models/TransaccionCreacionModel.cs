﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models {
    public class TransaccionCreacionModel : TransaccionModel {
        public IEnumerable<SelectListItem> Cuentas { get; set; }

        public IEnumerable<SelectListItem> Categorias { get; set; }

        [Display(Name = "Tipo de Operación")]
        public TipoOperacionModel TipoOperacionId { get; set; } = TipoOperacionModel.Ingreso;
    }
}
