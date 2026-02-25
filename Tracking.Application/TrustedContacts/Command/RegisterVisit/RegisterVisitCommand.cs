using MediatR;

namespace Tracking.Application.TrustedContacts.Command.RegisterVisit
{
    public class RegisterVisitCommand : IRequest<RegisterVisitCommandDTO>
    {
        public string TrackingId { get; set; }
    }
}
