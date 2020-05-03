using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door2 : MonoBehaviour

{
    void OnTriggerEnter2D(Collider2D Door2)
    {
        SceneManager.LoadScene(1);
    }
}

