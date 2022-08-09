using UnityEngine;

public class GlassWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject fx;

    private void Awake()
    {
        wall.gameObject.SetActive(true);
        fx.gameObject.SetActive(false);
    }

    public void CrashWall()
    {
        wall.gameObject.SetActive(false);
        fx.gameObject.SetActive(true);
    }
}