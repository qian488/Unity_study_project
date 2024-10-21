using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lobby
{
    public class LobbyLoadScene : MonoBehaviour
    {

        public void ReturnLobby()
        {
            SceneManager.LoadScene("Lobby");
        }

        public void StartRunning()
        {
            SceneManager.LoadScene("Running");
        }

        public void StartKiller()
        {
            SceneManager.LoadScene("Killer");
        }

        public void StartGal_first()
        {
            SceneManager.LoadScene("Gal_first");
        }

        public void StartPackage()
        {
            SceneManager.LoadScene("Package");
        }

        public void StartTestRPG()
        {
            SceneManager.LoadScene("overlookingrpg");
        }

        public void StartHuoyong2d()
        {
            SceneManager.LoadScene("huoying2d");
        }

        public void StartPVZ()
        {
            SceneManager.LoadScene("PVZ");
        }
    }
}

