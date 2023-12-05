using UnityEngine;

namespace Levels
{
    public class StageEndBasket : MonoBehaviour
    {
        [SerializeField] private Stage stage;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                stage.UpdateBasketCounter();
            }
        }
        
    }
    
}

