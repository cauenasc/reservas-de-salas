using Microsoft.AspNetCore.Mvc;
using reservas_de_salas.Interfaces;
using reservas_de_salas.Models;

namespace reservas_de_salas.Controllers
{
    public class SalaController : Controller
    {

        private readonly ISalaService _salaService;

        public SalaController(ISalaService salaService)
        {
            _salaService = salaService;
        }

        public async Task<IActionResult> Index()
        {
            var salas = await _salaService.GetAllSalasAsync();
            return View(salas);
        }

        public async Task<IActionResult> Details(long id)
        {
            var sala = await _salaService.GetByIdAsync(id);
            if (sala == null)
            {
                TempData["ErrorMessage"] = "Sala não encontrada.";
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }
        public IActionResult Create()
        {
            return View(new Sala());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Capacidade,Recursos")] Sala sala)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _salaService.SaveSalaAsync(sala);
                    TempData["SuccessMessage"] = "Sala criada com sucesso.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, $"Erro ao criar a sala: {ex.Message}");
                }
            }
            return View(sala);
        }
        public async Task<IActionResult> Edit(long id)
        {
            var sala = await _salaService.GetByIdAsync(id);
            if (sala == null)
            {
                TempData["ErrorMessage"] = "Sala não encontrada para edição.";
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nome,Capacidade,Recursos")] Sala sala)
        {
            if (id != sala.Id)
            {
                TempData["ErrorMessage"] = "ID da sala inválido.";
                return RedirectToAction(nameof(Index));
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _salaService.SaveSalaAsync(sala);
                    TempData["SuccessMessage"] = "Sala atualizada com sucesso.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Erro ao atualizar a sala: {ex.Message}");
                }
            }
            return View(sala);
        }
        public async Task<IActionResult> Delete(long id)
        {
            var sala = await _salaService.GetByIdAsync(id);
            if (sala == null)
            {
                TempData["ErrorMessage"] = "Sala não encontrada para exclusão.";
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                await _salaService.DeleteSalaAsync(id);
                TempData["SuccessMessage"] = "Sala excluída com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao excluir a sala: {ex.Message}";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

