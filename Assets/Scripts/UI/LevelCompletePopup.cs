using DG.Tweening;
using UnityEngine;

public class LevelCompletePopup
    : MonoBehaviour
{
    [SerializeField]
    private GameObject root;

    [SerializeField]
    private LevelManager
        levelManager;

    public void Show()
    {
        root.SetActive(false);

        DOVirtual.DelayedCall(2.5f, () =>
        {
            root.SetActive(true);
        });
    }

    public void Hide()
    {
        root.SetActive(false);
    }

    public void OnNextClicked()
    {
        levelManager.LoadNextLevel();
    }
}