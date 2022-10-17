using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Services {
    public class CategoriasRepository : ICategoriasRepository {
        private readonly string configurationString;

        public CategoriasRepository(IConfiguration configuration) {
            configurationString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(CategoriaModel categoria) { 
            using var connection = new SqlConnection(configurationString);
            
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO 
                                                              Categorias (Nombre, TipoOperacionID, UsuarioID)
                                                              VALUES (@Nombre, @TipoOperacionID, @UsuarioID);

                                                              SELECT SCOPE_IDENTITY();", categoria);

            categoria.Id = id;  
        }

        public async Task<IEnumerable<CategoriaModel>> ObtenerCategorias(int usuarioID) {
            using var connection = new SqlConnection(configurationString);

            return await connection.QueryAsync<CategoriaModel>(@"SELECT * FROM Categorias
                                                                 WHERE UsuarioID = @UsuarioID", new { usuarioID });
        }
    }
}
