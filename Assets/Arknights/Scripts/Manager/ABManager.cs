using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Arknights.Utils;

namespace Arknights.Manager
{
    public class ABManager : Single<ABManager>
    {
        /// <summary> 已加载的 AB 包 </summary>
        private Dictionary<string, AssetBundle> loadedDict = new Dictionary<string, AssetBundle>();

        /// <summary> 总的 AB 包 </summary>
        private AssetBundle mainAB;

        /// <summary> AB 构建清单 </summary>
        private AssetBundleManifest manifest;

        /// <summary> AB 包存储路径 </summary>
        private string abPath;

        public string ABPath
        {
            get
            {
                if (string.IsNullOrEmpty(abPath))
                {
                    abPath = Application.streamingAssetsPath + "/";

                    #if UNITY_ANDROID || UNITY_IOS
                        abPath = Application.persistentDataPath + "/";
                    #endif
                }
                return abPath;
            }
        }

        public string MainABName
        {
            get
            {
                #if UNITY_STANDALONE_WIN
                    return "Win";
                #elif UNITY_ANDROID
                    return "Android";
                #elif UNITY_IOS
                    return "IOS";
                #else
                    return "UnknownPlatform"; // 避免编译错误
                #endif
            }
        }

        /// <summary> **异步加载 AB 包** </summary>
        public async Task<AssetBundle> LoadAssetBundleAsync(string abName)
        {
            // 加载总包
            if (mainAB == null)
            {
                string singlePath = ABPath + MainABName;
                if (!File.Exists(singlePath))
                {
                    Debug.LogWarning($"未找到总包路径: {singlePath}");
                    return null;
                }
                mainAB = await Task.Run(() => AssetBundle.LoadFromFile(singlePath));
            }

            // 加载清单
            if (manifest == null)
            {
                manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            }

            // 加载依赖项
            string[] deps = manifest.GetAllDependencies(abName);
            foreach (var depName in deps)
            {
                if (!loadedDict.ContainsKey(depName))
                {
                    string depPath = ABPath + depName;
                    if (File.Exists(depPath))
                    {
                        loadedDict[depName] = await Task.Run(() => AssetBundle.LoadFromFile(depPath));
                    }
                    else
                    {
                        Debug.LogWarning($"依赖 AB 未找到: {depPath}");
                    }
                }
            }

            // 加载主 AB 包
            if (!loadedDict.TryGetValue(abName, out AssetBundle ab))
            {
                string abPath = ABPath + abName;
                if (File.Exists(abPath))
                {
                    ab = await Task.Run(() => AssetBundle.LoadFromFile(abPath));
                    loadedDict[abName] = ab;
                }
                else
                {
                    Debug.LogError($"未找到 AB 文件: {abPath}");
                }
            }
            return ab;
        }

        /// <summary> **卸载 AB 包（同时卸载依赖）** </summary>
        public void UnloadAssetBundle(string abName, bool unloadAllObjects = false)
        {
            if (loadedDict.TryGetValue(abName, out AssetBundle ab))
            {
                ab.Unload(unloadAllObjects);
                loadedDict.Remove(abName);
            }

            // 卸载依赖项
            if (manifest != null)
            {
                string[] deps = manifest.GetAllDependencies(abName);
                foreach (var dep in deps)
                {
                    if (loadedDict.TryGetValue(dep, out AssetBundle depAB))
                    {
                        depAB.Unload(unloadAllObjects);
                        loadedDict.Remove(dep);
                    }
                }
            }
        }

        /// <summary> **卸载所有 AB 包** </summary>
        public void UnloadAll(bool unloadAllObjects = false)
        {
            foreach (var ab in loadedDict.Values)
            {
                ab.Unload(unloadAllObjects);
            }
            loadedDict.Clear();
        }

        /// <summary> **加载资源（泛型版本）** </summary>
        public async Task<T> LoadAssetAsync<T>(string assetName, string abName) where T : Object
        {
            AssetBundle ab = await LoadAssetBundleAsync(abName);
            if (ab != null)
            {
                if (ab.Contains(assetName))
                {
                    return ab.LoadAsset<T>(assetName);
                }
                else
                {
                    Debug.LogError($"资源 {assetName} 不存在于 AB 包 {abName}");
                }
            }
            return null;
        }

        /// <summary> **加载资源（非泛型版本）** </summary>
        public async Task<Object> LoadAssetAsync(string assetName, string abName, System.Type type)
        {
            AssetBundle ab = await LoadAssetBundleAsync(abName);
            if (ab != null)
            {
                return ab.LoadAsset(assetName, type);
            }
            return null;
        }
    }
}

