using UnityEngine;

public class NonDamageWall : MonoBehaviour
{
    public void OffWall()
    {
        gameObject.SetActive(false);
    }
    public void OnWall()
    {
        gameObject.SetActive(true);
    }
}
