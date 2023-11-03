using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomizeSprite : MonoBehaviour
{

    [SerializeField] private List< Sprite > spriteList = new( );
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start( ) {

        spriteRenderer = GetComponent< SpriteRenderer >( );
        spriteRenderer.sprite = spriteList.ElementAt( Random.Range( 0, spriteList.Count ) );

    }


}
