using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketWeight.Core;
using MarketWeight.Core.Persistencia;
using MarketWeight.Ado.Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MarketWeight.mvc.ViewModels
{
    public class VMComprarMoneda
    {
        public uint IdMoneda { get; set; }

        public SelectList? ListaMoneda { get; set; }
        private readonly IRepoUsuario _repo;
        public VMComprarMoneda(uint IdUsuario, IEnumerable<Moneda> monedas)
        {
            ListaMoneda = new SelectList(monedas,
                                    dataTextField: nameof(Moneda.Nombre),
                                    dataValueField: nameof(Moneda.IdMoneda));
        }
    }
}