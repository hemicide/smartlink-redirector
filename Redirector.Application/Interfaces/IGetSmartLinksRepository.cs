using Redirector.Domain.Entities;

namespace Redirector.Application.Interfaces
{
    public interface IGetSmartLinksRepository
    {
        public Task<Smartlinks?> GetSmartLink();
    }
}
