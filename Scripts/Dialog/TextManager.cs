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

        private DialogState CurrentState { get; set; } = DialogState.Ready;
        public FuncRef NextDialogFunc { get; set; }

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
            _continueButton.Connect("pressed", this, "OnContinueDialogSignal");
            _continueButton.Visible = false;
        }

        /*
         * Handle the dialog next step depending of current dialog state.
         * It is called from player action key or continue button pressed signals.
         */
        public void OnContinueDialogSignal()
        {
            switch (CurrentState)
            {
                case DialogState.Reading:
                    OnTextTweenCompleted(null, null);
                    break;
                case DialogState.Ready:
                    HandleReadyState();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        /*
         * Callbacks the dialog manager to show the next dialogue if there is any
         */
        private void HandleReadyState()
        {
            _textLabel.Text = "";
            NextDialogFunc.CallFunc();
            SwitchState(DialogState.Reading);
        }

        /*
         * Completes the tween animation showing the full text
         */
        private void OnTextTweenCompleted(Object obj, NodePath node)
        {
            SwitchState(DialogState.Ready);
            _textLabel.PercentVisible = FinalValue;
            _audioStreamPlayer.StreamPaused = true;
            _continueButton.Visible = true;
            _textTween.RemoveAll();
        }

        /*
         * Assigns and display the the text in the dialog box 
         */
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

        /*
         * Switch current dialog state
         */
        private void SwitchState(DialogState nextState)
        {
            CurrentState = nextState;
        }
    }
}
