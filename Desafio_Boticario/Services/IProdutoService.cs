using Desafio_Boticario.Models;
using System.Collections.Generic;

namespace Desafio_Boticario.Services
{
    public interface IProdutoService
    {
        Produto GetProduto(int sku, List<Produto> db);
        List<Produto> CriarProduto(Produto produto, List<Produto> db);
        List<Produto> AtualizarProduto(int sku, Produto produto, List<Produto> db);
        void DeletarProduto(int sku, List<Produto> db);
        List<Produto> RecuperarProduto(int sku, List<Produto> db);
    }
}
