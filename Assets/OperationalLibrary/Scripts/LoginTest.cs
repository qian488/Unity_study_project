using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Operation
{
    public class LoginInfo
    {
        public string code;
        public string info;
    }

    public class LoginTest : MonoBehaviour
    {
        public InputField NameField;
        public InputField PasswordField;

        public void LoginClick()
        {
            string url = "http://127.0.0.1/tt.asp?name=" + NameField.text + "&pwd=" + PasswordField.text;
            StartCoroutine(Login(url));
        }

        IEnumerator Login(string url)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                LoginJSON(request.downloadHandler.text);
            }
        }

        void LoginJSON(string JSON)
        {
            LoginInfo info = JsonUtility.FromJson<LoginInfo>(JSON);
            if(info.code == "0")
            {
                Debug.Log("µÇÂ¼³É¹¦");
                SceneManager.LoadScene("Lobby");
            }
            else
            {
                Debug.Log("µÇÂ¼Ê§°Ü");
            }
        }
    }
}

