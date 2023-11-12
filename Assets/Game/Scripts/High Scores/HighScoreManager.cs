/*
 * Team: Team Bracket (Team 1)
 * Course: CSC-440-101
 * 
 * Name: High Score Manager
 * Script Objective: Manages Saving/Loading High Scores List and Adding New Scores
 * 
 */
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TeamBracket.HighScores {

    [System.Serializable]
    public class HighScore {

        public float timeSurvived;
        public int asteroidsDestroyed;
        public float overallScore;
        public string date;

        public HighScore( int asteroidsDestroyed, float timeSurvived ) {

            this.asteroidsDestroyed = asteroidsDestroyed;
            this.timeSurvived = timeSurvived;
            date = DateTime.Now.ToString( "MM/dd/yy" );

        }

    }

    [System.Serializable]
    public class HighScoresWrapper {

        public List<HighScore> scores;

        public HighScoresWrapper( List<HighScore> scores ) {

            this.scores = scores;

        }

    }

    public class HighScoreManager : MonoBehaviour {

        // FIXME: Unserialize this after testing is completed.
        [SerializeField] private List<HighScore> sortedScores = new( );

        [Header( "Points Allocation" )]
        [SerializeField] private float survivalPointsPerSecond = 1f;
        [SerializeField] private float pointsPerAsteroidDestroyed = 10f;


        private readonly string saveFileName = "scores.json";
        private bool isLoaded = false;

        // Readonly Public Variables. The targeted variables should NOT be changed during runtime and only should be assigned via the Unity Inspector window.
        public bool IsLoaded => isLoaded;
        public float SurvivalPointsPerSecond => survivalPointsPerSecond;
        public float PointsPerAsteroidDestroyed => survivalPointsPerSecond;
        public List<HighScore> HighScores => sortedScores;

        private void Start( ) {

            LoadScores( );


        }


        // Create HighScore object and calculate overall score based on the specified formula.
        private HighScore CreateHighScore( int asteroidsDestroyed, float timeSurvived ) {

            HighScore newHighScore = new( asteroidsDestroyed, timeSurvived ) {
                overallScore = ( timeSurvived * survivalPointsPerSecond ) + ( asteroidsDestroyed * pointsPerAsteroidDestroyed )
            };

            return newHighScore;

        }

        // This function is called by GameManager after the game is over.
        public void AddScore( int asteroidsDestroyed, float timeSurvived ) {

            // Add Score to List
            sortedScores.Add( CreateHighScore( asteroidsDestroyed, timeSurvived ) );

            // Sort List From Highest To Lowest
            sortedScores.Sort( ( a, b ) => b.overallScore.CompareTo( a.overallScore ) );

            if ( sortedScores.Count > 10 ) {

                sortedScores.RemoveRange( 10, sortedScores.Count - 10 );

            }

            // Save Scores to File
            SaveScores( );

        }

        public HighScore GetScore( int asteroidsDestroyed, float timeSurvived ) {

            HighScore foundScore = sortedScores.Find( score => score.timeSurvived == timeSurvived && score.asteroidsDestroyed == asteroidsDestroyed );

            return foundScore;

        }

        

        // Save Score List to File
        private void SaveScores( ) {

            Debug.Log( "SAVING SCORES: #" + sortedScores.Count );

            try {

                string filePath = Path.Combine( Application.persistentDataPath, saveFileName );

                using ( StreamWriter writer = new( filePath ) ) {
                    string json = JsonUtility.ToJson( new HighScoresWrapper( sortedScores ), true ); // Serialize a wrapper object
                    writer.Write( json );
                }

                Debug.Log( "Scores saved successfully." );


            } catch ( Exception e ) {

                Debug.LogError( "Error saving scores: " + e.Message );

            }


        }

        // Load Score List From File
        private void LoadScores( ) {

            string filePath = Application.persistentDataPath + "/" + saveFileName;

            Debug.Log( "LOAD FILE PATH: " + filePath );

            if ( File.Exists( filePath ) ) {

                try {

                    using StreamReader reader = new( filePath );
                    string json = reader.ReadToEnd( );
                    HighScoresWrapper wrapper = JsonUtility.FromJson<HighScoresWrapper>( json );

                    if ( wrapper != null ) {

                        sortedScores = wrapper.scores;
                        Debug.Log( "LOADED SCORES: #" + sortedScores.Count );

                    }

                } catch ( Exception e ) {

                    Debug.Log( "Error when loading saved scores: " + e.Message );

                }

            }

            isLoaded = true;

        }

    }

}