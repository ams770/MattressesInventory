using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory.API.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[TypeFilter(typeof(DomainExceptionFilter))]
public abstract class ApiControllerBase : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}
