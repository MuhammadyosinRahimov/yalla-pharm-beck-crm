using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Services.Interfaces;

namespace YallaPharm.API.Controllers;

[ApiController]
[Route("api/packagingType")]
[Authorize(Roles = "Administrator,SuperAdmin")]
public class PackagingTypeApiController : ControllerBase
{
    private readonly IPackagingTypeService _service;

    public PackagingTypeApiController(IPackagingTypeService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PackagingTypeDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] PackagingTypeDto dto)
    {
        try
        {
            var result = await _service.UpdateAsync(dto);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);
        return Ok(new { message = "Packaging type deleted successfully" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return NotFound(new { message = "Packaging type not found" });

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }
}
