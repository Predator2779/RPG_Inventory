using SaveSystem.Main;

namespace SaveSystem.SaveServices
{
    public interface ISaveService
    {
        public SaveData Save(SaveData saveData);
        public void Load(SaveData saveData);
    }
}