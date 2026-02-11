using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplePicker : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject basketPrefab;
    public int numBaskets = 4;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;

    [Header("UI References")]
    public GameObject startScreen;
    public GameObject gameUI;
    public Text roundText;
    public GameObject restartButton;

    [Header("Dynamic")]
    public List<GameObject> basketList;
    public int currentRound = 1;
    public int maxRounds = 4;
    public bool gameStarted = false;

    void Start()
    {
        if (startScreen != null) startScreen.SetActive(true);
        if (gameUI != null) gameUI.SetActive(false);
        if (restartButton != null) restartButton.SetActive(false);
        
        Time.timeScale = 0; // Pause the game at the start
    }

    public void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1; // Unpause the game
        
        if (startScreen != null) startScreen.SetActive(false);
        if (gameUI != null) gameUI.SetActive(true);
        
        SetupBaskets();
        UpdateRoundDisplay();
    }

    void SetupBaskets()
    {
        basketList = new List<GameObject>();

        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tempBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tempBasketGO.transform.position = pos;
            basketList.Add(tempBasketGO);
        }
    }

    public void AppleMissed()
    {
        if (!gameStarted) return;

        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tempGO in appleArray)
        {
            Destroy(tempGO);
        }

        GameObject[] branchArray = GameObject.FindGameObjectsWithTag("Branch");
        foreach (GameObject tempGO in branchArray)
        {
            Destroy(tempGO);
        }

        int basketIndex = basketList.Count - 1;
        GameObject basketGO = basketList[basketIndex];
        basketList.RemoveAt(basketIndex);
        Destroy(basketGO);

        if (basketList.Count == 0)
        {
            NextRound();
        }
    }

    void NextRound()
    {
        currentRound++;
        
        if (currentRound > maxRounds)
        {
            GameOver();
        }
        else
        {
            SetupBaskets();
            UpdateRoundDisplay();
        }
    }

    public void BranchCaught()
    {
        GameOver();
    }

    void GameOver()
    {
        gameStarted = false;
        Time.timeScale = 0; // Pause game
        
        if (roundText != null)
        {
            roundText.text = "Game Over";
        }
        
        if (restartButton != null)
        {
            restartButton.SetActive(true);
        }

        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tempGO in appleArray)
        {
            Destroy(tempGO);
        }

        GameObject[] branchArray = GameObject.FindGameObjectsWithTag("Branch");
        foreach (GameObject tempGO in branchArray)
        {
            Destroy(tempGO);
        }
        
        foreach (GameObject basket in basketList)
        {
            Destroy(basket);
        }
        basketList.Clear();
    }

    void UpdateRoundDisplay()
    {
        if (roundText != null)
        {
            roundText.text = "Round " + currentRound;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Unpause
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}