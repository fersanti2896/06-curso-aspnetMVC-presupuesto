using ManejoPresupuesto.Models;

namespace ManejoPresupuesto.Services {
    public interface ICuentasRepository {
        Task Crear(CuentaModel cuenta);
    }
}
