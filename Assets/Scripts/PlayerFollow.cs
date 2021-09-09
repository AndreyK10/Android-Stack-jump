using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public static PlayerFollow instance;
    private Player player;


    private void Awake()
    {
        instance = this;

        player = FindObjectOfType<Player>();
        gameObject.transform.SetParent(player.transform);
    }
    public void NoParent()
    {
        transform.parent = null;
    }
}
