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
    public void PlayerBeginningStats()
    {
        Assert.AreEqual(false, player.GodMode);
        Assert.AreEqual(true, player.CanMove);
        Assert.AreEqual(30f, player.BulletForce);
        Assert.AreEqual(0.5f, player.Firerate);
        Assert.AreEqual(10f, player.MoveSpeed);
        Assert.AreEqual(0, player.Money);
        Assert.AreEqual(100f, player.Health);
    }

    [Test]
    public void HealthbarEqualToHealth()
    {
        Assert.AreEqual(player.Health, player.HealthBar.healthSlider.value / 100);
    }

    [Test]
    public void ShootingPointPlacement()
    {
        Assert.IsTrue(player.ShootingPoint.IsChildOf(player.transform));
    }
}