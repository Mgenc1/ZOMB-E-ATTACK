using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShotgunScript : MonoBehaviour
{

    private float FireDelay = 0;
    public GameObject BloodFx;
    public GameObject BulletFx;

    public int BulletCount;
    public int BulletCountLeft;

    [Header("TMP")]
    public TMP_Text BulletCountText;
    public TMP_Text BulletCountLeftText;

    private ParticleSystem MuzzleParticle;

    [Header("CLIPS")]
    public AudioClip DrySound;
    public AudioClip ShotgunFire;

    private AudioSource aSource;

    void Start()
    {
        MuzzleParticle = GetComponentInChildren<ParticleSystem>();
        aSource = GetComponent<AudioSource>();
    }


    void Update()
    {

        if (Input.GetMouseButton(0) && FireDelay < Time.time)
        {
            FireDelay = Time.time + 1f;
            MuzzleParticle.Play();
            aSource.PlayOneShot(ShotgunFire);

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            RaycastHit Raycasthitt;

            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100, Color.red, Time.deltaTime);


            BulletCount = Mathf.Clamp(BulletCount, 0, 30);
            if (BulletCount == 0 && BulletCountLeft != 0)
            {
                BulletCount = 8;
                BulletCountLeft -= 8;
            }
            if (BulletCountLeft == 0 && BulletCount == 0)
            {
                BulletCount = 0;
                MuzzleParticle.Stop();
                AudioSource.PlayClipAtPoint(DrySound, transform.position);
            }
            BulletCountLeft = Mathf.Clamp(BulletCountLeft, 0, 90);

            if (Physics.Raycast(ray, out Raycasthitt, 100f))
            {
                Debug.Log(Raycasthitt.transform.gameObject.name);

                if (Raycasthitt.transform.tag == "Zombie")
                {
                    if (BulletCount != 0)
                    {
                        ZombieHelth zombiHealthh = Raycasthitt.transform.GetComponentInParent<ZombieHelth>();
                        PlayerScript player = GetComponentInParent<PlayerScript>();
                       
                        if (player.Mesafe>=15)
                        {
                            zombiHealthh.ZombieHelthFunction(10);
                            Debug.Log("10 HASAR");
                        }
                        else if(player.Mesafe>=12 && player.Mesafe<15)
                        {
                            zombiHealthh.ZombieHelthFunction(20);
                            Debug.Log("20 HASAR");
                        }
                        else if ( player.Mesafe<8)
                        {
                            zombiHealthh.ZombieHelthFunction(100);
                            Debug.Log("100 HASAR");
                        }
                        GameObject bloodEffect = Instantiate(BloodFx, Raycasthitt.point, Raycasthitt.transform.rotation);
                        Destroy(bloodEffect, 1f);
                    }

                }
                else
                {
                    if (BulletCount != 0 && BulletCountLeft != 0)
                    {
                        GameObject bulletEffect1 = Instantiate(BulletFx, Raycasthitt.point, Quaternion.LookRotation(Raycasthitt.normal));
                        GameObject bulletEffect2 = Instantiate(BulletFx, Raycasthitt.point + new Vector3(Random.Range(-0.5f,0.5f), Random.Range(-0.5f, 0.5f),0f), Quaternion.LookRotation(Raycasthitt.normal));
                        GameObject bulletEffect3 = Instantiate(BulletFx, Raycasthitt.point + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.LookRotation(Raycasthitt.normal));
                        GameObject bulletEffect4 = Instantiate(BulletFx, Raycasthitt.point + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.LookRotation(Raycasthitt.normal));
                        Destroy(bulletEffect1, 4f);
                        Destroy(bulletEffect2, 4f);
                        Destroy(bulletEffect3, 4f);
                        Destroy(bulletEffect4, 4f);
                    }

                }
            }
            if (BulletCount != 0)
                BulletCount--;


            BulletCountText.text = "" + BulletCount.ToString();
            BulletCountLeftText.text = "" + BulletCountLeft;

        }

    }
}
