using System.Collections.Generic;

namespace Desafio_Boticario.Models
{
    public class Inventario
    {
        public int? Quantity { get; set; }
        public List<WareHouse> WareHouses { get; set; }
    }
}
