using System;
using Godot;

namespace JustAnotherJungleQuestGame.Player.Controllers.Movement
{
    public class PlayerMovement : Node
    {
        private static readonly Vector2 FloorNormal = Vector2.Up;
        
        [Export] private float _speed = 300;
        [Export] private float _jumpSpeed = 1200;
        [Export] private int _gravity = 4000;

        public Vector2 Velocity { get; private set; } = Vector2.Zero;

        private KinematicBody2D _kinematicBody;

        public override void _Ready()
        {
            _kinematicBody = GetNode<KinematicBody2D>("../../../Player") ?? throw new ArgumentNullException(nameof(_kinematicBody));
        }

        public void Move(bool isOnFloor, Vector2 direction)
        {
            var newVelocity = Velocity;
            newVelocity.x = _speed * direction.x;
            newVelocity.y += _gravity * GetPhysicsProcessDeltaTime();
            
            // The player is jumping        
            if(isOnFloor && direction.y <= -1.0f) 
            {
                newVelocity.y = _jumpSpeed * direction.y;
            }
            
            Velocity = _kinematicBody.MoveAndSlide(newVelocity, FloorNormal);
        }
    }
}
 