using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterManager : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject visualCue;
    private Animator animator;

    private int direction = 0;

    private bool canMoveUp = true;
    private bool canMoveDown = true;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    public void SetVisualCue(bool toggle)
    {
        visualCue.SetActive(toggle);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        visualCue.SetActive(false);
        direction = 0;
    }

    void Start()
    {
        characterTransform = GetComponent<Transform>();
        animator.SetBool("isIdle", true);
        animator.SetBool("isRunningLeft", false);
        animator.SetBool("isRunningRight", false);
    }

    private void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (Input.GetKey(KeyCode.W) && canMoveUp)
            {
                characterTransform.position += Vector3.up * speed * Time.deltaTime;
                animator.SetBool("isIdle", false);
                direction = 1;
            }
            else if (Input.GetKey(KeyCode.S) && canMoveDown)
            {
                characterTransform.position += Vector3.down * speed * Time.deltaTime;
                animator.SetBool("isIdle", false);
                direction = 3;
            }
            else if (Input.GetKey(KeyCode.A) && canMoveLeft)
            {
                characterTransform.position += Vector3.left * speed * Time.deltaTime;
                animator.SetBool("isIdle", false);
                animator.SetBool("isRunningLeft", true);
                animator.SetBool("isRunningRight", false);
                direction = 4;
            }
            else if (Input.GetKey(KeyCode.D) && canMoveRight)
            {
                characterTransform.position += Vector3.right * speed * Time.deltaTime;
                animator.SetBool("isIdle", false);
                animator.SetBool("isRunningLeft", false);
                animator.SetBool("isRunningRight", true);
                direction = 2;
            }
            else
            {
                animator.SetBool("isIdle", true);
                animator.SetBool("isRunningLeft", false);
                animator.SetBool("isRunningRight", false);
                direction = 0;
            }
        }

        cameraTransform.position = new Vector3(characterTransform.position.x, characterTransform.position.y, cameraTransform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Edificio") || collision.gameObject.CompareTag("Pared"))
        {
            switch (direction)
            {
                case 1:
                    canMoveUp = false;
                    break;
                case 2:
                    canMoveRight = false;
                    break;
                case 3:
                    canMoveDown = false;
                    break;
                case 4:
                    canMoveLeft = false;
                    break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Edificio") || collision.gameObject.CompareTag("Pared"))
        {
            canMoveUp = true;
            canMoveDown = true;
            canMoveLeft = true;
            canMoveRight = true;
        }
    }
}
