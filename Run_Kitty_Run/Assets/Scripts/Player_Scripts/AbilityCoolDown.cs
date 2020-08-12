using UnityEngine;
using UnityEngine.UI;


public class AbilityCoolDown : MonoBehaviour
{

    public string abilityButtonAxisName = "Fire1";
    public Text coolDownTextDisplay;
    public Image cooldownOverlay;

    [SerializeField] private Ability ability;
    private GameObject player;
    private Image myButtonImage;
    private AudioSource abilitySource;
    private float coolDownDuration;
    private float nextReadyTime;
    private float coolDownTimeLeft;

    PlayerInputActions inputAction;


    void Start()
    {
        player = GameObject.Find("Player");
        Initialize(ability, player);
    }

     void Awake() {
        inputAction = new PlayerInputActions();
        inputAction.PlayerControls.Blink.performed += ctx => Blink();
        inputAction.PlayerControls.Shoot.performed += ctx => Shoot();
    }

    public void Initialize(Ability selectedAbility, GameObject firePointHolder)
    {
        ability = selectedAbility;
        coolDownDuration = ability.aBaseCoolDown;
        ability.Initialize(firePointHolder);
        AbilityReady();
    }

    void Blink(){
        if (GameManagerScript.instance.abilitiesEnabled)
        {
            bool coolDownComplete = (Time.time > nextReadyTime);
            if (coolDownComplete)
            {
                AbilityReady();
                ButtonTriggered();
            }
            else
            {
                CoolDown();
            }
        }
    }

    void Shoot(){
        if (GameManagerScript.instance.abilitiesEnabled)
        {
            bool coolDownComplete = (Time.time > nextReadyTime);
            if (coolDownComplete)
            {
                AbilityReady();
                ButtonTriggered();
            }
            else
            {
                CoolDown();
            }
        }
    }

    private void AbilityReady()
    {
        coolDownTextDisplay.enabled = false;
        cooldownOverlay.fillAmount = 0;
    }

    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        cooldownOverlay.fillAmount = coolDownTimeLeft/coolDownDuration;

    }

    private void ButtonTriggered()
    {
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;
        coolDownTextDisplay.enabled = true;
        
        ability.TriggerAbility();
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
