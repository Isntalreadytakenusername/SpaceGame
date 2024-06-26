﻿using GameCore.Actors;
using GameCore.Commands;
using GameCore.Factories;
using Merlin2d.Game;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Enums;
using System.Runtime.InteropServices;

namespace GameCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameContainer container = new GameContainer("Game window", 1000, 900);
            container.SetMap("resources/maps/space.tmx");
            //container.GetWorld().SetPhysics(new Gravity()); // physics? We don't need physics in space :)
            
            container.GetWorld().SetEndCondition(PlayerWizard.CheckForDefeatOrVictory);
            container.SetEndGameMessage(new Message("Congratulations!", 300, 300, 50, Color.Darkgreen, MessageDuration.Indefinite), MapStatus.Finished);
            container.SetEndGameMessage(new Message("Game Over!", 400, 300, 50, Color.Red, MessageDuration.Indefinite), MapStatus.Failed);

            container.GetWorld().AddInitAction(world =>
            {
                world.AddActor(new PlayerWizard(100, 100, "Player", 10, 3000, new NormalSpeedStrategy(), 1000000)); // reliable way of adding actors
            });

            container.Run();
        }
    }
}