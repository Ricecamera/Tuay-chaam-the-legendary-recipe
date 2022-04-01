// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;

// public class VanillaGainSPAll : Skill
// {
//     //constructor
//     public VanillaGainSPAll(string skillId, string skillName, string description, int cooldown, Sprite icon) : base(skillId, skillName, description, cooldown, icon, "TargetAllAlliances")
//     {
//         GainSPAllAlliance += ActionVanillaGainSPAll;
//         this.icon = icon;
//     }

//     //delegates
//     public Action<List<PakRender>, PakRender> GainSPAllAlliance;
//     //action
//     public void ActionVanillaGainSPAll(List<PakRender> target, PakRender self)
//     {
//         int spValue = 10;
//         foreach (PakRender e in target)
//         {
//             e.currentSp += spValue;
//             e.defBuffVfx.Play();
//         }
//         return;
//     }
// }
