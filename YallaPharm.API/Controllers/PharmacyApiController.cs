using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Services.Interfaces;

namespace YallaPharm.API.Controllers;

[ApiController]
[Route("api/pharmacy")]
[Authorize(Roles = "Administrator,SuperAdmin")]
public class PharmacyApiController : ControllerBase
{
    private readonly IPharmacyService _pharmacyService;

    public PharmacyApiController(IPharmacyService pharmacyService)
    {
        _pharmacyService = pharmacyService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PharmacyDto pharmacyDto)
    {
        var result = await _pharmacyService.CreateAsync(pharmacyDto);
        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] PharmacyDto pharmacyDto)
    {
        try
        {
            var result = await _pharmacyService.UpdateAsync(pharmacyDto);
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
        await _pharmacyService.DeleteAsync(id);
        return Ok(new { message = "Pharmacy deleted successfully" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _pharmacyService.GetByIdAsync(id);
        if (result == null)
            return NotFound(new { message = "Pharmacy not found" });

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _pharmacyService.GetAllAsync();
        return Ok(result);
    }
}
