using System;
using Godot;
using Object = Godot.Object;

namespace Justanotherjunglequestgame.Scripts.Dialog
{
    public class TextManager : Control
    {
        private const float TextSpeedRate = 0.02f;
        private const float InitialValue = 0.0f;
        private const float FinalValue = 1.0f;
        
        private const string PercentProperty = "percent_visible";

        public DialogState CurrentState { get; private set; } = DialogState.Ready;

        private AudioStreamPlayer _audioStreamPlayer;
        private TextureButton _continueButton;
        private RichTextLabel _textLabel;
        private Tween _textTween;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
            _continueButton = GetNode<TextureButton>("ContinueButton");
            _textLabel = GetNode<RichTextLabel>( "RichTextLabel");
            
            _textTween = GetNode<Tween>("Tween");
            _textTween.Connect("tween_completed", this, "OnTextTweenCompleted");
            _continueButton.Connect("pressed", this, "OnContinueButtonPressed");
            _continueButton.Visible = false;
        }

        /*
         * Called on accept key pressed signal from player input
         */
        public void OnAcceptKeySignal()
        {
            switch (CurrentState)
            {
                case DialogState.Reading:
                    OnTextTweenTweenCompleted(null, null);
                    break;
                case DialogState.Ready:
                    HandleReadyState();
                    break;
                case DialogState.Finished:
                    HandleFinishedState();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        /*
         * Removes dialog from scene
         */
        private void HandleFinishedState()
        {
            SwitchState(DialogState.Finished);
            GD.Print("Current state is FINISHED"); }

        /*
         * Prepares the dialog for the next text
         */
        private void HandleReadyState()
        {
            GD.Print("Current state is READY");
            _textLabel.Text = "";
            SwitchState(DialogState.Ready);
        }

        /*
         * Completes the tween animation showing the full text
         * and switch dialog state to ready
         */
        private void OnTextTweenTweenCompleted(Object obj, NodePath node)
        {
            GD.Print("On text tween completed call");
            SwitchState(DialogState.Ready);
            _textLabel.PercentVisible = FinalValue;
            _audioStreamPlayer.StreamPaused = true;
            _continueButton.Visible = true;
            _textTween.RemoveAll();
        }

        /*
         * 
         */
        public void DisplayText(string text)
        {
            GD.Print("Display text");
            SwitchState(DialogState.Reading);
            _audioStreamPlayer.StreamPaused = false;
            _continueButton.Visible = false;
            
            _textLabel.Text = text;
            _textLabel.PercentVisible = InitialValue;
            _textTween.InterpolateProperty(_textLabel, PercentProperty,  InitialValue, FinalValue, _textLabel.Text.Length * TextSpeedRate);
            _textTween.Start();
        }

        /*
         * 
         */
        private void OnContinueButtonPressed()
        {
            GD.Print("On continue button pressed call");
            OnAcceptKeySignal();
        }

        /*
         * 
         */
        private void SwitchState(DialogState nextState)
        {
            CurrentState = nextState;
        }
    }
}
