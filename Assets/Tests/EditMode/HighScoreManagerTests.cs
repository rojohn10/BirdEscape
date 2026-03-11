using NUnit.Framework;
using UnityEngine;

public class HighScoreManagerTests
{
    private GameObject managerObject;
    private HighScoreManager highScoreManager;

    [SetUp]
    public void SetUp()
    {
        // Clear any existing singleton
        if (HighScoreManager.Instance != null)
            Object.DestroyImmediate(HighScoreManager.Instance.gameObject);

        // Clear PlayerPrefs for clean test state
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();

        managerObject = new GameObject();
        highScoreManager = managerObject.AddComponent<HighScoreManager>();

        // Invoke Awake manually via reflection (EditMode does not auto-call Awake)
        var awakeMethod = typeof(HighScoreManager)
            .GetMethod("Awake", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.IsNotNull(awakeMethod, "Awake method not found on HighScoreManager");
        awakeMethod.Invoke(highScoreManager, null);
    }

    [TearDown]
    public void TearDown()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
        Object.DestroyImmediate(managerObject);
    }

    [Test]
    public void HighScore_DefaultsToZero_WhenNoSavedScore()
    {
        Assert.AreEqual(0, highScoreManager.HighScore);
    }

    [Test]
    public void TrySetHighScore_ReturnsTrue_WhenScoreBeatsHighScore()
    {
        bool result = highScoreManager.TrySetHighScore(10);

        Assert.IsTrue(result);
        Assert.AreEqual(10, highScoreManager.HighScore);
    }

    [Test]
    public void TrySetHighScore_ReturnsFalse_WhenScoreDoesNotBeatHighScore()
    {
        highScoreManager.TrySetHighScore(10);
        bool result = highScoreManager.TrySetHighScore(5);

        Assert.IsFalse(result);
        Assert.AreEqual(10, highScoreManager.HighScore);
    }

    [Test]
    public void TrySetHighScore_ReturnsFalse_WhenScoreEqualsHighScore()
    {
        highScoreManager.TrySetHighScore(10);
        bool result = highScoreManager.TrySetHighScore(10);

        Assert.IsFalse(result);
        Assert.AreEqual(10, highScoreManager.HighScore);
    }

    [Test]
    public void TrySetHighScore_PersistsToPlayerPrefs()
    {
        highScoreManager.TrySetHighScore(42);

        int saved = PlayerPrefs.GetInt("HighScore", 0);
        Assert.AreEqual(42, saved);
    }

    [Test]
    public void LoadHighScore_ReadsFromPlayerPrefs()
    {
        PlayerPrefs.SetInt("HighScore", 99);
        PlayerPrefs.Save();

        highScoreManager.LoadHighScore();

        Assert.AreEqual(99, highScoreManager.HighScore);
    }

    [Test]
    public void ResetHighScore_ClearsScoreAndPlayerPrefs()
    {
        highScoreManager.TrySetHighScore(50);
        highScoreManager.ResetHighScore();

        Assert.AreEqual(0, highScoreManager.HighScore);
        Assert.AreEqual(0, PlayerPrefs.GetInt("HighScore", 0));
    }

    [Test]
    public void TrySetHighScore_UpdatesProgressively()
    {
        highScoreManager.TrySetHighScore(5);
        Assert.AreEqual(5, highScoreManager.HighScore);

        highScoreManager.TrySetHighScore(15);
        Assert.AreEqual(15, highScoreManager.HighScore);

        highScoreManager.TrySetHighScore(10);
        Assert.AreEqual(15, highScoreManager.HighScore);

        highScoreManager.TrySetHighScore(20);
        Assert.AreEqual(20, highScoreManager.HighScore);
    }

    [Test]
    public void Singleton_IsSetOnAwake()
    {
        Assert.AreEqual(highScoreManager, HighScoreManager.Instance);
    }

    [Test]
    public void TrySetHighScore_ReturnsFalse_WhenScoreIsNegative()
    {
        bool result = highScoreManager.TrySetHighScore(-5);

        Assert.IsFalse(result);
        Assert.AreEqual(0, highScoreManager.HighScore);
    }

    [Test]
    public void TrySetHighScore_ReturnsFalse_WhenScoreIsZero()
    {
        bool result = highScoreManager.TrySetHighScore(0);

        Assert.IsFalse(result);
        Assert.AreEqual(0, highScoreManager.HighScore);
    }

    [Test]
    public void Singleton_IsNulledOnDestroy()
    {
        Assert.IsNotNull(HighScoreManager.Instance);

        Object.DestroyImmediate(managerObject);

        Assert.IsNull(HighScoreManager.Instance);

        // Recreate for TearDown safety
        managerObject = new GameObject();
        highScoreManager = managerObject.AddComponent<HighScoreManager>();
        var awakeMethod = typeof(HighScoreManager)
            .GetMethod("Awake", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        awakeMethod.Invoke(highScoreManager, null);
    }
}
