/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: Randomize Sprite
 * Script Objective: Randomly chooses from a list of sprites provided in the Unity Inspector. The game uses this script to randomize the asteroid appearances.
 * 
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TeamBracket {
    public class RandomizeSprite : MonoBehaviour {

        [SerializeField] private List<Sprite> spriteList = new( );
        private SpriteRenderer spriteRenderer;

        // Start is called before the first frame update
        private void Start( ) {

            // Randomize Sprite & Assign Choice to Sprite Renderer Component
            spriteRenderer = GetComponent<SpriteRenderer>( );
            spriteRenderer.sprite = spriteList.ElementAt( Random.Range( 0, spriteList.Count ) );

        }

    }

}