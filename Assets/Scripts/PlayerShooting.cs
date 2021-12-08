using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class PlayerShooting : MonoBehaviour
{
    //Projectile object variables
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = -20f;

    //Photon View
    PhotonView view;

    //Projectile SFX
    public AudioClip shootSound;

    //Colliders
    Collider2D playerCollider;

    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }   
    }

    private void Shoot()
    {
        //Instantiating the shooting projectile
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rocket>().parentPlayerCollider = playerCollider;
        Physics2D.IgnoreCollision(playerCollider, bullet.GetComponent<Rocket>().objectCollider, true);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        //Playing the shooting SFX
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, 0.2f);

    }
}
