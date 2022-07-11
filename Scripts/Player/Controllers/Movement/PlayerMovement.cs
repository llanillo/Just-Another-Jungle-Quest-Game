using Godot;

namespace Justanotherjunglequestgame.Scripts.Player.Controllers.Movement
{
    public class PlayerMovement : Node
    {
        [Export] private int Speed = 400;
        [Export] private int FallSpeed = -200;
        [Export] private int Gravity = 200;

        public Vector2 GetMovement(bool isOnFloor, Vector2 inputVelocity)
        {
            var newVelocity = inputVelocity * Speed;

            if (isOnFloor)
            {
                newVelocity.y = FallSpeed;
            }
        
            newVelocity.y += Gravity;
            return newVelocity;
        }
    }
}
 