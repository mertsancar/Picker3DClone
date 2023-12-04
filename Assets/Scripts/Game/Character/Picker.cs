using System.Collections.Generic;
using Game.Collectables;
using UnityEngine;

namespace Game.Character
{
    public class Picker : MonoBehaviour
    {
        public List<GameObject> collectedItems;

        public void PushCollectedItems()
        {
            foreach (var t in collectedItems)
            {
                t.GetComponent<Rigidbody>().AddForce(Vector3.forward * 350);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable")) collectedItems.Add(other.gameObject);
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Collectable")) collectedItems.Remove(other.gameObject);
        }
    }
    
}

