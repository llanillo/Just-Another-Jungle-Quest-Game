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

        private const string ActionSignalName = "ActionKeyPressedSignal";
        private Sprite _actionKeySprite;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
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
            playerManager.PlayerInput.Connect(ActionSignalName, this, "OnActionKeySignal", new Array { playerManager });
        }

        /*
         * Called on body exited signal from Area2D 
         */
        private void OnBodyExited(Node body)
        {
            if (!(body is PlayerManager playerManager)) return;
            
            _actionKeySprite.Visible = false;
            playerManager.PlayerInput.Disconnect(ActionSignalName, this, "OnActionKeySignal");
        }

        /*
         * Called on action key pressed signal from player input
         */
        private void OnActionKeySignal(PlayerManager playerManager)
        {
            if (!(_dialogScene?.Instance() is DialogManager dialogInstance)) return;
            if (!new File().FileExists(_jsonDialogPath)) return;
            
            GetTree().Root.AddChild(dialogInstance);
            dialogInstance.StartDialog(_jsonDialogPath, playerManager.PlayerInput);
        }
    }
}
