using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppLanchesWeb.Models
{
    public class CarrinhoCompraItem
    {
        [Key]
        public int CarrinhoComprItemId { get; set; }
        public Lanche Lanche { get; set; }
        public int Quantidade { get; set; }
        public string  CarrinhoCompraId { get; set; }

    }
}
