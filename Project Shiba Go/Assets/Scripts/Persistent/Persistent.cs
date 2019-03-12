// Persistent variables and functions that follow around the player and persists
// throughout the game. 

using UnityEngine;
using System.Collections;

namespace Persistent
{
    public partial class Persistent : MonoBehaviour
    {
        private static Persistent persistInstance;



        private void Awake()
        {
            if (persistInstance == null)
            {
                DontDestroyOnLoad(this);
                persistInstance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            saveDirectory = Application.persistentDataPath + "/saved_game_data";
            saveFile = saveDirectory + "/ShibaGameSave.bin";
        }
    }
}