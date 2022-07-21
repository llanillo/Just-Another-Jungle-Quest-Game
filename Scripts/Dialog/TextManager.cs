using System;
using Godot;
using Object = Godot.Object;

namespace Justanotherjunglequestgame.Scripts.Dialog
{
    public class TextManager : Panel
    {
        private const float TextSpeedRate = 0.02f;
        private const float InitialValue = 0.0f;
        private const float FinalValue = 1.0f;
        
        private const string PercentProperty = "percent_visible";
    
        public DialogState CurrentState { get; private set; }

        private AudioStreamPlayer _audioStreamPlayer;
        private TextureButton _continueButton;
        private RichTextLabel _textLabel;
        private Tween _textTween;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            CurrentState = DialogState.Ready;

            _audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
            _continueButton = GetNode<TextureButton>("ContinueButton");
            _textLabel = GetNode<RichTextLabel>( "RichTextLabel");
            
            _textTween = GetNode<Tween>("Tween");
            _textTween.Connect("tween_completed", this, "OnTextTweenCompleted");
            _continueButton.Connect("pressed", this, "OnContinueButtonPressed");
            _continueButton.Visible = false;
        }

        public override void _Input(InputEvent @event)
        {
            if (!@event.IsActionPressed("ui_accept")) return;
            
            switch (CurrentState)
            {
                case DialogState.Reading:
                    OnTextTweenTweenCompleted(null, null);
                    break;
                case DialogState.Finished:
                    HandleFinishedState();
                    break;
                case DialogState.Ready:
                    HandleReadyState();
                    break;
                case DialogState.Completed:
                    HandleCompletedState();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleFinishedState()
        {
            _textLabel.Text = "";
            SwitchState(DialogState.Ready);
        }

        private void HandleReadyState()
        {
            throw new NotImplementedException();
        }

        private void HandleCompletedState()
        {
            SwitchState(DialogState.Completed);
        }

        private void OnTextTweenTweenCompleted(Object obj, NodePath node)
        {
            SwitchState(DialogState.Finished);
            _textLabel.PercentVisible = FinalValue;
            _continueButton.Visible = true;
            _audioStreamPlayer.StreamPaused = true;
            _textTween.RemoveAll();
        }

        public void DisplayText(string text)
        {
            SwitchState(DialogState.Reading);
            _audioStreamPlayer.StreamPaused = false;
            _continueButton.Visible = false;
            
            _textLabel.Text = text;
            _textLabel.PercentVisible = InitialValue;
            _textTween.InterpolateProperty(_textLabel, PercentProperty,  InitialValue, FinalValue, _textLabel.Text.Length * TextSpeedRate);
            _textTween.Start();
        }

        private void OnContinueButtonPressed()
        {
            HandleFinishedState();
        }

        private void SwitchState(DialogState nextState)
        {
            CurrentState = nextState;
        }
    }
}
