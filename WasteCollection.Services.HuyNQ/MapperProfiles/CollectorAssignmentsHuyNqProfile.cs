using AutoMapper;
using WasteCollection.Entities.HuyNQ.Models;
using WasteCollection.Services.HuyNQ.DTOs;

namespace WasteCollection.Services.HuyNQ.MapperProfiles;

public class CollectorAssignmentsHuyNqProfile : Profile
{
    public CollectorAssignmentsHuyNqProfile()
    {
        CreateMap<CollectorAssignmentsHuyNqCreatedDto, CollectorAssignmentsHuyNq>(MemberList.None);

        CreateMap<CollectorAssignmentsHuyNq, CollectorAssignmentsHuyNqGetAllDto>(MemberList.None);
    }
}
