using Tracking.Application.Common.Dtos;

namespace Tracking.Application.Common.Exceptions
{
    public class DeleteFailureException : BaseException
    {
        public DeleteFailureException(MensajeUsuarioDTO message)
            : base(message)
        {
        }
    }
}
