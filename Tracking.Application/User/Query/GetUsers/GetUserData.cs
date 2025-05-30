using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.User.Query.GetUsers
{
    public class GetUserData
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}
