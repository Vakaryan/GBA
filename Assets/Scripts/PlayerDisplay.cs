using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace p28
{
    public class PlayerDisplay : MonoBehaviour
    {
        // VFX 
        public GameObject destroyEffect;
        public GameObject restoreEffect;


        private void Start()
        {
            destroyEffect.SetActive(false);
            restoreEffect.SetActive(false);
        }

        /// <summary>
        /// Launch destroy effect
        /// </summary>
        public void DestroyEffect()
        {
            destroyEffect.SetActive(true);
            StartCoroutine(EffectTimer());
        }

        /// <summary>
        /// Launch restore effect
        /// </summary>
        public void RestoreEffect()
        {
            restoreEffect.SetActive(true);
            StartCoroutine(EffectTimer());
        }


        /// <summary>
        /// Reset effects
        /// </summary>
        /// <returns></returns>
        IEnumerator EffectTimer()
        {
            yield return new WaitForSeconds(1.5f);
            destroyEffect.SetActive(false);
            restoreEffect.SetActive(false);
        }
    }
}
