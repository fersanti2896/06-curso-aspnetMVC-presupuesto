using ManejoPresupuesto.Models;
using ManejoPresupuesto.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupuesto.Controllers {
    public class TransaccionesController : Controller {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ITransaccionesRepository transaccionesRepository;
        private readonly ICuentasRepository cuentasRepository;
        private readonly ICategoriasRepository categoriasRepository;

        public TransaccionesController(IUsuarioRepository usuarioRepository,
                                       ITransaccionesRepository transaccionesRepository, 
                                       ICuentasRepository cuentasRepository, 
                                       ICategoriasRepository categoriasRepository) {
            this.usuarioRepository = usuarioRepository;
            this.transaccionesRepository = transaccionesRepository;
            this.cuentasRepository = cuentasRepository;
            this.categoriasRepository = categoriasRepository;
        }

        public async Task<IActionResult> Index() { 
            return View();
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerCuentas(int usuarioID) {
            var cuentas = await cuentasRepository.ListadoCuentas(usuarioID);

            return cuentas.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
        }

        private async Task<IEnumerable<SelectListItem>> ListadoCategoriasByTipoOperacion(int usuarioID, TipoOperacionModel tipoOperacion) { 
            var categorias = await categoriasRepository.ObtenerCategoriasByTipoOperacion(usuarioID, tipoOperacion);

            return categorias.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
        }

        /* Obtiene las categorias en base al tipo de operacion */
        [HttpPost]
        public async Task<IActionResult> ObtenerCategorias([FromBody] TipoOperacionModel tipoOperacion) {
            var usuarioID = usuarioRepository.ObtenerUsuarioID();
            var categorias = await ListadoCategoriasByTipoOperacion(usuarioID, tipoOperacion);

            return Ok(categorias);
        }

        /* Vista para crear una Transacción */
        public async Task<IActionResult> Crear() {
            var usuarioID = usuarioRepository.ObtenerUsuarioID();
            var modelo = new TransaccionCreacionModel();

            modelo.Cuentas = await ObtenerCuentas(usuarioID);
            modelo.Categorias = await ListadoCategoriasByTipoOperacion(usuarioID, modelo.TipoOperacionId);

            return View(modelo);
        }
    }
}
