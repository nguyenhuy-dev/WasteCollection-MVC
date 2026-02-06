using WasteCollection.Repositories.HuyNQ.Models;
using WasteCollection.Services.HuyNQ.DTOs;

namespace WasteCollection.MVCWebApp.HuyNQ.ViewModels;

public class CollectorAssignmentsHuyNqPageViewModel
{
    public PagedResult<CollectorAssignmentsHuyNqGetAllDto> PagedResult { get; set; } = new();

    public CollectorAssignmentsHuyNqSearchOptions Option { get; set; } = new("", null, null);

    public PagedRequest PagedRequest { get; set; } = new();
}
