using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class Sfx : MonoBehaviour
    {
        private AudioSource audioSrc;
        private int count = 0;

        [SerializeField] private AudioClip[] walkingSfx;
        [SerializeField] private AudioClip[] jumpingSfx;

        private void Start()
        {
            audioSrc = GetComponent<AudioSource>();
        }

        private void Update()
        {
            HorizontalMovement();
            VerticalMovement();
        }

        private void HorizontalMovement()
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                if (count >= walkingSfx.Length)
                {
                    count = 0;
                }
                if (!audioSrc.isPlaying)
                {
                    audioSrc.clip = walkingSfx[count];
                    audioSrc.Play();

                    count += 1;
                }
            }
        }

        private void VerticalMovement()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Character.Player.playerInstance.isGrounded)
                {
                    audioSrc.clip = jumpingSfx[0];
                    audioSrc.Play();
                }
                else
                {
                    if (Character.Player.playerInstance.canDoubleJump)
                    {
                        audioSrc.clip = jumpingSfx[1];
                        audioSrc.Play();
                    }
                }
            }
        }
    }
}