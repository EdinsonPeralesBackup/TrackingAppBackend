using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Application.Common.Interface;

namespace Tracking.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime HoraLocal()
        {
            var horaActualPacifico = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            var fechaActual = horaActualPacifico;
            return fechaActual.DateTime;
        }
    }
}
