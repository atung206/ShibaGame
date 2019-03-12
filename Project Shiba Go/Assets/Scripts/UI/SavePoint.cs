using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        string dbgPrefix = "save.point";

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (collision.CompareTag("Player"))
            {
                var persist = GameObject.Find("Persistent").GetComponent<Persistent.Persistent>();
                persist.SaveGameData(); 

                Debug.Log(dbgPrefix + ": " + "game data successfully saved");
            }
        }
    }
}
