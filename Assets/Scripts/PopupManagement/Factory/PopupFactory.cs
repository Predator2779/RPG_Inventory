using System;
using System.Collections.Generic;
using PopupManagement.Popups;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PopupManagement.Factory
{
    public class PopupFactory
    {
        private readonly Transform _popupContainer;
        private readonly List<BasePopup> _popupPrefabs;
        private readonly Dictionary<Type, BasePopup> _popupInstances = new ();

        public PopupFactory(Transform popupContainer, List<BasePopup> popupPrefabs)
        {
            _popupContainer = popupContainer;
            _popupPrefabs = popupPrefabs;
        }

        public T GetPopup<T>() where T : BasePopup
        {
            Type popupType = typeof(T);

            if (_popupInstances.TryGetValue(popupType, out var popup))
                return popup as T;

            var prefab = _popupPrefabs.Find(p => p is T);
            if (prefab != null)
            {
                var instance = Object.Instantiate(prefab, _popupContainer);
                _popupInstances[popupType] = instance;
                return instance as T;
            }

            Debug.LogError($"Popup of type {popupType} not found!");
            return null;
        }
    }
}