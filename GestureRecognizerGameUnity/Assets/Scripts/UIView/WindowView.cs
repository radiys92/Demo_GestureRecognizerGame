using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.Events;

namespace UIView
{
    public class WindowEvent : UnityEvent { }

    public class WindowView : View
    {
        public bool IsVisible 
        {
            get { return gameObject.activeSelf && gameObject.activeInHierarchy; }
        }

        public readonly UnityEvent OnShow = new WindowEvent();
        public readonly UnityEvent OnHide = new WindowEvent();

        public void SetVisibility(bool isVisible)
        {
            if (gameObject.activeSelf == isVisible)
                return;

            gameObject.SetActive(isVisible);
            if (isVisible)
                OnShow.Invoke();
            else
                OnHide.Invoke();
        }

        protected override void Start()
        {
            base.Start();
            SetVisibility(false);
        }
    }
}