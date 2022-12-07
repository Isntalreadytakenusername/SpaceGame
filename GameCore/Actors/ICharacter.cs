using Merlin2d.Game;
using Merlin2d.Game.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Actors
{
    public interface ICharacter: IMovable
    {
        public void ChangeHealth(int delta);
        public int GetHealth();

        public IWorld GetWorld();
        public void Die();
        public void AddEffect(ICommand effect);
        public void RemoveEffect(ICommand effect);

        public int GetActorOrientation();
        public void SetSpeed(double speed);
    }
}
