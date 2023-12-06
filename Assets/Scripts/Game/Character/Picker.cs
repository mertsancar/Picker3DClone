using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public class Picker : MonoBehaviour
    {
        private List<GameObject> collectedItems = new List<GameObject>();

        public void PushCollectedItems()
        {
            foreach (var t in collectedItems)
            {
                t.GetComponent<Rigidbody>().AddForce(Vector3.forward * 50);
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

