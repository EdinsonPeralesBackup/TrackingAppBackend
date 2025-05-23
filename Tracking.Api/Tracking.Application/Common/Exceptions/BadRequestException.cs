using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Common.Dtos;

namespace Tracking.Application.Common.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(MensajeUsuarioDTO message, Exception exception = null)
            : base(message, exception)
        {
        }
    }
}
