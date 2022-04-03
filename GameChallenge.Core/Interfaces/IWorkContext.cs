using GameChallenge.Core.DBEntities;
using GameChallenge.Core.DBEntities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameChallenge.Core.Interfaces
{
    public interface IWorkContext
    {
        ApplicationUser CurrentPlayer { get; set; }
    }
}
