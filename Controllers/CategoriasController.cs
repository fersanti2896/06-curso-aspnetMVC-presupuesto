using ManejoPresupuesto.Models;
using ManejoPresupuesto.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManejoPresupuesto.Controllers {
    public class CategoriasController : Controller {
        private readonly ICategoriasRepository categoriasRepository;
        private readonly IUsuarioRepository usuarioRepository;

        public CategoriasController(ICategoriasRepository categoriasRepository,
                                    IUsuarioRepository usuarioRepository) {
            this.categoriasRepository = categoriasRepository;
            this.usuarioRepository = usuarioRepository;
        }

        /* Vista para crear categoria */
        [HttpGet]
        public IActionResult Crear() { 
            return View();
        }

        /* Crear Categoria */
        [HttpPost]
        public async Task<IActionResult> Crear(CategoriaModel categoria) {
            var usuarioID = usuarioRepository.ObtenerUsuarioID();

            if (!ModelState.IsValid) { 
                return View(categoria);
            }

            categoria.UsuarioID = usuarioID;
            await categoriasRepository.Crear(categoria);

            return RedirectToAction("Index");
        }
    }
}
