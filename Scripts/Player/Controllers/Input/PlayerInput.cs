using Godot;
using static Godot.Input;

namespace Justanotherjunglequestgame.Scripts.Player.Controllers.Input
{
    public class PlayerInput : Node
    {

        public bool IsJumping { get; private set; }

        public Vector2 OnMovementInput()
        {
             var inputVelocity = Vector2.Zero;

             if (IsActionPressed("ui_right"))
             {
                 inputVelocity.x += 1;
             }
             if (IsActionPressed(("ui_left")))
             {
                 inputVelocity.x -= 1;
             }
             if (IsActionPressed(("ui_down")))
             {
                 inputVelocity.y += 1;
             }
             
             if (IsActionPressed(("ui_up")))
             {
                 inputVelocity.y -= 1;
             }

             return inputVelocity.Normalized();
        }
    }
}
