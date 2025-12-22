using System;
using UnityEngine;

public class RollCube : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, 1, 0));
    }
}