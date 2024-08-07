using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;

namespace TESTING
{
    public class TestCharacters : MonoBehaviour
    {
      
        void Start()
        {
            Character Alex = CharacterManager.instance.CreateCharacter("Alex");
            Character Rin = CharacterManager.instance.CreateCharacter("Rin");
            Character Azul = CharacterManager.instance.CreateCharacter("Azul");
        }


        void Update()
        {

        }
    }
}