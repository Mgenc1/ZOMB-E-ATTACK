using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SniperScript : MonoBehaviour
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

    private AudioSource aSource;

    [Header("CLIPS")]
    public AudioClip DrySound;
    public AudioClip SniperFire;

    public GameObject SniperZoomPanel;

    private bool Zoombool=true;

    void Start()
    {
        MuzzleParticle = GetComponentInChildren<ParticleSystem>();
        aSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1)&& Zoombool)
        {
            SniperZoomPanel.SetActive(true);
            Camera.main.GetComponent<Camera>().fieldOfView = 10;
            Zoombool = false;
        }
        else if (Input.GetMouseButtonDown(1)&& Zoombool==false)
        {
            SniperZoomPanel.SetActive(false);
            Camera.main.GetComponent<Camera>().fieldOfView = 60;
            Zoombool = true;
        }

        if (Input.GetMouseButton(0) && FireDelay < Time.time)
        {
            FireDelay = Time.time + 2f;
            MuzzleParticle.Play();
            aSource.PlayOneShot(SniperFire);

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            RaycastHit Raycasthitt;

            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100, Color.red, Time.deltaTime);


            BulletCount = Mathf.Clamp(BulletCount, 0, 30);
            if (BulletCount == 0 && BulletCountLeft != 0)
            {
                BulletCount = 30;
                BulletCountLeft -= 30;
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
                    if (BulletCount != 0 && BulletCountLeft != 0)
                    {
                        ZombieHelth zombiHealthh = Raycasthitt.transform.GetComponentInParent<ZombieHelth>();
                        zombiHealthh.ZombieHelthFunction(200);
                        GameObject bloodEffect = Instantiate(BloodFx, Raycasthitt.point, Raycasthitt.transform.rotation);
                        Destroy(bloodEffect, 1f);
                    }

                }
                else
                {
                    if (BulletCount != 0 && BulletCountLeft != 0)
                    {
                        GameObject bulletEffect1 = Instantiate(BulletFx, Raycasthitt.point, Quaternion.LookRotation(Raycasthitt.normal));
                        GameObject bulletEffect2 = Instantiate(BulletFx, Raycasthitt.point + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.LookRotation(Raycasthitt.normal));
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
