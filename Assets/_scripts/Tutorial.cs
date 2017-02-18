using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public int Seed;

    private void Awake()
    {
        Random.InitState(Seed);
        SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
    }
}
