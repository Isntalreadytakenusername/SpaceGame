using GameCore.Spells;
using Merlin2d.Game.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Actors
{
    public abstract class AbstractWizard: AbstractCharacter, IWizard
    {
        protected ICommand lastMove;
        protected int energy;
        protected ISpell spellToBeUsedSoonSomehowInUnknownManner;
        protected int spellCoolDownTime = 100;
        public AbstractWizard(string name, double speed, int health, ISpeedStrategy speedStrategy, int energy) : base(name, speed, health, speedStrategy)
        {
            this.energy = energy;
        }

        public virtual void Cast(string spellName)
        {
            this.spellToBeUsedSoonSomehowInUnknownManner = new SpellDirector(this).Build(spellName);
            
            if (this.spellToBeUsedSoonSomehowInUnknownManner.GetCost() <= this.energy && this.spellCoolDownTime >= 100)
            {
                this.energy -= this.spellToBeUsedSoonSomehowInUnknownManner.GetCost();
                // hopefully it makes sense to add it to the world at this point
                // I can cast it to IActor as they are related
                // after this I assume Update() will be called repeatedly on the spell object
                this.GetWorld().AddActor((Merlin2d.Game.Actors.IActor)this.spellToBeUsedSoonSomehowInUnknownManner);
                this.spellCoolDownTime = 0;
            }
        }

        public void UseSpell()
        {
            throw new NotImplementedException();
        }

        public void ChangeMana(int delta) 
        {
            this.energy += delta;
        }

        public int GetMana()
        {
            return this.energy;
        }

        public abstract int GetLastDirection();
    }
}
