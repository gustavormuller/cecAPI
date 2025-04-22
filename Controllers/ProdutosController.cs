using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cecAPI.Dto;
using cecAPI.models;
using Microsoft.AspNetCore.Mvc;

namespace cecAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IMapper mapper;
        public static IList<Produto> produtos { get; set; } = [];
        public static int id { get; set; } = 0;

        public ProdutosController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet]
        public IList<Produto> RecuperaProdutos()
        {
            return produtos;
        }

        [HttpPost]
        public void InserirProdutos(ProdutoInserirRequest produtoRequest)
        {
            Produto produto = mapper.Map<Produto>(produtoRequest);
            produto.Id = id;
            id++;
            produtos.Add(produto);
        }

        [HttpPut("{id}")]
        public void AlterarProduto(int id, [FromQuery] ProdutoInserirRequest produtoRequest)
        {
            for (int i = 0; i < produtos.Count; i++)
            {
                if (produtos[i].Id == id)
                {
                    produtos[i].Nome = produtoRequest.Nome;
                    produtos[i].Preco = produtoRequest.Preco;
                }
            }
        }

        [HttpDelete("{id}")]
        public void DeletarProduto(int id)
        {
            for (int i = 0; i < produtos.Count; i++)
            {
                if (produtos[i].Id == id)
                {
                    produtos.Remove(produtos[i]);
                }
            }
        }
    }
}