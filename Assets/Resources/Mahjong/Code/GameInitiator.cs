using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    private async void Start()
    {
        await DoSomething();
    }

    private UniTask DoSomething()
    {
        throw new NotImplementedException();
    }
}
