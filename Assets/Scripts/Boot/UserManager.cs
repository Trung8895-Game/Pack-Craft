using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager: MonoBehaviour
{
    public int BestScore;
    public static string NameUser;
    public static UserManager Instance = null;
    // Start is called before the first frame update
   
    private void Awake()
    {
        
        
        if(Instance==null)
        {
            Instance = this;
        }
        else if(Instance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
    public void Load()
    {
        Instance.BestScore = PlayerPrefs.GetInt("BestScoreKey", 0);
        NameUser = PlayerPrefs.GetString("NameUserKey", "");
    }

    public void SetBestScore(int bestScore)
    {
        
        if(Instance.BestScore<bestScore )
        {
            Instance.BestScore = bestScore;

            PlayerPrefs.SetInt("BestScoreKey", Instance.BestScore);
        }
    }

    
    public void Save()
    {
        PlayerPrefs.Save();
    }
}
