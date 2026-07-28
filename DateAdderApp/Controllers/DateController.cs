using DateAdderApp.Interfaces;
using DateAdderApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DateAdderApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DateController : ControllerBase
{
    private readonly IDateAddService _service;
    private readonly IDateAddRequestValidator _validator;

    public DateController(IDateAddService service, IDateAddRequestValidator validator)
    {
        _service = service;
        _validator = validator;
    }

    [HttpPost("add-days")]
    public IActionResult AddDays([FromBody] DateAddRequest request)
    {
        var validation = _validator.Validate(request);

        if (!validation.IsValid)
            return BadRequest(new { error = validation.ErrorMessage });

        var response = _service.AddDays(request);
        return Ok(response);
    }
}
