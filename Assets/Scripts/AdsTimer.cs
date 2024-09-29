using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsTimer : MonoBehaviour {

    public GameObject watchAds;
    public bool startTimer;
    Image heartCircle;
    float timeLeft = 5f;

    private void Start()
    {
        heartCircle = GetComponent<Image>();
        startTimer = false;
    }

    // Update is called once per frame
    void Update () {
        if (startTimer)
        {
            timeLeft -= Time.deltaTime;
            heartCircle.fillAmount = timeLeft / 5f;
            if (timeLeft < 0)
            {
                watchAds.SetActive(false);
            }
        }
    }


}
