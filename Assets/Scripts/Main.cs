using UnityEngine;
using Demo;
using WebSocketSharp;

public class Main : MonoBehaviour
{
    private void Start()
    {
        Game.Instance.Initialize();
    }

    private void Update()
    {
        Game.Instance.Update();
    }
}
