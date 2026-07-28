using DateAdderApp.Controllers;
using DateAdderApp.Interfaces;
using DateAdderApp.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace DateAdder.Tests.Controllers;

public class DateControllerTests
{
    private readonly IDateAddService _service = Substitute.For<IDateAddService>();
    private readonly IDateAddRequestValidator _validator = Substitute.For<IDateAddRequestValidator>();
    private readonly DateController _sut;

    public DateControllerTests()
    {
        _sut = new DateController(_service, _validator);
    }

    [Fact]
    public void AddDays_ValidRequest_ReturnsOkWithResponse()
    {
        var request = new DateAddRequest("31/01/2026", 1);
        var response = new DateAddResponse("31/01/2026", 1, "01/02/2026");

        _validator.Validate(request).Returns(ValidationResult.Ok());
        _service.AddDays(request).Returns(response);

        var result = _sut.AddDays(request) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(response, result!.Value);
    }

    [Fact]
    public void AddDays_InvalidRequest_ReturnsBadRequest()
    {
        var request = new DateAddRequest("bad-date", 1);
        _validator.Validate(request).Returns(ValidationResult.Fail("Invalid date."));

        var result = _sut.AddDays(request) as BadRequestObjectResult;

        Assert.NotNull(result);
        _service.DidNotReceive().AddDays(Arg.Any<DateAddRequest>());
    }

    [Fact]
    public void AddDays_InvalidRequest_DoesNotCallService()
    {
        var request = new DateAddRequest("", 1);
        _validator.Validate(request).Returns(ValidationResult.Fail("Date is required."));

        _sut.AddDays(request);

        _service.DidNotReceive().AddDays(Arg.Any<DateAddRequest>());
    }
}