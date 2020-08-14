using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
    public static PlayerAbilities instance;

    //contains all available abilities
    [SerializeField] public Ability[] allAbilities;

    // fields to display the cooldown in the UI
    private Image ab1coolDownOverlay;
    private Image ab1element;
    private Image ab1icon;
    private Image ab2coolDownOverlay;
    private Image ab2element;
    private Image ab2icon;

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

        // find ability UI elements & icons
        ab1element = GameObject.Find("UI/Canvas/Ability1").GetComponent<Image>();
        ab2element = GameObject.Find("UI/Canvas/Ability2").GetComponent<Image>();
        ab1icon = GameObject.Find("UI/Canvas/Ability1/Icon").GetComponent<Image>();
        ab2icon = GameObject.Find("UI/Canvas/Ability2/Icon").GetComponent<Image>();

        // find the object in the UI to display ability cooldown
        ab1coolDownOverlay = GameObject.Find("UI/Canvas/Ability1/CooldownOverlay").GetComponent<Image>();
        ab2coolDownOverlay = GameObject.Find("UI/Canvas/Ability2/CooldownOverlay").GetComponent<Image>();
    }

    void TriggerAbility1 ()
    {
        if (GameManagerScript.instance.abilitiesEnabled)
        {
            if (ab1Ready)
            {
                ab1nextReadyTime = ab1coolDownDuration + Time.time;
                ab1coolDownTimeLeft = ab1coolDownDuration;
                Debug.Log("TriggerAbility1: " + selectedAbility1.name);
                selectedAbility1.TriggerAbility();
            } else
            {
                Debug.Log(selectedAbility1.name + " cooldown left: " + ab1coolDownTimeLeft);
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
                Debug.Log("TriggerAbility2: " + selectedAbility2.name);
                selectedAbility2.TriggerAbility();
            } else
            {
                Debug.Log(selectedAbility2.name + " cooldown left: " + ab2coolDownTimeLeft);
            }
        }
    }

    void Update()
    {
        // check if abilities are ready
        ab1Ready = (Time.time > ab1nextReadyTime);
        ab2Ready = (Time.time > ab2nextReadyTime);
        // get color for transparency check
        var tempColor = ab1element.color;

        if (ab1Ready)
        {
            tempColor.a = 1f;
            ab1element.color = tempColor;
            ab1icon.color = tempColor;
            ab1coolDownOverlay.fillAmount = 0;
        }
        else
        {
            tempColor.a = 0.15f;
            ab1element.color = tempColor;
            ab1icon.color = tempColor;
            ab1coolDownTimeLeft -= Time.deltaTime;
            float ab1roundedCd = Mathf.Round(ab1coolDownTimeLeft);
            ab1coolDownOverlay.fillAmount = ab1coolDownTimeLeft / ab1coolDownDuration;
        }

        if (ab2Ready)
        {
            tempColor.a = 1f;
            ab2element.color = tempColor;
            ab2icon.color = tempColor;
            ab2coolDownOverlay.fillAmount = 0;
        }
        else
        {
            tempColor.a = 0.15f;
            ab2element.color = tempColor;
            ab2icon.color = tempColor;
            ab2coolDownTimeLeft -= Time.deltaTime;
            float ab2roundedCd = Mathf.Round(ab2coolDownTimeLeft);
            ab2coolDownOverlay.fillAmount = ab2coolDownTimeLeft / ab2coolDownDuration;
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
