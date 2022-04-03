using GameChallenge.Core.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameChallenge.Core.Interfaces
{
    public interface IPlayerCalculations
    {
        public int AvailablePoints(Player player);

        public int PointsGain(int pointsByUser, int rewardIncreaseByTimes);

        public int Challenge(int randomNumberGeneratedBySystem, int pointsByUser, int numberByUser, int rewardIncreaseByTimes);
    }
}
