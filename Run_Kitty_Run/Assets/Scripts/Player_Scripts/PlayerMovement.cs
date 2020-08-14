using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour 
{
    public Rigidbody2D playerRigidbody;
    //Sprites for player idle state
    public Sprite Up;
    public Sprite Down;
    public Sprite Right;
    public Sprite Left;
    private SpriteRenderer spriteRenderer;
    private Animator playerAnimator;
    //Float for base speed and input action, vectors and bool for player movement
    public float speed = 4.5F;
    private PlayerInputActions inputAction;
    private Vector2 movementInput;
    private Vector3 movement;
    private Vector3 inputDirection;
    private bool activation;
    //Float array for possible rotation angles
    private float[] possibleAngles = {-135,-45,45,135};
    
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
        //Calculate player movement based on performed input actions and play according movement animation
        float h = movementInput.x;
        float v = movementInput.y;
        var targetInput = new Vector3(h,v,0F);
        inputDirection = Vector3.Lerp(inputDirection, targetInput, Time.deltaTime*20F);
        MoveThePlayer(inputDirection);
        AnimateThePlayer();
    }

    //Move player rigidbody based on calculated movement
    void MoveThePlayer(Vector3 desiredDirection) 
    {
        movement.Set(desiredDirection.x, desiredDirection.y, 0F);
        movement = movement * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }
    
    //Play according movement animation depending on input actions (based on WASD)
    void AnimateThePlayer()
    {
        if(activation){
            ClearAnimations();
            if(Keyboard.current.wKey.isPressed){
                playerAnimator.SetBool("isWalkingUp", true);
            } else  if(Keyboard.current.sKey.isPressed){
                playerAnimator.SetBool("isWalkingDown", true);      
            } else  if(Keyboard.current.aKey.isPressed){
                playerAnimator.SetBool("isWalkingLeft", true);
            } else if (Keyboard.current.dKey.isPressed){
                playerAnimator.SetBool("isWalkingSide", true);
            }
        }
    }

    //Clear by leaving all movement animation states to avoid overlaying
    private void ClearAnimations(){
        playerAnimator.SetBool("isWalkingUp", false);
        playerAnimator.SetBool("isWalkingDown", false);
        playerAnimator.SetBool("isWalkingSide", false);
        playerAnimator.SetBool("isWalkingLeft", false);
    }

    void LateUpdate()
    {
        TurnThePlayer();
    }

    //Rotate player in idle animation state by using the suitable sprite
    //Therefore calculate the angle between player and mouse cursor position
    void TurnThePlayer() 
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        //Slice the possible rotation angles (360°) into four quarters and assign a fitting sprite to each one
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (angle < possibleAngles[3] && angle > possibleAngles[2])
            {
                spriteRenderer.sprite = Up;
            }
            else if (angle >= possibleAngles[1] && angle <= possibleAngles[2])
            {
                spriteRenderer.sprite = Right;
            }
            else if (angle < possibleAngles[1] && angle > possibleAngles[0])
            {
                spriteRenderer.sprite = Down;
            }
            else
            {
                spriteRenderer.sprite = Left;
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
