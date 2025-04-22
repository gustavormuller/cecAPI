using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cecAPI.models;
using Microsoft.AspNetCore.Mvc;

namespace cecAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        public static IList<Produto> produtos { get; set; } = [];

        [HttpGet]
        public IList<Produto> RecuperaProdutos()
        {
            return produtos;
        }

        [HttpPost]
        public void InserirProdutos(Produto produto)
        {
            produtos.Add(produto);
        }
    }
}