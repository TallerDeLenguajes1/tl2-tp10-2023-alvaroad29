using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace tl2_tp10_2023_alvaroad29.Models
{
    public interface ITableroRepository
    {
        public void Create(Tablero tablero);
        public void Update(int id, Tablero tablero);
        public Tablero GetById(int id);
        public List<Tablero> GetAll();
        public List<Tablero> GetAllById(int id);
        public void Remove(int id); 
    }
}