// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class Item : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * 2); 
    }
}
