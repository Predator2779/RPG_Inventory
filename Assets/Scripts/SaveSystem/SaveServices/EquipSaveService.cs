using System.Collections.Generic;
using System.Linq;
using Equipment;
using Inventory.Items;
using SaveSystem.Main;
using UnityEngine;

namespace SaveSystem.SaveServices
{
    public class EquipSaveService : ISaveService
    {
        private readonly EquipSlot[] _slots;

        public EquipSaveService(GameStateSaver gameStateSaver, EquipSlot[] slots)
        {
            _slots = slots;
            gameStateSaver.RegisterSaver(this);
        }

        public SaveData Save(SaveData saveData)
        {
            saveData.equipedItems.Clear();

            foreach (var slot in _slots)
            {
                if (slot.GetEquippedItem() is null)
                {
                    Debug.Log($"Слот {slot.name} пуст, пропускаем.");
                    continue;
                }

                saveData.equipedItems.Add(new KeyValuePair<string, Item>(slot.name, slot.GetEquippedItem()));
                Debug.Log($"Сохранён предмет {slot.GetEquippedItem().Name} в слот {slot.name}");
            }

            Debug.Log($"Всего сохранено предметов экипировки: {saveData.equipedItems.Count}");
            return saveData;
        }


        public void Load(SaveData saveData)
        {
            if (saveData.equipedItems == null || saveData.equipedItems.Count == 0)
            {
                Debug.LogWarning("Файл сохранения пуст, экипировка не загружена.");
                return;
            }

            foreach (var slot in _slots)
            {
                var slotName = slot.name;
                var equippedItem = saveData.equipedItems.FirstOrDefault(x => x.Key == slotName).Value;

                if (equippedItem != null)
                {
                    slot.Equip(equippedItem);
                    Debug.Log($"Экипирован предмет {equippedItem.Name} в слот {slot.name}");
                }
                else
                {
                    Debug.Log($"Слот {slot.name} остался пустым.");
                }
            }

            Debug.Log("Все предметы экипировки загружены.");
        }
    }
}