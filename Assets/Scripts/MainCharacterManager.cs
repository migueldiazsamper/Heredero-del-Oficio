using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterManager : MonoBehaviour
{
    public Transform characterTransform;
    [SerializeField] float speed = 1.0f;
    public Transform cameraTransform;
    void Start()
    {
        characterTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            characterTransform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            characterTransform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            characterTransform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            characterTransform.position += Vector3.right * speed * Time.deltaTime;
        }

        cameraTransform.position = new Vector3(characterTransform.position.x, characterTransform.position.y, cameraTransform.position.z);
    }
}
