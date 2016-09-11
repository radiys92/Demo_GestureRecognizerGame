using strange.extensions.mediation.impl;
using UnityEngine.UI;

public class DebugStatusBarView : View
{
    public Text StatusText;

    public void UpdateText(string s)
    {
        StatusText.text = s;
    }
}