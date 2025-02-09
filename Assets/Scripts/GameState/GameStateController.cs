using Health;
using Inventory.Items;
using Inventory.Services;
using PopupManagement.Popups;
using UnityEngine;

namespace GameState
{
    public class GameStateController
    {
        private readonly EndPopup _endPopup;
        private readonly HealthProcessor _heroHealth, _enemyHealth;
        private readonly Item[] _loot;
        private readonly InventoryService _inventoryService;

        public GameStateController(
            InventoryService inventoryService, 
            EndPopup endPopup, 
            HealthProcessor heroHealth, 
            HealthProcessor enemyHealth,
            Item[] loot)
        {
            _inventoryService = inventoryService;
            _endPopup = endPopup;
            _heroHealth = heroHealth;
            _enemyHealth = enemyHealth;
            _loot = loot;

            heroHealth.OnHealthIsZero += Lose;
            enemyHealth.OnHealthIsZero += Win;
        }
    
        private void Win()
        {
            var rand = Random.Range(0, _loot.Length);
            var item = _loot[rand].data;
        
            _inventoryService.AddItem(item);
            _endPopup.Show($"Поздравляю! Ты победил! {item.Name} {item.Stack}шт. добавлено в твой инвентарь");
            _enemyHealth.TakeHeal(100);
            Debug.Log("Enemy regenerated");
        }

        private void Lose()
        {
            _endPopup.Show("Извини, но ты проиграл. У тебя все получится, удачи!");
            _heroHealth.TakeHeal(100);
            Debug.Log("Health restored");
            _enemyHealth.TakeHeal(100);
            Debug.Log("Enemy regenerated");
        }
    }
}