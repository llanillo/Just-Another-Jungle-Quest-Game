using Godot;
using Godot.Collections;
using Justanotherjunglequestgame.Scripts.Dialog;
using Justanotherjunglequestgame.Scripts.Player.Controllers.Manager;

namespace Justanotherjunglequestgame.Scripts.NPC
{
    public class Npc : Area2D
    {
        [Export(PropertyHint.File)] private string _jsonDialogPath;
        [Export] private PackedScene _dialogScene;

        private const string ActionSignalMethod = "OnActionKeySignal";
        
        private EventManager _eventManager;
        private Sprite _actionKeySprite;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _eventManager = GetNode<EventManager>("/root/EventManager");
            _actionKeySprite = GetNode<Sprite>("Sprite_ActionKey");
            
            _actionKeySprite.Visible = false;

            Connect("body_entered", this, "OnBodyEntered");
            Connect("body_exited", this, "OnBodyExited");
        }

        /*
         * Called on body entered signal from Area2D
         */
        private void OnBodyEntered(Node body)
        {
            if (!(body is PlayerManager playerManager)) return;
            
            _actionKeySprite.Visible = true;
            _eventManager.Connect(EventManager.ActionKeySignal, this, ActionSignalMethod);
        }

        /*
         * Called on body exited signal from Area2D 
         */
        private void OnBodyExited(Node body)
        {
            if (!(body is PlayerManager playerManager)) return;
            
            _actionKeySprite.Visible = false;
            _eventManager.Disconnect(EventManager.ActionKeySignal, this, ActionSignalMethod);
        }

        /*
         * Called on action key pressed signal from player input
         */
        private void OnActionKeySignal()
        {
            if (!(_dialogScene?.Instance() is DialogManager dialogInstance)) return;
            if (!new File().FileExists(_jsonDialogPath)) return;
            
            GetTree().Root.AddChild(dialogInstance);
            dialogInstance.StartDialog(_jsonDialogPath);
        }
    }
}
