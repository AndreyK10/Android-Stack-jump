using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{

    public static GameplayController instance;

    [SerializeField] private Animator anim;

    [SerializeField] private PlatformSpawner platformSpawner;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private CinemachineVirtualCamera vcam1, vcam2, vcam3;


    private void Awake()
    {
        instance = this;

        platformSpawner.enabled = false;
        playerMovement.enabled = false;
    }

    public void StartGame()
    {
        SwitchCamera(vcam2, vcam1);
        platformSpawner.enabled = true;
        playerMovement.enabled = true;
    }

    public void GameOver()
    {
        ScoreManager.instance.UpdateHighScore();
        platformSpawner.enabled = false;
        StopMovingPlatforms();
        ChangeCamera();
        StartCoroutine(ReloadScene());
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
    private IEnumerator ReloadScene()
    {
        anim.SetTrigger("Fade");
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
