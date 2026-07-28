using Microsoft.Data.SqlClient;
using MiProyectoMVC.Models;
using System.Data;

namespace MiProyectoMVC.Data
{
    public class ClsMatriculaData
    {
        private readonly ClsAccesoDatos _db;

        public ClsMatriculaData(ClsAccesoDatos db)
        {
            _db = db;
        }

        public ClsMatriculaResponse MatricularEstudiante(ClsMatriculas matricula)
        {
            using (SqlConnection conn = _db.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_MatricularEstudiante", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Fk_Id_Curso", matricula.Tbl_Fk_Id_Curso);
                cmd.Parameters.AddWithValue("@Nombre", matricula.Tbl_Nombre);
                cmd.Parameters.AddWithValue("@Email", matricula.Tbl_Email);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                    return new ClsMatriculaResponse
                    {
                        Status = int.Parse(reader["status"].ToString()),
                        Message = reader["message"].ToString()
                    };

                return null;
            }
        }

        public ClsMatriculaResponse VerificarMatricula(string email, int idCurso)
        {
            using (SqlConnection conn = _db.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_VerificarMatricula", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Fk_Id_Curso", idCurso);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                    return new ClsMatriculaResponse
                    {
                        Status = int.Parse(reader["status"].ToString()),
                        Message = reader["message"].ToString()
                    };

                return null;
            }
        }
    }
}