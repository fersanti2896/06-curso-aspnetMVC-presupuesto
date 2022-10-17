using ManejoPresupuesto.Models;

namespace ManejoPresupuesto.Services {
    public interface ITransaccionesRepository {
        Task Crear(TransaccionModel transaccion);
    }
}
