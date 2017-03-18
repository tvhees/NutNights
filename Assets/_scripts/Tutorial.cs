using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

[ManagerDependency(typeof(ManagerContainer))]
public class Tutorial : BaseMonoBehaviour
{
    public int Seed;

    protected override void Awake()
    {
        base.Awake();
        Random.InitState(Seed);
    }

    private void Start()
    {
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        var load = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        while (!load.isDone)
        {
            yield return null;
        }
        GetManager<TutorialController>().DisplayMessage("This is a tutorial message", Vector2.zero);
    }
}
