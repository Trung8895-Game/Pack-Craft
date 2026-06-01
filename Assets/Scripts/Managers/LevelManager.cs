using UnityEngine;

public class LevelManager
    : MonoBehaviour
{
    [SerializeField]
    private LevelDatabase database;
    public LevelDatabase Database => database;


    public void LoadNextLevel()
    {
        int current = GameProgress.CurrentLevel;

        current++;

        if (current >=database.Levels.Count)
        {
            GameCompleted();

            return;
        }

        GameProgress.CurrentLevel = current;

        SceneLoader.LoadScene(database.Levels[GameProgress.CurrentLevel].SceneName);
    }

    private void GameCompleted()
    {
        Debug.Log("GAME COMPLETED");
        PlayerPrefs.DeleteAll();
        SceneLoader.LoadScene(database.Levels[GameProgress.CurrentLevel].SceneName);
    }
}