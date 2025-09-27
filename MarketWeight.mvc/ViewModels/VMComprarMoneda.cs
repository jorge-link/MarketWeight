using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MarketWeight.Core;

namespace MarketWeight.mvc.ViewModels
{
    public class VMComprarMoneda
    {
        public uint IdMoneda { get; set; }

        public SelectList ListaMoneda { get; set; }

        public IEnumerable<Moneda> Monedas { get; set; }

        public string? ImagenSeleccionada { get; set; }

        public VMComprarMoneda(IEnumerable<Moneda> monedas)
        {
            Monedas = monedas;
            ListaMoneda = new SelectList(monedas,
                                        dataTextField: nameof(Moneda.Nombre),
                                        dataValueField: nameof(Moneda.IdMoneda));
        }
    }
}
