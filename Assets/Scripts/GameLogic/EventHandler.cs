using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class EventHandler : MonoBehaviour
{
    private int _enemyCount;
    private GameObject _victoryScreen;
    private GameObject _defeatScreen;
    private GameObject _optionsButton;
    private GameObject _optionsMenu;
    private bool _won = false;
    private CameraFollow _cameraFollow;
    private SlingshotHandler _slingshotHandler;

    [Inject]
    public void Construct(
        CameraFollow cameraFollow,
        SlingshotHandler slingshotHandler,
        [Inject(Id = "enemyCount")] int enemyCount,
        [Inject(Id = "optionsButton")] GameObject optionsButton,
        [Inject(Id = "optionsMenu")] GameObject optionsMenu,
        [Inject(Id = "victoryScreen")] GameObject victoryScreen,
        [Inject(Id = "defeatScreen")] GameObject defeatScreen)
    {
        _cameraFollow = cameraFollow;
        _slingshotHandler = slingshotHandler;
        _enemyCount = enemyCount;
        _optionsButton = optionsButton;
        _optionsMenu = optionsMenu;
        _victoryScreen = victoryScreen;
        _defeatScreen = defeatScreen;

    }

    public void OnBirdLaunched(Rigidbody2D birdRigidbody)
    {
        _cameraFollow.SetTarget(birdRigidbody);
    }

    public void SetCameraStatus(bool status)
    {
        _cameraFollow.SetCameraActivity(status);
    }

    public void NextBirdIsUp()
    {
        if(!_won)
        {
            _cameraFollow.StopFollowing();
            if (!_slingshotHandler.NextBirdReady())
            {
                TriggerDefeatScreen();
            }
        }
        
    }

    public void DecreaseEnemyCount()
    {
        _enemyCount--;

        if (_enemyCount <= 0)
        {
            _cameraFollow.StopFollowing();
            TriggerVictoryScreen();
        }
    }

    public void LastBirdLaunched()
    {
        Debug.Log("last birdie");
    }


    public void TriggerVictoryScreen()
    {
        _won = true;
        _slingshotHandler.MakeActive(false);
        _slingshotHandler.SetFreezeBird(true);
        _victoryScreen.SetActive(true);
        _defeatScreen.SetActive(false);
        _optionsButton.SetActive(false);
        _optionsMenu.SetActive(false);
        SetCameraStatus(false);
    }


    public void TriggerDefeatScreen()
    {
        _defeatScreen.SetActive(true);
        _optionsButton.SetActive(false);
        _optionsMenu.SetActive(false);
        SetCameraStatus(false);
    }
}
