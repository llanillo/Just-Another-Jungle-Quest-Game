using Godot;
using Justanotherjunglequestgame.Scripts.Player.Controllers.Animation;
using Justanotherjunglequestgame.Scripts.Player.Controllers.Input;
using Justanotherjunglequestgame.Scripts.Player.Controllers.Movement;

namespace Justanotherjunglequestgame.Scripts.Player.Controllers.Manager
{
    public class PlayerManager : KinematicBody2D
    {
        private PlayerAnimation _playerAnimation;
        private PlayerMovement _playerMovement;
        public PlayerInput PlayerInput { get; private set; }
    
        public override void _Ready()
        {
            _playerAnimation = GetNode<PlayerAnimation>("Controllers/PlayerAnimation");
            _playerMovement = GetNode<PlayerMovement>("Controllers/PlayerMovement");
            PlayerInput = GetNode<PlayerInput>("Controllers/PlayerInput");
        }

        public override void _PhysicsProcess(float delta)
        {
            var inputVelocity =  PlayerInput.ProcessInput();
            _playerAnimation.PlayAnimations(IsOnFloor(), inputVelocity); // Plays the corresponding animation
            _playerMovement.Move(IsOnFloor(), inputVelocity);
        }

        public override void _Process(float delta)
        {
            
        }
    }
}
