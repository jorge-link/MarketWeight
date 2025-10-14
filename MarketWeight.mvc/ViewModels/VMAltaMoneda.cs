using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketWeight.mvc.ViewModels
{
    public class VMAltaMoneda
    {
        public uint IdMoneda { get; set; }
        public required decimal Precio { get; set; }
        public required decimal Cantidad { get; set; }
        public required string Nombre { get; set; }
        public string? Url { get; set; } 
    }
}
