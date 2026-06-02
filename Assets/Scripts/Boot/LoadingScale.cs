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
    void Start()
    {
        LocalScale = Percents.transform.localScale;
        RunLoading();
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public async UniTask RunLoading()
    {
        string[] labels={"Items","Loots"};
        long totalSize= await DownloadManager.GetTotalDownloadSize(labels);
        if(totalSize==0)
        {
            await UniTask.Delay(2000);
            LocalScale.x=1f;
            Percents.transform.localScale = LocalScale;
        }
        else
        {
            await CatalogUpdater.CheckForUpdates();
        
        await DownloadManager.DownloadLabels(labels,
        progress =>
        {
            Debug.Log("Progress: " + progress);
            LocalScale.x =
                progress;
            Percents.transform.localScale = LocalScale;
        });
           
        }
         if(LocalScale.x==1f)
        {
            SceneManager.LoadScene("Main");
        }
        
        
    }


}
