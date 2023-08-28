using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runningboy.UI
{
    public class SectorToggle : MonoBehaviour
    {
        [SerializeField]
        Toggle _toggle;
        [SerializeField]
        Text _label;
        [SerializeField, ReadOnly]
        byte _sectorNumber;

        Action onToggle;

        public void OnToggle()
        {
            onToggle?.Invoke();
        }

        public void SetToggle(byte sectorNumber, Action action)
        {
            _sectorNumber = sectorNumber;
            _label.text = $"Sector {_sectorNumber}";
            onToggle = action;
        }

        public void Select()
        {
            _toggle.isOn = true;
        }
    }
}