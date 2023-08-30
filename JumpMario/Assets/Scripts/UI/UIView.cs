using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Runningboy.UI
{
    public class TextData
    {
        public event Action<string> callback;

        private string _text;
        public string text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                callback?.Invoke(value);
            }
        }
    }

    public abstract class UIView : MonoBehaviour
    {
        static Dictionary<string, UIView> _uiViewDictionary = null;
        static Dictionary<string, TextData> _dataBindingTextDictionary = null;

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

        public static void SetValue(string dataID, string data)
        {
            if (_dataBindingTextDictionary == null)
                _dataBindingTextDictionary = new Dictionary<string, TextData>();

            if (!_dataBindingTextDictionary.TryGetValue(dataID, out TextData textData))
            {
                textData = new TextData();
                _dataBindingTextDictionary.Add(dataID, textData);
            }

            textData.text = data;
        }

        public static TextData GetValue(string dataID)
        {
            if (_dataBindingTextDictionary == null)
                _dataBindingTextDictionary = new Dictionary<string, TextData>();

            if (!_dataBindingTextDictionary.TryGetValue(dataID, out TextData textData))
            {
                textData = new TextData();
                _dataBindingTextDictionary.Add(dataID, textData);
            }

            return textData;
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