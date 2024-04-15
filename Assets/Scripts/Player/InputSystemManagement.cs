using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemManagement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator ani;
    Weapon weapon;
    HealthManager health;

    public WeaponObject weaponObject;

    Vector2 moveVec;
    public Vector3 mousePos;
    public Vector3 direction;

    //Check time to fire
    public float nextFireTime;

    public bool isAttack;

    public bool isStrengthen;

    public bool isReadyUltimate;

    public bool isFirePressed;


    // get direction oject
    Transform directionTemplate;


    // Ultimate skill 
    UltimateAttack ultimate;


    //sfx sounds

    SoundManager sfx;



    // Input System
    

    // Get vector movement.
    public void OnMove(InputAction.CallbackContext context)
    {
        moveVec = context.ReadValue<Vector2>();
    }


    // Get mouse position
    public void OnMousePosition(InputAction.CallbackContext context)
    {
        if (GameManager.instance.isPause || !health.isLive) return;
        mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        GameManager.instance.mousePos = mousePos;
        Vector3 dir = mousePos - transform.position;
        GameManager.instance.direction = dir.normalized;
        direction = dir.normalized;
    }


    // tab
    public void OnShowSkillUpgraded(InputAction.CallbackContext context)
    {
        GameObject view = GetComponent<LevelUpPlayer>().listViewSkillsUpgraded;
        if (context.started)
        {
            view.SetActive(true);
            FindObjectOfType<UIManagement>().propertyPopup.SetActive(true);
        }
        else if (context.canceled)
        {
            view.SetActive(false);
            FindObjectOfType<UIManagement>().propertyPopup.SetActive(false);
        }
    }

    //Dash
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!GetComponent<SkillMaxUpgraded>().isSkillDashUnlock) return;
            if (!GetComponent<SkillMaxUpgraded>().isReadyDash) return;
            GetComponent<SkillMaxUpgraded>().MaxLevelMoveSpeed();
        }
    }

    public void OnDirectionWeaponJoystick(InputAction.CallbackContext context)
    {
        if (GameManager.instance.isPause || !health.isLive) return;
        if (context.ReadValue<Vector2>() == Vector2.zero) return;
        GameManager.instance.direction = context.ReadValue<Vector2>().normalized;
        direction = context.ReadValue<Vector2>().normalized;
    }



    // Get Fire event

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!health.isLive || GameManager.instance.isPause) return;
        if (context.started)
        {
            isFirePressed = true;
        }
        else if (context.canceled)
        {
            isFirePressed = false;
        }
       
    } 

    public void OnUltimateSkill(InputAction.CallbackContext context)
    {
        if (!isReadyUltimate || GameManager.instance.isPause ||!health.isLive) return;
        if (context.started)
        {
            if(GetComponent<PlayerSetting>().weapon.isGun)
            isAttack = false;
            isStrengthen = true;
            if (!weaponObject.isGun)
            {
                ultimate.AttackUltimateSword();
            }
        }
        if (context.canceled)
        {
            isStrengthen = false;
            if (weaponObject.isGun)
            {
                ultimate.AttackUltimate();
            }
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.instance.isPause = true;
            FindObjectOfType<UIManagement>().pauseGame.gameObject.SetActive(true);
            FindObjectOfType<UIPauseGame>().PauseGame();
        }
    }


    //##############################################
    //##############################################

    private void Awake()
    {
        
        isAttack = false;
        sfx = FindObjectOfType<SoundManager>();
        health = GetComponent<HealthManager>();
    }

    private void Start()
    {
        weaponObject = GetComponent<PlayerSetting>().weapon;
        rb = GetComponent<Rigidbody2D>();
        ani = transform.GetChild(0).GetComponent<Animator>();
        weapon = transform.GetChild(2).GetComponent<Weapon>();
        nextFireTime = 0;
        directionTemplate = transform.GetChild(3).transform;
        ultimate = GetComponent<UltimateAttack>();
        if (GetComponent<PlayerSetting>().weapon.isGun)
        {
            isReadyUltimate = ultimate.isReadyUltimate;
        }
        else
        {
            isReadyUltimate = ultimate.isReadyUltimateSword;
        }
        
    }
    private void FixedUpdate()
    {
        if (!health.isLive) return;
        rb.MovePosition(transform.position + (Vector3)moveVec*GetComponent<PlayerSetting>().moveSpeed*Time.fixedDeltaTime);
    }
    private void Update()
    {
        if (!health.isLive) return;
        // change isAttack
        if(isFirePressed)
        {
            if (weaponObject.isGun)
            {
                if(!isStrengthen) isAttack = true;
            }
            else
            {
                isAttack = true;
            }

        }
        else
        {
            isAttack = false;
        }



        MoveAnimator();
        if (isAttack && Time.time > nextFireTime)
        {
            Attack();
           
        }
        else if(!isAttack)
        {
            weapon.AttackFinish();
        }

        DirectUnltimateSkill();
    }

    private void LateUpdate()
    {
        if (!health.isLive) return;
        TurningFace();
        
    }


    


    //#############################FUNCTION##################################

    // check animation Run when player run.
    public void MoveAnimator()
    {
        ani.SetBool("Run", moveVec != Vector2.zero);
    }


    // direct ultimate skill

    void DirectUnltimateSkill()
    {
        if (!GetComponent<PlayerSetting>().weapon.isGun) return;
        directionTemplate.gameObject.SetActive(isStrengthen);
        if (!isStrengthen || GameManager.instance.aim == GameManager.aimingStyle.nearestEnemy) return;
        float angle = Mathf.Atan2(GameManager.instance.direction.y, GameManager.instance.direction.x) * Mathf.Rad2Deg;
        directionTemplate.rotation = Quaternion.Euler(0, 0, angle-90);

    }


    // Turning face player.
    public void TurningFace()
    {
        Vector3 faceDir = transform.GetChild(0).transform.localScale;
        if (moveVec.x > 0)
        {
            faceDir.x = 1;
        }
        else if(moveVec.x<0)
        {
            faceDir.x = -1;
        }
        //faceDir.x = moveVec.x > 0 ? 1 : -1;
        transform.GetChild(0).transform.localScale = faceDir;
    }

    //Attack
    public void Attack()
    {
        
        nextFireTime = Time.time + GetComponent<PlayerSetting>().attackDelay;
        weapon.Attack();
    }    
}



