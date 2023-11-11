using FrontMVC.Interfaces;
using FrontMVC.Models.Cliente;
using FrontMVC.Models.Log;
using FrontMVC.Models.Prato;
using FrontMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FrontMVC.Controllers
{
    [Authorize]
    public class LogController : Controller
    {
        private readonly IServiceLog<LogModel> _logService;

        public LogController(IServiceLog<LogModel> logService)
        {
            _logService = logService;
        }

        public async Task<IActionResult> Index(string? like, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var list = await _logService.Listar();

            var pagedList = list.ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }


        public async Task<IActionResult> Detalhes(Guid id)
        {
            var Log = await _logService.FiltrarId(id);
            return View(Log);
        }
    }
}
