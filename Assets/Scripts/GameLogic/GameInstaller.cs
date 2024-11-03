using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public int enemyCount;
    public float cameraXPositiveEdge = 10f, cameraYPositiveEdge =5f, cameraXNegativeEdge = -10f, cameraYNegativeEdge = -5f;
    public GameObject optionsButton, optionsMenu, victoryScreen, defeatScreen;
    public CameraFollow cameraFollow;
    public EventHandler eventHandler;
    public SlingshotHandler slingshotHandler;

    public override void InstallBindings()
    {
        Container.Bind<CameraFollow>().FromInstance(cameraFollow).AsSingle();
        Container.Bind<EventHandler>().FromInstance(eventHandler).AsSingle();
        Container.Bind<SlingshotHandler>().FromInstance(slingshotHandler).AsSingle();
        Container.BindInstance(enemyCount).WithId("enemyCount");

        Container.BindInstance(cameraXPositiveEdge).WithId("cameraXPositiveEdge");
        Container.BindInstance(cameraYPositiveEdge).WithId("cameraYPositiveEdge");
        Container.BindInstance(cameraXNegativeEdge).WithId("cameraXNegativeEdge");
        Container.BindInstance(cameraYNegativeEdge).WithId("cameraYNegativeEdge");

        Container.BindInstance(optionsButton).WithId("optionsButton");
        Container.BindInstance(optionsMenu).WithId("optionsMenu");
        Container.BindInstance(victoryScreen).WithId("victoryScreen");
        Container.BindInstance(defeatScreen).WithId("defeatScreen");
    }
}
