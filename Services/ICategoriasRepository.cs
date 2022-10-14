using ManejoPresupuesto.Models;

namespace ManejoPresupuesto.Services {
    public interface ICategoriasRepository {
        Task Crear(CategoriaModel categoria);
    }
}
