using GameCore.Actors;
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
            container.GetWorld().SetPhysics(new Gravity());
            //container.GetWorld().SetFactory(new ActorFactory()); // using factory deletes container somehow... no thank you.
            container.SetEndGameMessage(new Message("Congratulations!", 500, 500, 50, Color.Darkgreen, MessageDuration.Indefinite), MapStatus.Finished);
            container.SetEndGameMessage(new Message("Game Over!", 500, 500, 50, Color.Red, MessageDuration.Indefinite), MapStatus.Failed);

            container.GetWorld().AddInitAction(world =>
            {
                world.AddActor(new PlayerWizard(100, 100, "Player", 10, 300, new NormalSpeedStrategy(), 100000));
                world.SetEndCondition(PlayerWizard.CheckForDefeatOrVictory);
                //world.AddActor(new Enemy(800, 800, "Player2", 2, 100, new NormalSpeedStrategy(), 100000));
                //IActor actor = world.GetActors().Find(x => x.GetName() == "Merlin");
                //world.CenterOn(actor);
            });

            container.Run();
        }
    }
}