using UnityEngine;

namespace Managers
{
    public class PersistenceManager : MonoBehaviour
    {
        public static int GetCurrentLevelIndex()
        {
            return PlayerPrefs.GetInt("CurrentLevelIndex", 0);
        }
        
        public static void SetCurrentLevelIndex(int value)
        {
            PlayerPrefs.SetInt("CurrentLevelIndex", value);
        }
        
    }
    
}

