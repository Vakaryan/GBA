using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace p28
{
    public class TestFocus : MonoBehaviour
    {
        public int currentFocus = 0;
        private void Start()
        {
            InvokeRepeating("ChangeFocus", 0.1f, 5f);
        }

        private void ChangeFocus()
        {
            int incr = 1;
            if(currentFocus > 10) { incr = -1; }
            else if(currentFocus < -5) { incr = 1; }
            currentFocus += incr;
            UIManager.Instance.DisplayFocus(currentFocus);
        }
    }
}
