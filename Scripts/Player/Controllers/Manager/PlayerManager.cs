using System;
using Godot;
using JustAnotherJungleQuestGame.Player.Controllers.Animation;
using JustAnotherJungleQuestGame.Player.Controllers.Input;
using JustAnotherJungleQuestGame.Player.Controllers.Movement;

namespace JustAnotherJungleQuestGame.Player.Controllers.Manager
{
    public class PlayerManager : KinematicBody2D
    {
        public PlayerAnimation PlayerAnimation { get; private set; }
        public PlayerMovement PlayerMovement { get; private set; }
        public PlayerInput PlayerInput { get; private set; }
        
        public override void _Ready()
        {
            PlayerAnimation = GetNode<PlayerAnimation>("Controllers/PlayerAnimation") ?? throw new ArgumentNullException(nameof(PlayerAnimation));
            PlayerMovement = GetNode<PlayerMovement>("Controllers/PlayerMovement") ?? throw new ArgumentNullException(nameof(PlayerMovement));
            PlayerInput = GetNode<PlayerInput>("Controllers/PlayerInput") ?? throw new  ArgumentNullException(nameof(PlayerInput));
        }

        public override void _PhysicsProcess(float delta)
        {
            var inputVelocity =  PlayerInput.ProcessInput();
            PlayerAnimation.PlayAnimations(IsOnFloor(), inputVelocity); // Plays the corresponding animation
            PlayerMovement.Move(IsOnFloor(), inputVelocity);
        }

        public override void _Process(float delta)
        {
            
        }
    }
}
