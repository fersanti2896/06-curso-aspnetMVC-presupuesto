using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Services {
    public class CuentasRepository : ICuentasRepository {
        private readonly string connectionString;

        public CuentasRepository(IConfiguration configuration) {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(CuentaModel cuenta) { 
            using var connection = new SqlConnection(connectionString);

            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO 
                                                         Cuentas (Nombre, TipoCuentaId, Balance, Descripcion)
                                                         VALUES (@Nombre, @TipoCuentaId, @Balance, @Descripcion);

                                                         SELECT SCOPE_IDENTITY();", cuenta);

            cuenta.Id = id;
        }
    }
}
