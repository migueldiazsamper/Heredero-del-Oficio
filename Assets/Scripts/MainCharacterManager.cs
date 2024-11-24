using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterManager : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    public Transform characterTransform;
    public Transform cameraTransform;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        characterTransform = GetComponent<Transform>();
        animator.SetBool("isIdle", true);
        animator.SetBool("isRunningLeft", false);
        animator.SetBool("isRunningRight", false);
    }

    void Update()
    {
        if ( ! DialogueManager.GetInstance().dialogueIsPlaying )
        {
            if ( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) )
            {
                if (Input.GetKey(KeyCode.W))
                {
                    characterTransform.position += Vector3.up * speed * Time.deltaTime;
                    animator.SetBool("isIdle", false);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    characterTransform.position += Vector3.down * speed * Time.deltaTime;
                    animator.SetBool("isIdle", false);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    characterTransform.position += Vector3.left * speed * Time.deltaTime;
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isRunningLeft", true);
                    animator.SetBool("isRunningRight", false);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    characterTransform.position += Vector3.right * speed * Time.deltaTime;
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isRunningLeft", false);
                    animator.SetBool("isRunningRight", true);
                }
            }
            else
            {
                animator.SetBool("isIdle", true);
                animator.SetBool("isRunningLeft", false);
                animator.SetBool("isRunningRight", false);
            }
        }

        cameraTransform.position = new Vector3(characterTransform.position.x, characterTransform.position.y, cameraTransform.position.z);
    }
}
