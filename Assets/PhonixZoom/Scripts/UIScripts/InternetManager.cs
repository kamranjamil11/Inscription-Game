using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InternetManager : MonoBehaviour
{
    public UnityEvent onInternetConnected;
    public UnityEvent onInternetDisconnected;
    public GameObject interNetPannel;

    private bool isConnected => Application.internetReachability == NetworkReachability.NotReachable;
    private Coroutine connectionCheckCoroutine;

    private void Start()
    {
        connectionCheckCoroutine = StartCoroutine(ConnectionCheckCoroutine());
    }

    private IEnumerator ConnectionCheckCoroutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(2f); // Adjust the interval as needed

        while (true)
        {
            CheckInternetConnection();

            yield return waitTime;
        }
    }

    private void CheckInternetConnection()
    {
        if (isConnected)
        {
            onInternetConnected.Invoke();
        }
        else
        {
            onInternetDisconnected.Invoke();
        }
    }
}
