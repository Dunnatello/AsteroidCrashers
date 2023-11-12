using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamBracket {
    public class ContextualNotifications : MonoBehaviour {

        [SerializeField] private GameObject animatedTextContainer;
        [SerializeField] private GameObject respawnText;
        [SerializeField] private GameObject reloadText;

        void ToggleVisibility( bool isVisible ) {

            animatedTextContainer.SetActive( isVisible );

        }

        public void ToggleTextVisiblity( string textName, bool isVisible ) {

            if ( ( isVisible && !animatedTextContainer.activeInHierarchy ) || ( !isVisible && animatedTextContainer.activeInHierarchy ) ) {

                ToggleVisibility( isVisible );

            }

            switch ( textName ) {

                case "Respawn":

                    respawnText.SetActive( isVisible );
                    reloadText.SetActive( false );
/*                    if ( reloadText.activeInHierarchy ) {

                        ToggleTextVisiblity( "Reload", false );

                    }*/

                    break;

                case "Reload":
                    reloadText.SetActive( isVisible );
                    break;

            }

        }

        // Start is called before the first frame update
        void Start( ) {

            ToggleVisibility( false );

        }

    }

}
