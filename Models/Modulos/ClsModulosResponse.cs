namespace MiProyectoMVC.Models.Modulos
{
    public class ClsModulosResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
    }
}