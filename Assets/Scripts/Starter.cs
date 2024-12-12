using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{

    private bool isWaiting = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(Deactivate());
        }
    }

    private IEnumerator Deactivate()
    {
        if (isWaiting = true) yield break;
        isWaiting = true;

        yield return new WaitForSeconds(0.25f);

        gameObject.SetActive(false);
        isWaiting = false;
    }
}
