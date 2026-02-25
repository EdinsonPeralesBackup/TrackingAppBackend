using MediatR;

namespace Tracking.Application.TrustedContacts.Command.RegisterTrustedContact
{
    public class RegisterTrustedContactCommand : IRequest<RegisterTrustedContactCommandDTO>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int IdUser { get; set; }
        public int IdUserCreate { get; set; }
    }
}
