using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovMobile : MonoBehaviour
{
    [Header("Configurações do Player")]
    public float speed;

    private Camera mainCamera;
    private float minX, maxX, minY, maxY;
    private float inicialSpeed;

    [Foldout("")] public DynamicJoystick joystickD;
    [Foldout("")] public FixedJoystick joystickF;
    [Foldout("")] public GameManager gManager;

    void Start()
    {
        mainCamera = Camera.main;
        CalculateMovementLimits();
        gManager = FindObjectOfType<GameManager>();
        inicialSpeed = speed;
    }

    void Update()
    {
        Vector2 direction = Vector2.zero;
        if(gManager.isPaused)
        {
            speed = 0;
        }
        else
        {
            speed = inicialSpeed;
        }

        if (joystickD != null && joystickD.gameObject.activeInHierarchy)
        {
            direction.x = joystickD.Horizontal;
            direction.y = joystickD.Vertical;
        }
        else if (joystickF != null && joystickF.gameObject.activeInHierarchy)
        {
            direction.x = joystickF.Horizontal;
            direction.y = joystickF.Vertical;
        }

        MovePlayer(direction);
    }

    void MovePlayer(Vector2 direction)
    {
        Vector3 movement = new Vector3(direction.x, direction.y, 0f) * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + movement;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;

        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Rotaciona para a direita
        }
        else if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Rotaciona para a esquerda
        }
    }

    void CalculateMovementLimits()
    {
        if (mainCamera != null)
        {
            float playerHalfWidth = transform.localScale.x / 2f;
            float playerHalfHeight = transform.localScale.y / 2f;

            Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
            Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

            minX = bottomLeft.x + playerHalfWidth;
            maxX = topRight.x - playerHalfWidth;
            minY = bottomLeft.y + playerHalfHeight;
            maxY = topRight.y - playerHalfHeight;
        }
    }
}
