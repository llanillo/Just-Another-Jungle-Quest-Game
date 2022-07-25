using System;
using Godot;
using Justanotherjunglequestgame.Scripts.Player.Controllers.Input;
using Justanotherjunglequestgame.Scripts.Player.Controllers.Manager;
using Object = Godot.Object;

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

        private PlayerInput _playerInput;
        private HBoxContainer _hBoxDialogContainer;
        private TextureRect _portraitRect;
        private TextManager _textManager;
        
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
            _hBoxDialogContainer = GetNode<HBoxContainer>("Rect/HBox/");
            _portraitRect = GetNode<TextureRect>("Rect/HBox/TextureRect");
            _textManager = GetNode<TextManager>("Rect/HBox/VBox");
            
            _textManager.NextDialogFunc = GD.FuncRef(this, "ShowNextDialog");
        }

        public override void StartDialog(string jsonPath, Object playerInput)
        {
            base.StartDialog(jsonPath, playerInput);

            _playerInput = playerInput as PlayerInput ?? throw new NullReferenceException("Player reference null");
            _playerInput.CanMove = false;
            _playerInput.Connect("AcceptKeyPressedSignal", _textManager, "OnContinueDialogSignal");
            
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
                _playerInput.CanMove = true;
                _playerInput.Disconnect("AcceptKeyPressedSignal", _textManager, "OnContinueDialogSignal");
                QueueFree();
            }
        }
    }
}