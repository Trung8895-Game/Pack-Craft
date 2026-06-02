using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        //UserManager.Instance.Save();
    }
    void Awake()
    {
       
        DontDestroyOnLoad(this);

    }
    async void Start()
    {
        //UserManager.Instance.Load();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async UniTask DownloadResource()
    {
        

    await DownloadManager
    .DownloadLabel(
        "Items");
    }
}
