namespace MiProyectoMVC.Models.Contenido
{
    public class ClsContenidoResponse
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Tipo { get; set; }
        public string? Url { get; set; }
        public int Status { get; set; }
        public string? Message { get; set; }
    }
}