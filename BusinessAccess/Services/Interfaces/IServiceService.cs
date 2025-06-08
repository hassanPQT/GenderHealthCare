using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(Guid id);
        Task<Service> CreateAsync(Service dto);
        Task<Service?> UpdateAsync(Guid id, Service dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
