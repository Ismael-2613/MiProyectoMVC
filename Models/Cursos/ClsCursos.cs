namespace MiProyectoMVC.Models
{

    public class ClsCursos
    {
     public int Tbl_Id_Curso { get; set; }
     public String Tbl_Nombre { get; set; }
     public String Tbl_Descripcion { get; set; }
     public String Tbl_Duracion { get; set; }
     public String Tbl_Nivel { get; set; }
     public bool Tbl_Status { get; set; }      
     public bool clsEstaMatriculado { get; set; } // Propiedad para indicar si el estudiante está matriculado en el curso
    }

}