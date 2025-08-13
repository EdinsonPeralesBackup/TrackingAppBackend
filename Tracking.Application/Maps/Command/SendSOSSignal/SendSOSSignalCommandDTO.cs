using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.Maps.Command.SendSOSSignal
{
    public class SendSOSSignalCommandDTO
    {
        public string Message { get; set; }
        public bool FallBackUsed { get; set; }
    }
}
