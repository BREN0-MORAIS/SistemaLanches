using AppLanchesWeb.Context;
using AppLanchesWeb.Models;
using AppLanchesWeb.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLanchesWeb.Repository
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;
        public LancheRepository(AppDbContext contexto)
        {
            _context = contexto;
        }
        public IEnumerable<Lanche> lanches => _context.Lanches.Include(c=> c.categoria);
        public IEnumerable<Lanche> lanchesPredeiridos => _context.Lanches.Where(p=> p.IsLanchePreferido).Include(c=>c.categoria);
        public Lanche GetById(int lancheId)
        {
           return _context.Lanches.FirstOrDefault(d => lancheId == d.LancheId);
        }
    }
}
