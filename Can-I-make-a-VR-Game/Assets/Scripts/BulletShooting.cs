using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooting : MonoBehaviour
{
    public float speed = 40;
    public GameObject bullet;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float shootVolume { get; set; } = 1.0f;
   

    // Update is called once per frame
    public void Fire()
    {
        //Quaternion bulletRotation = Quaternion.Euler(barrel.position.x+90, barrel.position.y, barrel.position.z);
        GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
        audioSource.PlayOneShot(audioClip, shootVolume);
        Destroy(spawnedBullet, 5);
    }

}
