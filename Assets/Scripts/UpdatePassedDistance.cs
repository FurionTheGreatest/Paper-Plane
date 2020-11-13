using TMPro;
using UnityEngine;

public class UpdatePassedDistance : MonoBehaviour
{
    public Transform playerPosition;

    private TMP_Text _textOfPlayerPosition;

    private const int DistanceDivider = 10;

    private void Awake()
    {
        _textOfPlayerPosition = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(UpdatePlayerPosition),2f,1/2f);
    }

    private void UpdatePlayerPosition()
    {
        if(playerPosition.position.y < 0)
            _textOfPlayerPosition.text = 0 + " m";
        else
            _textOfPlayerPosition.text = (playerPosition.position.y / DistanceDivider).ToString("f2") + " m";
    }

    private void OnEnable()
    {
        playerPosition = LevelManager.instance.currentPlayer.transform;
    }
}
