using UnityEngine;
using System.Collections;

public class StageObject : MonoBehaviour
{
    void Awake()
    {
        float computed_z = transform.position.y;
        transform.position = new Vector3( transform.position.x, transform.position.y, transform.position.y );
    }
}
