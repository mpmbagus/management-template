using MANAGEMENT.HELPER.Helpers;
using MANAGEMENT.TEMPLATE.Data;
using MANAGEMENT.TEMPLATE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MANAGEMENT.TEMPLATE.Controller;

[ApiController]
[Route("master")]
public class ExampleController : ControllerBase
{
    private readonly ILogger<ExampleController> _logger;
    private readonly AppDBContext _context;

    public ExampleController(ILogger<ExampleController> logger, AppDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    /// <summary>
    /// Select all from DBContext
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        try
        {
            var result = await _context.Models.ToListAsync();
            return JsonResponse.Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return JsonResponse.Error(ex.Message);
        }
    }

    /// <summary>
    /// Select data based on identifier from DBContext
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(dynamic id)
    {
        try
        {
            var result = await _context.Models.FindAsync(id);
            return JsonResponse.Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return JsonResponse.Error(ex.Message);
        }
    }

    /// <summary>
    /// Create new data
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Create(Model model)
    {
        try
        {
            model.Id = 0;
            _context.Models.Add(model);
            await _context.SaveChangesAsync();
            return JsonResponse.CreatedAtAction(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return JsonResponse.Error(ex.Message);
        }
    }

    /// <summary>
    /// Update data based on input model
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> Update(Model model)
    {
        try
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return JsonResponse.ExecuteSuccess();
        }
        catch (DbUpdateConcurrencyException e)
        {
            _logger.LogError(e.Message);
            if (!TableExists(model.Id))
                return JsonResponse.DataNotFound();
            return JsonResponse.Error(e.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return JsonResponse.Error(ex.Message);
        }
    }

    /// <summary>
    /// Delete data based on id
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var part = await _context.Models.FindAsync(id);
            if (part == null)
                return JsonResponse.DataNotFound();

            _context.Models.Remove(part);
            await _context.SaveChangesAsync();
            return JsonResponse.ExecuteSuccess();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return JsonResponse.Error(ex.Message);
        }
    }

    private bool TableExists(int id)
    {
        return _context.Models.Any(e => e.Id == id);
    }
}
