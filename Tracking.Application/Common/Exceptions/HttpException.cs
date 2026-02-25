using Tracking.Application.Common.Dtos;

namespace Tracking.Application.Common.Exceptions
{
    public class HttpException : BaseException
    {
        public HttpException(int statusCode, MensajeUsuarioDTO message, Exception exception = null)
            : base(message, exception)
        {
            StatusCode = statusCode;
            Errores = new List<MensajeUsuarioDTO>();
        }

        public int StatusCode { get; }
        public IList<MensajeUsuarioDTO> Errores { get; set; }
    }
}
