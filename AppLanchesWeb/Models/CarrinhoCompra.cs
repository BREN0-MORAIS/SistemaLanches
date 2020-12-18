using AppLanchesWeb.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLanchesWeb.Models
{
    public class CarrinhoCompra
    {
        private static AppDbContext _contexto { get; set; }
        public CarrinhoCompra(AppDbContext contexto)
        {
            _contexto = contexto;
        }
        public string CarrinhoCompraId { get; set; }
        List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

        //obeter da sessão um carrinho de compra que o usuario ta coletando
        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //definindo um sessão
            ISession session
                //acessa o contexto atual, se  condição não for nulo retorna uma sessão
                = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            //retorna o CarrinhoId se existir se não existir ira Gerar um numero aleatorio pelo Guid
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            //seta na seção 
            session.SetString("CarrinhoId", carrinhoId);

            //retorna o Id do contexto atual
            return new CarrinhoCompra(_contexto)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionarCarrinho(Lanche lanche, int quantidade)
        {
            var carrinhoCompraItem = _contexto.CarrinhoCompraItems.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);

            //verifica se o carrinho existe e se não existire cria um
            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };

                _contexto.CarrinhoCompraItems.Add(carrinhoCompraItem);
            }
            else//se existir o carrinho com o item então incrementa a quantidade
            {
                carrinhoCompraItem.Quantidade++;
            }
            _contexto.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche, int quantidade)
        {
            var carrinhoCompraItem = _contexto.CarrinhoCompraItems.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;
            if (carrinhoCompraItem != null)
            {
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _contexto.CarrinhoCompraItems.Remove(carrinhoCompraItem);
                }
            }
            _contexto.SaveChanges();

            return quantidadeLocal;
        }
    }
}
