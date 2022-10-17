using ManejoPresupuesto.Models;
using ManejoPresupuesto.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupuesto.Controllers {
    public class TransaccionesController : Controller {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ITransaccionesRepository transaccionesRepository;
        private readonly ICuentasRepository cuentasRepository;

        public TransaccionesController(IUsuarioRepository usuarioRepository,
                                       ITransaccionesRepository transaccionesRepository, 
                                       ICuentasRepository cuentasRepository) {
            this.usuarioRepository = usuarioRepository;
            this.transaccionesRepository = transaccionesRepository;
            this.cuentasRepository = cuentasRepository;
        }

        public async Task<IActionResult> Index() { 
            return View();
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerCuentas(int usuarioID) {
            var cuentas = await cuentasRepository.ListadoCuentas(usuarioID);

            return cuentas.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
        }

        /* Vista para crear una Transacción */
        public async Task<IActionResult> Crear() { 
            var usuarioID = usuarioRepository.ObtenerUsuarioID();
            var modelo = new TransaccionCreacionModel();

            modelo.Cuentas = await ObtenerCuentas(usuarioID);

            return View(modelo);
        }
                
    }
}
