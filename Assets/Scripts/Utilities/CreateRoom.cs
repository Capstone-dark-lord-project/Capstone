using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateRoom : MonoBehaviour
{
    public void Create()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
