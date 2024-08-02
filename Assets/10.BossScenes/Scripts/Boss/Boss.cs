using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    enum EnemyState {
        Idle,
        Attack1,
        Attack2,
        Attack3,
        Die
    }
    EnemyState enemyState;

    GameObject player;

    float currentTime = 2f;
    float attackDelay = 4f;

    public GameObject attack1Factory;
    public GameObject attack2Factory;
    public GameObject attack3Factory;

    public int attack2Prefabs = 5;

    public int hp = 200;
    int maxHp = 200;
    public Slider hpSlider;

    public GameObject Explosion;

    public BossCameraController cameraController;

    public Image attack3;
    public Slider attackGauge;

    public int attack3Damage = 40;

    Animator animator;

    public Text gameState;

    public AudioSource BossSound;
    public AudioSource ClickSound;

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Idle;

        player = GameObject.Find("Player");

        hp = maxHp;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState) {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Attack1:
                Attack1();
                break;
            case EnemyState.Attack2:
                break;
            case EnemyState.Attack3:
                break;
            case EnemyState.Die:
                Die();
                break;
        }

        hpSlider.value = hp;
    }

    void Idle() {
        currentTime += Time.deltaTime;
        if (currentTime >= attackDelay) {
            BossSound.Play();
            int randomIndex = Random.Range(0, 3);
            if (randomIndex == 0) {
                enemyState = EnemyState.Attack1; 
            }
            else if (randomIndex == 1) { 
                enemyState = EnemyState.Attack2;
                StartCoroutine(Attack2Coroutine());
            }
            else if (randomIndex == 2) {
                enemyState = EnemyState.Attack3;
                StartCoroutine(Attack3Coroutine()); 
            }
            currentTime = 0;
        }
    }

    void Attack1() {
        animator.SetTrigger("Attack");
        GameObject attack = Instantiate(attack1Factory);
        attack.transform.position = transform.position;
        Destroy(attack, 5f);
        enemyState = EnemyState.Idle;
    }

    void Die() { 
        gameObject.SetActive(false);

        GameObject explosion = Instantiate(Explosion);
        explosion.transform.localScale = new Vector3(10f, 10f, 1f);
        explosion.transform.position = transform.position;
        Destroy(explosion, 0.6f);

        gameState.gameObject.SetActive(true);
        gameState.text = "Game Clear";

        Invoke("EndScene", 3.5f);
    }

    public void Damaged(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            hp = 0;
            enemyState = EnemyState.Die;
        }
    }

    IEnumerator Attack2Coroutine() {
        animator.SetTrigger("Attack");
        float[] randomX = new float[attack2Prefabs];

        for (int i = 0; i < attack2Prefabs; i++)
        {
            randomX[i] = Random.Range(-8f, 0f);
        }

        for (int i = 0; i < attack2Prefabs; i++)
        {
            Vector3 spawnPosition = new Vector3(randomX[i], 5f, 0);
            GameObject attack2 = Instantiate(attack2Factory);
            attack2.transform.position = spawnPosition;
            Destroy(attack2, 5f);

            yield return new WaitForSeconds(1f);
        }
        enemyState = EnemyState.Idle;
    }

    IEnumerator Attack3Coroutine()
    {
        cameraController.StartZoom();
        PlayerMove playerMove = player.GetComponent<PlayerMove>();
        playerMove.enabled = false;

        GameObject leg = Instantiate(attack3Factory);
        Vector3 legPosition = new Vector3(playerMove.transform.position.x+10, playerMove.transform.position.y, playerMove.transform.position.z);
        leg.transform.position = legPosition;

        float attack3Time = 4f;
        float elapsedTime = 0f;
        attack3.gameObject.SetActive(true);
        attackGauge.value = 0f;

        bool canIncrease = true;

        while (elapsedTime < attack3Time)
        {
            elapsedTime += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.E) && canIncrease)
            {
                ClickSound.Play();
                attackGauge.value = Mathf.Clamp(attackGauge.value + 10, 0, 100);
                canIncrease = false;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                canIncrease = true;
            }

            attackGauge.value = Mathf.Clamp(attackGauge.value - 40 * Time.deltaTime, 0, 100);
            yield return null;
        }

        if (attackGauge.value < 70f)
        {
            Debug.Log("회피 실패");
            leg.GetComponent<Attack3>().Attack();
        }
        else
        {
            Debug.Log("회피 성공");
            leg.GetComponent<Attack3>().AttackFail();
        }

        yield return new WaitForSeconds(1f);

        Destroy(leg);
        cameraController.EndZoom();
        playerMove.enabled = true;
        attack3.gameObject.SetActive(false);
        enemyState = EnemyState.Idle;
    }

    void EndScene() {
        SceneManager.LoadScene("ClearScene");
    }
}
