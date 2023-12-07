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
        
        public static int GetCurrentLevelId()
        {
            return PlayerPrefs.GetInt("CurrentLevelId", 0);
        }
        
        public static void SetCurrentLevelId(int value)
        {
            PlayerPrefs.SetInt("CurrentLevelId", value);
        }
        
    }
    
}

