using backend.Services.Interfaces;
using backend.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    /// <summary>
    /// Get all categories possible for a sae subject
    /// Note: Only teachers can access this route
    /// </summary>
    /// <returns>A list with all the categories</returns>
    /// <response code="200">Returns a list with all the categories</response>
    /// <response code="401">If the user is not a teacher</response>
    /// <response code="400">If an error occured</response>
    [HttpGet]
    [Authorize(Roles = RoleAccesses.Teacher)]
    public ActionResult GetCategories()
    {
        try
        {
            var categories = _categoryService.GetCategories();
            return Ok(categories);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest();
        }
    }
}
