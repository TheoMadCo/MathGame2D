using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI scoreText;
    public Button[] answerButtons;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public Button retryButton;
    public Button backButton;
    public TextMeshProUGUI DifficultyInfo;

    private int correctAnswer;
    private float score;
    private int roundCount;
    private const int maxRounds = 10;

    void Start()
    {
        score = 0;
        roundCount = 0;
        resultPanel.SetActive(false);
        GenerateQuestion();
        DisplayDifficulty();
    }

    void GenerateQuestion()
    {
        if (roundCount >= maxRounds)
        {
            ShowResult();
            return;
        }

        int a, b;
        string operation = OperationSelectionManager.selectedOperation;
        string difficulty = OperationSelectionManager.difficultyLevel;
        List<int> answers = new List<int>();

        int range = difficulty == "Easy" ? 10 : difficulty == "Medium" ? 20 : 50;

        switch (operation)
        {
            case "Addition":
                a = Random.Range(1, range + 1);
                b = Random.Range(1, range + 1);
                correctAnswer = a + b;
                questionText.text = $"{a} + {b} = ?";
                break;
            case "Subtraction":
                a = Random.Range(1, range + 1);
                b = Random.Range(1, a + 1); // Ensure b <= a
                correctAnswer = a - b;
                questionText.text = $"{a} - {b} = ?";
                break;
            case "Multiplication":
                a = Random.Range(1, range + 1);
                b = Random.Range(1, range + 1);
                correctAnswer = a * b;
                questionText.text = $"{a} * {b} = ?";
                break;
            case "Division":
                b = Random.Range(1, range + 1);
                correctAnswer = Random.Range(1, range + 1);
                a = b * correctAnswer; // Ensure a is divisible by b
                questionText.text = $"{a} / {b} = ?";
                break;
        }

        answers.Add(correctAnswer);

        while (answers.Count < 3)
        {
            int wrongAnswer = Random.Range(1, range * 2);
            if (!answers.Contains(wrongAnswer))
            {
                answers.Add(wrongAnswer);
            }
        }

        answers = ShuffleList(answers);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int answer = answers[i];
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answer.ToString();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(answer));
        }

        roundCount++;
    }

    void OnAnswerSelected(int selectedAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
            score += 1;
        }
        else
        {
            score -= 0.25f;
        }

        scoreText.text = "Score: " + score;
        ResetButtons();
        GenerateQuestion();
    }

    string GetScoreText()
    {
        if (score < 3)
        {
            return "Next time will go better!";
        }
        else if (score < 6)
        {
            return "Nice work";
        }
        else if (score < 8)
        {
            return "Good job!";
        }
        else
        {
            return "Excellent";
        }
    }

    void ResetButtons()
    {
        foreach (Button button in answerButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    List<int> ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    void ShowResult()
    {
        resultPanel.SetActive(true);
        string motivationalText = GetScoreText();
        resultText.text = $"{score} out of 10\n" + motivationalText;
        retryButton.onClick.AddListener(RetryGame);
        backButton.onClick.AddListener(BackToSelection);
    }

    void RetryGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void BackToSelection()
    {
        SceneManager.LoadScene("SelectionScene");
    }

    string difficultyLevel()
    {
        return OperationSelectionManager.difficultyLevel;
    }

    void DisplayDifficulty()
    {
        DifficultyInfo.text = DifficultyInfo.text + difficultyLevel();
    }
}
