using Godot;
using static Godot.Input;

namespace Justanotherjunglequestgame.Scripts.Player.Controllers.Input
{
    public class PlayerInput : Node
    {
        [Signal] public delegate void ActionKeyPressedSignal();
        [Signal] public delegate void AcceptKeyPressedSignal();

        public Vector2 InputVelocity { get; private set; }
        public bool CanMove { get; set; } = true;

        public override void _Input(InputEvent @event)
        {
            if (@event.IsActionPressed("ui_action"))
            {
                EmitSignal("ActionKeyPressedSignal");
            }

            if (@event.IsActionPressed("ui_accept"))
            {
                EmitSignal("AcceptKeyPressedSignal");
            }
        }

        public Vector2 ProcessInput()
        {
            
            return OnMovementInput();
        }

        private Vector2 OnMovementInput()
        {
            if (!CanMove) return Vector2.Zero;
            
             var inputVelocity = Vector2.Zero;

             if (IsActionPressed("ui_right"))
             {
                 inputVelocity.x = 1;
             }
             
             if (IsActionPressed(("ui_left")))
             {
                 inputVelocity.x = -1;
             }
             
             if (IsActionJustPressed(("ui_up")))
             {
                 inputVelocity.y = -1;
             }
             
             return inputVelocity;
        }
    }
}
