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
                    Debug.Log($"Slot {slot.name} is empty");
                    continue;
                }

                saveData.equipedItems.Add(new EquipSlotData(slot.name, slot.GetEquippedItem()));
                Debug.Log($"Item {slot.GetEquippedItem().Name} saved to slot {slot.name}");
            }

            Debug.Log($"Total saved equipment items: {saveData.equipedItems.Count}");
            return saveData;
        }


        public void Load(SaveData saveData)
        {
            if (saveData.equipedItems == null || saveData.equipedItems.Count == 0)
            {
                Debug.LogWarning("No saved equipment items!");
                return;
            }

            Debug.Log($"Loading {saveData.equipedItems.Count} equipment items...");

            foreach (var slot in _slots)
            {
                var slotData = saveData.equipedItems.FirstOrDefault(x => x.identrifier == slot.name);

                if (slotData != null && slotData.item != null)
                {
                    slot.Equip(slotData.item);
                    Debug.Log($"{slotData.item.Name} equip to {slot.name}");
                }
                else
                {
                    Debug.Log($"Item not found for slot {slot.name}");
                }
            }

            Debug.Log("All items successfully loaded.");
        }

    }
}