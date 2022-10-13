using ManejoPresupuesto.Models;
using ManejoPresupuesto.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupuesto.Controllers {
    public class CuentasController : Controller {
        private readonly ITiposCuentasRepository tiposCuentasRepository;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ICuentasRepository cuentasRepository;

        public CuentasController(ITiposCuentasRepository tiposCuentasRepository, 
                                 IUsuarioRepository usuarioRepository, 
                                 ICuentasRepository cuentasRepository) {
            this.tiposCuentasRepository = tiposCuentasRepository;
            this.usuarioRepository = usuarioRepository;
            this.cuentasRepository = cuentasRepository;
        }

        /* Listado de Cuentas */
        public async Task<IActionResult> Index(){
            

            return View();
        }

        /* Muestra el formulario para crear una cuenta */
        [HttpGet]
        public async Task<IActionResult> Crear() {
            var usuarioID = usuarioRepository.ObtenerUsuarioID();
            var modelo = new CuentaCreacionModel();

            modelo.TiposCuentas = await ObtenerTiposCuentas(usuarioID);

            return View(modelo);
        }

        /* Crea una cuenta */
        [HttpPost]
        public async Task<IActionResult> Crear(CuentaCreacionModel cuenta) {
            var usuarioID = usuarioRepository.ObtenerUsuarioID();
            var tipoCuenta = await tiposCuentasRepository.ObtenerTipoCuentaById(cuenta.TipoCuentaId, usuarioID);

            if (tipoCuenta is null) {
                return RedirectToAction("NoEncontrado", "Home");
            }

            if (!ModelState.IsValid) {
                cuenta.TiposCuentas = await ObtenerTiposCuentas(usuarioID);

                return View(cuenta);
            }

            await cuentasRepository.Crear(cuenta);

            return RedirectToAction("Index");
        }

        /* Obtiene el listado de tipos cuenta para select en el formulario */
        private async Task<IEnumerable<SelectListItem>> ObtenerTiposCuentas(int usuarioID) {
            var tiposCuentas = await tiposCuentasRepository.ObtenerListadoByUsuarioID(usuarioID);

            return tiposCuentas.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
        }
    }
}
