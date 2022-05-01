using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace p28
{
    // Sound Manager class for music and FX
    public class SoundManager : MonoBehaviour
    {
        public AudioSource backgroundMusic;

        #region Singleton
        public static SoundManager Instance;
        private void Awake()
        {
            if(Instance != null && Instance != this) { Destroy(this); }
            else { Instance = this; }
        }
        #endregion


        public void PlayBackgroundMusic()
        {
            backgroundMusic.Play();
        }
    }
}
