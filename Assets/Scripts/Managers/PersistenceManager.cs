using UnityEngine;

namespace Managers
{
    public class PersistenceManager : MonoBehaviour
    {
        public static int GetCurrentLevelNumber()
        {
            return PlayerPrefs.GetInt("CurrentLevelNumber", 1);
        }
        
        public static void SetCurrentLevelNumber(int value)
        {
            PlayerPrefs.SetInt("CurrentLevelNumber", value);
        }
        
    }
    
}

