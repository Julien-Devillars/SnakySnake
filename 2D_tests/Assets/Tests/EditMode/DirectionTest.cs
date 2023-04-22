using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DirectionTest
{
    [Test]
    public void checkAllDirection()
    {
        Assert.AreEqual(Direction.directions.Count, 5);
        Assert.AreEqual(Direction.directions[Direction.direction.Left], Vector3.left);
        Assert.AreEqual(Direction.directions[Direction.direction.Right], Vector3.right);
        Assert.AreEqual(Direction.directions[Direction.direction.Down], Vector3.down);
        Assert.AreEqual(Direction.directions[Direction.direction.Up], Vector3.up);
        Assert.AreEqual(Direction.directions[Direction.direction.None], Vector3.zero);
    }
    [Test]
    public void checkAllDirections()
    {
        Assert.AreEqual(Direction.direction.Left, Direction.Left);
        Assert.AreEqual(Direction.direction.Right, Direction.Right);
        Assert.AreEqual(Direction.direction.Down, Direction.Down);
        Assert.AreEqual(Direction.direction.Up, Direction.Up);
        Assert.AreEqual(Direction.direction.None, Direction.None);
    }

}
