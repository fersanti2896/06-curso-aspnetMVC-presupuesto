using ManejoPresupuesto.Models;
using ManejoPresupuesto.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupuesto.Controllers {
    public class CuentasController : Controller {
        private readonly ITiposCuentasRepository tiposCuentasRepository;
        private readonly IUsuarioRepository usuarioRepository;

        public CuentasController(ITiposCuentasRepository tiposCuentasRepository, 
                                 IUsuarioRepository usuarioRepository) {
            this.tiposCuentasRepository = tiposCuentasRepository;
            this.usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Crear() {
            var usuarioID = usuarioRepository.ObtenerUsuarioID();
            var tiposCuentas = await tiposCuentasRepository.ObtenerListadoByUsuarioID(usuarioID);
            var modelo = new CuentaCreacionModel();

            modelo.TiposCuentas = tiposCuentas.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));

            return View(modelo);
        }
    }
}
