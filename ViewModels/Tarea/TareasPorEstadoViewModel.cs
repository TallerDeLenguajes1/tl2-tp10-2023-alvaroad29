using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class TareasPorEstadoViewModel
    {
        public string Estado { get; set; }
        public List<TareaViewModel> TareasPorEstado { get; set; }
    }
}