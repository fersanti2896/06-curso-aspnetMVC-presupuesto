using ManejoPresupuesto.Models;

namespace ManejoPresupuesto.Services {
    public interface ICategoriasRepository {
        Task Crear(CategoriaModel categoria);
        Task<IEnumerable<CategoriaModel>> ObtenerCategorias(int usuarioID);
    }
}
