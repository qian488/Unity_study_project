using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyLoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
