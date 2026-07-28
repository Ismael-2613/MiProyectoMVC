using Microsoft.Data.SqlClient;
using MiProyectoMVC.Models;
using System.Data;

namespace MiProyectoMVC.Data
{
    public class ClsInstructoresData
    {
        private readonly ClsAccesoDatos _db;

        public ClsInstructoresData(ClsAccesoDatos db)
        {
            _db = db;
        }

        public List<ClsProfesores> ListarInstructores()
        {
            List<ClsProfesores> lista = new List<ClsProfesores>();

            using (SqlConnection conn = _db.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_ListarProfesores", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new ClsProfesores
                    {
                        // cambia los nombres según tus propiedades
                        Tbl_Id_Profesores = int.Parse(reader["Id"].ToString()),
                        Tbl_Nombre = reader["Nombre"].ToString(),
                        Tbl_Apellido = reader["Apellido"].ToString(),
                        Tbl_Emaiil = reader["Email"].ToString(),
                        Tbl_Materia = reader["Materia"].ToString()
                    });
                }
            }

            return lista;
        }
    }
}