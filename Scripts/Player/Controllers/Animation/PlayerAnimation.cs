using System;
using Godot;

namespace JustAnotherJungleQuestGame.Player.Controllers.Animation
{
    public class PlayerAnimation : Node
    {
        private const string IdleAnim = "Idle";
        private const string WalkAnim = "Walk";
        private const string RunAnim = "Run";
        private const string JumpAnim = "Jump";
    
        private AnimatedSprite _animatedSprite;
    
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _animatedSprite = GetNode<AnimatedSprite>("../../AnimatedSprite") ?? throw new ArgumentNullException(nameof(_animatedSprite));
        }

        public void PlayAnimations(bool isOnFloor, Vector2 inputVelocity)
        {
            if (inputVelocity.x >= 1)
            {
                _animatedSprite.Play(RunAnim);
                _animatedSprite.FlipH = false;
            }
            else if (inputVelocity.x <= -1)
            {
                _animatedSprite.Play(RunAnim);
                _animatedSprite.FlipH = true;
            }
            else 
            {
                _animatedSprite.Play(IdleAnim);
            }

            if (!isOnFloor)
            {
                _animatedSprite.Play(JumpAnim);
            }
        }
    }
}
