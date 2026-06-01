using UnityEngine;

public static class GameProgress
{
    private const string CurrentLevelKey =
        "CURRENT_LEVEL";

    public static int CurrentLevel
    {
        get
        {
            //PlayerPrefs.DeleteAll();
            return PlayerPrefs.GetInt(CurrentLevelKey,0);
        }
        set
        {
            PlayerPrefs.SetInt(CurrentLevelKey,value);

            PlayerPrefs.Save();
        }
    }
}