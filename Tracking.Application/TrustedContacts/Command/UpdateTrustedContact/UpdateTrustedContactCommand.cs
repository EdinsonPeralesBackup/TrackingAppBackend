using MediatR;

namespace Tracking.Application.TrustedContacts.Command.UpdateTrustedContact
{
    public class UpdateTrustedContactCommand : IRequest<UpdateTrustedContactCommandDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int IdUserUpdate { get; set; }
    }
}
