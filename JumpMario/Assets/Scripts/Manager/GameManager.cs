using System;
using UnityEngine;
using Runningboy.Utility;
using Runningboy.Collection;
using Cinemachine;
using Sirenix.OdinInspector;

namespace Runningboy.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        #region Drag Event

        public event EventHandler onBeginDrag;
        public event EventHandler onDuringDrag;
        public event EventHandler onEndDrag;

        readonly DragCallbackArgs dragCallbackArgs = new DragCallbackArgs();

        [Header("Drag Event")]
        [SerializeField]
        private bool _reverse = false;

        public void OnBeginDrag(in object sender, in Vector2 start, in Vector2 current)
        {
            dragCallbackArgs.startScreenPosition = start;
            dragCallbackArgs.currentScreenPosition = current;
            dragCallbackArgs.reverse = _reverse;
            onBeginDrag?.Invoke(sender, dragCallbackArgs);
        }

        public void OnDuringDrag(in object sender, in Vector2 start, in Vector2 current)
        {
            dragCallbackArgs.startScreenPosition = start;
            dragCallbackArgs.currentScreenPosition = current;
            dragCallbackArgs.reverse = _reverse;
            onDuringDrag?.Invoke(sender, dragCallbackArgs);
        }

        public void OnEndDrag(in object sender, in Vector2 start, in Vector2 current)
        {
            dragCallbackArgs.startScreenPosition = start;
            dragCallbackArgs.currentScreenPosition = current;
            dragCallbackArgs.reverse = _reverse;
            onEndDrag?.Invoke(sender, dragCallbackArgs);
        }

        #endregion

        #region Section

        [Header("Section")]
        [SerializeField, ReadOnly]
        private Section currentSection;
        [SerializeField, ReadOnly]
        private Section nextSection;

        private CinemachineConfiner2D _confiner2D;
        public CinemachineConfiner2D confiner2D
        {
            get
            {
                if (_confiner2D == null)
                {
                    _confiner2D = FindObjectOfType<CinemachineConfiner2D>();
                }

                return _confiner2D;
            }
            set
            {
                _confiner2D = value;
            }
        }

        public void RegistNextSection(Section section)
        {
            if (section == null)
            {
                Debug.LogError("The next section is null.");
                return;
            }
            else if (currentSection == null) // For test
            {
                currentSection = section;
            }

            nextSection = section;
        }

        public void ChangeSection(Section previousSection)
        {
            if (previousSection == null || previousSection != currentSection)
                return;

            currentSection = nextSection;
            nextSection = null;

            if (currentSection != null)
            {
                confiner2D.m_BoundingShape2D = currentSection.GetComponent<Collider2D>();
            }
        }

        #endregion
    }
}
