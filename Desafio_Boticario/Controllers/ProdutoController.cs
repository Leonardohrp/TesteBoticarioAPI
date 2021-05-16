using Desafio_Boticario.Models;
using Desafio_Boticario.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Desafio_Boticario.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        Db db = new Db();
        private readonly IProdutoService _serv;

        public ProdutoController(IProdutoService serv)
        {
            _serv = serv;
        }

        //Seleciona Produto por SKU
        [HttpGet("{sku}")]
        public IActionResult GetBySku(int sku)
        {
            try
            {
                var produto = _serv.GetProduto(sku, Db.db);
                if (produto is null)
                    return new JsonResult(new { Success = false, Message = "Produto não cadastrado" });

                return new JsonResult(new { Success = true, Data = produto });
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(new { Success = false, Message = ex.Message }));
            }
        }

        //Criação de Produto
        [HttpPost]
        public IActionResult PostBySku(Produto model)
        {
            try
            {
                Db.db = _serv.CriarProduto(model, Db.db);
                return new JsonResult(Ok(new { Success = true, Message = "Produto Cadastrado com Sucesso", Data = model }));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(new { Success = false, Message = ex.Message }));
            }
        }

        //Atualização de Produto
        [HttpPut("{sku}")]
        public IActionResult PutBySku(int sku, Produto produto)
        {
            try
            {
                Db.db = _serv.AtualizarProduto(sku, produto, Db.db);

                return new JsonResult(Ok(new { Success = true, Message = "Produto Atualizado com Sucesso", Data = produto }));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(new { Success = false, Message = ex.Message }));
            }
        }

        //Deleção de Produto
        [HttpDelete("{sku}")]
        public IActionResult DeleteBySku(int sku)
        {
            try
            {
                _serv.DeletarProduto(sku, Db.db);
                return new JsonResult(Ok(new { Success = true, Message = "Produto Deletado com Sucesso" }));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(new { Success = false, Message = ex.Message }));
            }
        }

        //Recuperação de Produto
        [HttpPost("{sku}")]
        public IActionResult RecoverBySku(int sku)
        {
            try
            {
                Db.db = _serv.RecuperarProduto(sku, Db.db);
                return new JsonResult(Ok(new { Success = true, Message = "Produto Recuperado com Sucesso" }));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(new { Success = false, Message = ex.Message }));
            }
        }
    }
}
