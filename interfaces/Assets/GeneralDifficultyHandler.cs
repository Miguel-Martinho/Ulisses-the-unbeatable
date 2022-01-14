using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GeneralDifficultyHandler : MonoBehaviour
{
    [SerializeField]
    private float levelLength_EASY;
    [SerializeField]
    private float levelLength_MEDIUM;
    [SerializeField]
    private float levelLength_HARD;

    [SerializeField]
    private GameObject player;
    private PlayerMovement pMovement;
    private float playerSpeed;

    private float initialPos => player.transform.position.x;
    private float endPos => (OptionsManager.instance.EnemyAndObstacles == Difficulty.Easy ? initialPos + levelLength_EASY :
                OptionsManager.instance.EnemyAndObstacles == Difficulty.Medium ? 
        initialPos + levelLength_MEDIUM :  initialPos + levelLength_HARD);

    #region Constants
    private const float initialPosBuffer = 20;
    
    private const float obsYPos = -1.66f, obsZPos = -0.97f;
    private const float enemyFrontPos = 9, enemyBackPos = 13;
    private const float endYPos = 5, endZPos = -1;
    #endregion

    [SerializeField]
    private float obstacleOffset = 10;

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
        //EndLevel end = endObject.GetComponent<EndLevel>();
        //end.init(gameOverPanel, endLevelPanel);

        //Randomly select First Obstacle Type From Possible Types


    
        

        int iteration =0;
        int maxIteration = 100;

        while (obsDifficulty.CurrentEntities != obsDifficulty.MaxNumberOfEntities ||
            enDifficulty.CurrentEntities != enDifficulty.MaxNumberOfEntities)
        {
            //Debug.Log("it: " + iteration);
            foreach (Transform g in transform)
            {
                Destroy(g.gameObject);
            }
            obstaclePos.Clear();

            obsDifficulty.CurrentEntities = 0;
            enDifficulty.CurrentEntities = 0;

            int objNumb = 0;
            int it = 0;

            //Randomly select First Obstacle Pos
            Vector3 pos = new Vector3(Random.Range(initialPos + initialPosBuffer, endPos), obsYPos, obsZPos);
            obsDifficulty.AddEntity(obstaclesPREFAB[0], pos);
            obstaclePos.Add(pos.x);

            

            iteration++;

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
                    if (Mathf.Abs(closerObsPos - newPos.x) > Mathf.Abs(f - newPos.x))
                    {
                        closerObsPos = f;
                    }
                }

                
                int maxIt = 
                    (enDifficulty.MaxNumberOfEntities + obsDifficulty.MaxNumberOfEntities ) 
                    * 50;

                if (Mathf.Abs(closerObsPos - newPos.x) < obstacleOffset)
                {
                    //Debug.Log("i " + iteration);
                    it++;

                    if (it > maxIt)
                    {
                        //Debug.Log("WHY" + it);                      
                        break;  
                    }
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
                int probalitity = (int)UGYSTO.Remap(newPos.x, closerObsPos, maxDistance, 0, 100);
                probalitity = Mathf.Abs(probalitity);

                //Random Range and see if it can be placed
                int temp = Random.Range(-100, 101);


                if (temp > probalitity)
                {
                    //Debug.Log($"SUCCESS Value_ {temp} PROB: {probalitity} // PosToTest: {newPos.x}");
                    objNumb++;
                    if (r == ObsType.Obstace)
                        obsDifficulty.AddEntity(obstaclesPREFAB[Random.Range(0, obstaclesPREFAB.Length)], newPos);
                    else if (r == ObsType.Enemy)
                    {

                        float enemyStartPos = initialPos - Random.Range(20, 31);
                        Vector3 enPos = new Vector3(enemyStartPos, newPos.y, GetEnemyZPos());
                        float t = (-initialPos + newPos.x) / playerSpeed;
                        float speed = (-enemyStartPos + newPos.x) / t;


                        GameObject enemy = enDifficulty.AddEntity(enemyPREFAB, enPos, speed);
                        pMovement.onStart += () =>
                        {
                            if(enemy != null)
                                enemy.GetComponent<EnemyActor>().StartRun();
                        };
                    }
                    obstaclePos.Add(newPos.x);
                    continue;
                }
                //Debug.Log($"FAILED Value_ {temp} PROB: {probalitity} // PosToTest: {newPos.x}");


                it++;
                //Continue
                //Save Higher Probabilty
                //If 3 times have passed Assign Position to higher probability
            }


            //Debug.Log("Enemies: " + enDifficulty.CurrentEntities + "/" + enDifficulty.MaxNumberOfEntities);
            //Debug.Log("Obstacles: " + obsDifficulty.CurrentEntities + "/" + obsDifficulty.MaxNumberOfEntities);

            if (iteration == maxIteration)
            {
                break;
            }

           
        }
    }

    private float GetEnemyZPos()
    {
        if (Random.Range(0, 2) == 0) 
            return Random.Range(1f,0);
        return Random.Range(-2f, -3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
