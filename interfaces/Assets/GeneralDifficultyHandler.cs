using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GeneralDifficultyHandler : MonoBehaviour
{
    [SerializeField]
    private float levelLength;

    [SerializeField]
    private GameObject player;
    private PlayerMovement pMovement;
    private float playerSpeed;

    private float initialPos => player.transform.position.x;
    private float endPos => initialPos + levelLength;

    #region Constants
    private const float initialPosBuffer = 20;
    private const float obstacleOffset = 10;
    private const float obsYPos = -1.66f, obsZPos = -0.97f;
    private const float enemyFrontPos = 9, enemyBackPos = 13;
    private const float endYPos = 5, endZPos = -1;
    #endregion

    private ObstacleDifficultyHandler obsDifficulty;
    private EnemyDifficultyHandler enDifficulty;

    [SerializeField]
    private GameObject enemyPREFAB, endPREFAB;
    [SerializeField]
    private GameObject[] obstaclesPREFAB;


    [SerializeField]
    private GameObject gameOverPanel, endLevelPanel;

    private List<float> obstaclePos;

    private enum ObsType
    {
        Enemy, 
        Obstace
    }

    // Start is called before the first frame update
    void Start()
    {
        obsDifficulty = GetComponent<ObstacleDifficultyHandler>();
        enDifficulty = GetComponent<EnemyDifficultyHandler>();
        obstaclePos = new List<float> { };
        pMovement = player.GetComponent<PlayerMovement>();
        playerSpeed = pMovement.WalkingVelocity;
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        GameObject endObject = Instantiate(endPREFAB, new Vector3(endPos, endYPos, endZPos), Quaternion.identity);
        EndLevel end = endObject.GetComponent<EndLevel>();
        end.init(gameOverPanel, endLevelPanel);

        //Randomly select First Obstacle Type From Possible Types
        

        //Randomly select First Obstacle Pos
        Vector3 pos = new Vector3(Random.Range(initialPos + initialPosBuffer, endPos), obsYPos, obsZPos);
        obsDifficulty.AddEntity(obstaclesPREFAB[0], pos);
        obstaclePos.Add(pos.x);

        int objNumb = 0;
        int it = 0;
                     
        while (obsDifficulty.CurrentEntities < obsDifficulty.MaxNumberOfEntities 
            || enDifficulty.CurrentEntities < enDifficulty.MaxNumberOfEntities)
        {

            ObsType r = (ObsType)1;
            //Select Next Obstacle Type 
            //0 Obj - 1 Enemy
            if (obsDifficulty.CurrentEntities >= obsDifficulty.MaxNumberOfEntities)
            {
                r = (ObsType)0;
            }
            else if (enDifficulty.CurrentEntities >= enDifficulty.MaxNumberOfEntities)
            {
                r = (ObsType)1;
            }
            else     
            {
                r = (ObsType)Random.Range(0, 2);
            }


            //Select Next Obstacle Pos
            Vector3 newPos = new Vector3(Random.Range(initialPos + initialPosBuffer, endPos), obsYPos, obsZPos);
            
         
            float maxDistance = 0;
            //float closerObsPos = obstaclePos.(x => Mathf.Abs(x - newPos.x));

            float closerObsPos = 999;
            foreach (float f in obstaclePos)
            {
                if(Mathf.Abs(closerObsPos - newPos.x) > Mathf.Abs(f - newPos.x))
                {
                    closerObsPos = f;
                }
            }
              
            if (Mathf.Abs(closerObsPos - newPos.x) < obstacleOffset)
            {
               
               
                it++;
                if (it > 20) return;
                continue;
            }
           
            if (newPos.x - closerObsPos > 0)
            {
                maxDistance = initialPos + initialPosBuffer;
            }
            else
            {
                maxDistance = endPos;
            }

            //Assign a probabilty mapping the distance from the already placed objects 
            int probalitity = (int)UGYSTO.Remap(newPos.x, closerObsPos, maxDistance,0,100);
            probalitity = Mathf.Abs(probalitity);
            
            //Random Range and see if it can be placed
            int temp = Random.Range(50,101);
            if(temp < probalitity)
            {              
                objNumb++;
                if (r == ObsType.Obstace)
                    obsDifficulty.AddEntity(obstaclesPREFAB[Random.Range(0,obstaclesPREFAB.Length)], newPos);
                else if (r == ObsType.Enemy) {

                    float enemyStartPos = initialPos - Random.Range(20,31);
                    Vector3 enPos = new Vector3(enemyStartPos, newPos.y,newPos.z);
                    float t = (-initialPos + newPos.x) / playerSpeed;
                    float speed = (-enemyStartPos + newPos.x) / t;


                    GameObject enemy = enDifficulty.AddEntity(enemyPREFAB, enPos, speed);
                    pMovement.onStart += () => 
                    { 
                        enemy.GetComponent<EnemyActor>().StartRun(); 
                    };
                }
                obstaclePos.Add(newPos.x);
            }

            //Continue
            //Save Higher Probabilty
            //If 3 times have passed Assign Position to higher probability
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
