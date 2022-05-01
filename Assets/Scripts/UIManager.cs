using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace p28
{
    public class UIManager : MonoBehaviour
    {
        public GameObject startingPanel;
        public Slider focusGauge;

        #region Singleton
        public static UIManager Instance;
        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(this); }
            else { Instance = this; }
        }
        #endregion

        private void Start()
        {
            focusGauge.gameObject.SetActive(false);
            startingPanel.SetActive(true);
            StartCoroutine(StartPanelTimer());
        }

        /// <summary>
        /// Let the start panel visible for 3 seconds then hides it
        /// </summary>
        /// <returns></returns>
        IEnumerator StartPanelTimer()
        {
            yield return new WaitForSeconds(3);
            startingPanel.SetActive(false);
            SoundManager.Instance.PlayBackgroundMusic();
            focusGauge.gameObject.SetActive(true);
        }


        /// <summary>
        /// Set the gauge to the set focus
        /// Values go between 0 and 5
        /// </summary>
        /// <param name="focusRate"></param>
        public void DisplayFocus(int focusRate)
        {
            int val = Mathf.Clamp(focusRate, 0, 5);
            focusGauge.value = val;
        }
    }
}
