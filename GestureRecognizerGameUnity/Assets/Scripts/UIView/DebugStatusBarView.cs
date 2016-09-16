using strange.extensions.mediation.impl;
using UnityEngine.UI;

namespace UIView
{
    public class DebugStatusBarView : View
    {
        public Text StatusText;

        public void UpdateText(string s)
        {
            StatusText.text = s;
        }
    }
}