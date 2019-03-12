using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

namespace MainMenu
{
    public class ButtonManager : MonoBehaviour
    {
        [SerializeField] private Persistent.Persistent persistInstance;

        public void StartBtn()
        {
            string dbgPrefix = "start";
            string dbgMessage = "No save file detected";

            if (File.Exists(persistInstance.saveFile)) {
                dbgMessage = "Loading game from save file";
                persistInstance.LoadGameData();
            }

            Debug.Log(dbgPrefix + ": " + dbgMessage);
        }

        public void StartNewBtn()
        {
            string dbgPrefix = "start.new";

            if (File.Exists(persistInstance.saveFile))
            {
                Debug.Log(dbgPrefix + ": " + "deleting existing game file");
                File.Delete(persistInstance.saveFile);
            }

            Debug.Log(dbgPrefix + ": " + "starting new game file");
            persistInstance.LoadGameData();
        }
    }
}