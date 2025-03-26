using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace TankGame.Data
{
    public class PrefabManager : MonoBehaviour
    {
        // 单例实例
        public static PrefabManager Instance;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // 加载预制体的公共接口
        public void LoadPrefab(string relativePath, System.Action<GameObject> onPrefabLoaded)
        {
            StartCoroutine(InternalLoadPrefab(relativePath, onPrefabLoaded));
        }

        public IEnumerator LoadPrefabCoroutine(string relativePath, System.Action<GameObject> onPrefabLoaded)
        {
            yield return StartCoroutine(InternalLoadPrefab(relativePath, onPrefabLoaded));
        }

        private IEnumerator InternalLoadPrefab(string relativePath, System.Action<GameObject> onPrefabLoaded)
        {
            // 将相对路径转换为绝对路径
            string absolutePath = Application.dataPath + "/" + relativePath.Replace("Assets/", "");

            // 使用 file:// 协议
            string url = "file://" + absolutePath;

            // 创建一个 UnityWebRequest 对象，用于从本地路径加载预制体
            UnityWebRequest uwr = UnityWebRequest.Get(url);

            // 发送请求并等待响应
            yield return uwr.SendWebRequest();

            // 检查请求是否完成且没有错误
            if (uwr.isDone && uwr.result != UnityWebRequest.Result.ProtocolError && uwr.result != UnityWebRequest.Result.ConnectionError)
            {
                // 检查下载处理程序是否是 DownloadHandlerAssetBundle 类型
                if (uwr.downloadHandler is DownloadHandlerAssetBundle)
                {
                    AssetBundle bundle = ((DownloadHandlerAssetBundle)uwr.downloadHandler).assetBundle;

                    if (bundle != null)
                    {
                        // 从 AssetBundle 中加载预制体
                        GameObject prefab = bundle.LoadAsset<GameObject>(Path.GetFileNameWithoutExtension(relativePath));

                        if (prefab != null)
                        {
                            // 调用回调函数，传递加载的预制体
                            onPrefabLoaded?.Invoke(prefab);

                            // 卸载 AssetBundle 以释放资源
                            bundle.Unload(false);
                        }
                        else
                        {
                            Debug.LogError("Failed to load prefab from AssetBundle: " + Path.GetFileNameWithoutExtension(relativePath));
                        }
                    }
                    else
                    {
                        Debug.LogError("Failed to load AssetBundle");
                    }
                }
                else
                {
                    // 如果下载处理程序不是 DownloadHandlerAssetBundle 类型，尝试从字节数据加载
                    // 获取下载的字节数据
                    byte[] bytes = uwr.downloadHandler.data;

                    // 将字节数据写入临时文件
                    string tempFilePath = Path.Combine(Application.dataPath, "Temp", Path.GetFileName(absolutePath));
                    Directory.CreateDirectory(Path.GetDirectoryName(tempFilePath));
                    File.WriteAllBytes(tempFilePath, bytes);

                    // 将路径转换为相对于项目文件夹的路径
                    string assetPath = "Assets/Temp/" + Path.GetFileName(absolutePath);

                    // 导入临时文件到项目中
                    AssetDatabase.ImportAsset(assetPath);

                    // 加载预制体
                    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

                    if (prefab != null)
                    {
                        // 调用回调函数，传递加载的预制体
                        onPrefabLoaded?.Invoke(prefab);
                    }
                    else
                    {
                        Debug.LogError("Failed to load prefab from bytes.");
                    }
                }
            }
            else
            {
                // 如果请求失败，输出错误信息
                Debug.LogError("Error loading prefab: " + uwr.error);
            }
        }


    }
}

