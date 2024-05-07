using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinRoom : MonoBehaviour
{
    public void JoinPage()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
