using UnityEngine;

public enum WeaponType
{
    Bullet,
    Bomb
}

public class Player : MonoBehaviour
{
    // Weaponry
    public GameObject bullet;
    public GameObject bomb;
    public Vector2 groundDispenseVelocity;      // 수평 이동 속도
    public Vector2 verticalDispenseVelocity;    // 초기 수직 속도

    // References
    public Transform trnsGun;
    public Transform trnsGunTip;
    public GameObject playerGun;
    public GameObject playerBomb;

    public SpriteRenderer sprRndPlayer;
    public SpriteRenderer sprRndGun;

    // Movement
    public float movementSpeed;
    private Vector2 inputVector;

    // Animation
    private int curAnimIndex = 0;
    private float animTimer = 0;
    public int fps;

    public Sprite[] spritesRunRight;
    public Sprite[] spritesRunLeft;
    public Sprite[] spritesRunUp;
    public Sprite[] spritesRunDown;
    public Sprite spriteIdleRight;
    public Sprite spriteIdleleft;
    public Sprite spriteIdleUp;
    public Sprite spriteIdleDown;

    private Vector2 mousePos;
    private Vector2 mouseWorldPos;

    public WeaponType currentWeapon = WeaponType.Bullet;

    void Update()
    {
        Movement();
        RotateGun();
        Animate();
        FlipSprites();
        ChangeWeapon();
        Shoot();
    }

    void Movement()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        if (inputVector != Vector2.zero)
        {
            transform.position += (Vector3)inputVector.normalized * movementSpeed * Time.deltaTime;
        }
    }

    void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentWeapon = WeaponType.Bullet;
            trnsGun = playerGun.transform;
            trnsGunTip = playerGun.transform.GetChild(0);
            playerGun.SetActive(true);
            playerBomb.SetActive(false);

        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            currentWeapon = WeaponType.Bomb;
            trnsGun = playerBomb.transform;
            trnsGunTip = playerBomb.transform.GetChild(0);
            playerGun.SetActive(false);
            playerBomb.SetActive(true);
        }
    }

    void RotateGun()
    {
        mousePos = Input.mousePosition;

        // Set Weapons
        Vector3 objectPos = Camera.main.WorldToScreenPoint(trnsGun.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        trnsGun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (currentWeapon)
            {
                // Weapon 1: 총구에 총알 생성
                case WeaponType.Bullet:
                    GameObject instantiatedBullet = Instantiate(bullet, trnsGunTip.position, trnsGunTip.rotation);
                    break;

                // Weapon 2: 총구에 수류탄 생성
                case WeaponType.Bomb:
                    GameObject instantiatedBomb = Instantiate(bomb, trnsGunTip.position, Quaternion.identity);
                    instantiatedBomb.GetComponent<FakeHeightObject>().Initialize(trnsGun.right * Random.Range(groundDispenseVelocity.x, groundDispenseVelocity.y), Random.Range(verticalDispenseVelocity.x, verticalDispenseVelocity.y));
                    break;
            }
        }
    }

    void Animate()
    {
        if (inputVector != Vector2.zero)
        {
            Sprite[] currentAnimation = spritesRunRight;

            if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
            {
                if (inputVector.x > 0)
                {
                    currentAnimation = spritesRunRight;
                }
                else
                {
                    currentAnimation = spritesRunLeft;
                }
            }
            else
            {
                if (inputVector.y > 0)
                {
                    currentAnimation = spritesRunUp;
                }
                else
                {
                    currentAnimation = spritesRunDown;
                }
            }

            if (animTimer > 1f / fps)
            {
                sprRndPlayer.sprite = currentAnimation[curAnimIndex];

                curAnimIndex++;

                if (curAnimIndex >= currentAnimation.Length)
                    curAnimIndex = 0;

                animTimer = 0;
            }

            animTimer += Time.deltaTime;
        }
        else
        {
            UpdateIdleSprite();
            curAnimIndex = 0;
            animTimer = 0;
        }
    }

    void UpdateIdleSprite()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Mathf.Abs(mouseWorldPos.x - transform.position.x) > Mathf.Abs(mouseWorldPos.y - transform.position.y))
        {
            if (mouseWorldPos.x > transform.position.x)
            {
                sprRndPlayer.sprite = spriteIdleRight;
            }
            else
            {
                sprRndPlayer.sprite = spriteIdleleft;
            }
        }
        else
        {
            if (mouseWorldPos.y > transform.position.y)
            {
                sprRndPlayer.sprite = spriteIdleUp;
            }
            else
            {
                sprRndPlayer.sprite = spriteIdleDown;
            }
        }
    }

    void FlipSprites()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mouseWorldPos.x > transform.position.x)
        {
            sprRndGun.flipY = false;
            trnsGunTip.localPosition = new Vector3(trnsGunTip.localPosition.x, Mathf.Abs(trnsGunTip.localPosition.y), trnsGunTip.localPosition.z);
        }
        else if (mouseWorldPos.x <= transform.position.x)
        {
            sprRndGun.flipY = true;
            trnsGunTip.localPosition = new Vector3(trnsGunTip.localPosition.x, -Mathf.Abs(trnsGunTip.localPosition.y), trnsGunTip.localPosition.z);
        }
    }

}
