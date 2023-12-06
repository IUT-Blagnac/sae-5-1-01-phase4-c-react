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
