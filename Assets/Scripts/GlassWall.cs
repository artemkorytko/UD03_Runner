using DefaultNamespace;
using UnityEngine;

public class GlassWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject fx;
    [SerializeField] private GameConfig gameConfig;
    private void Awake()
    {
        wall.gameObject.SetActive(true);
        fx.gameObject.SetActive(false);
        gameConfig.OnCrash += GameConfigOnOnCrash;
    }

    private void GameConfigOnOnCrash()
    {
        fx.gameObject.SetActive(true);
    }

    public void CrashWall()
    {
        wall.gameObject.SetActive(false);
        fx.gameObject.SetActive(true);
        gameConfig.OnCrashGlassWall();
    }
}