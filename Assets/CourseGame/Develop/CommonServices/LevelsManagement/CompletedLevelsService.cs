using Assets.CourseGame.Develop.CommonServices.DataManagement;
using Assets.CourseGame.Develop.CommonServices.DataManagement.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.CourseGame.Develop.CommonServices.LevelsManagement
{
    public class CompletedLevelsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private List<int> _completedLevels = new();

        public CompletedLevelsService(PlayerDataProvider playerDataProvider)
        {
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public bool IsLevelCompleted(int levelNimber) => _completedLevels.Contains(levelNimber);

        public bool TryAddLevelToCompleted(int levelNumber)
        {
            if(IsLevelCompleted(levelNumber))
                return false;

            _completedLevels.Add(levelNumber);
            return true;
        }

        public void ReadFrom(PlayerData data)
        {
            _completedLevels.Clear();
            _completedLevels.AddRange(data.CompletedLevels);
        }

        public void WriteTo(PlayerData data)
        {
            data.CompletedLevels.Clear();
            data.CompletedLevels.AddRange(data.CompletedLevels);
        }
    }
}
