using Microsoft.Data.SqlClient;
using MiProyectoMVC.Models;
using MiProyectoMVC.Models.Modulos;
using System.Data;

namespace MiProyectoMVC.Data
{
    public class ClsModulosData
    {
        private readonly ClsAccesoDatos _db;

        public ClsModulosData(ClsAccesoDatos db)
        {
            _db = db;
        }

        public List<ClsModulosResponse> ListarModulos(int idCurso)
{
    List<ClsModulosResponse> lista = new List<ClsModulosResponse>();
    try
    {
        using (SqlConnection conn = _db.ObtenerConexion())
        {
            SqlCommand cmd = new SqlCommand("SP_ListarModulos", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Fk_Id_Curso", idCurso);
            SqlDataReader reader = cmd.ExecuteReader();
            Console.WriteLine("Filas leídas: " + reader.HasRows);
            while (reader.Read())
            {
                Console.WriteLine("Leyendo módulo: " + reader["Nombre"].ToString());
                lista.Add(new ClsModulosResponse
                {
                    Id          = int.Parse(reader["Id"].ToString()),
                    Nombre      = reader["Nombre"].ToString(),
                    Descripcion = reader["Descripcion"].ToString(),
                    Orden       = int.Parse(reader["Orden"].ToString())
                });
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
    return lista;
}

        public ClsModulosResponse AgregarModulo(ClsModulosRequest modulo)
        {
            using (SqlConnection conn = _db.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_AgregarModulo", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Fk_Id_Curso", modulo.Tbl_Fk_Id_Curso);
                cmd.Parameters.AddWithValue("@Nombre", modulo.Tbl_Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", modulo.Tbl_Descripcion);
                cmd.Parameters.AddWithValue("@Orden", modulo.Tbl_Orden);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                    return new ClsModulosResponse
                    {
                        Status = int.Parse(reader["status"].ToString()),
                        Message = reader["message"].ToString()
                    };

                return null;
            }
        }
    }
}