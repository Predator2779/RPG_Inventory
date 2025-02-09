using System;
using System.Collections.Generic;
using System.IO;
using SaveSystem.SaveServices;
using UnityEngine;

namespace SaveSystem.Main
{
    public class GameStateSaver : MonoBehaviour
    {
        private const string FileName = "gamestate.save";
        private readonly List<ISaveService> _saveServices = new();
        private string _savePath;

        public void Init()
        {
            _savePath = Path.Combine(Application.persistentDataPath, FileName);
            Load();
        }
        
        private void OnApplicationPause(bool pauseStatus) => Save();
        private void OnApplicationQuit() => Save();

        [ContextMenu("Save")]
        public void Save()
        {
            var saveData = new SaveData();

            foreach (var service in _saveServices)
                saveData = service.Save(saveData);

            PutSaveData(saveData, _savePath);
            Debug.Log($"All services saved data.");
        }

        [ContextMenu("Load")]
        public void Load()
        {
            var saveData = GetSaveData(_savePath);

            foreach (var service in _saveServices)
                service.Load(saveData);

            Debug.Log($"All services loaded data.");
        }

        public void RegisterSaver(ISaveService saveService)
        {
            if (_saveServices.Contains(saveService))
            {
                Debug.LogWarning("This ISaver object already exists!");
                return;
            }

            _saveServices.Add(saveService);
            Debug.Log($"Registered save service: {saveService.GetType().Name}");
        }

        public void RemoveSaver(ISaveService saveService)
        {
            if (!_saveServices.Contains(saveService))
            {
                Debug.LogWarning("This ISaver object does not exist!");
                return;
            }

            _saveServices.Remove(saveService);
            Debug.Log($"Removed save service: {saveService.GetType().Name}");
        }

        private void PutSaveData(SaveData saveData, string savePath)
        {
            try
            {
                string json = JsonUtility.ToJson(saveData, true);
                string encryptedData = EncryptionUtility.Encrypt(json);

                using (var writer = new StreamWriter(savePath, false))
                    writer.Write(json);

                Debug.Log($"Data saved to {savePath}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error saving data: {ex.Message}");
            }
        }

        private SaveData GetSaveData(string savePath)
        {
            try
            {
                if (!File.Exists(savePath))
                {
                    Debug.LogWarning("Save file not found, returning new SaveData instance.");
                    return new SaveData();
                }

                using (var reader = new StreamReader(savePath))
                {
                    string encryptedData = reader.ReadToEnd();
                    string json = EncryptionUtility.Decrypt(encryptedData);
                    Debug.Log($"Data readed and encrypted");
                    return JsonUtility.FromJson<SaveData>(encryptedData);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error loading data: {ex.Message}");
                return new SaveData();
            }
        }
    }
}
