using UnityEngine;
using UnityEngine.SceneManagement;

public class OperationSelectionManager : MonoBehaviour
{
    public static string selectedOperation;

    public void SelectAddition()
    {
        Debug.Log("Button clicked!");
        selectedOperation = "Addition";
        SceneManager.LoadScene("GameScene");

    }

    public void SelectSubtraction()
    {
        Debug.Log("Button clicked!");
        selectedOperation = "Subtraction";
        SceneManager.LoadScene("GameScene");
    }

    public void SelectMultiplication()
    {
        Debug.Log("Button clicked!");
        selectedOperation = "Multiplication";
        SceneManager.LoadScene("GameScene");
    }

    public void SelectDivision()
    {
        Debug.Log("Button clicked!");
        selectedOperation = "Division";
        SceneManager.LoadScene("GameScene");
    }
}
