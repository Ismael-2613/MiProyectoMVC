namespace MiProyectoMVC.Models.Contenido
{
    public class ClsContenidoRequest
    {
        public int Tbl_Fk_Id_Modulo { get; set; }
        public string? Tbl_Titulo { get; set; }
        public string? Tbl_Tipo { get; set; }
        public string? Tbl_Url { get; set; }
    }
}