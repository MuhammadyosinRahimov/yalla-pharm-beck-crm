using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Services.Interfaces;
using YallaPharm.Domain.Enums;

namespace YallaPharm.API.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize(Roles = "SuperAdmin,Administrator,Operator")]
public class OrderApiController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderApiController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "SuperAdmin,Administrator")]
    public async Task<IActionResult> Delete(string id)
    {
        await _orderService.DeleteAsync(id);
        return Ok(new { message = "Order deleted successfully" });
    }

    [HttpPost("update-state")]
    [Authorize(Roles = "SuperAdmin,Administrator,Courier")]
    public async Task<IActionResult> UpdateState([FromBody] UpdateOrderStateDto dto)
    {
        var result = await _orderService.UpdateStateAsync(dto);
        if (result == null)
            return NotFound(new { message = "Order not found" });

        return Ok(result);
    }

    [HttpGet("order-number/{id}")]
    public async Task<IActionResult> GetOrderNumber(string id)
    {
        var result = await _orderService.GetOrderNumberAsync(id);
        if (result == null)
            return NotFound(new { message = "Order not found" });

        return Ok(new { orderNumber = result });
    }

    [HttpGet("by-state/{orderState}/{count}/{orderOrClientInfo?}")]
    public async Task<IActionResult> GetByState(OrderState orderState, int count, string? orderOrClientInfo = null)
    {
        var result = await _orderService.GetByStateAsync(orderState, count, orderOrClientInfo);
        return Ok(result);
    }

    [HttpPost("by-state")]
    public async Task<IActionResult> GetByStateFilter([FromBody] OrdersByStateFilterDto filter)
    {
        var result = await _orderService.GetByStateFilterAsync(filter);
        return Ok(result);
    }

    [HttpGet("{orderId}/{orderState?}")]
    public async Task<IActionResult> GetById(string orderId, OrderState? orderState = null)
    {
        var result = await _orderService.GetByIdAsync(orderId, orderState);
        if (result == null)
            return NotFound(new { message = "Order not found" });

        return Ok(result);
    }

    [HttpPost("consulting")]
    public async Task<IActionResult> CreateConsulting([FromBody] OrderDto orderDto)
    {
        var result = await _orderService.CreateConsultingAsync(orderDto);
        return Ok(result);
    }

    [HttpPost("insearch/{orderId}")]
    public async Task<IActionResult> InSearch(string orderId)
    {
        var result = await _orderService.InSearchAsync(orderId);
        if (result == null)
            return NotFound(new { message = "Order not found" });

        return Ok(result);
    }

    [HttpPost("waiting-client/{orderId}")]
    public async Task<IActionResult> WaitingClient(string orderId)
    {
        var result = await _orderService.WaitingClientAsync(orderId);
        if (result == null)
            return NotFound(new { message = "Order not found" });

        return Ok(result);
    }

    [HttpPost("placement/{orderId}")]
    public async Task<IActionResult> Placement(string orderId)
    {
        var result = await _orderService.PlacementAsync(orderId);
        if (result == null)
            return NotFound(new { message = "Order not found" });

        return Ok(result);
    }

    [HttpPost("delivered/{orderId}")]
    public async Task<IActionResult> Delivered(string orderId)
    {
        var result = await _orderService.DeliveredAsync(orderId);
        if (result == null)
            return NotFound(new { message = "Order not found" });

        return Ok(result);
    }

    [HttpPost("cancellation/{orderId}")]
    public async Task<IActionResult> Cancellation(string orderId)
    {
        var result = await _orderService.CancellationAsync(orderId);
        if (result == null)
            return NotFound(new { message = "Order not found" });

        return Ok(result);
    }

    [HttpPost("rejection/{orderId}")]
    public async Task<IActionResult> Rejection(string orderId)
    {
        var result = await _orderService.RejectionAsync(orderId);
        if (result == null)
            return NotFound(new { message = "Order not found" });

        return Ok(result);
    }

    [HttpPost("return-from-rejection/{orderId}")]
    public async Task<IActionResult> ReturnFromRejection(string orderId)
    {
        var result = await _orderService.ReturnFromRejectionAsync(orderId);
        if (result == null)
            return NotFound(new { message = "Order not found" });

        return Ok(result);
    }

    [HttpPost("return-products/{orderId}")]
    public async Task<IActionResult> ReturnProducts(string orderId)
    {
        var result = await _orderService.ReturnProductsAsync(orderId);
        if (result == null)
            return NotFound(new { message = "Order not found" });

        return Ok(result);
    }

    [HttpGet("pharmacy-orders/{orderId}")]
    public async Task<IActionResult> GetPharmacyOrders(string orderId)
    {
        var result = await _orderService.GetPharmacyOrdersAsync(orderId);
        return Ok(result);
    }
}
