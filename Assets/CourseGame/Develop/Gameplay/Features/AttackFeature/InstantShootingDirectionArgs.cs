using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.CourseGame.Develop.Gameplay.Features.AttackFeature
{
    public class InstantShootingDirectionArgs
    {
        private List<InstantShotDirectionArgs> _args;

        public InstantShootingDirectionArgs(params InstantShotDirectionArgs[] args)
        {
            _args = new List<InstantShotDirectionArgs>(args);
        }

        public IReadOnlyList<InstantShotDirectionArgs> Args => _args;

        public void Add(InstantShotDirectionArgs shotInDeirectionArgs)
        {
            var arg = _args.FirstOrDefault(ar => ar.Angel == shotInDeirectionArgs.Angel);

            if(arg != null)
            {
                arg.ProjectileCounts += shotInDeirectionArgs.ProjectileCounts;
                return;
            }

            _args.Add(shotInDeirectionArgs);
        }

        public void Remove(InstantShotDirectionArgs shotInDeirectionArgs)
        {
            var arg = _args.FirstOrDefault(ar => ar.Angel == shotInDeirectionArgs.Angel);
            
            if(arg != null)
            {
                arg.ProjectileCounts -= shotInDeirectionArgs.ProjectileCounts;  

                if(arg.ProjectileCounts <= 0)   
                    _args.Remove(shotInDeirectionArgs);
            }
        }
    }

    public class InstantShotDirectionArgs
    {
        private int _angel;
        private int _projectileCounts;

        public InstantShotDirectionArgs(int angel, int projectileCounts)
        {
            _angel = angel;
            _projectileCounts = projectileCounts;
        }

        public int Angel => _angel;
        public int ProjectileCounts
        {
            get => _projectileCounts;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _projectileCounts = value;
            }
        }
    }
}
