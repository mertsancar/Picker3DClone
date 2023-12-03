using System;
using UnityEngine;

namespace Levels
{
    public class StageEndBasket : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                EventManager.instance.TriggerEvent(EventNames.UpdateBasketCounter);
            }
        }
        
    }
    
}

