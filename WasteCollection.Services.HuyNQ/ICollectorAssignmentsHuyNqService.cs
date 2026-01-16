using WasteCollection.Entities.HuyNQ.Models;
using WasteCollection.Repositories.HuyNQ.Models;
using WasteCollection.Services.HuyNQ.DTOs;

namespace WasteCollection.Services.HuyNQ;

public interface ICollectorAssignmentsHuyNqService
{
    Task<List<CollectorAssignmentsHuyNq>> GetAllAsync();

    Task<CollectorAssignmentsHuyNq> GetByIdAsync(Guid id);

    Task<List<CollectorAssignmentsHuyNq>> SearchAsync(CollectorAssignmentsHuyNqSearchOptions options);

    Task<int> CreateAsync(CollectorAssignmentsHuyNqCreatedDto request);

    Task<int> UpdateAsync(CollectorAssignmentsHuyNq asm);

    Task<bool> DeleteAsync(Guid id);
}
