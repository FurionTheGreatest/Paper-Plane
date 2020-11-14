using UnityEngine;

public class DestroyOrDisable : MonoBehaviour
{
    public bool isDestroying;

    private float _lowerTreshold = -10f;

    private void Update()
    {
        CheckForDestroyOrDisable();
    }

    private void CheckForDestroyOrDisable()
    {
        if (LevelManager.instance.currentPlayer.Equals(null) || !(gameObject.transform.position.y - LevelManager.instance.currentPlayer.transform.position.y <
              _lowerTreshold)) return;
        
        if (isDestroying)
            Destroy(gameObject);
        else Pooler.Despawn(gameObject);
    }
}
