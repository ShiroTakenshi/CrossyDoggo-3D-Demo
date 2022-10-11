using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Range(0.01f, 1f)] float moveDuration = 0.2f;
    [SerializeField, Range(0.01f, 1f)] float jumpHigh = 0.5f;
    private float RightBoundary;
    private float BackBoundary;
    private float LeftBoundary;

    public void SetUp(int minZPos, int extent)
    {
        BackBoundary = minZPos - 1;
        LeftBoundary = -(extent + 1);
        RightBoundary = extent + 1;
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.UpArrow))
        //     Debug.Log("Forward");
        // if (Input.GetKeyDown(KeyCode.DownArrow))
        //     Debug.Log("Backward");

        var MoveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
            MoveDir += new Vector3(0, 0, 1);

        if (Input.GetKey(KeyCode.DownArrow))
            MoveDir += new Vector3(0, 0, -1);

        if (Input.GetKey(KeyCode.RightArrow))
            MoveDir += new Vector3(1, 0, 0);

        if (Input.GetKey(KeyCode.LeftArrow))
            MoveDir += new Vector3(-1, 0, 0);


        // if (MoveDir == Vector3.zero)
        //     return;
        // if (IsJumping() == false)
        //     Jump(MoveDir);

        if (MoveDir != Vector3.zero && IsJumping() == false)
            Jump(MoveDir);
    }
    private void Jump(Vector3 TargetDirection)
    {
        // var TargetPosition = transform.position + new Vector3(TargetDirection.x, 0, TargetDirection.y);
        var targetPosition = transform.position + TargetDirection;

        transform.LookAt(targetPosition);
        var moveSeq = DOTween.Sequence(transform);
        moveSeq.Append(transform.DOMoveY(jumpHigh, moveDuration / 2));
        moveSeq.Append(transform.DOMoveY(0f, moveDuration / 2));

        // if (targetPosition.z <= BackBoundary || targetPosition.x <= LeftBoundary || targetPosition.x >= RightBoundary)
        //     return;

        // if (Tree.AllPosition.Contains(targetPosition))
        //     return;


        // transform.DOMoveY(0.5f, 0.1f).onComplete(() => transform.DOMoveY(0, 0.1f));

        transform.DOMoveX(targetPosition.x, moveDuration);
        transform.DOMoveZ(targetPosition.z, moveDuration);
    }
    private bool IsJumping()
    {
        return DOTween.IsTweening(transform);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            AnimatedDie();
        }

    }

    private void AnimatedDie()
    {
        //(nilai, waktu) 
        //animasi gepeng.
        transform.DOScaleY(0.1f, 0.2f);
        transform.DOScaleX(1.1f, 0.2f);
        transform.DOScaleZ(1.1f, 0.2f);
        this.enabled = false;
        // dieParticles.Play();
    }

}
