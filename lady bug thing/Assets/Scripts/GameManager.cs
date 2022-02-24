using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject ladyBugPre, blackOutScreen;
    public GameObject[] spawnPoints;
    public List<GameObject> ladyBugs;
    public Text noOfLadyBugs;

    private int ladyBugCount;
    private bool bugEscaped = false;

    void Start()
    {
        ladyBugCount = Random.Range(2, 9);
        noOfLadyBugs.text = $"Ladybugs:{ladyBugCount}";
        for (int i = 0; i <= ladyBugCount - 1; i++)
        {
            ladyBugs.Add(Instantiate(ladyBugPre, spawnPoints[i].transform.position, transform.rotation));
            ladyBugs[i].GetComponent<Ladybug>().ladyBugNum = i;
        }
    }

    private void Update()
    {
        if(ladyBugs.Count != 1 && bugEscaped == false)
        {
            noOfLadyBugs.text = $"Ladybugs:{ladyBugs.Count}";
        }
        else if (ladyBugs.Count == 1)
        {
            noOfLadyBugs.text = $"Ladybugs:{ladyBugs.Count}";
            Invoke("RestartLevel", 2);
        }
        else
        {
            blackOutScreen.SetActive(true);
            noOfLadyBugs.text = "";
            for (int i = 0; i <= ladyBugs.Count - 1; i++)
            {
                if (ladyBugs[i].GetComponent<Ladybug>().grounded == true && ladyBugs[i] != null)
                {
                    ladyBugs[i].SetActive(false);
                }
            }
            Invoke("RestartLevel", 2);
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void LadyBugEscaped()
    {
        bugEscaped = true;
    }

    public void removeThisLadyBug(int removeThis)
    {
        for (int i = 0; i <= ladyBugs.Count - 1; i++)
        {
            if(removeThis == ladyBugs[i].GetComponent<Ladybug>().ladyBugNum)
            {
                ladyBugs.RemoveAt(i);
            }
        }
    }
}