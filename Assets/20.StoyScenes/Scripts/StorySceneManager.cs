using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StorySceneManager : MonoBehaviour
{
    public Text DialogueText;
    public string[] npcDialogueLines = {
        "(치직) 여기는 본부, 들리나.",
        "알다시피 우린 지구에 침략한 저 외계인들을 쫓아내기 위한 비밀작전 수행중이다.",
        "인간이 승리하는 법은 단 한가지, 저 ufo 안에 숨어있는 옥토르(보스 몹)를 처치해야한다.",
        "지금부터 작전 설명을 시작하지.",
        "우선 적들의 눈에 띄지 않도록 ufo로 잠입해라. 미리 설치해 둔 뒷문으로 들어가면 옥토리안(기본 몹)들이 옥토르의 방을 지키며 순찰을 돌고 있을거다.",
        "그 녀석들을 처치하며 우리의 목표, 옥토르가 숨어있는 공간의 열쇠를 찾아라.",
        "열쇠를 찾아 옥토르가 숨어있는 방문을 열어 옥토르를 처치하라. 명령이다.",
        "쉽지는 않을거다.. 무사히 복귀하도록"
    };
    public string[] playerDialogueLines = {
        "예 들립니다.",
        "예 알겠습니다.",
        "예 알겠습니다!"
    };

    public CanvasGroup canvasGroup;

    public Image playerImage;
    public Image npcImage;

    private int npcLineIndex = 0;
    private int playerLineIndex = 0;
    private bool isNpcTurn = false;
    private bool isFadingOut = false;

    void Start()
    {
        DialogueText.text = npcDialogueLines[0];
        canvasGroup.alpha = 1;
        playerImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isFadingOut)
            {
                ShowNextLine();
            }
        }
    }

    void ShowNextLine()
    {
        if (isNpcTurn)
        {
            if (npcLineIndex < npcDialogueLines.Length - 1)
            {
                playerImage.gameObject.SetActive(false);
                npcImage.gameObject.SetActive(true);

                npcLineIndex++;
                DialogueText.text = npcDialogueLines[npcLineIndex];
            }
            else
            {
                isFadingOut = true;
                StartCoroutine(FadeOutAndLoadScene());
                return;
            }
            if (npcLineIndex == 0 || npcLineIndex == 3 || npcLineIndex == 6)
            {
                isNpcTurn = false;
            }
        }
        else
        {
            if (playerLineIndex < playerDialogueLines.Length)
            {
                npcImage.gameObject.SetActive(false);
                playerImage.gameObject.SetActive(true);

                DialogueText.text = playerDialogueLines[playerLineIndex];
                playerLineIndex++;
            }
            else
            {
                DialogueText.text = "주인공의 대화가 끝났습니다.";
            }

            isNpcTurn = true;
        }
    }

    IEnumerator FadeOutAndLoadScene()
    {
        if (canvasGroup != null)
        {
            
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 0;
        }

        
        SceneManager.LoadScene("GeneralShooting");
        
        
    }
}
