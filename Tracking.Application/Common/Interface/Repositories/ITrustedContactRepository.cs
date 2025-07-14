using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.TrustedContacts.Command.ChangeStatusConfidenceContact;
using Tracking.Application.TrustedContacts.Command.DeleteTrustedContact;
using Tracking.Application.TrustedContacts.Command.RegisterTrustedContact;
using Tracking.Application.TrustedContacts.Command.UpdateTrustedContact;
using Tracking.Application.TrustedContacts.Query.GetSpecificTrustedContact;
using Tracking.Application.TrustedContacts.Query.GetTrustedContact;

namespace Tracking.Application.Common.Interface.Repositories
{
    public interface ITrustedContactRepository
    {
        Task<RegisterTrustedContactCommandDTO> RegisterTrustedContacts(RegisterTrustedContactCommand command);
        Task<IEnumerable<GetTrustedContactQueryDTO>> GetTrustedContacts(GetTrustedContactQuery command);
        Task<GetSpecificTrustedContactQueryDTO> GetSpecificTrustedContacts(GetSpecificTrustedContactQuery command);
        Task<UpdateTrustedContactCommandDTO> UpdateTrustedContacts(UpdateTrustedContactCommand command);
        Task<DeleteTrustedContactCommandDTO> DeleteTrustedContacts(DeleteTrustedContactCommand command);
        Task<ChangeStatusConfidenceContactCommandDTO> ChangeStatusTrustedContacts(ChangeStatusConfidenceContactCommand command);
    }
}
