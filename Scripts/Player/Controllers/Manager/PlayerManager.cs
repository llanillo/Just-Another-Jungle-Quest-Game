using Godot;
using Justanotherjunglequestgame.Scripts.Player.Controllers.Input;
using Justanotherjunglequestgame.Scripts.Player.Controllers.Movement;

namespace Justanotherjunglequestgame.Scripts.Player.Controllers.Manager
{
    public class PlayerManager : KinematicBody2D
    {
        private static readonly Vector2 FloorNormal = Vector2.Up;
        private PlayerInput _playerInput;
        private PlayerMovement _playerMovement;
    
        public override void _Ready()
        {
            _playerInput = GetNode<PlayerInput>("Controllers/PlayerInput");
            _playerMovement = GetNode<PlayerMovement>("Controllers/PlayerMovement");
        }

        public override void _PhysicsProcess(float delta)
        {
            var inputVelocity = PlayerInput.OnMovementInput();
            var velocity = MoveAndSlide(_playerMovement.GetMoveVelocity(IsOnFloor(), inputVelocity), FloorNormal);
            _playerMovement.Velocity = velocity; // Updates the current velocity
        }

        public override void _Process(float delta)
        {
            
        }
    }
}
