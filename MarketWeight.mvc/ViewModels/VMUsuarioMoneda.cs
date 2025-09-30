using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MarketWeight.Core;

namespace MarketWeight.mvc.ViewModels
{
    public class VMUsuarioMoneda
    {
        public uint IdMoneda { get; set; }
        public string Nombre { get; set; }
        public string? Url { get; set; }
        public decimal Cantidad { get; set; }
    }
}
