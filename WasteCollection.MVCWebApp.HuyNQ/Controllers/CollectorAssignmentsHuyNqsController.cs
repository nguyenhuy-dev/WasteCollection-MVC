using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WasteCollection.Entities.HuyNQ.Models;
using WasteCollection.MVCWebApp.HuyNQ.ViewModels;
using WasteCollection.Repositories.HuyNQ.Models;
using WasteCollection.Services.HuyNQ;
using WasteCollection.Services.HuyNQ.DTOs;

namespace WasteCollection.MVCWebApp.HuyNQ.Controllers;

[Authorize]
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

    /*
    public async Task<IActionResult> Index()
    {
        //var wasteCollectionDbContext = _context.CollectorAssignmentsHuyNqs.Include(c => c.ReportHuyNq);
        //return View(await wasteCollectionDbContext.ToListAsync());

        var asms = await _collectorAsmService.GetAllAsync();

        return View(asms);
    }
    */

    public async Task<IActionResult> Index(CollectorAssignmentsHuyNqSearchOptions option, PagedRequest pagedRequest)
    {
        //var wasteCollectionDbContext = _context.CollectorAssignmentsHuyNqs.Include(c => c.ReportHuyNq);
        //return View(await wasteCollectionDbContext.ToListAsync());

        var asms = await _collectorAsmService.SearchAsync(option);

        var paged = await _collectorAsmService.PaginateAsync(asms, pagedRequest);

        var vm = new CollectorAssignmentsHuyNqPageViewModel
        {
            PagedResult = paged,
            Option = option,
            PagedRequest = pagedRequest
        };

        return View(vm);
    }

    // GET: CollectorAssignmentsHuyNqs/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
            return NotFound();

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
    public async Task<IActionResult> Create([Bind("ReportHuyNqid,Status,ArrivalTime,CompletionTime,CollectedWeight,ProofImageUrl,EstimatedArrivalTime,Notes")] CollectorAssignmentsHuyNqCreatedDto request)
    {
        if (ModelState.IsValid)
        {
            //collectorAssignmentsHuyNq.AssignmentId = Guid.NewGuid();
            //_context.Add(collectorAssignmentsHuyNq);
            //await _context.SaveChangesAsync();

            var result = await _collectorAsmService.CreateAsync(request);

            if (result > 0)
                return RedirectToAction(nameof(Index));
        }

        var reports = await _reportService.GetAllAsync();

        ViewData["ReportHuyNqid"] = new SelectList(reports, "ReportId", "Address", request.ReportHuyNqid);
        return View(request);
    }

    // GET: CollectorAssignmentsHuyNqs/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
            return NotFound();

        //var collectorAssignmentsHuyNq = await _context.CollectorAssignmentsHuyNqs.FindAsync(id);

        var collectorAssignmentsHuyNq = await _collectorAsmService.GetByIdAsync(id.Value);

        if (collectorAssignmentsHuyNq == null)
            return NotFound();

        var reports = await _reportService.GetAllAsync();

        ViewData["ReportHuyNqid"] = new SelectList(reports, "ReportId", "Address", collectorAssignmentsHuyNq.ReportHuyNqid);
        return View(collectorAssignmentsHuyNq);
    }

    // POST: CollectorAssignmentsHuyNqs/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CollectorAssignmentsHuyNq collectorAssignmentsHuyNq)
    {
        //if (id != collectorAssignmentsHuyNq.AssignmentId)
        //{
        //    return NotFound();
        //}

        if (ModelState.IsValid)
        {
            try
            {
                //_context.Update(collectorAssignmentsHuyNq);
                //await _context.SaveChangesAsync();

                var result = await _collectorAsmService.UpdateAsync(collectorAssignmentsHuyNq);

                if (result > 0)
                    return RedirectToAction(nameof(Index));
            }
            //catch (DbUpdateConcurrencyException)
            catch (Exception)
            {
                //if (!CollectorAssignmentsHuyNqExists(collectorAssignmentsHuyNq.AssignmentId))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}

                throw;
            }
            //return RedirectToAction(nameof(Index));
        }

        //ViewData["ReportHuyNqid"] = new SelectList(_context.ReportsHuyNqs, "ReportId", "ReportId", collectorAssignmentsHuyNq.ReportHuyNqid);

        var reports = await _reportService.GetAllAsync();
        ViewData["ReportHuyNqid"] = new SelectList(reports, "ReportId", "Address", collectorAssignmentsHuyNq.ReportHuyNqid);

        return View(collectorAssignmentsHuyNq);
    }


    // GET: CollectorAssignmentsHuyNqs/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
            return NotFound();

        //var collectorAssignmentsHuyNq = await _context.CollectorAssignmentsHuyNqs
        //    .Include(c => c.ReportHuyNq)
        //    .FirstOrDefaultAsync(m => m.AssignmentId == id);

        var collectorAssignmentsHuyNq = await _collectorAsmService.GetByIdAsync(id.Value);

        if (collectorAssignmentsHuyNq == null)
            return NotFound();

        return View(collectorAssignmentsHuyNq);
    }

    // POST: CollectorAssignmentsHuyNqs/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        //var collectorAssignmentsHuyNq = await _context.CollectorAssignmentsHuyNqs.FindAsync(id);
        //if (collectorAssignmentsHuyNq != null)
        //{
        //    _context.CollectorAssignmentsHuyNqs.Remove(collectorAssignmentsHuyNq);
        //}

        //await _context.SaveChangesAsync();

        var result = await _collectorAsmService.DeleteAsync(id);
        
        if (result)
            return RedirectToAction(nameof(Index));

        return RedirectToAction(nameof(Delete), new { id });
    }

    //private bool CollectorAssignmentsHuyNqExists(Guid id)
    //{
    //    return _context.CollectorAssignmentsHuyNqs.Any(e => e.AssignmentId == id);
    //}
}
