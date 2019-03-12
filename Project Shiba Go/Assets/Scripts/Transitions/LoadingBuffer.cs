using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Portal
{
    public partial class LoadingBuffer : MonoBehaviour
    {
        private Persistent.Persistent persistVars;



        private void Awake()
        {
            persistVars = GameObject.Find("Persistent").GetComponent<Persistent.Persistent>();
        }
            
        private void FixedUpdate()
        {

            StartCoroutine(SceneJump());
        }

        private IEnumerator SceneJump()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(persistVars.nextStage);
            
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}