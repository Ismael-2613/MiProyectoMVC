using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiProyectoMVC.Data;
using MiProyectoMVC.Models;

namespace MiProyectoMVC;

public class UsuariosController : Controller
{
    // Declara la variable de conexion
    private readonly ClsAccesoDatos _db;

    // Constructor que inyecta clsAccesoDatos
    public UsuariosController (ClsAccesoDatos db)
    {
        _db = db;
    }


    // Devuelve la vista
    [HttpGet]
    public IActionResult Registrar()
    {
        return View();
    }

    // Recibe los datos 
    [HttpPost]
    public IActionResult Registrar(ClsUsuarios usuario)
    {
        using (SqlConnection conn = _db.ObtenerConexion())
        {
            SqlCommand cmd = new SqlCommand("SP_RegistrarUsuarios", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@User", usuario.Tbl_Username);
            cmd.Parameters.AddWithValue("@Pass", usuario.Tbl_Pass);
            cmd.ExecuteNonQuery();

            ViewBag.Mensaje = "Usuario registrado exitosamente";
            return View();
        }
    }
}
