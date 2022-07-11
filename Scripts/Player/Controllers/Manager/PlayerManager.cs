using Godot;
using Justanotherjunglequestgame.Scripts.Player.Controllers.Input;
using Justanotherjunglequestgame.Scripts.Player.Controllers.Movement;

namespace Justanotherjunglequestgame.Scripts.Player.Controllers.Manager
{
    public class PlayerManager : Node
    {
        private PlayerInput _playerInput;
        private PlayerMovement _playerMovement;
    
        public override void _Ready()
        {
            _playerInput = GetNode<PlayerInput>("Controllers/PlayerInput");
        }

        public override void _PhysicsProcess(float delta)
        {
            Vector2 inputVelocity = _playerInput.OnMovementInput();
        }
    }
}
