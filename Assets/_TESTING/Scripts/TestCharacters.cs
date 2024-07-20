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
            Character character = CharacterManager.instance.CreateCharacter("Rin");
        }


        void Update()
        {

        }
    }
}