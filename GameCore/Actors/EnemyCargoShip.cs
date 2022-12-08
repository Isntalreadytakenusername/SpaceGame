﻿using Merlin2d.Game;
using Merlin2d.Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Actors
{
    internal class EnemyCargoShip: AbstractEnemy
    {
        private IMessage healthMessage;
        public EnemyCargoShip(int x, int y, string name, double speed, int health, ISpeedStrategy speedStrategy, int energy, PlayerWizard protagonist) : base(x, y, name, speed, health, speedStrategy, energy, protagonist)
        {
            animation = new Animation("resources/sprites/enemy_cargo_ship.png", 336, 230);
            this.SetAnimation(animation);
            this.GetAnimation().Start();

            this.healthMessage = new Message("Health: " + this.health, 300, 300, 10, Color.White, MessageDuration.Indefinite);
            this.healthMessage.SetAnchorPoint(this);
        }

        public override void Die() 
        {
            this.countdownToDisappear = 20;
            this.protagonist.DestroyedCargoShip();
        }

        public override void Update()
        {
            this.healthMessage.SetText("Health: " + this.health);

            DieCountdownCheck();
            this.PassedThroughBlockade();
            this.moveLeft.Execute();
            this.spellCoolDownTime++;
            this.Cast("Damage");

            if (old_speed != speed)
            {
                RecreateMove();
                old_speed = speed;
            }
        }
    }
}