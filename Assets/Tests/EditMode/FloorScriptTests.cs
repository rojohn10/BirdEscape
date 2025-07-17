using NUnit.Framework;
using UnityEngine;

public class FloorScriptTests
{
    private GameObject floorObject;
    private FloorScript floorScript;

    [SetUp]
    public void SetUp()
    {
        floorObject = new GameObject();
        floorScript = floorObject.AddComponent<FloorScript>();

        // Set private fields via reflection for predictable tests
        typeof(FloorScript).GetField("moveSpeed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(floorScript, 0.08f);
        typeof(FloorScript).GetField("resetPositionX", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(floorScript, -5.32f);
        typeof(FloorScript).GetField("startPositionX", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(floorScript, 0f);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(floorObject);
    }

    [Test]
    public void Update_MovesFloorLeft_WhenTimeScaleIsOne()
    {
        // Arrange
        Time.timeScale = 1f;
        floorObject.transform.position = new Vector3(1f, 0f, 0f);

        // Act
        floorScript.Update();

        // Assert
        Assert.Less(floorObject.transform.position.x, 1f);
    }

    [Test]
    public void Update_DoesNotMoveFloor_WhenTimeScaleIsNotOne()
    {
        // Arrange
        Time.timeScale = 0f;
        floorObject.transform.position = new Vector3(1f, 0f, 0f);

        // Act
        floorScript.Update();

        // Assert
        Assert.AreEqual(1f, floorObject.transform.position.x);
    }

    [Test]
    public void Update_ResetsPosition_WhenPastThreshold()
    {
        // Arrange
        Time.timeScale = 1f;
        floorObject.transform.position = new Vector3(-6f, 0f, 0f);

        // Act
        floorScript.Update();

        // Assert
        Assert.AreEqual(0f, floorObject.transform.position.x);
    }
}