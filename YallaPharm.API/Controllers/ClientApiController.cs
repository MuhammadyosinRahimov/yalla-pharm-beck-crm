using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Services.Interfaces;

namespace YallaPharm.API.Controllers;

[ApiController]
[Route("api/clients")]
[Authorize(Roles = "SuperAdmin,Administrator")]
public class ClientApiController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientApiController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClientDto clientDto)
    {
        var result = await _clientService.CreateAsync(clientDto);
        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] ClientDto clientDto)
    {
        try
        {
            var result = await _clientService.UpdateAsync(clientDto);
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
        await _clientService.DeleteAsync(id);
        return Ok(new { message = "Client deleted successfully" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _clientService.GetByIdAsync(id);
        if (result == null)
            return NotFound(new { message = "Client not found" });

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _clientService.GetAllAsync();
        return Ok(result);
    }
}
