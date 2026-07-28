using Microsoft.AspNetCore.Mvc;
using MiProyectoMVC.Data;
using MiProyectoMVC.Models;

namespace MiProyectoMVC.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly ClsAccesoDatos _db;

        public EstudiantesController(ClsAccesoDatos db)
        {
            _db = db;
        }

        public IActionResult Cursos()
        {
            string email = HttpContext.Session.GetString("Email");

            ClsCursosData cursosData = new ClsCursosData(_db);
            ClsMatriculaData matriculaData = new ClsMatriculaData(_db);

            List<ClsCursos> listaCursos = cursosData.ListarCursos();

            // Verificar cuáles cursos tiene matriculados
            foreach (var curso in listaCursos)
            {
                ClsMatriculaResponse verificacion = matriculaData.VerificarMatricula(email, curso.Tbl_Id_Curso);
                curso.clsEstaMatriculado = verificacion != null && verificacion.Status == 1;
            }

            return View(listaCursos);
        }




        [HttpPost]
        public IActionResult Matricular(ClsMatriculas matricula)
        {
            ClsMatriculaData matriculaData = new ClsMatriculaData(_db);
            ClsMatriculaResponse resultado = matriculaData.MatricularEstudiante(matricula);

            if (resultado != null && resultado.Status == 1)
            {
                ViewBag.Mensaje = resultado.Message;
                ViewBag.Exito = true;
            }
            else
            {
                ViewBag.Mensaje = "Error al matricularse";
                ViewBag.Exito = false;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Matricular(int id)
        {
            string email = HttpContext.Session.GetString("Email");

            ClsMatriculaData matriculaData = new ClsMatriculaData(_db);
            ClsMatriculaResponse verificacion = matriculaData.VerificarMatricula(email, id);

            ViewBag.IdCurso = id;
            ViewBag.Email = email;

            if (verificacion != null && verificacion.Status == 1)
            {
                ViewBag.YaMatriculado = true;
                ViewBag.Mensaje = "Ya estás matriculado en este curso";
            }
            else
            {
                ViewBag.YaMatriculado = false;
            }

            return View();
        }
    }
}