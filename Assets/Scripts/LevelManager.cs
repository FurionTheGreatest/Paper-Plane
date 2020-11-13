using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool isPlaying;
    public GameObject mainMenuUI;
    public GameObject inGameUI;
    public GameObject playerPrefab;
    public GameObject currentPlayer;
    
    private CinemachineVirtualCamera _camera;
    private GameScriptsEnabler _scriptsEnabler;
    
    private bool _isExitEnabled;
    public TMP_Text backToMenuPopup;
    private const float TimeToExit = 2f;
    private WaitForSeconds _waitForLabelShow = new WaitForSeconds(TimeToExit);

    private void Awake()
    {
        _camera = FindObjectOfType<CinemachineVirtualCamera>();
        _scriptsEnabler = FindObjectOfType<GameScriptsEnabler>(true);
    }

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !_isExitEnabled)
        {
            _isExitEnabled = true;
            if (backToMenuPopup == null) return;
            StartCoroutine(ShowLabel());

            StartCoroutine(nameof(ExitGameToMenu));
        }
    }

    public void Play()
    {
        if (currentPlayer != null)
        {
            currentPlayer = GameObject.FindWithTag("Player").gameObject;
        
            Destroy(currentPlayer);
        }
        
        _scriptsEnabler.enabled = false;

        currentPlayer = Instantiate(playerPrefab);
        SwitchUI(true);

        _camera.ForceCameraPosition(Vector3.zero, quaternion.identity);
        
        _camera.LookAt = currentPlayer.transform;
        _camera.Follow = currentPlayer.transform;
        
        _scriptsEnabler.enabled = true;

        isPlaying = true;
    }

    public void BackToMenu()
    {
        SwitchUI(false);
        _scriptsEnabler.enabled = false;
        isPlaying = false;
    }

    private void SwitchUI(bool isPlaying)
    {
        inGameUI.SetActive(isPlaying);
        mainMenuUI.SetActive(!isPlaying);
    }
    
    private IEnumerator ShowLabel()
    {
        backToMenuPopup.enabled = true;
        yield return _waitForLabelShow;
        backToMenuPopup.enabled = false;
        _isExitEnabled = false;
    }
    
    private IEnumerator ExitGameToMenu()
    {
        yield return null;
        float counter = 0;
        
        while (counter < TimeToExit)
        {
            counter += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackToMenu();
            }

            yield return null;
        }
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
