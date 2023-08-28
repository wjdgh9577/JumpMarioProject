using System.Collections.Generic;
using UnityEngine;

namespace Runningboy.UI
{
    public abstract class UIView : MonoBehaviour
    {
        static Dictionary<string, UIView> _uiViewDictionary = null;

        [SerializeField]
        string _viewID;

        #region Virtual

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        #endregion

        #region public

        public static bool TryGetView(string viewID, out UIView view)
        {
            if (_uiViewDictionary == null)
            {
                InitUIViewDictionary();
            }

            return _uiViewDictionary.TryGetValue(viewID, out view);
        }

        public static UIView GetView(string viewID)
        {
            if (TryGetView(viewID, out UIView view))
            {
                return view;
            }
            return null;
        }

        #endregion

        #region Private

        private static void InitUIViewDictionary()
        {
            _uiViewDictionary = new Dictionary<string, UIView>();
            var canvases = FindObjectsOfType<Canvas>();
            foreach (var canvas in canvases)
            {
                var views = canvas.transform.GetComponentsInChildren<UIView>(true);
                foreach (var newView in views)
                {
                    newView.AddUIView();
                }
            }
        }

        private void AddUIView()
        {
            if (string.IsNullOrEmpty(_viewID))
            {
                Debug.LogWarningFormat("Invalid view ID. {0}", gameObject.name);
                _viewID = gameObject.name;
            }

            _uiViewDictionary.Add(_viewID, this);
        }

        #endregion
    }
}