using Microsoft.Data.SqlClient;
using MiProyectoMVC.Models.Contenido;
using System.Data;

namespace MiProyectoMVC.Data
{
    public class ClsContenidoData
    {
        private readonly ClsAccesoDatos _db;

        public ClsContenidoData(ClsAccesoDatos db)
        {
            _db = db;
        }

        public List<ClsContenidoResponse> ListarContenido(int idCurso)
        {
            List<ClsContenidoResponse> lista = new List<ClsContenidoResponse>();

            using (SqlConnection conn = _db.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_ListarContenido", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fk_Id_Curso", idCurso);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new ClsContenidoResponse
                    {
                        Id = int.Parse(reader["Id"].ToString()!),
                        Titulo = reader["Titulo"].ToString(),
                        Tipo = reader["Tipo"].ToString(),
                        Url = reader["Url"].ToString()
                    });
                }
            }

            return lista;
        }

        public ClsContenidoResponse AgregarContenido(ClsContenidoRequest contenido)
        {
            using (SqlConnection conn = _db.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_AgregarContenido", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Fk_Id_Curso", contenido.Tbl_Fk_Id_Curso);
                cmd.Parameters.AddWithValue("@Titulo", contenido.Tbl_Titulo);
                cmd.Parameters.AddWithValue("@Tipo", contenido.Tbl_Tipo);
                cmd.Parameters.AddWithValue("@Url", contenido.Tbl_Url);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                    return new ClsContenidoResponse
                    {
                        Status = int.Parse(reader["status"].ToString()),
                        Message = reader["message"].ToString()
                    };

                return null;
            }
        }
    }
}