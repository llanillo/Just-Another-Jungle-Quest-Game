using System;
using Godot;
using Justanotherjunglequestgame.Scripts.System;

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

        // private PlayerInput _playerInput;
        private PlayerStatus _playerStatus;
        private EventManager _eventManager;
        private HBoxContainer _hBoxDialogContainer;
        private TextureRect _portraitRect;
        private TextManager _textManager;
        
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
            _hBoxDialogContainer = GetNode<HBoxContainer>("Rect/HBox/") ?? throw new ArgumentNullException(nameof(_hBoxDialogContainer));
            _portraitRect = GetNode<TextureRect>("Rect/HBox/TextureRect") ?? throw new ArgumentNullException(nameof(_portraitRect));
            _textManager = GetNode<TextManager>("Rect/HBox/VBox") ?? throw new ArgumentNullException(nameof(_textManager));
            _playerStatus = GetNode<PlayerStatus>("/root/PlayerStatus") ?? throw new ArgumentNullException(nameof(_playerStatus));
            _eventManager = GetNode<EventManager>("/root/EventManager") ?? throw new ArgumentNullException(nameof(_eventManager));
            
            _textManager.NextDialogFunc = GD.FuncRef(this, "ShowNextDialog");
        }

        public override void StartDialog(string jsonPath)
        {
            base.StartDialog(jsonPath);

            if ((_eventManager is null) || (_playerStatus is null)) return;

            _playerStatus.CanMove = false;
            _eventManager.Connect(EventManager.AcceptKeySignal, _textManager, TextManager.NextTextSignal);
            
            ShowNextDialog();
        }

        /*
         * Display text in the dialog box while there' still text in the queue.
         * Loads the portrait image and position according to the json values and
         * assigns it to the TextureRect
         */
        private void ShowNextDialog()
        {
            if (DialoguesQueue.Count > 0)
            {
                var dialogue = DialoguesQueue.Dequeue();
                var portraitName = (string) dialogue[PortraitJsonProperty] + PortraitExtension;
                        
                switch (int.Parse((string) dialogue[PositionJsonProperty]))
                {
                    case 0:
                        _hBoxDialogContainer.MoveChild(_portraitRect, LeftPosition);
                        break;
                    case 1:
                        _hBoxDialogContainer.MoveChild(_portraitRect, RightPosition);
                        break;
                }
                        
                _portraitRect.Visible = true;
                _portraitRect.Texture = (Texture) GD.Load(PortraitSpritePath + portraitName);
                _textManager.DisplayText((string) dialogue[TextJsonProperty]);
            }
            else
            {
                _playerStatus.CanMove = true;
                _eventManager.Disconnect(EventManager.AcceptKeySignal, _textManager, TextManager.NextTextSignal);
                QueueFree();
            }
        }
    }
}