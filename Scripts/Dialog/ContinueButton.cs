using System;
using Godot;

namespace JustAnotherJungleQuestGame.Dialog
{
    public class ContinueButton : TextureButton
    {
        private const string BounceAnimation = "Bounce";
    
        public override void _Ready()
        {
            var animationPlayer = GetChild<AnimationPlayer>(0) ?? throw new ArgumentNullException();
            animationPlayer.Play(BounceAnimation);
        }

    }
}
