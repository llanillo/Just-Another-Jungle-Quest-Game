using System;
using System.Collections.Generic;
using System.IO;
using Godot;
using Godot.Collections;
using File = Godot.File;

namespace Justanotherjunglequestgame.Scripts.Dialog
{
    public abstract class Dialog : Node
    {
        private protected const string PortraitSpritePath = "res://Assets/Sprites/Dialog/";
        private protected readonly Queue<Dictionary> DialoguesQueue = new Queue<Dictionary>();

        /*
         * Must be called from any dialog scene instance
         *  to start the dialog box with the json 
         */
        public virtual void StartDialog(string jsonPath)
        {
            var dictionary = LoadDialogueFromJson(jsonPath);
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
            var file = new File();
            if (!file.FileExists(jsonPath)) throw new FileNotFoundException();

            file.Open(jsonPath, File.ModeFlags.Read);
            var jsonValues = file.GetAsText();
            file.Close();
            
            JSONParseResult jsonResult = JSON.Parse(jsonValues);

            if (jsonResult.Error == Error.Ok)
            {
                return jsonResult.Result as Dictionary;
            }

            throw new ApplicationException("Could not parsed JSON");
        }
    }
}
