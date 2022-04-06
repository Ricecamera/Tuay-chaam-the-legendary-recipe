using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatScene : MonoBehaviour
{
    void Start()
    {
        CharacterSelecter.instance.ResetCharacter();
    }
}
