using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using WasteCollection.Entities.HuyNQ.Models;
using WasteCollection.Services.HuyNQ;

namespace WasteCollection.MVCWebApp.HuyNQ.Controllers;

public class CollectorAssignmentsHuyNqsController : Controller
{
    // private readonly WasteCollectionDbContext _context;
    //public CollectorAssignmentsHuyNqsController(WasteCollectionDbContext context)
    //{
    //    _context = context;
    //}

    private readonly ICollectorAssignmentsHuyNqService _collectorAsmService;

    private readonly ReportsHuyNqService _reportService;

    public CollectorAssignmentsHuyNqsController(ICollectorAssignmentsHuyNqService collectorAsmService, ReportsHuyNqService reportService)
    {
        _collectorAsmService ??= collectorAsmService;
        _reportService ??= reportService;
    } 
    
    // GET: CollectorAssignmentsHuyNqs
    public async Task<IActionResult> Index()
    {
        //var wasteCollectionDbContext = _context.CollectorAssignmentsHuyNqs.Include(c => c.ReportHuyNq);
        //return View(await wasteCollectionDbContext.ToListAsync());

        var asms = await _collectorAsmService.GetAllAsync();

        return View(asms);
    }

    // GET: CollectorAssignmentsHuyNqs/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        //var collectorAssignmentsHuyNq = await _context.CollectorAssignmentsHuyNqs
        //    .Include(c => c.ReportHuyNq)
        //    .FirstOrDefaultAsync(m => m.AssignmentId == id);

        var asm = await _collectorAsmService.GetByIdAsync(id.Value);

        if (asm == null)
            return NotFound();

        return View(asm);
    }

    // GET: CollectorAssignmentsHuyNqs/Create
    public async Task<IActionResult> Create()
    {
        //ViewData["ReportHuyNqid"] = new SelectList(_context.ReportsHuyNqs, "ReportId", "ReportId");

        var reports = await _reportService.GetAllAsync();
        ViewData["ReportHuyNqid"] = new SelectList(reports, "ReportId", "Address");

        return View();
    }
     
    // POST: CollectorAssignmentsHuyNqs/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("AssignmentId,ReportHuyNqid,AssignedDate,Status,ArrivalTime,CompletionTime,CollectedWeight,ProofImageUrl,EstimatedArrivalTime,Notes")] CollectorAssignmentsHuyNq collectorAssignmentsHuyNq)
    {
        if (ModelState.IsValid)
        {
            //collectorAssignmentsHuyNq.AssignmentId = Guid.NewGuid();
            //_context.Add(collectorAssignmentsHuyNq);
            //await _context.SaveChangesAsync();

            var result = await _collectorAsmService.CreateAsync(collectorAssignmentsHuyNq);

            if (result > 0)
                return RedirectToAction(nameof(Index));
        }

        var reports = await _reportService.GetAllAsync();

        ViewData["ReportHuyNqid"] = new SelectList(reports, "ReportId", "Address", collectorAssignmentsHuyNq.ReportHuyNqid);
        return View(collectorAssignmentsHuyNq);
    }

    /*

    // GET: CollectorAssignmentsHuyNqs/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var collectorAssignmentsHuyNq = await _context.CollectorAssignmentsHuyNqs.FindAsync(id);
        if (collectorAssignmentsHuyNq == null)
        {
            return NotFound();
        }
        ViewData["ReportHuyNqid"] = new SelectList(_context.ReportsHuyNqs, "ReportId", "ReportId", collectorAssignmentsHuyNq.ReportHuyNqid);
        return View(collectorAssignmentsHuyNq);
    }

    // POST: CollectorAssignmentsHuyNqs/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("AssignmentId,ReportHuyNqid,AssignedDate,Status,ArrivalTime,CompletionTime,CollectedWeight,ProofImageUrl,EstimatedArrivalTime,Notes")] CollectorAssignmentsHuyNq collectorAssignmentsHuyNq)
    {
        if (id != collectorAssignmentsHuyNq.AssignmentId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(collectorAssignmentsHuyNq);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectorAssignmentsHuyNqExists(collectorAssignmentsHuyNq.AssignmentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["ReportHuyNqid"] = new SelectList(_context.ReportsHuyNqs, "ReportId", "ReportId", collectorAssignmentsHuyNq.ReportHuyNqid);
        return View(collectorAssignmentsHuyNq);
    }

    // GET: CollectorAssignmentsHuyNqs/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var collectorAssignmentsHuyNq = await _context.CollectorAssignmentsHuyNqs
            .Include(c => c.ReportHuyNq)
            .FirstOrDefaultAsync(m => m.AssignmentId == id);
        if (collectorAssignmentsHuyNq == null)
        {
            return NotFound();
        }

        return View(collectorAssignmentsHuyNq);
    }

    // POST: CollectorAssignmentsHuyNqs/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var collectorAssignmentsHuyNq = await _context.CollectorAssignmentsHuyNqs.FindAsync(id);
        if (collectorAssignmentsHuyNq != null)
        {
            _context.CollectorAssignmentsHuyNqs.Remove(collectorAssignmentsHuyNq);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CollectorAssignmentsHuyNqExists(Guid id)
    {
        return _context.CollectorAssignmentsHuyNqs.Any(e => e.AssignmentId == id);
    }
    */
}
