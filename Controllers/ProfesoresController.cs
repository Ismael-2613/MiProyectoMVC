using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiProyectoMVC.Data;
using MiProyectoMVC.Models;
using MiProyectoMVC.Models.Modulos;

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
}