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
        private readonly ItemData[] _loot;
        private readonly InventoryService _inventoryService;

        public GameStateController(
            InventoryService inventoryService, 
            EndPopup endPopup, 
            HealthProcessor heroHealth, 
            HealthProcessor enemyHealth,
            ItemData[] loot)
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
            var item = _loot[rand].item;
        
            _inventoryService.AddItem(item);
            _endPopup.Show($"Congratulate! You win! {item.Name} added to your inventory");
            _enemyHealth.TakeHeal(100);
            Debug.Log("Enemy regenerated");
        }

        private void Lose()
        {
            _endPopup.Show("I'm sorry, but you lost. Better luck next time.");
            _heroHealth.TakeHeal(100);
            Debug.Log("Health restored");
            _enemyHealth.TakeHeal(100);
            Debug.Log("Enemy regenerated");
        }
    }
}