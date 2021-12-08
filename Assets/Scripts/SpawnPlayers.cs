using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public CinemachineFreeLook playerCamera = null;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        var player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        playerCamera.Follow = player.transform;
        playerCamera.LookAt = player.transform;
    }

}
