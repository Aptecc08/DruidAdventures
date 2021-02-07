using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterMechanics : MonoBehaviour
{
    //Основные параметры 
    public float speedMove; //скорость персонажа
    public float speedMoveDeafault; //дефолтная скорость персонажа
    public float jumpPover; //сила прыжка

    //Уникальные параметры форм
    private bool flight; //полет
    private bool secrecy; // скрытность
    private bool swimming; // умение плавать
    private float damage; //сила атаки



    //Параметры геймплея для персонажа
    private float gravityForce; //гравитация персонажа
    private Vector3 moveVector; //направление движения персонажа 

    //Ссылки на компоненты
    private CharacterController ch_controller;

    private void Start()
    {
        ch_controller = GetComponent<CharacterController>();
        DefaultForm(); //персонаж принимает дефолтный облик при начале игры
    }

    private void Update()
    {
        CharacterMove();
        if (flight == false) GamingGravity();
        if (Input.GetKeyDown(KeyCode.1)) FlyingForm();
    }

    //метод перемещения персонажа
    private void CharacterMove()
    {
        //перемещение по поверхности
        moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal") * speedMove;
        moveVector.z = Input.GetAxis("Vertical") * speedMove;

        if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        moveVector.y = gravityForce;
        ch_controller.Move(moveVector * Time.deltaTime);//метод передвижения по направлению
    }

    //метод гравитации
    private void GamingGravity()
    {
        if (!ch_controller.isGrounded) gravityForce -= 20f * Time.deltaTime;
        else gravityForce = -1f;
        if (Input.GetKeyDown(KeyCode.Space) && ch_controller.isGrounded) gravityForce = jumpPover;
    }

    //сброс всех параметров при смене облика
    private void ResetParameters()
    {
        flight = false;
        secrecy = false;
        swimming = false;
        damage = 0;
        speedMove = speedMoveDeafault;
    }

    //дефолтный облик
    private void DefaultForm()
    {
        ResetParameters();
    }

    //летающий облик
    private void FlyingForm()
    {
        ResetParameters();
        flight = true;
    }
    
    //плавающий облик
    private void FloatingForm()
    {
        ResetParameters();
        swimming = true;
        speedMove = 0.75 * speedMoveDeafault;
    }
    
    //атакающий облик
    private void AtackingForm()
    {
        ResetParameters();
        damage = 50;
    }

    //быстрый облик
    private void QuickForm()
    {   
        ResetParameters();
        speedMove = 2 * speedMoveDeafault;
    }

}
