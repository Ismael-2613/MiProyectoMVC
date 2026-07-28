using System;
using System.ComponentModel.DataAnnotations;

namespace MiProyectoMVC.Models
{
    public class ClsMatriculas
    {
        public int Tbl_Id_Matricula { get; set; }
        public String Tbl_Fk_Id_Curso { get; set; }
        public String Tbl_Nombre { get; set; }
        public String Tbl_Email { get; set; }
        public String Tbl_Fecha { get; set; }
        public String Tbl_Status { get; set; }
    }
}