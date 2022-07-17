using Godot;
using Justanotherjunglequestgame.Scripts.Player.Controllers.Animation;
using Justanotherjunglequestgame.Scripts.Player.Controllers.Input;
using Justanotherjunglequestgame.Scripts.Player.Controllers.Movement;

namespace Justanotherjunglequestgame.Scripts.Player.Controllers.Manager
{
    public class PlayerManager : KinematicBody2D
    {
        private static readonly Vector2 FloorNormal = Vector2.Up;
        
        private PlayerAnimation _playerAnimation;
        private PlayerMovement _playerMovement;
        private PlayerInput _playerInput;
    
        public override void _Ready()
        {
            _playerAnimation = GetNode<PlayerAnimation>("Controllers/PlayerAnimation");
            _playerMovement = GetNode<PlayerMovement>("Controllers/PlayerMovement");
            _playerInput = GetNode<PlayerInput>("Controllers/PlayerInput");
        }

        public override void _PhysicsProcess(float delta)
        {
            var inputVelocity = PlayerInput.OnMovementInput();
            var velocity = MoveAndSlide(_playerMovement.GetMoveVelocity(IsOnFloor(), inputVelocity), FloorNormal);
            _playerAnimation.PlayAnimations(IsOnFloor(), inputVelocity); // Plays the corresponding animation
            _playerMovement.Velocity = velocity; // Updates the current velocity
        }

        public override void _Process(float delta)
        {
            
        }
    }
}
