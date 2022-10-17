using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Services {
    public class TransaccionesRepository : ITransaccionesRepository {
        private readonly string connectionString;

        public TransaccionesRepository(IConfiguration configuration) {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /* Creando Transaccion */
        public async Task Crear(TransaccionModel transaccion) {
            using var connection = new SqlConnection(connectionString);

            var id = await connection.QuerySingleAsync<int>("Transacciones_NuevoRegistro", 
                                                            new { 
                                                                transaccion.UsuarioID, 
                                                                transaccion.FechaTransaccion, 
                                                                transaccion.Monto,
                                                                transaccion.CategoriaID, 
                                                                transaccion.CuentaID, 
                                                                transaccion.Nota 
                                                            },
                                                            commandType: System.Data.CommandType.StoredProcedure);

            transaccion.Id = id;
        }
    }
}
