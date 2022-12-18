using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.Scripts.Player;

public class EditMode
{
    public Player Player = new();

    [Test]
    public void PlayerBeginningStats()
    {
        Assert.AreEqual(false, Player.GodMode);
        Assert.AreEqual(true, Player.CanMove);
        Assert.AreEqual(30f, Player.BulletForce);
        Assert.AreEqual(0.5f, Player.Firerate);
        Assert.AreEqual(10f, Player.MoveSpeed);
        Assert.AreEqual(0, Player.Money);
        Assert.AreEqual(100f, Player.Health);
    }

    [Test]
    public void HealthbarEqualToHealth()
    {
        Assert.AreEqual(Player.Health, Player.HealthBar.healthSlider.value / 100);
    }

    [Test]
    public void ShootingPointPlacement()
    {
        Assert.IsTrue(Player.ShootingPoint.IsChildOf(Player.transform));
    }
}