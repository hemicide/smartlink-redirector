using Microsoft.EntityFrameworkCore;
using Redirector.Persistence.Contexts;
using Redirector.Application.Interfaces;
using Redirector.Domain.Entities;

namespace Redirector.Persistence.Repositories
{
    public class GetSmartLinksRepository : IGetSmartLinksRepository
    {
        private readonly ModelContext _context;
        private readonly ISmartLink _smartLink;

        public GetSmartLinksRepository(ModelContext context, ISmartLink smartLink)
        {
            _context = context;
            _smartLink = smartLink;
        }

        public async Task<Smartlinks?> GetSmartLink()
        {
            return await _context.Smartlinks
                .Where(s => s.Link == _smartLink.GetLink())
                .FirstOrDefaultAsync();
        }
    }
}
