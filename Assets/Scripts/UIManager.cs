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
        }
    }
}
