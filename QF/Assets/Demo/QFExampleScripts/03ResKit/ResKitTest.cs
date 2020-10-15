using System;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;
/// <summary>
/// ResKit模块：
/// 1.优点：开发简便，开发模式和非开发模式的设计使得打包十分方便,资源加载的操作也十分简单，直接使用api即可，无需关心底层的实现
/// 2.缺点：加载大型资源时无法获取实现进度，不过这个可以用Loading代替进度显示，也是一种解决方案
/// 3.总体来说还行，中小型项目非热更项目可以使用，若是热更项目，建议使用XAsset或者等QF的热更
/// 4.暂时还不知道怎么自定义加载ab包路径，可能需要修改源码，作者说可以自己通过继承IRes实现自定义加载ab(1.自定义路径加载ab已解决，在FileMgr脚本的GetFileInInner函数里,将Assets同级目录资源AssetBundles放在指定路径即可)
/// </summary>
public class ResKitTest : MonoBehaviour
{
    /// <summary>
    /// 1.每一个需要加载资源的单元（脚本、界面）申请一个 ResLoader
    /// 2.ResLoader 本身会记录该脚本加载过的资源
    /// </summary>
    private ResLoader mResLoader = ResLoader.Allocate ();
    /// <summary>
    /// Loading显示
    /// </summary>
    [SerializeField]private GameObject loadGame;
    /// <summary>
    /// png资源名
    /// </summary>
    [SerializeField]private List<string> resPngName;
    /// <summary>
    /// prefab资源名
    /// </summary>
    [SerializeField]private List<string> resPrefabName;
    /// <summary>
    /// audio资源名
    /// </summary>
    [SerializeField]private List<string> resAudioName;
    
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.unityLogger.logEnabled = true;
    }
    
    void Start()
    {
        //全局只需要初始化一次
        ResMgr.Init(FilePath.PersistentDataPath+"AssetBundles/");
        loadGame.SetActive(false);
        Debug.Log($"{FilePath.PersistentDataPath}");
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width/2,0,150,50),"同步加载Png" ))
        {
            foreach (var res in resPngName)
            {
                var game=new GameObject(res);
                game.transform.position = Random.insideUnitSphere * 5;
                var spriteRenderer=game.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite=LoadRes<Sprite>(res);
            }
        }
        if (GUI.Button(new Rect(Screen.width/2,50,150,50),"同步加载audio" ))
        {
            var game=new GameObject("sleepDifficulty");
            var audioSource=game.AddComponent<AudioSource>();
            audioSource.clip=LoadRes<AudioClip>("sleepDifficulty");
            audioSource.Play();
        }
        if (GUI.Button(new Rect(Screen.width/2,100,150,50),"同步加载Prefab" ))
        {
            foreach (var res in resPrefabName)
            {
                var game=Instantiate(LoadRes<GameObject>(res));
                game.transform.position = Random.insideUnitSphere * 5;
            }
        }
        if (GUI.Button(new Rect(Screen.width/2,150,150,50),"异步加载单个audio" ))
        {
            loadGame.SetActive(true);
            LoadResAsyn(resAudioName[2], () =>
            {
                var game=new GameObject(resAudioName[2]);
                var audioSource=game.AddComponent<AudioSource>();
                audioSource.clip=LoadRes<AudioClip>(resAudioName[2]);
                audioSource.Play();
                loadGame.SetActive(false);
            });
        }
        if (GUI.Button(new Rect(Screen.width/2,200,150,50),"异步加载audio队列" ))
        {
            loadGame.SetActive(true);
            LoadResAsyn(resAudioName, () =>
            {
                foreach (var res in resAudioName)
                {
                    var game=new GameObject(res);
                    var audioSource=game.AddComponent<AudioSource>();
                    audioSource.clip=LoadRes<AudioClip>(res);
                    audioSource.Play();
                }
                loadGame.SetActive(false);
            });
        }
        if (GUI.Button(new Rect(Screen.width/2,250,150,50),"同步加载scene" ))
        {
            ResKitSceneLoad.Instance.SceneLoad("ResTest");
        }
        if (GUI.Button(new Rect(Screen.width/2,300,150,50),"异步加载scene" ))
        {
            ResKitSceneLoad.Instance.SceneLoadAsyn("ResKitScene",loadGame);
        }
        if (GUI.Button(new Rect(Screen.width/2,350,150,50),"加载网络资源" ))
        {
            LoaclAndRemoteLoadRes("http://file.liangxiegame.com/296b0166-bdea-47d5-ac87-4b55c91df16f.png");
        }
        if (GUI.Button(new Rect(Screen.width/2,400,150,50),"加载本地资源" ))
        {
            LoaclAndRemoteLoadRes("F:/FTP_Server/VideoImage_File/心灵之春.png");
        }
        if (GUI.Button(new Rect(Screen.width/2,450,150,50),"加载Resources资源" ))
        {
            ResourcesLoadRes("辽阔海岸");
        }
        if (GUI.Button(new Rect(Screen.width/2,500,150,50),"释放资源引用" ))
        {
            DisposeRes();
        }
    }
    /// <summary>
    /// 同步资源加载
    /// </summary>
    /// <param name="resName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private T LoadRes<T>(string resName) where T: UnityEngine.Object
    {
        ResetApply();
        if(string.IsNullOrEmpty(resName))
            return null;
        return mResLoader.LoadSync<T>(resName);
    }
    /// <summary>
    /// 1.异步资源加载(暂时无法获取实时进度)
    /// 2.在编辑器环境下，好像还是同步加载，打包出去才会进行真正的异步加载
    /// </summary>
    /// <param name="resName">加载队列</param>
    private void LoadResAsyn(string resName,Action call)
    {
        ResetApply();
        if(string.IsNullOrEmpty(resName))
            return;
        mResLoader.Add2Load(resName, (succeed, res) =>
        {
            if (succeed)
                call();
        });
        mResLoader.LoadAsync();
    }
    /// <summary>
    /// 1.异步队列资源加载(暂时无法获取实时进度)
    /// 2.在编辑器环境下，好像还是同步加载，打包出去才会进行真正的异步加载
    /// </summary>
    /// <param name="resName">加载队列</param>
    private void LoadResAsyn(List<string> resName,Action call)
    {
        ResetApply();
        if (resName.Count != 0)
        {
            mResLoader.Add2Load(resName);
            mResLoader.LoadAsync(() =>
            {
                call();
            });
        }
    }
    /// <summary>
    /// 从网络或者本地加载资源
    /// </summary>
    /// <param name="url"></param>
    private void LoaclAndRemoteLoadRes(string url)
    {
        ResetApply();
        mResLoader.Add2Load ("netimage:" + url,(succeed,res) => {
            if (succeed)
            {
                var game = new GameObject("netimage");
                game.transform.position = Random.insideUnitSphere * 5;
                var spriteRenderer = game.AddComponent<SpriteRenderer>();
                var texture2D = res.Asset as Texture2D;
                var sprite = Sprite.Create(texture2D,new Rect(0,0,texture2D.width,texture2D.height),Vector2.one * 0.5f);
                spriteRenderer.sprite = sprite;
            }
        });
        mResLoader.LoadAsync(); 
    }
    /// <summary>
    /// 从Resources加载Sprite资源
    /// </summary>
    /// <param name="url"></param>
    private void ResourcesLoadRes(string urlName)
    {
        ResetApply();
        var sprite = mResLoader.LoadSprite ("resources://"+urlName);
        var game = new GameObject("resources");
        game.transform.position = Random.insideUnitSphere * 5;
        var spriteRenderer = game.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
    /// <summary>
    /// 场景销毁
    /// </summary>
    void OnDestroy()
    {
        DisposeRes();
    }
    /// <summary>
    /// 资源释放
    /// </summary>
    private void DisposeRes()
    {
        // 释放所有本脚本加载过的资源
        // 释放只是释放资源的引用
        // 当资源的引用数量为 0 时，会进行真正的资源卸载操作
        if (mResLoader == null)
        {
            Debug.Log($"已释放资源的引用");
            return;
        }
        Debug.Log($"释放资源");
        mResLoader.Recycle2Cache();
        mResLoader = null;
    }
    /// <summary>
    /// 判断重新申请
    /// </summary>
    private void ResetApply()
    {
        if (mResLoader == null)
            mResLoader = ResLoader.Allocate();
    }
}
