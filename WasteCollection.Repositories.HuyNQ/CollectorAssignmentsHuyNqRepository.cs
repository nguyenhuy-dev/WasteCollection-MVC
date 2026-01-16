using Microsoft.EntityFrameworkCore;
using WasteCollection.Entities.HuyNQ.Models;
using WasteCollection.Repositories.HuyNQ.Base;
using WasteCollection.Repositories.HuyNQ.DBContext;
using WasteCollection.Repositories.HuyNQ.Models;

namespace WasteCollection.Repositories.HuyNQ;

public class CollectorAssignmentsHuyNqRepository : GenericRepository<CollectorAssignmentsHuyNq>
{
    public CollectorAssignmentsHuyNqRepository() { }

    public CollectorAssignmentsHuyNqRepository(WasteCollectionDbContext context) => _context = context;

    public new async Task<List<CollectorAssignmentsHuyNq>> GetAllAsync()
    {
        var items = await _context.CollectorAssignmentsHuyNqs
            .Include(c => c.ReportHuyNq)
            .ToListAsync();

        return items;
    }

    public new async Task<CollectorAssignmentsHuyNq> GetById(Guid id)
    {
        var item = await _context.CollectorAssignmentsHuyNqs
            .Include(c => c.ReportHuyNq)
            .FirstOrDefaultAsync(c => c.AssignmentId == id);

        return item ?? new();
    }

    public async Task<List<CollectorAssignmentsHuyNq>> SearchAsync(CollectorAssignmentsHuyNqSearchOptions options)
    {
        var status = options.Status ?? string.Empty;
        var collectedWeight = options.CollectedWeight;
        var reportDate = options.ReportDate;

        var items = await _context.CollectorAssignmentsHuyNqs
            .Include(c => c.ReportHuyNq)
            .Where(c =>
                (c.Status.Contains(status) || string.IsNullOrEmpty(status)) &&
                (c.CollectedWeight == collectedWeight || collectedWeight == 0 || collectedWeight == null) &&
                (
                    (
                        reportDate != null &&
                        c.ReportHuyNq.ReportDate != null &&
                        c.ReportHuyNq.ReportDate.Value.Date == reportDate.Value.Date
                    ) ||
                    reportDate == null
                )
            )
            .ToListAsync();

        return items;
    }
}
