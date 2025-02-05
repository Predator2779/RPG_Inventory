using UnityEngine;

namespace Inventory
{
    public class InventorySaver : MonoBehaviour
    {
        private const string SaveKey = "InventoryData";

        public void Save(InventoryData data)
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(SaveKey, json);
        }

        // public InventoryData Load()
        // {
        //     // if (!PlayerPrefs.HasKey(SaveKey)) return new InventoryData();
        //     // string json = PlayerPrefs.GetString(SaveKey);
        //     // return JsonUtility.FromJson<InventoryData>(json);
        // }
    }
}