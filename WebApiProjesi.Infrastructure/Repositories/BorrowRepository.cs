using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Domain.Entities;
using WebApiProjesi.Domain.Interfaces;
using WebApiProjesi.Infrastructure.Data;

namespace WebApiProjesi.Infrastructure.Repositories
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly ApplicationDbContext _context;

        public BorrowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Borrow>> GetAllAsync(CancellationToken cancellationToken) => await _context.Borrow.ToListAsync();
        public async Task<Borrow?> GetByIdAsync(int id) => await _context.Borrow.FindAsync(id);
        public async Task AddAsync(Borrow borrow)
        {
            await _context.AddAsync(borrow);
        }
        public async Task Update(Borrow borrow)
        {
            _context.Update(borrow);
        }
        public async Task DeleteByIdAsync(int id)
        {
            var borrow = await GetByIdAsync(id);

            if (borrow != null)
            {
                _context.Borrow.Remove(borrow);
            }
        }
        public async Task<IEnumerable<Borrow>> FindAsync(Expression<Func<Borrow, bool>> predicate)
        {
            return await _context.Borrow
                .Where(predicate)
                .ToListAsync();
        }
    }
}
