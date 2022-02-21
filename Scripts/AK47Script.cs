using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AK47Script : MonoBehaviour
{
    public float FireDelay = 0;
    public GameObject BloodFx;
    public GameObject BulletFx;

    public int BulletCount;
    public int BulletCountLeft;

    public TMP_Text BulletCountText;
    public TMP_Text BulletCountLeftText;

    private ParticleSystem MuzzleParticle;

    private AudioSource aSource;
    public AudioClip aSourceClip;
    public AudioClip UzıFire;

    void Start()
    {
        MuzzleParticle = GetComponentInChildren<ParticleSystem>();
        aSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
        if (Input.GetMouseButton(0) && FireDelay < Time.time)
        {
            //aSource.clip = aSourceClip;
            //aSource.Play();
            aSource.PlayOneShot(aSourceClip);

            FireDelay = Time.time + 0.1f;
            MuzzleParticle.Play();

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            RaycastHit Raycasthitt;

            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100, Color.red, Time.deltaTime);


            BulletCount = Mathf.Clamp(BulletCount, 0, 30);
            if (BulletCount==0 && BulletCountLeft!=0)
            {
                BulletCount = 30;
                BulletCountLeft -= 30;
            }
            if (BulletCountLeft==0 && BulletCount==0)
            {

                BulletCount = 0;
                AudioSource.PlayClipAtPoint(UzıFire, transform.position);
                MuzzleParticle.Stop();
            }
            BulletCountLeft = Mathf.Clamp(BulletCountLeft, 0, 90);

            if (Physics.Raycast(ray, out Raycasthitt,100f))
            {
                Debug.Log(Raycasthitt.transform.gameObject.name);

                if (Raycasthitt.transform.tag=="Zombie")
                {
                    if (BulletCount!=0 && BulletCountLeft!=0)
                    {
                        ZombieHelth zombiHealthh = Raycasthitt.transform.GetComponentInParent<ZombieHelth>();
                        zombiHealthh.ZombieHelthFunction(10);
                        GameObject bloodEffect = Instantiate(BloodFx, Raycasthitt.point, Raycasthitt.transform.rotation);
                        Destroy(bloodEffect, 1f);
                    }
                    
                }
                else
                {
                    if (BulletCount != 0 && BulletCountLeft != 0)
                    {
                        GameObject bulletEffect = Instantiate(BulletFx, Raycasthitt.point, Quaternion.LookRotation(Raycasthitt.normal));
                        Destroy(bulletEffect, 4f);
                    }
                    
                }
            }
            if(BulletCount!=0)
            BulletCount--;


            BulletCountText.text = "" + BulletCount.ToString();
            BulletCountLeftText.text = "" + BulletCountLeft;

        }
        
    }
}
