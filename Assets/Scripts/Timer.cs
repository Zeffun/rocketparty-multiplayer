using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private double duration = 60.0f;

    private Hashtable Custom;
    private double startTime;

    private Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Custom = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            Custom.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(Custom);
        }
        else
        {
            startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
        }
        timerText = GetComponent<Text>();
        Assert.IsNotNull(timerText);
    }

    // Update is called once per frame
    void Update()
    {
        double timeRemaining = duration - PhotonNetwork.Time + startTime;
        if (timeRemaining > 0.0f)
        {
            TimeSpan interval = TimeSpan.FromSeconds(timeRemaining);
            timerText.gameObject.SetActive(false);
            timerText.text = string.Format("Time left: {0:00}:{1:00}", interval.Minutes, interval.Seconds);
            timerText.gameObject.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}
