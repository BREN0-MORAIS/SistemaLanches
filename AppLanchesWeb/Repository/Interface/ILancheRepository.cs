using AppLanchesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLanchesWeb.Repository.Interface
{
    public interface ILancheRepository
    {
        IEnumerable<Lanche> lanches { get; }
        IEnumerable<Lanche> lanchesPredeiridos { get; }
        Lanche GetById(int lancheId);
    }
}
