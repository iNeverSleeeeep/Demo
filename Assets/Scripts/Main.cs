using UnityEngine;
using Demo;
using System;

public class Main : MonoBehaviour
{
    private void Start()
    {
        Application.runInBackground = true;
        Game.Instance.Initialize();
    }

    private void Update()
    {
        Game.Instance.Update();

        if (Input.GetKeyDown(KeyCode.Z))
        {

        }
    }
}
