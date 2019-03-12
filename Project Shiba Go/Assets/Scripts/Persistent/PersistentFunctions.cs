// Persistent functions

using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Persistent
{
    public partial class Persistent
    {
        public void SaveGameData()
        {
            player = Character.Player.playerInstance;

            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            var localData = new LocalGameData();

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Create(saveFile);
            formatter.Serialize(file, localData);

            file.Close();
        }

        public void LoadGameData()
        {
            loadedGame = true;

            if (File.Exists(saveFile))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.OpenRead(saveFile);
                var loadedGameData = (LocalGameData)formatter.Deserialize(file);
                file.Close();

                player = Character.Player.playerInstance;

                loadedSpawnPoint = new Vector3(loadedGameData.positionX, loadedGameData.positionY, loadedGameData.positionZ);
                player.canDoubleJump = loadedGameData.canDoubleJump;

                persistInstance.nextStage = loadedGameData.sceneID;
            }
            else
            {
                persistInstance.nextStage = "Start Scene";
            }

            SceneManager.LoadScene("Transition Scene");
        }

        public void SpawnOnLoadGame()
        {
            player = Character.Player.playerInstance;
            player.transform.position = loadedSpawnPoint;
            Camera.main.transform.position = loadedSpawnPoint;
        }
    }
}