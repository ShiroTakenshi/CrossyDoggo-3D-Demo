using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    //serializefield section
    [SerializeField] ParticleSystem dieParticles;
    [SerializeField] TMP_Text stepText;
    [SerializeField, Range(0.01f, 1f)] float moveDuration = 0.2f;
    [SerializeField, Range(0.01f, 1f)] float jumpHigh = 0.5f;
    [SerializeField] private int maxTravel;
    [SerializeField] private int currentTravel;
    [SerializeField] AudioSource walkingSound;
    [SerializeField] AudioSource ketabrakSound;
    [SerializeField] AudioSource busSound;
    public bool IsDie { get => this.enabled == false; }


    //private or public section
    private float rightBoundary;
    private float leftBoundary;
    private float backBoundary;
    public int MaxTravel { get => maxTravel; }
    public int CurrentTravel { get => currentTravel; }



    public void SetUp(int minZPos, int extent)
    {
        backBoundary = minZPos - 1;
        leftBoundary = -(extent + 1);
        rightBoundary = extent + 1;
    }



    private void Update()
    {
        busSound.Play();
        var MoveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
            MoveDir += new Vector3(0, 0, 1);

        if (Input.GetKey(KeyCode.DownArrow))
            MoveDir += new Vector3(0, 0, -1);

        if (Input.GetKey(KeyCode.RightArrow))
            MoveDir += new Vector3(1, 0, 0);

        if (Input.GetKey(KeyCode.LeftArrow))
            MoveDir += new Vector3(-1, 0, 0);

        if (MoveDir != Vector3.zero && IsJumping() == false)
            Jump(MoveDir);
    }
    private void Jump(Vector3 TargetDirection)
    {
        //atur rotasi
        var targetPosition = transform.position + TargetDirection;
        transform.LookAt(targetPosition);

        // loncat ke atas
        var moveSeq = DOTween.Sequence(transform);
        moveSeq.Append(transform.DOMoveY(jumpHigh, moveDuration / 2));
        moveSeq.Append(transform.DOMoveY(0f, moveDuration / 2));


        if (Tree.AllPosition.Contains(targetPosition))
            return;

        if (targetPosition.z <= backBoundary || targetPosition.x <= leftBoundary || targetPosition.x >= rightBoundary)
            return;

        // gerak maju / mundur / samping
        transform.DOMoveX(targetPosition.x, moveDuration);
        transform
                .DOMoveZ(targetPosition.z, moveDuration)
                .OnComplete(UpdateTravel);
        walkingSound.Play();
    }

    private void UpdateTravel()
    {
        currentTravel = (int)this.transform.position.z;

        if (currentTravel > maxTravel)
            maxTravel = currentTravel;

        stepText.text = "Step: " + maxTravel.ToString();
    }

    public bool IsJumping()
    {
        return DOTween.IsTweening(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.enabled == false)
            return;
        // di execute sekali pada frame ketika nempel pertama kali
        var Bus = other.GetComponent<Car>();
        if (Bus != null)
        {
            Crash(Bus);
        }

        if (other.tag == "Bus")
        {
            // Debug.Log("Hit " + other.name);
            // AnimateDie();
        }
    }

    private void Crash(Car car)
    {

        // Gepeng
        transform.DOScaleY(0.1f, 0.2f);
        transform.DOScaleX(3, 0.2f);
        transform.DOScaleZ(2, 0.2f);
        this.enabled = false;
        dieParticles.Play();
        ketabrakSound.Play();

    }

    private void OnTriggerStay(Collider other)
    {
        // di execute setiap frame selama masih nempel
    }

    private void OnTriggerExit(Collider other)
    {
        // diexecute sekali [ada frame ketika tidak nempel
    }

}