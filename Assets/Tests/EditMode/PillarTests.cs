using NUnit.Framework;
using UnityEngine;

public class PillarTests
{
    private GameObject pillarObject;
    private Pillar pillar;
    private GameObject topPillarObject;
    private GameObject bottomPillarObject;

    [SetUp]
    public void SetUp()
    {
        pillarObject = new GameObject();
        pillar = pillarObject.AddComponent<Pillar>();

        topPillarObject = new GameObject();
        bottomPillarObject = new GameObject();

        // Assign pillar references via reflection (private fields)
        typeof(Pillar).GetField("topPillar", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(pillar, topPillarObject);
        typeof(Pillar).GetField("bottomPillar", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(pillar, bottomPillarObject);

        // Set speed for predictable movement
        typeof(Pillar).GetField("speed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(pillar, 3f);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(topPillarObject);
        Object.DestroyImmediate(bottomPillarObject);
        // Destroy pillarObject if it still exists
        if (pillarObject != null)
            Object.DestroyImmediate(pillarObject);
    }

    [Test]
    public void SetPillars_SetsTopAndBottomLocalPositions()
    {
        pillar.SetPillars();

        // The bottom pillar's y should be between minY and maxY
        float minY = (float)typeof(Pillar).GetField("minY", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(pillar);
        float maxY = (float)typeof(Pillar).GetField("maxY", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(pillar);

        float botY = bottomPillarObject.transform.localPosition.y;
        Assert.GreaterOrEqual(botY, minY);
        Assert.LessOrEqual(botY, maxY);

        // The top pillar's y should be bottom + Gap
        float gap = (float)typeof(Pillar).GetField("Gap", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
            .GetValue(null);
        float topY = topPillarObject.transform.localPosition.y;
        Assert.AreEqual(botY + gap, topY, 0.01f);
    }

    [Test]
    public void Update_MovesPillarLeft_WhenTimeScaleIsOne()
    {
        Time.timeScale = 1f;
        pillarObject.transform.position = new Vector3(1f, 0f, 0f);

        pillar.Update();

        Assert.Less(pillarObject.transform.position.x, 1f);
    }

    [Test]
    public void Update_DoesNotMovePillar_WhenTimeScaleIsZero()
    {
        Time.timeScale = 0f;
        pillarObject.transform.position = new Vector3(1f, 0f, 0f);

        pillar.Update();

        Assert.AreEqual(1f, pillarObject.transform.position.x);
    }

    //[Test]
    //public void Update_DestroysPillar_WhenOffScreen()
    //{
    //    Time.timeScale = 1f;
    //    pillarObject.transform.position = new Vector3(-4f, 0f, 0f);

    //    pillar.Update();

    //    // Simulate end of frame destruction
    //    Object.DestroyImmediate(pillarObject);

    //    Assert.IsNull(Object.FindObjectOfType<Pillar>());
    //}
}