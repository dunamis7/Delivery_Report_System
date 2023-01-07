using Delivery_Report_System.Models.Data;
using Delivery_Report_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery_Report_System.Controllers;

[Authorize]
[ApiController]
[Route("deliveries")]
public class DeliveriesController :ControllerBase
{
    private readonly IElasticSearchService _service;


    public DeliveriesController(IElasticSearchService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> SearchDelivery(string keyword)
    {
        var results = await _service.SearchDelivery(keyword);
        return Ok(results);
    }

    [HttpGet]
    public async Task<IActionResult> GetDelivery(string id)
    {
        var response = await _service.GetDelivery(id);
        return Ok(response);
    }



    [HttpPost]
    public async Task<IActionResult> CreateDelivery(Delivery delivery)
    {
        await _service.CreateDelivery(delivery);
        return Ok();
    }
    
    
    
    [HttpPut]
    public async Task<IActionResult> UpdateDelivery(string id, Delivery delivery)
    {
        var getResponse = await _service.GetDelivery(id);

        if (getResponse==null) return NotFound();
        
        await _service.UpdateDelivery(id, delivery);

        var newDelivery = await _service.GetDelivery(id);
        return Ok(newDelivery);
    }
    
    
    [HttpDelete]
    public async Task<IActionResult> DeleteDelivery(string id)
    {
        var getResponse = await _service.GetDelivery(id);
        if (getResponse==null) return NotFound();

        await _service.DeleteDelivery(id);
        return Ok();
    }
    
}