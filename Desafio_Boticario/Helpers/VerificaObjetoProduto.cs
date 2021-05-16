using Desafio_Boticario.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Desafio_Boticario.Helpers
{
    public class VerificaObjetoProduto
    {
        public static void VerificaCamposObjeto(Produto produto)
        {
            try
            {
                if (string.IsNullOrEmpty(produto.Sku.ToString()))
                {
                    throw new Exception("Campo SKU vazio");
                }
                if (string.IsNullOrEmpty(produto.Name))
                {
                    throw new Exception("Campo Name vazio");
                }
                if (produto.Inventory is null)
                {
                    throw new Exception("Objeto Inventory vazio");
                }
                if(produto.Inventory.WareHouses is null)
                {
                    throw new Exception("Objeto WareHouses vazio");
                }
                if (produto.Inventory.WareHouses.Any(p => string.IsNullOrEmpty(p.Locality.ToString())))
                {
                    throw new Exception("Objeto WareHouses com Campo Locality vazio");
                }
                if (produto.Inventory.WareHouses.Any(p => string.IsNullOrEmpty(p.Quantity.ToString())))
                {
                    throw new Exception("Objeto WareHouses com Campo Quantity vazio");
                }
                if (produto.Inventory.WareHouses.Any(p => string.IsNullOrEmpty(p.Type)))
                {
                    throw new Exception("Objeto WareHouses com Campo Type vazio");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void VerificaProdutoExistente(int sku, List<Produto> db)
        {
            try
            {
                bool skuExistente = db.Any(p => p.Sku == sku);

                if (skuExistente)
                {
                    throw new Exception("Dois produtos são considerados iguais se os seus skus forem iguais");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
