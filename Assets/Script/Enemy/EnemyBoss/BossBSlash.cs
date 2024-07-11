using UnityEngine;

public class BossBSlash : MonoBehaviour
{
    [SerializeField] private float speed = 4;

    private void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }
}
