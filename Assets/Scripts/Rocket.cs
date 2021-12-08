using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rocket : MonoBehaviour
{
    //public GameObject hitEffect;
    PhotonView view;

    public float speed;
    public int maxBounce = 5;
    public string hitTag;

    public Collider2D parentPlayerCollider;

    private int currentBounce = 0;
    public Collider2D objectCollider;

    private void Start()
    {
        objectCollider = GetComponent<Collider2D>();
        view = GetComponent<PhotonView>();
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        //Destroy(gameObject);
        if (view.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }*/

    //added from single player version (below)

    private void OnEnable()
    {
        currentBounce = 0;
        StartCoroutine(EnableColliderAfter(0.2f));
    }

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == hitTag)
        {
            collision.collider.GetComponent<PlayerController>().TakeDamage();
        }

        if (currentBounce >= maxBounce)
        {
            if (view.IsMine)
            {
                PhotonNetwork.Destroy(transform.gameObject);
                
            }
            return;
        }

        Vector2 collisionNormal = collision.contacts[0].normal;
        float collisionAngle = Vector2.SignedAngle(-transform.up, collisionNormal);
        transform.up = -transform.up;
        transform.Rotate(Vector3.forward, collisionAngle * 2);
        currentBounce++;
    }

    private IEnumerator EnableColliderAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Physics2D.IgnoreCollision(parentPlayerCollider, objectCollider, false);
    }


}
