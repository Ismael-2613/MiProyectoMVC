using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiProyectoMVC.Data;
using MiProyectoMVC.Models;
using MiProyectoMVC.Models.Contenido;
using MiProyectoMVC.Models.Modulos;


namespace MiProyectoMVC;

public class UsuariosController : Controller
{
    // Declara la variable de conexion
    private readonly ClsAccesoDatos _db;

    // Constructor que inyecta clsAccesoDatos
    public UsuariosController(ClsAccesoDatos db)
    {
        _db = db;
    }


    // Devuelve la vista
    [HttpGet]
    public IActionResult Registrar()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    public IActionResult MenuEstudiantes()
    {
        return View("~/Views/Estudiantes/MenuEstudiante.cshtml");
    }

    [HttpGet]
    public IActionResult MenuProfesores()
    {
        return View("~/Views/Profesores/MenuProfesores.cshtml");

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

    public IActionResult Login(ClsUsuarios usuario)
    {
        using (SqlConnection conn = _db.ObtenerConexion())
        {
            SqlCommand cmd = new SqlCommand("SP_Login", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@User", usuario.Tbl_Username);
            cmd.Parameters.AddWithValue("@Pass", usuario.Tbl_Pass);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string rol = reader["Tbl_Rol"].ToString();
                string email = reader["Tbl_Username"].ToString();

                HttpContext.Session.SetString("Email", email);
                HttpContext.Session.SetString("Rol", rol);

                if (rol == "Profesor")
                    return RedirectToAction("MenuProfesores", "Usuarios");
                else
                    return RedirectToAction("MenuEstudiantes", "Usuarios");
            }
            else
            {
                ViewBag.Mensaje = "Usuario o contraseña incorrectos";
                return View();
            }
        }

        
    }

    [HttpGet]
        public IActionResult VerCurso(int id)
        {
            ClsModulosData modulosData = new ClsModulosData(_db);
            List<ClsModulosResponse> listaModulos = modulosData.ListarModulos(id);
            ViewBag.IdCurso = id;
            return View(listaModulos);
        }

        [HttpGet]
        public IActionResult VerModulo(int id)
        {
            ClsContenidoData contenidoData = new ClsContenidoData(_db);
            List<ClsContenidoResponse> listaContenido = contenidoData.ListarContenido(id);
            ViewBag.IdCurso = id;
            return View(listaContenido);
        }


}
