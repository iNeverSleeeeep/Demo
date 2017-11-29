using UnityEngine;
using Demo;

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
