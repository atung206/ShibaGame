// 

using UnityEngine;

namespace MainCamera
{
    public class CameraTrail : MonoBehaviour
    {
        private float lagSpeed = 0.125f;
        private static CameraTrail cam;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            if (cam == null)
            {
                cam = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void FixedUpdate()
        {
            var target = GameObject.FindWithTag("Player").GetComponent<Character.Player>();
            var offset = new Vector3(0f, 0f, -10f);

            Vector3 cameraPos = target.transform.position + offset;
            Vector3 smoothPos = Vector3.Lerp(transform.position, cameraPos, lagSpeed);

            transform.position = smoothPos;
        }
    }
}
