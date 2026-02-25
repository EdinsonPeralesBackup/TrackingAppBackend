using MediatR;

namespace Tracking.Application.TrustedContacts.Command.ChangeStatusConfidenceContact
{
    public class ChangeStatusConfidenceContactCommand : IRequest<ChangeStatusConfidenceContactCommandDTO>
    {
        public int Id { get; set; }
    }
}
