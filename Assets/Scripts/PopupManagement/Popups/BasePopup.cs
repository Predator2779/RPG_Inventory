using Equipment;
using UnityEngine;

namespace PopupManagement.Popups
{
    public abstract class BasePopup : MonoBehaviour
    {
        protected EquipmentController _equipmentController;

        public void Initialize(EquipmentController equipmentController)
        {
            _equipmentController = equipmentController;
        }
        
        public virtual void Show(object data)
        {
            gameObject.SetActive(true);
            Setup(data);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        protected abstract void Setup(object data);
    }
}