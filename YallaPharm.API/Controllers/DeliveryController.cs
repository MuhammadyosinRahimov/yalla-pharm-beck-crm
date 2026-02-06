using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Services.Interfaces;

namespace YallaPharm.API.Controllers;

[ApiController]
[Route("api/delivery")]
[AllowAnonymous]
public class DeliveryController : ControllerBase
{
    private readonly IDeliveryService _deliveryService;

    public DeliveryController(IDeliveryService deliveryService)
    {
        _deliveryService = deliveryService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send([FromBody] DeliverySendDto dto)
    {
        var result = await _deliveryService.SendAsync(dto);
        if (!result)
            return BadRequest(new { message = "Failed to send delivery" });

        return Ok(new { message = "Delivery sent successfully" });
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] DeliveryUpdateDto dto)
    {
        var result = await _deliveryService.UpdateAsync(dto);
        if (!result)
            return BadRequest(new { message = "Failed to update delivery" });

        return Ok(new { message = "Delivery updated successfully" });
    }
}
