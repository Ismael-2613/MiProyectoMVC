using Microsoft.Data.SqlClient;

namespace MiProyectoMVC.Data
{

    public class ClsAccesoDatos
    {
        // Iconfiguration = permite leer el appsetting.json
        private readonly IConfiguration _config;
        public ClsAccesoDatos(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection(
                _config.GetConnectionString("DefaultConnection"));
            conexion.Open();
            return conexion;
        }
    }

}