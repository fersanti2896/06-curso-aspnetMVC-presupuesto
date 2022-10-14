﻿using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Services {
    public class CuentasRepository : ICuentasRepository {
        private readonly string connectionString;

        public CuentasRepository(IConfiguration configuration) {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /* Crea una cuenta */
        public async Task Crear(CuentaModel cuenta) { 
            using var connection = new SqlConnection(connectionString);

            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO 
                                                         Cuentas (Nombre, TipoCuentaId, Balance, Descripcion)
                                                         VALUES (@Nombre, @TipoCuentaId, @Balance, @Descripcion);

                                                         SELECT SCOPE_IDENTITY();", cuenta);

            cuenta.Id = id;
        }

        /* Trae todo el listado de cuenta por el id del usuario */
        public async Task<IEnumerable<CuentaModel>> ListadoCuentas(int usuarioID) {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<CuentaModel>(@"SELECT C.Id, C.Nombre, Balance, TC.Nombre AS TipoCuenta 
                                                              FROM Cuentas C
                                                              INNER JOIN TiposCuentas TC
                                                              ON TC.Id = C.TipoCuentaId
                                                              WHERE TC.UsuarioID = @UsuarioID
                                                              ORDER BY TC.Orden", new { usuarioID });
        }
    }
}