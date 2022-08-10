using Godot;

namespace JustAnotherJungleQuestGame.System
{
    public class EventManager : Node
    {
        [Signal] public delegate void ActionKeyPressedSignal();
        [Signal] public delegate void AcceptKeyPressedSignal();

        public const string AcceptKeySignal = "AcceptKeyPressedSignal";
        public const string ActionKeySignal = "ActionKeyPressedSignal";
    }
}
