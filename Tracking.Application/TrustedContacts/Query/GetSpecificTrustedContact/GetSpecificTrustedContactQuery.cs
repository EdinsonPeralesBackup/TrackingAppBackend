using MediatR;

namespace Tracking.Application.TrustedContacts.Query.GetSpecificTrustedContact
{
    public class GetSpecificTrustedContactQuery : IRequest<GetSpecificTrustedContactQueryDTO>
    {
        public int IdTrustedContact { get; set; }
    }
}
