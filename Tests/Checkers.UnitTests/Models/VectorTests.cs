using Checkers.Rules.Enums;
using Checkers.Rules.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkers.Rules.Models.Tests;

[TestClass()]
public class VectorTests
{
    [TestMethod()]
    public void NotEqualsToNullTest()
    {
        var vector = new Vector(Direction.LeftTop, 2);

        var isEqualsToNull = vector.Equals(null);

        Assert.AreEqual(false, isEqualsToNull);

    }

    [TestMethod()]
    public void EqualsTest()
    {
        var vector1 = new Vector(Direction.LeftTop, 2);


        var vector2 = new Vector(Direction.LeftTop, 2);

        var actual = vector1.Equals(vector2);

        Assert.IsTrue(actual);
    }

    [TestMethod()]
    public void NotEqualsTest()
    {
        var vector1 = new Vector(Direction.LeftTop, 2);

        var vector2 = new Vector(Direction.RightBottom, 2);

        var actual = vector1.Equals(vector2);

        Assert.IsFalse(actual);
    }
}