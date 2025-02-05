using System;
using System.Collections.Generic;
using PopupManagement.Popups;
using UnityEngine;

namespace PopupManagement.Factory
{
    [Serializable]
    public class FactorySetting
    {
        [Header("Required components")]
        [SerializeField] private Transform _popupContainer;
        [SerializeField] private List<BasePopup> _popupPrefabs;

        public PopupFactory InitializeFactory()
        {
            return new PopupFactory(_popupContainer, _popupPrefabs);
        }
    }
}