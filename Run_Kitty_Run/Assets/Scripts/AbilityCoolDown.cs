using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AbilityCoolDown : MonoBehaviour
{

    public string abilityButtonAxisName = "Fire1";
    //public Image darkMask;
    public Text coolDownTextDisplay;

    [SerializeField] private Ability ability;
    private GameObject player;
    private Image myButtonImage;
    private AudioSource abilitySource;
    private float coolDownDuration;
    private float nextReadyTime;
    private float coolDownTimeLeft;


    void Start()
    {
        player = GameObject.Find("Player");
        Initialize(ability, player);
    }

    public void Initialize(Ability selectedAbility, GameObject firePointHolder)
    {
        ability = selectedAbility;
        //myButtonImage = GetComponent<Image>();
        //abilitySource = GetComponent<AudioSource>();
        //myButtonImage.sprite = ability.aSprite;
        //darkMask.sprite = ability.aSprite;
        coolDownDuration = ability.aBaseCoolDown;
        ability.Initialize(firePointHolder);
        AbilityReady();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.instance.abilitiesEnabled)
        {
            bool coolDownComplete = (Time.time > nextReadyTime);
            if (coolDownComplete)
            {
                AbilityReady();
                if (Input.GetButtonDown(abilityButtonAxisName))
                {
                    Debug.Log("Ability triggered: " + ability.name);
                    ButtonTriggered();
                }
            }
            else
            {
                CoolDown();
                if (Input.GetButtonDown(abilityButtonAxisName))
                {
                    Debug.Log(coolDownTimeLeft);
                }
            }
        }
    }

    private void AbilityReady()
    {
        coolDownTextDisplay.enabled = false;
        //darkMask.enabled = false;
    }

    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(coolDownTimeLeft);
        coolDownTextDisplay.text = coolDownTimeLeft.ToString("F1");
        //darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);

    }

    private void ButtonTriggered()
    {
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;
        //darkMask.enabled = true;
        coolDownTextDisplay.enabled = true;

        //abilitySource.clip = ability.aSound;
        //abilitySource.Play();
        ability.TriggerAbility();
    }
}