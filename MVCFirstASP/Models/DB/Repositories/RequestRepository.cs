
using Microsoft.EntityFrameworkCore;

namespace MVCFirstASP.Models.DB.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly BlogContext _context;

        public RequestRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task Add(Request request)
        {

            var entry = _context.Entry(request);
            if(entry.State == EntityState.Detached)
            {
                await _context.Requests.AddAsync(request);
            }

            _context.SaveChanges();
        }

        public async Task<Request[]> GetAll()
        {
            return await _context.Requests.ToArrayAsync();
        }
    }
}
