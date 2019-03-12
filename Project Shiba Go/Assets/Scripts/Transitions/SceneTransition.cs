using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Portal
{
    public partial class SceneTransition : MonoBehaviour
    {
        private Persistent.Persistent persistVars;

        [SerializeField] private string destination;
        [SerializeField] private GameObject player;



        private void Start()
        {
            persistVars = GameObject.Find("Persistent").GetComponent<Persistent.Persistent>();
            player = GameObject.Find("Player");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                persistVars.thruPortal = true;
                persistVars.nextStage = destination;

                // this will be removed later, because when moving between stages the connecting
                // portals will have the same name.. ideally we're not going to have anything like
                // jumping from one side to the other.. hopefully :) ......
                //
                // ideally the destination portal will have the same name as the portal we went through, so this
                // is the line we'd use to set the destination portal.
                // persistVars.destinationPortal = gameObject.name;
                if (gameObject.name == "Start Left")
                {
                    persistVars.destinationPortal = "Start Right";
                }
                else
                {
                    persistVars.destinationPortal = "Start Left";
                }

                GameObject.Find("Transitions").GetComponent<Animator>().SetTrigger("changeScene");
            }
        }

        public void LoadBufferScene()
        {
            StartCoroutine(AsynchronousSceneLoad());
        }

        private IEnumerator AsynchronousSceneLoad()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Transition Scene");
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        public void SpawnPlayer() { 
            if (persistVars.loadedGame)
            {
                persistVars.SpawnOnLoadGame();
                persistVars.loadedGame = false;
            }
            else
            {
                SpawnAfterPortal();
            }
        }

        private void SpawnAfterPortal()
        {
            if (persistVars.thruPortal && SceneManager.GetActiveScene().name != "Transition Scene")
            {
                GameObject portal = GameObject.Find(persistVars.destinationPortal); 
                Transform spawnPoint = portal.transform.GetChild(0);

                player.transform.position = spawnPoint.position;
                persistVars.thruPortal = false;
                persistVars.destinationPortal = "";
                persistVars.currentStage = persistVars.nextStage;
                persistVars.nextStage = "";

                Camera.main.transform.position = spawnPoint.position;
            }
        }
    }
}