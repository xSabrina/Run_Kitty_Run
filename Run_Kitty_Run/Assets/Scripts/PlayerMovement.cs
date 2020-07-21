using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour 
{
    public Rigidbody2D playerRigidbody;
    public Sprite Up;
    public Sprite Down;
    public Sprite Side;
    public float speed = 4.5f;
    SpriteRenderer spriteRenderer;
    Animator playerAnimator;
    PlayerInputActions inputAction;
    Vector2 movementInput;
    Vector3 movement;
    Vector3 inputDirection;

    private bool activation;
    
    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        activation = false;
    }

    void Awake() {
        inputAction = new PlayerInputActions();
        inputAction.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputAction.PlayerControls.Move.performed += ctx => activation = true;
    }

    void FixedUpdate() 
    {
        float h = movementInput.x;
        float v = movementInput.y;
        var targetInput = new Vector3(h,v,0);
        inputDirection = Vector3.Lerp(inputDirection, targetInput, Time.deltaTime*20f);
        MoveThePlayer(inputDirection);
        AnimateThePlayer();
    }

    void MoveThePlayer(Vector3 desiredDirection) 
    {
        movement.Set(desiredDirection.x, desiredDirection.y, 0f);
        movement = movement * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }
    
    void AnimateThePlayer()
    {
        if(activation){
            ClearAnimations();
            if(Keyboard.current.wKey.isPressed){
                playerAnimator.SetBool("isWalkingUp", true);
            } else  if(Keyboard.current.sKey.isPressed){
                playerAnimator.SetBool("isWalkingDown", true);      
            } else  if(Keyboard.current.aKey.isPressed){
                playerAnimator.SetBool("isWalkingSide", true);    
                transform.rotation = Quaternion.Euler(0, 180, 0);
            } else if (Keyboard.current.dKey.isPressed){
                playerAnimator.SetBool("isWalkingSide", true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void ClearAnimations(){
        playerAnimator.SetBool("isWalkingUp", false);
        playerAnimator.SetBool("isWalkingDown", false);
        playerAnimator.SetBool("isWalkingSide", false);
    }

    void LateUpdate()
    {
        TurnThePlayer();
    }

    void TurnThePlayer() 
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("UseSkill"))
        {
            //Adjust player sprite depending on angle
            if (angle < -225 && angle > -315 || angle < 135 && angle > 45)
            {
                spriteRenderer.sprite = Up;
            }
            else if (angle >= -45 && angle <= 45)
            {
                spriteRenderer.sprite = Side;
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }
            else if (angle < -45 && angle > -125)
            {
                spriteRenderer.sprite = Down;
            }
            else
            {
                spriteRenderer.sprite = Side;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }


}
