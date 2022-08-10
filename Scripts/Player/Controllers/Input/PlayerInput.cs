using System;
using Godot;
using JustAnotherJungleQuestGame.System;
using static Godot.Input;

namespace JustAnotherJungleQuestGame.Player.Controllers.Input
{
    public class PlayerInput : Node
    {
        public Vector2 InputVelocity { get; private set; }
        public bool CanMove { get; set; } = true;

        private EventManager _eventManager;
        
        public override void _Ready()
        {
            _eventManager = GetNode<EventManager>("/root/EventManager") ?? throw new ArgumentNullException(nameof(_eventManager));
        }

        public override void _Input(InputEvent @event)
        {
            if (@event.IsActionPressed("ui_action"))
            {
                _eventManager.EmitSignal(EventManager.ActionKeySignal);
            }

            if (@event.IsActionPressed("ui_accept"))
            {
                _eventManager.EmitSignal(EventManager.AcceptKeySignal);
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

             InputVelocity = inputVelocity;
             return inputVelocity;
        }
    }
}
