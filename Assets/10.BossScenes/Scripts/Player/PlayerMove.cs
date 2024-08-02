using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerMove : MonoBehaviour
{
    public float speed = 3.0f;
    Vector3 dir;

    public GameObject bulletFactory;
    public GameObject bombFactory;

    public GameObject bulletPosition;
    public GameObject bombPosition;

    float currentTime = 6;
    float bombTime = 6;

    public int hp = 100;
    int maxHp = 100;
    public Slider hpSlider;

    public Image bombGauge;

    public GameObject Explosion;

    public Text gameState;

    public AudioSource bulletSound;
    public AudioSource BombSound;
    public AudioSource aidSound;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        dir = new Vector3(h, v, 0);

        transform.position += dir * speed * Time.deltaTime;

        if (Input.GetMouseButtonDown(0)&&Time.timeScale == 1f) {
            bulletSound.Play();

            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = bulletPosition.transform.position;
            Destroy(bullet,5f);
        }

        currentTime += Time.deltaTime;
        bombGauge.fillAmount = Mathf.Clamp01(currentTime / bombTime);

        if (currentTime > bombTime) { 
            if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1f) {
                BombSound.Play();
                GameObject bomb = Instantiate(bombFactory);
                bomb.transform.position = bombPosition.transform.position;
                currentTime = 0;
            }
        }
        hpSlider.value = hp;
        if (hp == 0)
        {
            gameObject.SetActive(false);
            GameObject explosion = Instantiate(Explosion);
            explosion.transform.position = transform.position;
            Destroy(explosion, 0.6f);

            gameState.gameObject.SetActive(true);
            gameState.text = "Game Over";

            Invoke("EndScene", 2f);
        }
    }

    public void Damaged(int damage) {
        GameObject explosion = Instantiate(Explosion);
        explosion.transform.localScale = new Vector3(3f, 3f, 1f);
        explosion.transform.position = transform.position;
        Destroy(explosion, 0.6f);
        hp -= damage;
        if (hp < 0) { 
            hp = 0;
        }
    }
    public void Heal(int damage)
    {
        aidSound.Play();
        hp += damage;
        if (hp > 100)
        {
            hp = 100;
        }
    }

    void EndScene() {
        SceneManager.LoadScene("DefeatScene");
    }
}
