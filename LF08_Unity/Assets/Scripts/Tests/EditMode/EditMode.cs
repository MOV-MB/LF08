using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.Scripts.Player;

public class EditMode
{
    public Player player = new Player();

    [Test]
    public void StartingHealthTest()
    {
        Assert.AreEqual(100f, player.Health);
    }
}
