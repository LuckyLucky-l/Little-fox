using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterHouse : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ScenceNumber.isScendScense=true;
            if (ScenceNumber.isScendScense)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
