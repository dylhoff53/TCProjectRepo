using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public LayerMask mask;
    public int magCount;
    public float speed;
    public float maxSpeed;
    private Vector2 move, mouseLook, joystickLook;
    public bool canMove;
    private Vector3 rotationTarget;
    public bool isPc;
    public float lastMoveStateX;
    public float lastMoveStateY;
    public bool isAcc;
    public float accCounter;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }

    public void OnJoystickLook(InputAction.CallbackContext context)
    {
        joystickLook = context.ReadValue<Vector2>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPc)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mouseLook);

            if(Physics.Raycast(ray, out hit, 1000f, mask))
            {
                rotationTarget = hit.point;
            }

            movePayerWithAim();
        } else
        {
            if(joystickLook.x == 0 && joystickLook.y == 0)
            {
                movePlayer();
            } else
            {
                movePayerWithAim();
            }
        }

        /*if (Physics.Raycast(ray, out hit, 500f, mask))
        {

            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<Enemy>();
            }
        }*/
    }

    public void movePlayer()
    {

        if(transform.position.x < -3.5f && move.x <= 0 || transform.position.x > 3.5f && move.x >= 0)
        {
            move.x = 0;
        }
        if(transform.position.y < -2.5f && move.y <= 0 || transform.position.y > 2.5f && move.y >= 0)
        {
            move.y = 0;
        }
        Vector3 movement = new Vector3(move.x, move.y, 0f).normalized;
        if(movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

    }

    public void movePayerWithAim()
    {
        if (isPc)
        {
            var lookPos = rotationTarget - transform.position;
            var rotation = Quaternion.LookRotation(lookPos);
            Vector3 aimDirection = new Vector3(rotationTarget.x, rotationTarget.y, 0f);

            if(aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
            }
        }
        else
        {
            Vector3 aimDirection = new Vector3(joystickLook.x, joystickLook.y, 0f);
            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), 0.15f);
            }
        }
        if (move.x != 0 || move.y != 0)
        {
            isAcc = true;
        }
        else if (move.x == 0 && move.y == 0)
        {
            isAcc = false;
        }

        if (isAcc == true)
        {
            if (speed < maxSpeed)
            {
                speed += accCounter;
            } else if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }else if(isAcc == false)
        {
            if (speed > 0f)
            {
                speed -= accCounter;
            } else if (speed < 0f)
            {
                speed = 0f;
            }
        }

        if (transform.position.x < -3.5f && move.x <= 0 || transform.position.x > 3.5f && move.x >= 0)
        {
            move.x = 0;
        }
        if (transform.position.y < -2.5f && move.y <= 0 || transform.position.y > 2.5f && move.y >= 0)
        {
            move.y = 0;
        }

        Vector3 movement = new Vector3(move.x, move.y, 0f).normalized;
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
        lastMoveStateX = move.x;
        lastMoveStateY = move.y;
    }
}
