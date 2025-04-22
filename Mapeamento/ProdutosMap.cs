using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cecAPI.models;
using FluentNHibernate.Mapping;

namespace cecAPI.Mapeamento
{
    public class ProdutosMap : ClassMap<Produto>
    {
        public ProdutosMap()
        {
            Schema("nhibernate");
            Table("produto");
            Id(produto => produto.Id).Column("id");
            Map(produto => produto.Nome).Column("nome");
            Map(produto => produto.Preco).Column("preco");
        }
    }
}