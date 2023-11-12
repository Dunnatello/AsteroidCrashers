/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: High Score Lister
 * Script Objective: Lists all high scores in the "Scores" scene in a UI list. 
 * 
 */
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace TeamBracket.HighScores {
    public class HighScoreLister : MonoBehaviour {

        [Header( "References" )]
        [SerializeField] private GameObject listingPrefab;
        [SerializeField] private GameObject scoreContainer;
        [SerializeField] private HighScoreManager highScoreManager;

        private readonly TimeStringCreator timeStringCreator = new( );

        // Start is called before the first frame update
        void Start( ) {

            LoadScores( );
        }

        void LoadScores( ) {

            if ( highScoreManager.IsLoaded ) { // Scores loaded, show them in the list.

                ShowScores( );

            } else { // Keep waiting until the file is loaded completely.

                Invoke( nameof( LoadScores ), 0.1f );

            }

        }

        void ShowScores( ) {

            // Get all high scores.
            List<HighScore> highScores = highScoreManager.HighScores;

            for ( int i = 0; i < 10; i++ ) {

                // If current index exceeds the list of high scores, break from the list so that the loop doesn't go out of bounds.
                if ( i > highScores.Count - 1 )
                    break;

                GameObject newListing = Instantiate( listingPrefab );

                // Update Rank Text
                newListing.transform.GetChild( 0 ).GetComponent< TextMeshProUGUI >( ).text = ( i + 1 ).ToString( );

                // Update Date Text
                newListing.transform.GetChild( 1 ).GetComponent<TextMeshProUGUI>( ).text = highScores.ElementAt( i ).date;

                // Update Overall Score Text
                newListing.transform.GetChild( 2 ).GetComponent<TextMeshProUGUI>( ).text = string.Format( "{0:0.00}", highScores.ElementAt( i ).overallScore );

                // Update Time Survived Text
                newListing.transform.GetChild( 3 ).GetComponent<TextMeshProUGUI>( ).text = timeStringCreator.GetNewTimeString( highScores.ElementAt( i ).timeSurvived );

                // Update Asteroids Destroyed Text
                newListing.transform.GetChild( 4 ).GetComponent<TextMeshProUGUI>( ).text = highScores.ElementAt( i ).asteroidsDestroyed.ToString( );

                // Add Listing to High Scores List
                newListing.transform.SetParent( scoreContainer.transform );

            }

        }

    }

}