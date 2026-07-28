using Microsoft.Data.SqlClient;
using MiProyectoMVC.Models;
using System.Data;

namespace MiProyectoMVC.Data
{
    public class ClsCursosData
    {
        private readonly ClsAccesoDatos _db;

        public ClsCursosData(ClsAccesoDatos db)
        {
            _db = db;
        }

        public List<ClsCursos> ListarCursos()
        {
            List<ClsCursos> lista = new List<ClsCursos>();

            using (SqlConnection conn = _db.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_ListarCursos", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new ClsCursos
                    {
                        Tbl_Id_Curso = int.Parse(reader["Id"].ToString()),
                        Tbl_Nombre = reader["Nombre"].ToString(),
                        Tbl_Descripcion = reader["Descripcion"].ToString(),
                        Tbl_Duracion = reader["Duracion"].ToString(),
                        Tbl_Nivel = reader["Nivel"].ToString()
                    });
                }
            }

            return lista;
        }
    }
}