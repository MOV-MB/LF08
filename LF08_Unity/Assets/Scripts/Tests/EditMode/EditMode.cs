using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.Scripts.Player;

public class EditMode
{
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    public Player player = new Player();
=======
    public Player Player = new();
>>>>>>> parent of d696cfc (Fixed Unit Tests Locally)
=======
    public Player player = new Player();
>>>>>>> parent of 19036c6 (Changed some assembly defs)
=======
    public Player player = new Player();
>>>>>>> parent of cd4aea0 (Merge pull request #2 from MOV-MB/GameSceneMerge)

    [Test]
    public void PlayerBeginningStats()
    {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> parent of 19036c6 (Changed some assembly defs)
=======
>>>>>>> parent of cd4aea0 (Merge pull request #2 from MOV-MB/GameSceneMerge)
        Assert.AreEqual(false, player.GodMode);
        Assert.AreEqual(true, player.CanMove);
        Assert.AreEqual(30f, player.BulletForce);
        Assert.AreEqual(0.5f, player.Firerate);
        Assert.AreEqual(10f, player.MoveSpeed);
        Assert.AreEqual(0, player.Money);
        Assert.AreEqual(100f, player.Health);
<<<<<<< HEAD
<<<<<<< HEAD
=======
        Assert.AreEqual(false, Player.GodMode);
        Assert.AreEqual(true, Player.CanMove);
        Assert.AreEqual(30f, Player.BulletForce);
        Assert.AreEqual(0.5f, Player.Firerate);
        Assert.AreEqual(10f, Player.MoveSpeed);
        Assert.AreEqual(0, Player.Money);
        Assert.AreEqual(100f, Player.Health);
>>>>>>> parent of d696cfc (Fixed Unit Tests Locally)
=======
>>>>>>> parent of 19036c6 (Changed some assembly defs)
=======
>>>>>>> parent of cd4aea0 (Merge pull request #2 from MOV-MB/GameSceneMerge)
    }

    [Test]
    public void HealthbarEqualToHealth()
    {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        Assert.AreEqual(player.Health, player.HealthBar.healthSlider.value / 100);
=======
        Assert.AreEqual(Player.Health, Player.HealthBar.healthSlider.value / 100);
>>>>>>> parent of d696cfc (Fixed Unit Tests Locally)
=======
        Assert.AreEqual(player.Health, player.HealthBar.healthSlider.value / 100);
>>>>>>> parent of 19036c6 (Changed some assembly defs)
=======
        Assert.AreEqual(player.Health, player.HealthBar.healthSlider.value / 100);
>>>>>>> parent of cd4aea0 (Merge pull request #2 from MOV-MB/GameSceneMerge)
    }

    [Test]
    public void ShootingPointPlacement()
    {
        Assert.IsTrue(player.ShootingPoint.IsChildOf(player.transform));
    }
}