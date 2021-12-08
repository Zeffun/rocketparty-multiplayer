using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    PhotonView view;
    public float speed = 8f;
    public Rigidbody2D rb;
    public Camera cam;


    // Start is called before the first frame update
    private void Start()
    {
        view = GetComponent<PhotonView>();

        if (view.IsMine)
        {

            rb = GetComponent<Rigidbody2D>();

            cam = FindObjectOfType<Camera>();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (view.IsMine)
        {
            //Movement
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;

            //Aiming
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDirection = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 270f;
            rb.rotation = angle;

        }
    }

    //Change this later
    public void TakeDamage()
    {
        Debug.Log("meow");
    }
}
