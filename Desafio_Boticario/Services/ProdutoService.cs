using Desafio_Boticario.Helpers;
using Desafio_Boticario.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Desafio_Boticario.Services
{
    public class ProdutoService : IProdutoService
    {
        public List<Produto> AtualizarProduto(int sku, Produto produto, List<Produto> db)
        {
            try
            {
                DeletarProduto(sku, db);
                produto.Sku = sku;
                return CriarProduto(produto, db);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Produto> CriarProduto(Produto produto, List<Produto> db)
        {
            try
            {
                VerificaObjetoProduto.VerificaCamposObjeto(produto);

                VerificaObjetoProduto.VerificaProdutoExistente(produto.Sku, db);

                db.Add(produto);

                return db;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeletarProduto(int sku, List<Produto> db)
        {
            try
            {
                var produto = GetProduto(sku, db);
                if (produto is null)
                    throw new Exception("Produto não cadastrado");

                db.RemoveAll(p => p.Sku == sku);
            }   
            catch (Exception)
            {
                throw;
            }
        }

        public Produto GetProduto(int sku, List<Produto> db)
        {
            try
            {
                var produto = db.FirstOrDefault(p => p.Sku == sku);
      
                return produto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Produto> RecuperarProduto(int sku, List<Produto> db)
        {
            try
            {
                var produto = GetProduto(sku, db);
                if (produto is null)
                    throw new Exception("Produto não cadastrado");

                produto = SomarWareHouses(produto);

                return AtualizarProduto(sku, produto, db);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Produto SomarWareHouses(Produto produto)
        {
            try
            {
                var quantidade = produto.Inventory.WareHouses.Sum(p => p.Quantity);

                produto.Inventory.Quantity = quantidade;

                produto = IsMarketable(produto);

                return produto;
            }
            catch (Exception)
            {
                throw new Exception("Falha ao somar as quantidades dos WareHouses");
            }
        }

        public Produto IsMarketable(Produto produto)
        {
            try
            {
                produto.IsMarketable = false;

                if (produto.Inventory.Quantity > 0)
                    produto.IsMarketable = true;

                return produto;
            }
            catch (Exception)
            {
                throw new Exception("Falha atribuir IsMarketable ao Produto"); 
            }
        }
    }
}
