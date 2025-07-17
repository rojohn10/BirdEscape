using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

//public class BirdTests
//{
//    // A Test behaves as an ordinary method
//    [Test]
//    public void BirdTestsSimplePasses()
//    {
//        // Use the Assert class to test conditions
//    }

//    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
//    // `yield return null;` to skip a frame.
//    [UnityTest]
//    public IEnumerator BirdTestsWithEnumeratorPasses()
//    {
//        // Use the Assert class to test conditions.
//        // Use yield to skip a frame.
//        yield return null;
//    }
//}

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BirdTests
{
    private GameObject birdObject;
    private Bird bird;
    private Rigidbody2D rigidBody;
    private GameObject logicObject;
    private LogicScript logicScript;

    [SetUp]
    public void SetUp()
    {
        // Create LogicScript mock
        logicObject = new GameObject();
        logicScript = logicObject.AddComponent<LogicScript>();

        // Create Bird
        birdObject = new GameObject();
        rigidBody = birdObject.AddComponent<Rigidbody2D>();
        bird = birdObject.AddComponent<Bird>();

        // Assign required references via reflection (private fields)
        typeof(Bird).GetField("rigidBody", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(bird, rigidBody);
        typeof(Bird).GetField("logicScript", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(bird, logicScript);

        // Set bird alive
        typeof(Bird).GetField("birdIsAlive", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(bird, true);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(birdObject);
        Object.DestroyImmediate(logicObject);
    }

    [UnityTest]
    public IEnumerator GoUp_SetsVelocityAndCurrentSpeed()
    {
        bird.GoUp();
        yield return null;

        // Bird should be going up
        Assert.Greater(rigidBody.velocity.y, 0);

        // Current speed should be set to startingSpeed
        float startingSpeed = (float)typeof(Bird).GetField("startingSpeed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(bird);
        float currentSpeed = (float)typeof(Bird).GetField("currentSpeed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(bird);
        Assert.AreEqual(startingSpeed, currentSpeed);
    }

    [UnityTest]
    public IEnumerator OnCollisionEnter2D_SetsBirdNotAliveAndCallsGameOver()
    {
        // Simulate collision
        var collisionObj = new GameObject();
        var collider = collisionObj.AddComponent<BoxCollider2D>();
        var collision = new Collision2D();

        // Use reflection to call OnCollisionEnter2D
        var method = typeof(Bird).GetMethod("OnCollisionEnter2D", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        method.Invoke(bird, new object[] { collision });

        yield return null;

        bool birdIsAlive = (bool)typeof(Bird).GetField("birdIsAlive", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(bird);
        Assert.IsFalse(birdIsAlive);

        Object.DestroyImmediate(collisionObj);
    }
}
