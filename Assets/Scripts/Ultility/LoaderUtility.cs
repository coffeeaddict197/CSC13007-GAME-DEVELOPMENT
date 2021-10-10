using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LoaderUtility : MonoSingleton<LoaderUtility>
{

    public string GetText(string scene, string path)
    {
        TextAsset asset = this.GetAsset<TextAsset>(scene, path);
        if (asset == null)
        {
            return string.Empty;
        }
        return asset.text;
    }

    public string GetText(string path)
    {
        return this.GetText("Application", path);
    }

    public void LoadAsync(List<AssetPath> datas, UnityAction unityAction)
    {
        int index = 0;
        int loading = 0;
        foreach (AssetPath assetPath in datas)
        {
            if (!this.HasAsset(assetPath.path))
            {
                loading++;
                base.StartCoroutine(this.GetAsync(assetPath.scene, assetPath.path, delegate
                {
                    index++;
                    if (index >= loading && unityAction != null)
                    {
                        unityAction();
                    }
                }));
            }
        }
        if (index == 0 && unityAction != null)
        {
            unityAction();
        }
    }

    public T GetAsset<T>(string scene, string fileName) where T : Object
    {
        T t = this.TryGetAsset<T>(scene, fileName);
        if (t == null)
        {
            t = this.Load<T>(fileName);
            this.loadeds.Add(new AssetData
            {
                scene = scene,
                path = fileName,
                asset = t
            });
        }
        // ưDebug.Log("LOAD ASSETS: " + t + " PATH: " + fileName);
        return t;
    }

    public T GetAsset<T>(string fileName) where T : Object
    {
        return this.GetAsset<T>("Application", fileName);
    }

    public T GetAssetComponent<T>(string scene, string fileName) where T : Component
    {
        T t = this.TryGetAsset<T>(scene, fileName);
        if (t == null)
        {
            t = this.Load<GameObject>(fileName).GetComponent<T>();
            this.loadeds.Add(new AssetData
            {
                scene = scene,
                path = fileName,
                asset = t
            });
        }
        return t;
    }

    public T GetAssetComponent<T>(string fileName) where T : Component
    {
        return this.GetAssetComponent<T>("Application", fileName);
    }

    public void UnLoad(Object asset)
    {
        List<AssetData> list = (from e in this.loadeds
                                where e.asset.Equals(asset)
                                select e).ToList<AssetData>();
        foreach (AssetData assetData in list)
        {
            this.UnLoad(assetData);
        }
    }

    public void UnLoad(AssetData assetData)
    {
        this.loadeds.Remove(assetData);
    }

    public void UnLoad(string scene, string path)
    {
        IEnumerable<AssetData> enumerable = from e in this.loadeds
                                            where e.IsMatch(scene, path)
                                            select e;
        foreach (AssetData assetData in enumerable)
        {
            this.UnLoad(assetData);
        }
    }

    public void UnLoadScene(string scene)
    {
        List<AssetData> list = (from e in this.loadeds
                                where e.scene.Equals(scene)
                                select e).ToList<AssetData>();
        foreach (AssetData assetData in list)
        {
            this.UnLoad(assetData);
        }
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }

    private T Load<T>(string path) where T : Object
    {
        string directoryName = Path.GetDirectoryName(path);
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
        return Resources.Load<T>(Path.Combine(directoryName, fileNameWithoutExtension));
    }

    private IEnumerator GetAsync(string scene, string path, UnityAction unityAction)
    {
        string folder = Path.GetDirectoryName(path);
        string newFolder = Path.GetFileNameWithoutExtension(path);
        if (!this.HasAsset(scene, path))
        {
            this.loadeds.Add(new AssetData
            {
                scene = scene,
                path = path,
                asset = Resources.Load(Path.Combine(folder, newFolder))
            });
        }
        yield return new WaitForSeconds(0.1f);
        if (unityAction != null)
        {
            unityAction();
        }
        yield break;
    }

    private T TryGetAsset<T>(string scene, string fileName) where T : Object
    {
        AssetData assetData = this.loadeds.Find((AssetData e) => e.IsMatch(scene, fileName));
        if (assetData != null)
        {
            return (T)((object)assetData.asset);
        }
        return (T)((object)null);
    }

    private bool HasAsset(string fileName)
    {
        return this.loadeds.Find((AssetData e) => e.IsMatch(fileName)) != null;
    }

    private bool HasAsset(string scene, string fileName)
    {
        return this.loadeds.Find((AssetData e) => e.IsMatch(scene, fileName)) != null;
    }

    private List<AssetData> loadeds = new List<AssetData>();
}

public class AssetData
{
    public bool IsMatch(string path)
    {
        return this.path.Equals(path);
    }

    public bool IsMatch(string scene, string path)
    {
        return this.scene.Equals(scene) && this.path.Equals(path);
    }

    public string scene;

    public string path;

    public Object asset;
}
public class AssetPath
{
    public string scene;

    public string path;
}