using Desafio_Boticario.Models;
using Desafio_Boticario.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace TesteAPI
{
    public class UnitTestProduto
    {
        Db db = new Db();

        [Test, Order(1)]
        public void CriarProduto()
        {
            //arrange
            var produto = new Produto()
            {
                Sku = 1,
                Name = "Teste1",
                Inventory = new Inventario()
                {
                    Quantity = null,
                    WareHouses = new List<WareHouse>()
                    {
                        new WareHouse()
                        {
                            Locality = "Sorocaba",
                            Quantity = 6,
                            Type = "ECOMMERCE"
                        },
                        new WareHouse()
                        {
                            Locality = "São Paulo",
                            Quantity = 2,
                            Type = "PHYSICAL_STORE"
                        }
                    }
                },
                IsMarketable = null
            };

            //act
            ProdutoService servico = new ProdutoService();
            servico.CriarProduto(produto, Db.db);

            var produtoCriado = servico.GetProduto(produto.Sku, Db.db);

            //assert
            if (produtoCriado is null)
            {
                Assert.Fail();
            }

            Assert.AreEqual(produto, produtoCriado);
        }

        [Test, Order(2)]
        public void AtualizarProduto()
        {
            //arrange
            var sku = 1;

            var produto = new Produto()
            {
                Sku = 1,
                Name = "Teste2",
                Inventory = new Inventario()
                {
                    Quantity = null,
                    WareHouses = new List<WareHouse>()
                    {
                        new WareHouse()
                        {
                            Locality = "Outra cidade",
                            Quantity = 20,
                            Type = "ECOMMERCE"
                        },
                        new WareHouse()
                        {
                            Locality = "São Paulo",
                            Quantity = 15,
                            Type = "PHYSICAL_STORE"
                        }
                    }
                },
                IsMarketable = null
            };

            //act
            ProdutoService servico = new ProdutoService();
            servico.AtualizarProduto(sku, produto, Db.db);
            var produtoAtualizado = servico.GetProduto(sku, Db.db);

            //assert
            if(produtoAtualizado is null)
            {
                Assert.Fail();
            }

            Assert.AreEqual(produto, produtoAtualizado);
        }

        [Test, Order(3)]
        public void ExcluirProduto()
        {
            //arrange
            var sku = 1;

            //act
            ProdutoService servico = new ProdutoService();
            servico.DeletarProduto(sku, Db.db);
            var produto = servico.GetProduto(sku, Db.db);

            //assert
            if (produto is null)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test, Order(4)]
        public void RecuperarProduto()
        {
            //arrange
            CriarProduto();
            var sku = 1;
            var quantidadeEsperada = 8;

            //act
            ProdutoService servico = new ProdutoService();
            Db.db = servico.RecuperarProduto(sku, Db.db);
            var produtoRecuperado = servico.GetProduto(sku, Db.db);

            //assert
            if (produtoRecuperado is null)
            {
                Assert.Fail();
            }

            if (produtoRecuperado.IsMarketable == true)
            {
                Db.db.Clear();
                Assert.AreEqual(quantidadeEsperada, produtoRecuperado.Inventory.Quantity);
            }
            else
            {
                Db.db.Clear();
                Assert.Fail();
            }
        }
    }
}