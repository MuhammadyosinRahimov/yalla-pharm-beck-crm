using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Services.Interfaces;

namespace YallaPharm.API.Controllers;

[ApiController]
[Route("api/productTemplate")]
[Authorize(Roles = "Administrator,SuperAdmin")]
public class ProductTemplateApiController : ControllerBase
{
    private readonly IProductTemplateService _service;

    public ProductTemplateApiController(IProductTemplateService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductTemplateDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] ProductTemplateDto dto)
    {
        var result = await _service.UpdateAsync(dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);
        return Ok(new { message = "Product template deleted successfully" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return NotFound(new { message = "Product template not found" });

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpPost("image")]
    public async Task<IActionResult> SaveImage([FromForm] ProductTemplateImageDto dto)
    {
        var result = await _service.SaveImageAsync(dto);
        return Ok(new { imagePath = result });
    }
}
