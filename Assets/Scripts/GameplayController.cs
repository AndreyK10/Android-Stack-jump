using UnityEngine;
using Cinemachine;

public class GameplayController : MonoBehaviour
{

    public static GameplayController instance;

    [SerializeField] private PlatformSpawner platformSpawner;

    [SerializeField] private CinemachineVirtualCamera vcam1, vcam2, vcam3;


    private void Awake()
    {
        instance = this;

        platformSpawner.enabled = false;
    }

    public void StartGame()
    {
        SwitchCamera(vcam2, vcam1);
        platformSpawner.enabled = true;
    }

    public void GameOver()
    {
        ScoreManager.instance.UpdateHighScore();
        platformSpawner.enabled = false;
        StopMovingPlatforms();
        ChangeCamera();
    }

    private void StopMovingPlatforms()
    {
        foreach (Platform platform in platformSpawner.spawnedPlatforms)
        {
            if (platform.TryGetComponent(out PlatformMovement platformMovement))
            {
                platformMovement.enabled = false;
            }
        }
    }

    private void ChangeCamera()
    {
        vcam3.Follow = null;
        vcam3.LookAt = null;
        SwitchCamera(vcam3, vcam2);
    }

    private void SwitchCamera(CinemachineVirtualCamera cameraTo, CinemachineVirtualCamera cameraFrom)
    {
        cameraTo.Priority = 10;
        cameraFrom.Priority = 9;
    }
}
