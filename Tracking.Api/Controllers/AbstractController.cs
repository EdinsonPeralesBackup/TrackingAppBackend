using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tracking.Api.Services;
using Tracking.Application.Common.Interface;

namespace Tracking.Api.Controllers
{
    public abstract class AbstractController : ControllerBase
    {
        private IMediator _mediator;
        private ICurrentUser _CurrentUser;
        private ICryptography _cryptography;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ICurrentUser CurrentUser => HttpContext.Session.GetString("dataUser") != null ? JsonConvert.DeserializeObject<CurrentUser>(HttpContext.Session.GetString("dataUser")) : null;
        protected ICryptography Cryptography => _cryptography ??= HttpContext.RequestServices.GetService<ICryptography>();
    }
}
