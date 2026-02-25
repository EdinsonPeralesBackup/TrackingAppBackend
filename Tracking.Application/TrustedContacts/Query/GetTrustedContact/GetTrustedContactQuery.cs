using MediatR;

namespace Tracking.Application.TrustedContacts.Query.GetTrustedContact
{
    public class GetTrustedContactQuery : IRequest<IEnumerable<GetTrustedContactQueryDTO>>
    {
        public int IdUser { get; set; }
    }
}
