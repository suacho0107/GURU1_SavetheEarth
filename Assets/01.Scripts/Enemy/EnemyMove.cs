using System.Collections;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    #region ���� ����
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    private Rigidbody2D enemyRb;
    private Transform player;

    private float enemySpeed = 3f;
    private float originSpeed;
    private float detectionRange = 8f;
    private float stopChasingDistance = 4f;
    private float nextFireTime;
    private float fireRate = 2f;
    private int moveDir;
    private int curDir;
    private bool canChange;
    private Vector2 moveDirection;
    #endregion

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        moveDir = Random.Range(1, 5); // 1-4  -- 1=down 2=left 3=right 4=up
        if (moveDir < 1 || moveDir > 4)
        {
            while (moveDir < 1 || moveDir > 4) { moveDir = Random.Range(1, 5); }
        }
        canChange = true; // ���� ���� ���� ���

        // �÷��̾� ������Ʈ ã��
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nextFireTime = Time.time + fireRate;

        // �⺻ �ӵ� ����
        originSpeed = enemySpeed;
    }

    private void Update()
    {
        if (player == null)
            return;

        float _distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (_distanceToPlayer <= detectionRange)
        {
            // �÷��̾� ����(�ӵ� ����)
            FollowPlayer(_distanceToPlayer);
            enemySpeed = 5f;
        }
        else
        {
            // �⺻ �̵�
            enemySpeed = originSpeed;
            MovementHandler();
        }
    }

    private void FixedUpdate()
    {
        // �÷��̾� �̵�
        // moveDirection ������ ������ �̵� ���� ����
        enemyRb.MovePosition(enemyRb.position + (moveDirection * enemySpeed) * Time.fixedDeltaTime);
    }

    #region �� �̵�
    void ChangeDirection()
    {
        // Can move in any direction
        if (EnemyDetection.pathOpenDown == true && EnemyDetection.pathOpenLeft == true && EnemyDetection.pathOpenRight == true &&
                                                                                                 EnemyDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5); // 1-4  -- 1=down 2=left 3=right 4=up
            if (moveDir < 1 || moveDir > 4)
            {
                while (moveDir < 1 || moveDir > 4)
                {
                    moveDir = Random.Range(1, 5);
                    if (moveDir == curDir)
                    {
                        moveDir = Random.Range(1, 5);
                    }
                }
            }
        }

        // Cannot move right
        else if (EnemyDetection.pathOpenDown == true && EnemyDetection.pathOpenLeft == true && EnemyDetection.pathOpenRight == false &&
                                                                                                     EnemyDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5); // 1-4  -- 1=down 2=left 3=right 4=up
            while (moveDir < 1 || moveDir > 4 || moveDir == 3)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        // Cannot move Left or Right
        else if (EnemyDetection.pathOpenDown == true && EnemyDetection.pathOpenLeft == false && EnemyDetection.pathOpenRight == false &&
                                                                                                      EnemyDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5);
            while (moveDir < 1 || moveDir > 4 || moveDir == 2 || moveDir == 3)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        // Can only move up
        else if (EnemyDetection.pathOpenDown == false && EnemyDetection.pathOpenLeft == false && EnemyDetection.pathOpenRight == false &&
                                                                                                       EnemyDetection.pathOpenUp == true)
        {
            moveDir = 4;
        }

        // Can only move Up or Right
        else if (EnemyDetection.pathOpenDown == false && EnemyDetection.pathOpenLeft == false && EnemyDetection.pathOpenRight == true &&
                                                                                                      EnemyDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5);// 1-4  -- 1=down 2=left 3=right 4=up
            while (moveDir < 3 || moveDir > 4) // can only be 3 or 4
            {
                moveDir = Random.Range(1, 5);
            }
        }

        // Can move Left, Right or Up
        else if (EnemyDetection.pathOpenDown == false && EnemyDetection.pathOpenLeft == true && EnemyDetection.pathOpenRight == true &&
                                                                                                     EnemyDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5);
            while (moveDir > 4 || moveDir < 2) // Cannot be 1
            {
                moveDir = Random.Range(1, 5);
            }
        }

        // Can only move Left or Up
        else if (EnemyDetection.pathOpenDown == false && EnemyDetection.pathOpenLeft == true && EnemyDetection.pathOpenRight == false &&
                                                                                                      EnemyDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5); // 1 = down 2 = left 3 = right 4 = up
            while (moveDir < 1 || moveDir > 4 || moveDir == 1 || moveDir == 3)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        // Cannot move left
        else if (EnemyDetection.pathOpenDown == true && EnemyDetection.pathOpenLeft == false && EnemyDetection.pathOpenRight == true &&
                                                                                                     EnemyDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5);
            while (moveDir < 1 || moveDir > 4 || moveDir == 2)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        // Can move any direction except Up
        else if (EnemyDetection.pathOpenDown == true && EnemyDetection.pathOpenLeft == true && EnemyDetection.pathOpenRight == true &&
                                                                                                   EnemyDetection.pathOpenUp == false)
        {
            moveDir = Random.Range(1, 5);
            while (moveDir < 1 || moveDir > 3)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        // Cannot move Right or Up
        else if (EnemyDetection.pathOpenDown == true && EnemyDetection.pathOpenLeft == true && EnemyDetection.pathOpenRight == false &&
                                                                                                   EnemyDetection.pathOpenUp == false)
        {
            moveDir = Random.Range(1, 5); // 1 = down 2 = left 3 = right 4 = up
            while (moveDir < 1 || moveDir > 2)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        // Cannot move Left or Up
        else if (EnemyDetection.pathOpenDown == true && EnemyDetection.pathOpenLeft == false && EnemyDetection.pathOpenRight == true &&
                                                                                                   EnemyDetection.pathOpenUp == false)
        {
            moveDir = Random.Range(1, 5);
            while (moveDir < 1 || moveDir > 3)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        // Can only move Down
        else if (EnemyDetection.pathOpenDown == true && EnemyDetection.pathOpenLeft == false && EnemyDetection.pathOpenRight == false &&
                                                                                                   EnemyDetection.pathOpenUp == false)
        {
            moveDir = 1;
        }

        // Can only move Left
        else if (EnemyDetection.pathOpenDown == false && EnemyDetection.pathOpenLeft == true && EnemyDetection.pathOpenRight == false &&
                                                                                                   EnemyDetection.pathOpenUp == false)
        {
            moveDir = 2;
        }

        // Can only move Right
        else if (EnemyDetection.pathOpenDown == false && EnemyDetection.pathOpenLeft == false && EnemyDetection.pathOpenRight == true &&
                                                                                                   EnemyDetection.pathOpenUp == false)
        {
            moveDir = 3;
        }

        else { moveDir = 4; } // Default: up
    }

    void MoveLeft()
    {
        curDir = 2;
        enemyRb.transform.GetChild(0).GetChild(0).eulerAngles = new Vector3(0, 0, -90);
        moveDirection = Vector2.left;
    }

    void MoveRight()
    {
        curDir = 3;
        enemyRb.transform.GetChild(0).GetChild(0).eulerAngles = new Vector3(0, 0, 90);
        moveDirection = Vector2.right;
    }

    void MoveUp()
    {
        curDir = 4;
        enemyRb.transform.GetChild(0).GetChild(0).eulerAngles = new Vector3(0, 0, 180);
        moveDirection = Vector2.up;
    }

    void MoveDown()
    {
        curDir = 1;
        enemyRb.transform.GetChild(0).GetChild(0).eulerAngles = new Vector3(0, 0, 0);
        moveDirection = Vector2.down;
    }
    
    void MovementHandler()
    {
        if (moveDir == 1)
        {
            if (EnemyDetection.pathOpenDown == true)
            {
                MoveDown();
            }
            else { ChangeDirection(); }
        }
        else if (moveDir == 2)
        {
            if (EnemyDetection.pathOpenLeft == true)
            {
                MoveLeft();
            }
            else { ChangeDirection(); }
        }
        else if (moveDir == 3)
        {
            if (EnemyDetection.pathOpenRight == true)
            {
                MoveRight();
            }
            else { ChangeDirection(); }
        }
        else if (moveDir == 4)
        {
            if (EnemyDetection.pathOpenUp == true)
            {
                MoveUp();
            }
            else { ChangeDirection(); }
        }

        if (canChange == true)
        {
            StartCoroutine(RandomMovement());
        }
    }
    #endregion

    #region �÷��̾� ����
    void FollowPlayer(float distanceToPlayer)
    {
        // �÷��̾� ���� �ֽ�
        Vector3 facePlayer = player.position - enemyRb.transform.position;
        enemyRb.transform.up = -facePlayer;

        // �÷��̾� ����
        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }

        if (distanceToPlayer > stopChasingDistance)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            moveDirection = directionToPlayer;
        }
        else
        {
            // ���� ����
            moveDirection = Vector2.zero;
        }
    }

    void Fire()
    {
        if (player == null)
            return;

        Vector2 direction = (player.position - firePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletPrefab.GetComponent<EnemyBullet01>().speed;
    }
    #endregion

    IEnumerator RandomMovement()
    {
        canChange = false;
        var timer = Random.Range(0.5f, 2.5f);
        yield return new WaitForSeconds(timer);
        ChangeDirection();
        yield return new WaitForSeconds(0.5f);
        canChange = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            ChangeDirection();
        }
    }
}