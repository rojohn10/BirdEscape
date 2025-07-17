using NUnit.Framework;
using UnityEngine;

public class PillarMiiddleScriptTests
{
    private GameObject pillarMiddleObject;
    private PillarMiiddleScript pillarMiiddleScript;
    private GameObject logicObject;
    private LogicScript logicScript;
    private GameObject birdObject;
    private BoxCollider2D birdCollider;

    [SetUp]
    public void SetUp()
    {
        // Create LogicScript mock
        logicObject = new GameObject();
        logicScript = logicObject.AddComponent<LogicScript>();

        // Create PillarMiiddleScript
        pillarMiddleObject = new GameObject();
        pillarMiiddleScript = pillarMiddleObject.AddComponent<PillarMiiddleScript>();

        // Assign LogicScript via reflection (private field)
        typeof(PillarMiiddleScript).GetField("logicScript", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(pillarMiiddleScript, logicScript);

        // Create bird object with layer 3
        birdObject = new GameObject();
        birdObject.layer = 3;
        birdCollider = birdObject.AddComponent<BoxCollider2D>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(pillarMiddleObject);
        Object.DestroyImmediate(logicObject);
        Object.DestroyImmediate(birdObject);
    }

    [Test]
    public void OnTriggerEnter2D_IncrementsScore_WhenLayerIs3()
    {
        // Get initial score via reflection
        int initialScore = (int)typeof(LogicScript).GetField("playerScore", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(logicScript);

        // Simulate trigger
        var method = typeof(PillarMiiddleScript).GetMethod("OnTriggerEnter2D", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        method.Invoke(pillarMiiddleScript, new object[] { birdCollider });

        // Get new score
        int newScore = (int)typeof(LogicScript).GetField("playerScore", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(logicScript);

        Assert.AreEqual(initialScore + 1, newScore);
    }

    [Test]
    public void OnTriggerEnter2D_DoesNotIncrementScore_WhenLayerIsNot3()
    {
        birdObject.layer = 0; // Not layer 3

        int initialScore = (int)typeof(LogicScript).GetField("playerScore", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(logicScript);

        var method = typeof(PillarMiiddleScript).GetMethod("OnTriggerEnter2D", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        method.Invoke(pillarMiiddleScript, new object[] { birdCollider });

        int newScore = (int)typeof(LogicScript).GetField("playerScore", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(logicScript);

        Assert.AreEqual(initialScore, newScore);
    }
}