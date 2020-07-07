using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
    public static PlayerAbilities instance;

    // contains all available abilities
    [SerializeField] public Ability[] allAbilities;

    // fields to display the cooldown in the UI
    private Text ab1coolDownTextDisplay;
    private Text ab2coolDownTextDisplay;

    // fields to manage the cooldown of ability 1
    private float ab1coolDownDuration;
    private float ab1nextReadyTime;
    private float ab1coolDownTimeLeft;
    private bool ab1Ready = true;

    // fields to manage the cooldown of ability 2
    private float ab2coolDownDuration;
    private float ab2nextReadyTime;
    private float ab2coolDownTimeLeft;
    private bool ab2Ready = true;

    // fields for the selected abilities themself
    private Ability selectedAbility1;
    private Ability selectedAbility2;

    PlayerInputActions inputAction;

    void Awake()
    {
        inputAction = new PlayerInputActions();
        inputAction.PlayerControls.Blink.performed += ctx => TriggerAbility1();
        inputAction.PlayerControls.Shoot.performed += ctx => TriggerAbility2();
    }

    void Start()
    {
        PlayerAbilities.instance = this;
        Initialize();
    }    

    public void Initialize()
    {
        // select the abilities according to the game manager
        selectedAbility1 = allAbilities[GameManagerScript.instance.selectedAbility1];
        selectedAbility2 = allAbilities[GameManagerScript.instance.selectedAbility2];

        // initialize the abilities
        selectedAbility1.Initialize(gameObject);
        selectedAbility2.Initialize(gameObject);

        // set the cooldown duration of the selected abilies
        ab1coolDownDuration = selectedAbility1.aBaseCoolDown;
        ab2coolDownDuration = selectedAbility2.aBaseCoolDown;

        // find the text in the UI to display ability cooldown
        ab1coolDownTextDisplay = GameObject.Find("UI/Canvas/Ability1/Text").GetComponent<Text>();
        ab2coolDownTextDisplay = GameObject.Find("UI/Canvas/Ability2/Text").GetComponent<Text>();

        ab1coolDownTextDisplay.enabled = false;
        ab2coolDownTextDisplay.enabled = false;
    }

    void TriggerAbility1 ()
    {
        if (GameManagerScript.instance.abilitiesEnabled)
        {
            if (ab1Ready)
            {
                ab1nextReadyTime = ab1coolDownDuration + Time.time;
                ab1coolDownTimeLeft = ab1coolDownDuration;
                ab1coolDownTextDisplay.enabled = true;
                Debug.Log("TriggerAbility1: " + selectedAbility1.name);
                selectedAbility1.TriggerAbility();
            } else
            {
                Debug.Log(ab1coolDownTimeLeft);
            }
        }
    }

    void TriggerAbility2 ()
    {
        if (GameManagerScript.instance.abilitiesEnabled)
        {
            if (ab2Ready)
            {
                ab2nextReadyTime = ab2coolDownDuration + Time.time;
                ab2coolDownTimeLeft = ab2coolDownDuration;
                ab2coolDownTextDisplay.enabled = true;
                Debug.Log("TriggerAbility2: " + selectedAbility2.name);
                selectedAbility2.TriggerAbility();
            } else
            {
                Debug.Log(ab2coolDownTimeLeft);
            }
        }
    }
    
    void Update()
    {
        // check if abilities are ready
        ab1Ready = (Time.time > ab1nextReadyTime);
        ab2Ready = (Time.time > ab2nextReadyTime);

        if (ab1Ready)
        {
            ab1coolDownTextDisplay.enabled = false;
        } else
        {
            ab1coolDownTimeLeft -= Time.deltaTime;
            float ab1roundedCd = Mathf.Round(ab1coolDownTimeLeft);
            ab1coolDownTextDisplay.text = ab1coolDownTimeLeft.ToString("F1");
        }

        if (ab2Ready)
        {
            ab2coolDownTextDisplay.enabled = false;
        } else
        {
            ab2coolDownTimeLeft -= Time.deltaTime;
            float ab2roundedCd = Mathf.Round(ab2coolDownTimeLeft);
            ab2coolDownTextDisplay.text = ab2coolDownTimeLeft.ToString("F1");
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
