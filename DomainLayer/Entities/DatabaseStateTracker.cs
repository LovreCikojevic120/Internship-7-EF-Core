using DataLayer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public static class DatabaseStateTracker
    {
        public static User? CurrentUser;
    }
}
