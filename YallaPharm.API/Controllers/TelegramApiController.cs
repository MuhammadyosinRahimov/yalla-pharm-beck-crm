using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Services.Interfaces;

namespace YallaPharm.API.Controllers;

[ApiController]
[Route("telegram")]
[AllowAnonymous]
public class TelegramApiController : ControllerBase
{
    private readonly ITelegramService _telegramService;

    public TelegramApiController(ITelegramService telegramService)
    {
        _telegramService = telegramService;
    }

    [HttpPost]
    public async Task<IActionResult> Webhook([FromBody] TelegramUpdateDto update)
    {
        await _telegramService.ProcessUpdateAsync(update);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetStatus()
    {
        var result = await _telegramService.GetStatusAsync();
        return Ok(result);
    }
}
