using Godot;
using File = System.IO.File;
using Justanotherjunglequestgame.Scripts.Dialog;
using Justanotherjunglequestgame.Scripts.Player.Controllers.Manager;

namespace Justanotherjunglequestgame.Scripts.NPC
{
    public class Npc : Node
    {
        [Export(PropertyHint.Dir)] private string _jsonDialogPath;
        [Export] private PackedScene _dialogScene;
        
        private const string ActionSignalName = "ActionKeySignal";
        private Sprite _actionKeySprite;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _actionKeySprite = GetNode<Sprite>("Sprite_ActionKey");
            _actionKeySprite.Visible = false;

            Connect("body_entered", this, "OnBodyEntered");
            Connect("body_exited", this, "OnBodyExited");
        }

        private void OnBodyEntered(Node body)
        {
            if (!(body is PlayerManager playerManager)) return;
            
            _actionKeySprite.Visible = true;
            playerManager.PlayerInput.Connect(ActionSignalName, this, "OnActionKeySignal");
            playerManager.PlayerInput.bCanMove = false;
        }

        private void OnBodyExited(Node body)
        {
            if (!(body is PlayerManager playerManager)) return;
            
            _actionKeySprite.Visible = false;
            playerManager.PlayerInput.Disconnect(ActionSignalName, this, "OnActionKeySignal");
        }

        private void OnActionKeySignal()
        {
            if (!(_dialogScene?.Instance() is DialogManager dialogInstance)) return;
            if (!File.Exists(_jsonDialogPath)) return;

            GetTree().Root.AddChild(dialogInstance);
            dialogInstance.StartDialog(_jsonDialogPath);
        }
    }
}
