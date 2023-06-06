using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DirectionTest
{
    [Test]
    public void checkDirectionDictionnary()
    {
        Assert.AreEqual(Direction.directions.Count, 5);
        Assert.AreEqual(Direction.directions[Direction.direction.Left], Vector3.left);
        Assert.AreEqual(Direction.directions[Direction.direction.Up], Vector3.up);
        Assert.AreEqual(Direction.directions[Direction.direction.Right], Vector3.right);
        Assert.AreEqual(Direction.directions[Direction.direction.Down], Vector3.down);
        Assert.AreEqual(Direction.directions[Direction.direction.None], Vector3.zero);
    }
    [Test]
    public void checkDirectionVariables()
    {
        Assert.AreEqual(Direction.direction.Left, Direction.Left);
        Assert.AreEqual(Direction.direction.Up, Direction.Up);
        Assert.AreEqual(Direction.direction.Right, Direction.Right);
        Assert.AreEqual(Direction.direction.Down, Direction.Down);
        Assert.AreEqual(Direction.direction.None, Direction.None);
    }
    [Test]
    public void checkGetDirectionsFrom2Points()
    {

        Direction.direction direction = new Direction.direction();

        direction = Direction.getDirectionFrom2Points(Vector3.zero, Vector3.left);
        Assert.AreEqual(direction, Direction.Left);
        direction = Direction.getDirectionFrom2Points(Vector3.right, Vector3.zero);
        Assert.AreEqual(direction, Direction.Left);

        direction = Direction.getDirectionFrom2Points(Vector3.zero, Vector3.up);
        Assert.AreEqual(direction, Direction.Up);
        direction = Direction.getDirectionFrom2Points(Vector3.down, Vector3.zero);
        Assert.AreEqual(direction, Direction.Up);

        direction = Direction.getDirectionFrom2Points(Vector3.zero, Vector3.right);
        Assert.AreEqual(direction, Direction.Right);
        direction = Direction.getDirectionFrom2Points(Vector3.left, Vector3.zero);
        Assert.AreEqual(direction, Direction.Right);

        direction = Direction.getDirectionFrom2Points(Vector3.zero, Vector3.down);
        Assert.AreEqual(direction, Direction.Down);
        direction = Direction.getDirectionFrom2Points(Vector3.up, Vector3.zero);
        Assert.AreEqual(direction, Direction.Down);

        direction = Direction.getDirectionFrom2Points(Vector3.zero, Vector3.zero);
        Assert.AreEqual(direction, Direction.None);
    }


    [Test]
    public void checkSorting()
    {

        List<Vector3> points = new List<Vector3>();
        points.Add(new Vector3(5, 0, 0));
        points.Add(new Vector3(6, 0, 0));
        points.Add(new Vector3(1, 0, 0));
        points.Add(new Vector3(3, 0, 0));
        points.Add(new Vector3(-1, 0, 0));

        foreach (Vector3 point in points)
        {
            Debug.Log(point);
        }
        Debug.Log("SORT");
        points.Sort((p1, p2) => p1.x.CompareTo(p2.x));
        foreach(Vector3 point in points)
        {
            Debug.Log(point);
        }
    }

}
