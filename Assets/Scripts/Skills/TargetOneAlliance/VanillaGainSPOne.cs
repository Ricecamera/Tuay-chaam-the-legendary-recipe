// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;
// public class VanillaGainSPOne : Skill
// {
//     //constructor
//     public VanillaGainSPOne(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetOneAlliance")
//     {
//         GainSPOneAlliance += ActionVanillaGainSPOne;
//         this.icon = icon;
//     }

//     //delegates
//     public Action<List<PakRender>, PakRender> GainSPOneAlliance;
//     //action
//     private void ActionVanillaGainSPOne(List<PakRender> target, PakRender self)
//     {
//         target[0].currentSp += 30;
//         target[0].defBuffVfx.Play();
//         return;
//     }
// }
