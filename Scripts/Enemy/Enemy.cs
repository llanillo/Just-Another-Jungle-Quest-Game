using Godot;

namespace JustAnotherJungleQuestGame.Enemy
{
    public class Enemy : KinematicBody2D
    {
        private static readonly Vector2 FloorNormal = Vector2.Up;
        private const int Speed = 300;
        private const int Gravity = 400;

        private Vector2 _velocity = new Vector2(-Speed, 0);
        
        public override void _Ready()
        {
            SetPhysicsProcess(false);
        }

        public override void _PhysicsProcess(float delta)
        {
            _velocity.y += Gravity * delta;
            
            if (IsOnWall())
            {
                _velocity.x *= -1;
            }

            _velocity.y = MoveAndSlide(_velocity, FloorNormal).y;
        }
    }
}
