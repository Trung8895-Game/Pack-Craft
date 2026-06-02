using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private LevelDatabase database;
    public LevelDatabase Database => database;

    private async void Awake()
    {
        database = await AddressableManager.LoadAssetAsync<LevelDatabase>(AddressableKeys.LevelDatabase);
    }
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