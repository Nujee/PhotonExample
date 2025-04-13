using UnityEngine;

namespace Assets.Scripts.Core.Bootstrap
{
    public sealed class GameStartup : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            RegisterServices();
            //GameStateMachine.Instance.ChangeState(new BootstrapState());
        }

        private void RegisterServices()
        {
            //ServiceLocator.Register<SceneLoader>(new SceneLoader());
            //ServiceLocator.Register<InputService>(new InputService());
        }
    }
}