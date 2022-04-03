using GameChallenge.Core.DBEntities;
using GameChallenge.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameChallenge.Core.Services
{
    public class PlayerCalculations : IPlayerCalculations
    {

        public int AvailablePoints(Player player)
        {
            return Convert.ToInt32(player.PlayerBets.Sum(m => m.Amount));
        }

        public int PointsGain(int pointsByUser, int rewardIncreaseByTimes)
        {
            return pointsByUser * rewardIncreaseByTimes;
        }

        public int Challenge(int randomNumberGeneratedBySystem, int pointsByUser, int numberByUser, int rewardIncreaseByTimes)
        {
            int pointsGain = PointsGain(pointsByUser, rewardIncreaseByTimes);
            if (numberByUser != randomNumberGeneratedBySystem)
            {
                //In case of Lose, gain points converted to minus value
                pointsGain = -pointsGain;
            }

            return pointsGain;
        }
    }
}
