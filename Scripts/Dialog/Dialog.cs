using System.Collections.Generic;
using Godot;
using Godot.Collections;
using File = System.IO.File;

namespace Justanotherjunglequestgame.Scripts.Dialog
{
    public abstract class Dialog : Control
    {
        // [Export(PropertyHint.Dir)] public string JsonPath;
        private protected const string PortraitPrefixPath = "Assets/Sprites/";
        private const string JsonPrefixPath = "Assets/Json/";

        private protected readonly Queue<Dictionary> DialoguesQueue = new Queue<Dictionary>();

        /*
         * Starts the dialog box with the json from the jsonPath
         */
        public virtual void StartDialog(string jsonPath)
        {
            Dictionary dictionary = LoadDialogueFromJson(jsonPath);
            for (var i = 0; i < dictionary.Count; i++)
            {
                var dialogue = dictionary[i.ToString()] as Dictionary;
                DialoguesQueue.Enqueue(dialogue);
            }
        }
        
        private static Dictionary LoadDialogueFromJson(string jsonPath)
        {
            var absoluteJsonPath = JsonPrefixPath + jsonPath + ".json";
            if (!File.Exists(absoluteJsonPath)) return new Dictionary();

            string jsonValues = File.ReadAllText(absoluteJsonPath);
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
