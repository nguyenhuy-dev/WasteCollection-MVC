using WasteCollection.Entities.HuyNQ.Models;
using WasteCollection.Repositories.HuyNQ;
using WasteCollection.Repositories.HuyNQ.Models;
using WasteCollection.Services.HuyNQ.Exceptions;

namespace WasteCollection.Services.HuyNQ;

public class CollectorAssignmentsHuyNqService : ICollectorAssignmentsHuyNqService
{
    private readonly CollectorAssignmentsHuyNqRepository _collectorAsmRepository;

    public CollectorAssignmentsHuyNqService() => _collectorAsmRepository ??= new();

    public async Task<int> CreateAsync(CollectorAssignmentsHuyNq asm)
    {
        try
        {
            return await _collectorAsmRepository.CreateAsync(asm);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var asm = await _collectorAsmRepository.GetByIdAsync(id) 
                ?? throw new NotFoundException("Collector Assignment not found.");

            bool isDeleted = await _collectorAsmRepository.RemoveAsync(asm);

            return isDeleted;
        } 
        catch (Exception)
        {
            throw;
        } 
    }

    public async Task<List<CollectorAssignmentsHuyNq>> GetAllAsync()
    {
        try
        {
            return await _collectorAsmRepository.GetAllAsync();
        } 
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CollectorAssignmentsHuyNq> GetByIdAsync(Guid id)
    {
        try
        {
            var asm = await _collectorAsmRepository.GetByIdAsync(id) 
                ?? throw new NotFoundException("Collector Assignment not found.");
            return asm;
        } 
        catch (Exception)
        {
            throw; 
        }
    }

    public async Task<List<CollectorAssignmentsHuyNq>> SearchAsync(CollectorAssignmentsHuyNqSearchOptions options)
    {
        try
        {
            return await _collectorAsmRepository.SearchAsync(options);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> UpdateAsync(CollectorAssignmentsHuyNq asm)
    {
        try
        {
            return await _collectorAsmRepository.UpdateAsync(asm);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
