using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runningboy.Utility;
using Runningboy.Map;
using Cinemachine;
using Sirenix.OdinInspector;

namespace Runningboy.Manager
{
    public class MapManager : Singleton<MapManager>
    {
        [SerializeField]
        GameObject player;

        // TODO: 메인 메뉴를 통한 스테이지 진입 구현 후 다시 수정 필요. 혹은 맵을 벗어나는 경우가 없도록 레벨 디자인에 신경써야 됨.
        private Dictionary<SectionData, Section> sectionDic = new Dictionary<SectionData, Section>();
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

        private void Start()
        {
            var sections = GetComponentsInChildren<Section>();
            foreach (var section in sections)
            {
                var data = section.Init();
                if (data != null)
                {
                    sectionDic.Add(data.Value, section);
                }
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

        [Button]
        public bool SetMap(byte sectorNum, byte sectionNum)
        {
            SectionData key = new SectionData(sectorNum, sectionNum);

            if (sectionDic.TryGetValue(key, out Section section))
            {
                player.SetActive(false);
                
                section.ReturnToCheckPoint(player.transform);
                
                foreach (var _section in sections)
                {
                    _section.SetActiveTileMap(false);
                }
                sections.Clear();
                spareSection?.SetActiveTileMap(false);
                spareSection = null;

                player.SetActive(true);

                return true;
            }

            return false;
        }
    }
}