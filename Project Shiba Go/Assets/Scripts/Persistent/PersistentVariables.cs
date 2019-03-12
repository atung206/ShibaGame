// Persistent variables

using UnityEngine;
using System;

namespace Persistent
{
    public partial class Persistent
    {
        // save/load data
        public string saveDirectory;
        public string saveFile;
        public bool loadedGame = false;
        public Vector3 loadedSpawnPoint = new Vector3(-120, -19, 0);

        // player
        private Character.Player player;

        // transition data
        public bool thruPortal = false;
        public string destinationPortal;
        public string currentStage = "Start Scene";
        public string nextStage;

        [Serializable]
        public class LocalGameData
        {
            // player data
            public float positionX = persistInstance.player.transform.position.x;
            public float positionY = persistInstance.player.transform.position.y;
            public float positionZ = persistInstance.player.transform.position.z;
            public bool canDoubleJump = persistInstance.player.canDoubleJump;

            // game data
            public string sceneID = persistInstance.currentStage;
        }
    }
}
