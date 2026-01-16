using WasteCollection.Entities.HuyNQ.Models;
using WasteCollection.Repositories.HuyNQ.Models;

namespace WasteCollection.Services.HuyNQ;

public interface ICollectorAssignmentsHuyNqService
{
    Task<List<CollectorAssignmentsHuyNq>> GetAllAsync();

    Task<CollectorAssignmentsHuyNq> GetByIdAsync(Guid id);

    Task<List<CollectorAssignmentsHuyNq>> SearchAsync(CollectorAssignmentsHuyNqSearchOptions options);

    Task<int> CreateAsync(CollectorAssignmentsHuyNq asm);

    Task<int> UpdateAsync(CollectorAssignmentsHuyNq asm);

    Task<bool> DeleteAsync(Guid id);
}
