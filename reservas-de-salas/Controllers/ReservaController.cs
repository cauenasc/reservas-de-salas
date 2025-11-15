using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using reservas_de_salas.Models;
using reservas_de_salas.Services;

namespace reservas_de_salas.Controllers
{
    public class ReservaController : Controller
    {
      private readonly ReservasFacade _reservasFacade;

        public ReservaController(ReservasFacade reservasFacade)
        {
            _reservasFacade = reservasFacade;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Indicadores = await _reservasFacade.GetIndicadoresAsync();
            return View(await _reservasFacade.ListarReservasAsync());
        }

        public async Task<IActionResult> Create()
        {

         ViewBag.Usuarios = new SelectList(await _reservasFacade.ListarUsuariosAsync(), "Id", "Email");
            ViewBag.Salas = new SelectList(await _reservasFacade.ListarSalasAsync(), "Id", "Nome");
            var r = new Reserva {
                Data = System.DateTime.Today,
                HoraInicio = System.TimeSpan.FromHours(8),
                HoraFim = System.TimeSpan.FromHours(9)
            };
            return View(r);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Usuarios = new SelectList(await _reservasFacade.ListarUsuariosAsync(), "Id", "Email", reserva.UsuarioId);
                ViewBag.Salas = new SelectList(await _reservasFacade.ListarSalasAsync(), "Id", "Nome", reserva.SalaId);
                return View(reserva);

            }
            var msg = await _reservasFacade.ReservaAsync(reserva);
            TempData[msg.Contains("sucesso") ? "SuccessMessage" : "ErrorMessage"] = msg;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
          var r = await _reservasFacade.GetByIdASync(id);
           
            ViewBag.Usuarios = new SelectList(await _reservasFacade.ListarUsuariosAsync(), "Id", "Email", r.UsuarioId);
            ViewBag.Salas = new SelectList(await _reservasFacade.ListarSalasAsync(), "Id", "Nome", r.SalaId);
            return View(r);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Usuarios = new SelectList(await _reservasFacade.ListarUsuariosAsync(), "Id", "Email", reserva.UsuarioId);
                ViewBag.Salas = new SelectList(await _reservasFacade.ListarSalasAsync(), "Id", "Nome", reserva.SalaId);
                return View(reserva);

            }
            var msg = await _reservasFacade.ReservaAsync(reserva);
            TempData[msg.Contains("sucesso") ? "SuccessMessage" : "ErrorMessage"] = msg;
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(long id)
        {
            var reserva = await _reservasFacade.GetByIdASync(id);
            return View(reserva);
        }
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _reservasFacade.DeleteAsync(id);
            TempData["SuccessMessage"] = "Reserva excluída com sucesso.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(long id)
        {
            var r = await _reservasFacade.GetByIdASync(id);
            return View(r);
        }

    }
}
