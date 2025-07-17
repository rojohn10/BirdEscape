using NUnit.Framework;
using UnityEngine;

public class GameControllerTests
{
    private GameObject controllerObject;
    private GameController gameController;
    private GameObject pillarObject;
    private Pillar pillarPrefab;

    [SetUp]
    public void SetUp()
    {
        // Create GameController
        controllerObject = new GameObject();
        gameController = controllerObject.AddComponent<GameController>();

        // Create Pillar prefab
        pillarObject = new GameObject();
        pillarPrefab = pillarObject.AddComponent<Pillar>();

        // Assign pillar prefab via reflection (private field)
        typeof(GameController).GetField("pillarPrefab", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(gameController, pillarPrefab);

        // Set frequency for fast test
        typeof(GameController).GetField("frequency", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(gameController, 1);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(controllerObject);
        Object.DestroyImmediate(pillarObject);
        foreach (var pillar in Object.FindObjectsOfType<Pillar>())
        {
            Object.DestroyImmediate(pillar.gameObject);
        }
    }

    [Test]
    public void SingletonInstance_IsSetOnAwake()
    {
        // Use reflection to invoke the private Awake method  
        typeof(GameController).GetMethod("Awake", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .Invoke(gameController, null);
        Assert.AreEqual(gameController, GameController.Instance);
    }

    [Test]
    public void SpawnPillar_CreatesPillarInstance()
    {
        int initialCount = Object.FindObjectsOfType<Pillar>().Length;
        gameController.SpawnPillar();
        int newCount = Object.FindObjectsOfType<Pillar>().Length;
        Assert.AreEqual(initialCount + 1, newCount);
    }

    //[Test]
    //public void Update_SpawnsPillarAfterFrequencyFrames()
    //{
    //    // Use reflection to invoke the private Awake method  
    //    typeof(GameController).GetMethod("Awake", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(gameController, null);
    //    int initialCount = Object.FindObjectsOfType<Pillar>().Length;

    //    // Use reflection to invoke the private Update method  
    //    var updateMethod = typeof(GameController).GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
    //    for (int i = 0; i < 2; i++)
    //    {
    //        updateMethod.Invoke(gameController, null);
    //    }

    //    int newCount = Object.FindObjectsOfType<Pillar>().Length;
    //    Assert.Greater(newCount, initialCount);
    //}
}