using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float speed = -2f;

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0f, 0f, speed * Time.deltaTime * TimeController.current.timeScale);
    }
}
