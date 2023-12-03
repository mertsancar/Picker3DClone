using System;
using UnityEngine;

namespace Levels
{
    public class StageEndBasket : MonoBehaviour
    {
        public Stage stage;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                stage.basketCounter++;
                stage.stageBase.basketCounterText.text = stage.basketCounter + "/" + stage.basketCapacity;
            }
        }
        
    }
    
}

