using Godot;
using Justanotherjunglequestgame.Scripts.Player.Controllers.Input;

namespace Justanotherjunglequestgame.Scripts.Dialog
{
    public class DialogManager : Dialog
    {
        private const string PortraitJsonProperty = "portrait";
        private const string PositionJsonProperty = "position";
        private const string TextJsonProperty = "text";
        private const string PortraitExtension = ".png";

        private const int LeftPosition = 0;
        private const int RightPosition = 1;
        
        private TextureRect _portraitRect;
        private TextManager _textManager;
        
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
            _portraitRect = GetNode<TextureRect>("Rect/HBox/TextureRect");
            _textManager = GetNode<TextManager>("Rect/HBox/VBox");
        }

        public override void StartDialog(string jsonPath, PlayerInput playerInput)
        {
            base.StartDialog(jsonPath, playerInput);
            playerInput.Connect("AcceptKeyPressedSignal", _textManager, "OnAcceptKeySignal");
            DisplayText(playerInput);
        }

        /*
         * Display text in the dialog box while there' still text in the queue.
         * Loads the portrait image and position according to the json values and
         * assigns it to the TextureRect
         */
        private void DisplayText(PlayerInput playerInput)
        {
            if (DialoguesQueue.Count <= 0) return;
            var counter = 0;
            while (_textManager.CurrentState != DialogState.Finished)
            {
                GD.Print("Call: " + counter++);
                if (_textManager.CurrentState != DialogState.Ready) continue;
                    
                var dialogue = DialoguesQueue.Dequeue();
                var portraitPath = (string) dialogue[PortraitJsonProperty] + PortraitExtension;
                    
                switch (int.Parse((string) dialogue[PositionJsonProperty]))
                {
                    case 0:
                        MoveChild(_portraitRect, LeftPosition);
                        break;
                    case 1:
                        MoveChild(_portraitRect, RightPosition);
                        break;
                }
                    
                _portraitRect.Visible = true;
                _portraitRect.Texture = (Texture) GD.Load(PortraitPrefixPath + portraitPath);
                _textManager.DisplayText((string) dialogue[TextJsonProperty]);
            }

            playerInput.Disconnect("OnAcceptKeyPressedSignal", _textManager, "OnAcceptKeySignal");
            EmitSignal("DialogEndedSignal");
            QueueFree();
        }
    }
}