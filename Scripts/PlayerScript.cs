using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float Speed;
    private CharacterController Character;

    public enum MoveSelection { playerMovement, move2 };
    public MoveSelection moveSelection = MoveSelection.playerMovement;

    private float YMove; 
    public float JumpHeight;
    public float GravityValue;

    public float MauseSpeedX;

    [HideInInspector] public float Mesafe;
    public Text DistanceText;



    void Start()
    {
        Character = GetComponent<CharacterController>();
    }


    void Update()
    {
       
        switch (moveSelection)
        {
            case MoveSelection.playerMovement:
                PlayerMovement();
                break;
            case MoveSelection.move2:
                Move2();
                break;
        }
        PlayerHorizontalRotation();
        CursorLockAndVisible();
        //DistanceToZombie();
        ChooseWeapon();
    }

    private void PlayerMovement()
    {
        Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), YMove, Input.GetAxis("Vertical"));
        Movement = transform.TransformDirection(Movement);
        Character.Move(Movement * Time.deltaTime * Speed);
        Debug.Log("PlayerMovment");

        if (Character.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                YMove = JumpHeight; 
            }
        }
        else
        {
            YMove -= GravityValue;
        }
        Debug.Log(Character.isGrounded);
    }

    private void Move2()
    {
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * Speed, 0f, Input.GetAxis("Vertical") * Time.deltaTime * Speed);
        Debug.Log("Move2");
    }

    private void PlayerHorizontalRotation()
    {
        transform.Rotate(0f, Input.GetAxis("Mouse X") * Time.deltaTime * MauseSpeedX, 0f);
    }

    private void CursorLockAndVisible() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DistanceToZombie()
    {
        RaycastHit hitDistance;
        if (Physics.Raycast(transform.position, transform.forward, out hitDistance, 500f))
        {
            //if (hitDistance.transform.gameObject.tag=="Zombie")
            //{
            //    Mesafe = Vector3.Distance(transform.position, hitDistance.transform.position);
            //    DistanceText.text = "DISTANCE:" + Mesafe.ToString("F2") + "m";
            //}
        }
    }

    public GameObject Weapon1;
    public GameObject Weapon2;
    public GameObject Weapon3;

    private void ChooseWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Weapon1.SetActive(true);
            Weapon2.SetActive(false); 
            Weapon3.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Weapon1.SetActive(false);
            Weapon2.SetActive(true);
            Weapon3.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Weapon1.SetActive(false);
            Weapon2.SetActive(false);
            Weapon3.SetActive(true);
        }
    }
}


