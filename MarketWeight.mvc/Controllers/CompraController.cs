using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketWeight.Core;
using MarketWeight.Core.Persistencia;
using MarketWeight.Ado.Dapper;
using MarketWeight.mvc.ViewModels;

namespace MarketWeight.mvc.Controllers
{
    public class CompraController : Controller
    {
        private readonly IRepoUsuario _repo;
        private readonly IRepoMoneda _repoM;

        public CompraController(IRepoUsuario repo, IRepoMoneda repoM)
        {
            _repo = repo;
            _repoM = repoM;
        }

        [HttpGet]
        public async Task<IActionResult> ComprarMoneda(uint IdUsuario)
        {
            var usuario = await _repo.DetalleAsync(IdUsuario);
            if (usuario is null)
                return NotFound();

            var monedas = await _repoM.ObtenerAsync();

            var vmcomprarmoneda = new VMComprarMoneda(usuario.IdUsuario, monedas);

            return View(vmcomprarmoneda);
        }
    }
}