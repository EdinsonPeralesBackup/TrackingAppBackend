using MediatR;

namespace Tracking.Application.TrustedContacts.Command.DeleteTrustedContact
{
    public class DeleteTrustedContactCommand : IRequest<DeleteTrustedContactCommandDTO>
    {
        public int IdTrustedContact { get; set; }
    }
}
