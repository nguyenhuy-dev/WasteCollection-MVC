using WasteCollection.Repositories.HuyNQ.Models;
using WasteCollection.Services.HuyNQ.DTOs;

namespace WasteCollection.MVCWebApp.HuyNQ.ViewModels;

public class CollectorAssignmentsHuyNqPageViewModel
{
    public IEnumerable<CollectorAssignmentsHuyNqGetAllDto> CollectorAssignmentsHuyNqGetAllDtos { get; set; } = [];

    public CollectorAssignmentsHuyNqSearchOptions Option { get; set; } = new("", null, null);
}
