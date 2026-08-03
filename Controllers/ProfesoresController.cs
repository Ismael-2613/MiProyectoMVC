using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiProyectoMVC.Data;
using MiProyectoMVC.Models;
using MiProyectoMVC.Models.Modulos;
using MiProyectoMVC.Models.Contenido;


namespace MiProyectoMVC.Controllers;

public class ProfesoresController : Controller
{
    private readonly ClsAccesoDatos _db;

    public ProfesoresController(ClsAccesoDatos db)
    {
        _db = db;
    }

    public IActionResult Profesores()
    {
        ClsInstructoresData instructoresData = new ClsInstructoresData(_db);
        List<ClsProfesores> listaProfesores = instructoresData.ListarInstructores();
        return View(listaProfesores);
    }
    [HttpGet]
    public IActionResult VerCurso(int id)
    {
        Console.WriteLine("ID recibido: " + id);
        ClsModulosData modulosData = new ClsModulosData(_db);
        List<ClsModulosResponse> listaModulos = modulosData.ListarModulos(id);
        Console.WriteLine("Total en controller: " + listaModulos.Count);
        ViewBag.IdCurso = id;
        ViewBag.Total = listaModulos.Count;
        return View("VerCursos", listaModulos); // ← agrega listaModulos aquí
    }

    [HttpGet]
    public IActionResult AgregarModulo(int idCurso)
    {
        ViewBag.IdCurso = idCurso;
        return View();
    }

    [HttpPost]
    public IActionResult AgregarModulo(ClsModulosRequest modulo)
    {
        ClsModulosData modulosData = new ClsModulosData(_db);
        ClsModulosResponse resultado = modulosData.AgregarModulo(modulo);

        if (resultado != null && resultado.Status == 1)
            return RedirectToAction("VerCurso", new { id = modulo.Tbl_Fk_Id_Curso });
        else
        {
            ViewBag.Error = "Error al agregar módulo";
            return View();
        }
    }

    // Ver contenido de un módulo
    [HttpGet]
    public IActionResult VerModulo(int id, int idCurso)
    {
        ClsContenidoData contenidoData = new ClsContenidoData(_db);
        List<ClsContenidoResponse> listaContenido = contenidoData.ListarContenido(id); // ← id es el módulo
        ViewBag.IdModulo = id;
        ViewBag.IdCurso = idCurso;
        return View(listaContenido);
    }

    [HttpGet]
    public IActionResult AgregarContenido(int idModulo, int idCurso)
    {
        ViewBag.IdModulo = idModulo;
        ViewBag.IdCurso = idCurso;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AgregarContenido(ClsContenidoRequest contenido, IFormFile archivo)
    {
        Console.WriteLine("IdModulo recibido: " + contenido.Tbl_Fk_Id_Modulo);

        if (archivo == null || archivo.Length == 0)
        {
            ViewBag.Error = "Debes seleccionar un archivo";
            ViewBag.IdModulo = contenido.Tbl_Fk_Id_Modulo;
            return View();
        }

        string carpeta = contenido.Tbl_Tipo == "Video" ? "videos" : "documentos";
        string rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", carpeta);

        if (!Directory.Exists(rutaCarpeta))
            Directory.CreateDirectory(rutaCarpeta);

        string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName);
        string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        {
            await archivo.CopyToAsync(stream);
        }

        contenido.Tbl_Url = $"/uploads/{carpeta}/{nombreArchivo}";

        ClsContenidoData contenidoData = new ClsContenidoData(_db);
        ClsContenidoResponse resultado = contenidoData.AgregarContenido(contenido);

        if (resultado != null && resultado.Status == 1)
            return RedirectToAction("VerModulo", new { id = contenido.Tbl_Fk_Id_Modulo, idCurso = Request.Form["idCurso"] });
        else
        {
            ViewBag.Error = resultado?.Message ?? "Error al agregar contenido";
            ViewBag.IdModulo = contenido.Tbl_Fk_Id_Modulo;
            return View();
        }
    }
}