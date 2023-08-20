using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runningboy.Manager;
using Runningboy.Utility;
using Runningboy.Map;
using Cinemachine;

namespace Runningboy.Manager
{
    public class MapManager : Singleton<MapManager>
    {
        #region Section

        // TODO: ���� �޴��� ���� �������� ���� ���� �� �ٽ� ���� �ʿ�. Ȥ�� ���� ����� ��찡 ������ ���� �����ο� �Ű��� ��.
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

            if (sections.Count == 1 || (spareSection != null && spareSection != section))
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