using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tl2_tp10_2023_alvaroad29.Models
{
    public interface ITareaRepository
    {
        public void Create(int idTablero, Tarea tarea);
        public void Update(int id, Tarea tarea);
        public Tarea GetById(int id);
        public List<Tarea> GetAllByIdUsuario(int id);
        public List<Tarea> GetAllByIdTablero(int id);
        public void Remove(int id); 
        public void AsignarTarea(int idUsuario, int idTarea);

        public List<Tarea> GetAllByEstado(EstadoTarea estado);
    }
}