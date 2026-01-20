using AutoMapper;
using WasteCollection.Entities.HuyNQ.Models;
using WasteCollection.Repositories.HuyNQ;
using WasteCollection.Repositories.HuyNQ.Models;
using WasteCollection.Services.HuyNQ.DTOs;
using WasteCollection.Services.HuyNQ.Exceptions;

namespace WasteCollection.Services.HuyNQ;

public class CollectorAssignmentsHuyNqService : ICollectorAssignmentsHuyNqService
{
    private readonly CollectorAssignmentsHuyNqRepository _collectorAsmRepository;

    private readonly IMapper _mapper;

    public CollectorAssignmentsHuyNqService(IMapper mapper)
    {
        _collectorAsmRepository ??= new();

        _mapper = mapper;
    }
        
    public async Task<int> CreateAsync(CollectorAssignmentsHuyNqCreatedDto request)
    {
        try
        {
            var asm = _mapper.Map<CollectorAssignmentsHuyNq>(request);
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

    public async Task<List<CollectorAssignmentsHuyNqGetAllDto>> GetAllAsync()
    {
        try
        {
            var asms = await _collectorAsmRepository.GetAllAsync();
            var asmDtos = new List<CollectorAssignmentsHuyNqGetAllDto>();

            asms.ForEach(asm =>
            {
                var asmDto = _mapper.Map<CollectorAssignmentsHuyNq, CollectorAssignmentsHuyNqGetAllDto>(asm);
                asmDto.Address = asm.ReportHuyNq.Address;
                asmDtos.Add(asmDto);
            });

            return asmDtos;
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
            var asm = await _collectorAsmRepository.GetById(id) 
                ?? throw new NotFoundException("Collector Assignment not found.");
            return asm;
        } 
        catch (Exception)
        {
            throw; 
        }
    }

    public async Task<List<CollectorAssignmentsHuyNqGetAllDto>> SearchAsync(CollectorAssignmentsHuyNqSearchOptions options)
    {
        try
        {
            var asms = await _collectorAsmRepository.SearchAsync(options);

            var asmDtos = asms.Select(asm =>
            {
                var asmDto = _mapper.Map<CollectorAssignmentsHuyNq, CollectorAssignmentsHuyNqGetAllDto>(asm);
                asmDto.Address = asm.ReportHuyNq.Address;

                return asmDto;
            });

            return [.. asmDtos];
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
