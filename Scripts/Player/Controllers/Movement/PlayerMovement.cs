using Godot;

namespace Justanotherjunglequestgame.Scripts.Player.Controllers.Movement
{
    public class PlayerMovement : Node
    {
        [Export] private float _speed = 300;
        [Export] private float _jumpSpeed = 1200;
        [Export] private int _gravity = 4000;

        public Vector2 Velocity { get; set; } = Vector2.Zero;

        public Vector2 GetMoveVelocity(bool isOnFloor, Vector2 direction)
        {
            var output = Velocity;
            output.x = _speed * direction.x;
            output.y += _gravity * GetPhysicsProcessDeltaTime();
            
            // The player is jumping        
            if(isOnFloor && direction.y <= -1.0f) 
            {
                output.y = _jumpSpeed * direction.y;
            }

            return output; 
        }
    }
}
 