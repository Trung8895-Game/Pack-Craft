using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScale : MonoBehaviour
{
    
    public GameObject Percents;
    private Vector3 LocalScale;
    public int velocity ;
    // Start is called before the first frame update
    private async void Awake()
    {
        LocalScale = Percents.transform.localScale;
        await RunLoading();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public async UniTask RunLoading()
    {
         await CatalogUpdater.CheckForUpdates();
        string[] labels={"Items","Loots","Database","Recipe","Goal","LevelGoal","LevelDifinition","Icon"};
        long totalSize= await DownloadManager.GetTotalDownloadSize(labels);
        Debug.Log("totalSize: " + totalSize);
        if(totalSize==0)
        {
            await UniTask.Delay(2000);
            LocalScale.x=1f;
            Percents.transform.localScale = LocalScale;
        }
        else
        {
           
        
            await DownloadManager.DownloadLabels(labels,
        progress =>
        {
            Debug.Log("Progress: " + progress);
            LocalScale.x =
                progress;
            Percents.transform.localScale = LocalScale;
        });
           
        }
        await AddressablePreloader.Preload();
         if(LocalScale.x==1f)
        {
            SceneManager.LoadScene("Main");
        }
        
        
    }


}
