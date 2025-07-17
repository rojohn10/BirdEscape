using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class LogicScriptTests
{
    private GameObject logicObject;
    private LogicScript logicScript;
    private Text mockScoreText;
    private GameObject mockGameOverScreen;
    private GameObject mockHomeScreen;

    [SetUp]
    public void SetUp()
    {
        logicObject = new GameObject();
        logicScript = logicObject.AddComponent<LogicScript>();

        // Mock UI elements
        mockScoreText = new GameObject().AddComponent<Text>();
        mockGameOverScreen = new GameObject();
        mockHomeScreen = new GameObject();

        // Assign mocks via reflection (private fields)
        typeof(LogicScript).GetField("scoreText", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(logicScript, mockScoreText);
        typeof(LogicScript).GetField("gameOverScreen", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(logicScript, mockGameOverScreen);
        typeof(LogicScript).GetField("homeScreen", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(logicScript, mockHomeScreen);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(logicObject);
        Object.DestroyImmediate(mockScoreText.gameObject);
        Object.DestroyImmediate(mockGameOverScreen);
        Object.DestroyImmediate(mockHomeScreen);
    }

    [Test]
    public void AddScore_UpdatesScoreAndUI()
    {
        logicScript.AddScore(5);
        Assert.AreEqual("5", mockScoreText.text);

        logicScript.AddScore(10);
        Assert.AreEqual("15", mockScoreText.text);
    }

    [Test]
    public void ShowHomeScreen_ActivatesHomeScreen()
    {
        mockHomeScreen.SetActive(false);
        logicScript.ShowHomeScreen();
        Assert.IsTrue(mockHomeScreen.activeSelf);
    }

    [Test]
    public void HideHomeScreen_DeactivatesHomeScreen()
    {
        mockHomeScreen.SetActive(true);
        logicScript.HideHomeScreen();
        Assert.IsFalse(mockHomeScreen.activeSelf);
    }

    [Test]
    public void ShowGameOverScreen_ActivatesGameOverScreen()
    {
        mockGameOverScreen.SetActive(false);
        logicScript.ShowGameOverScreen();
        Assert.IsTrue(mockGameOverScreen.activeSelf);
    }

    [Test]
    public void HideGameOverScreen_DeactivatesGameOverScreen()
    {
        mockGameOverScreen.SetActive(true);
        logicScript.HideGameOverScreen();
        Assert.IsFalse(mockGameOverScreen.activeSelf);
    }
}