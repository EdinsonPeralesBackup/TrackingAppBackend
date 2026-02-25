using Tracking.Application.Common.Dtos;

namespace Tracking.Application.Common.Exceptions
{
    public abstract class BaseException : Exception
    {
        public BaseException(MensajeUsuarioDTO message, Exception exception = null)
            : base(message.Descripcion, exception)
        {
            MensajeUsuario = message;
        }

        public MensajeUsuarioDTO MensajeUsuario { get; }
    }
}
