using WasteCollection.Entities.HuyNQ.Models;

namespace WasteCollection.Services.HuyNQ.DTOs;

public class CollectorAssignmentsHuyNqGetAllDto
{
    public DateTime? AssignedDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime? ArrivalTime { get; set; }

    public DateTime? CompletionTime { get; set; }

    public decimal? CollectedWeight { get; set; }

    public string ProofImageUrl { get; set; } = string.Empty;

    public DateTime? EstimatedArrivalTime { get; set; }

    public string Notes { get; set; }

    public virtual ReportsHuyNq ReportHuyNq { get; set; }
}
