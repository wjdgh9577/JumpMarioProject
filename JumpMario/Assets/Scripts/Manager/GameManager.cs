using System;
using System.Collections.Generic;
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

        private List<Section> sections = new List<Section>();
        private Section spareSection = null;

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
            if (!sections.Contains(section))
            {
                sections.Add(section);
                section.SetActiveTileMap(true);
            }

            if (sections.Count == 1 || spareSection != null)
            {
                confiner2D.m_BoundingShape2D = section.polygonCollider2D;
                spareSection?.SetActiveTileMap(false);
                spareSection = null;
            }
        }

        public void ChangeSection(Section previousSection)
        {
            sections.Remove(previousSection);

            if (sections.Count > 0)
            {
                previousSection.SetActiveTileMap(false);
                confiner2D.m_BoundingShape2D = sections[0].polygonCollider2D;
            }
            else
            {
                spareSection = previousSection;
            }
        }

        #endregion
    }
}
