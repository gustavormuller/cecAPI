using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cecAPI.Dto;
using cecAPI.models;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using ISession = NHibernate.ISession;

namespace cecAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ISession session;

        public ProdutosController(IMapper mapper, ISession session)
        {
            this.mapper = mapper;
            this.session = session;
        }

        [HttpGet]
        public IList<Produto> RecuperaProdutos()
        {
            IQueryable<Produto> query = session.Query<Produto>();
            IList<Produto> produtos = query.ToList();

            return produtos;
        }

        [HttpPost]
        public void InserirProdutos(ProdutoInserirRequest produtoRequest)
        {
            Produto produto = mapper.Map<Produto>(produtoRequest);

            ITransaction transaction = session.BeginTransaction();
            try
            {
                session.Save(produto);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }

        [HttpPut("{id}")]
        public void AlterarProduto(int id, [FromQuery] ProdutoInserirRequest produtoRequest)
        {
            Produto produto = session.Get<Produto>(id);

            ITransaction transaction = session.BeginTransaction();

            try
            {
                produto.Nome = produtoRequest.Nome;
                produto.Preco = produtoRequest.Preco;

                session.Update(produto);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }

        [HttpDelete("{id}")]
        public void DeletarProduto(int id)
        {
            Produto produto = session.Get<Produto>(id);

            ITransaction transaction = session.BeginTransaction();

            try
            {
                session.Delete(produto);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }

        [HttpGet("{id}")]
        public Produto RecuperarProduto(int id)
        {
            Produto produto = session.Get<Produto>(id);
            return produto;
        }
    }
}