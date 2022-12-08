using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameCore.Commands;
using Merlin2d.Game.Actions;

namespace GameCore.Spells
{
    internal class SpellDirector: ISpellDirector
    {
        ISpellBuilder builder;
        IWizard SpellCaster;

        Dictionary<string, SpellInfo> spellInfo = new Dictionary<string, SpellInfo>()
        {
            { "Damage", new SpellInfo() { Name = "Damage", SpellType = SpellType.ProjectileSpell, EffectNames = new List<string>() { "Damage" }, AnimationPath = "resources/sprites/fireball.png", AnimationWidth = 35, AnimationHeight = 35 } },
            { "Heal", new SpellInfo() { Name = "Heal", SpellType = SpellType.SelfCastSpell, EffectNames = new List<string>() { "Heal" }, AnimationPath = "resources/sprites/heal.png", AnimationWidth = 64, AnimationHeight = 64 } },
            { "SlowDown", new SpellInfo() { Name = "SlowDown", SpellType = SpellType.ProjectileSpell, EffectNames = new List<string>() { "SlowDown" }, AnimationPath = "resources/sprites/slowdown.png", AnimationWidth = 35, AnimationHeight = 35 } },
            {"BigBoom", new SpellInfo() { Name = "BigBoom", SpellType = SpellType.ProjectileSpell, EffectNames = new List<string>() { "BigBoom" }, AnimationPath = "resources/sprites/big_boom.png", AnimationWidth = 50, AnimationHeight = 50 } }
        };

        Dictionary<string, int> spellPrice = new Dictionary<string, int>()
        {
            { "Damage", 10 },
            { "Heal", 10 },
            { "SlowDown", 10 },
            { "BigBoom", 10 }
        };

        public SpellDirector(IWizard SpellCaster)
        {
            this.SpellCaster = SpellCaster;
        }
        
        public ISpell Build(string spellName)
        {
            if (spellInfo.ContainsKey(spellName))
            {
                SpellInfo info = spellInfo[spellName];
                if (info.SpellType == SpellType.ProjectileSpell)
                {
                    this.builder = new ProjectileSpellBuilder(info, spellPrice[spellName], this.SpellCaster);
                    try
                    {
                        this.builder.SetAnimation(new Merlin2d.Game.Animation(spellInfo[spellName].AnimationPath, spellInfo[spellName].AnimationWidth, spellInfo[spellName].AnimationHeight)); // this is failing rarely with no reason
                    }
                    catch
                    {
                        this.builder.SetAnimation(new Merlin2d.Game.Animation("resources/sprites/fireball.png", 35, 35));
                    }

                    foreach (string effect in spellInfo[spellName].EffectNames) 
                    {
                        this.builder.AddEffect(effect);
                    }
                    return this.builder.GetSpell();
                }
                else if (info.SpellType == SpellType.SelfCastSpell)
                {
                    this.builder = new SelfCastSpellBuilder(info, spellPrice[spellName], this.SpellCaster);
                    this.builder.SetAnimation(new Merlin2d.Game.Animation(spellInfo[spellName].AnimationPath, spellInfo[spellName].AnimationWidth, spellInfo[spellName].AnimationHeight));
                    foreach (string effect in spellInfo[spellName].EffectNames)
                    {
                        this.builder.AddEffect(effect);
                    }
                    return this.builder.GetSpell();
                }
            }
            throw new Exception("Spell is not created");
            return null;
        }
    }
}
