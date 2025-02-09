using SaveSystem.Main;

namespace SaveSystem.Services
{
    public interface ISaveService
    {
        public SaveData Save(SaveData saveData);
        public void Load(SaveData saveData);
    }
}