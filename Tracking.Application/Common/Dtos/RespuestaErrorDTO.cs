namespace Tracking.Application.Common.Dtos
{
    public class RespuestaErrorDTO
    {
        public MensajeUsuarioDTO Mensaje { get; set; }
        public IEnumerable<MensajeUsuarioDTO> Errores { get; set; }
        public object InternalException { get; set; }

        public RespuestaErrorDTO()
        {
            Mensaje = new MensajeUsuarioDTO();
            Errores = new List<MensajeUsuarioDTO>();
        }
    }
}
