namespace Desafio_Boticario.Models
{
    public class Produto
    {
        public int Sku { get; set; }
        public string Name { get; set; }
        public Inventario Inventory { get; set; }
        public bool? IsMarketable { get; set; }
    }
}
