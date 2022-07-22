using System.Collections.Generic;
using Godot;
using Godot.Collections;
using Justanotherjunglequestgame.Scripts.Player.Controllers.Input;
using File = System.IO.File;

namespace Justanotherjunglequestgame.Scripts.Dialog
{
    public abstract class Dialog : Node
    {
        [Signal] public delegate void DialogEndedSignal();
        
        private protected const string PortraitPrefixPath = "Assets/Sprites/";
        private protected readonly Queue<Dictionary> DialoguesQueue = new Queue<Dictionary>();

        /*
         * Starts the dialog box with the json from the jsonPath
         */
        public virtual void StartDialog(string jsonPath, PlayerInput playerInput)
        {
            Dictionary dictionary = LoadDialogueFromJson(jsonPath);
            for (var i = 0; i < dictionary.Count; i++)
            {
                var dialogue = dictionary[i.ToString()] as Dictionary;
                DialoguesQueue.Enqueue(dialogue);
            }
        }
        
        /*
         * Loads the dialogues from the json file and returns it as a dictionary
         */
        private static Dictionary LoadDialogueFromJson(string jsonPath)
        {
            if (!File.Exists(jsonPath)) return new Dictionary();

            string jsonValues = File.ReadAllText(jsonPath);
            JSONParseResult jsonResult = JSON.Parse(jsonValues);

            if (jsonResult.Error != Error.Ok)
            {
                return jsonResult.Result as Dictionary;
            }

            GD.Print(jsonPath + "can't be parsed to Json\nError: " + jsonResult.ErrorString);
            return new Dictionary();
        }
    }
}
