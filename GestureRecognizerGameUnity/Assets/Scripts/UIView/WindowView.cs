using strange.extensions.mediation.impl;
using UnityEngine.Events;

namespace UIView
{
    public class WindowEvent : UnityEvent { }

    public class WindowView : View
    {
        public readonly UnityEvent OnShow = new WindowEvent();
        public readonly UnityEvent OnHide = new WindowEvent();

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        protected void Awake()
        {
            Hide();
        }
    }
}