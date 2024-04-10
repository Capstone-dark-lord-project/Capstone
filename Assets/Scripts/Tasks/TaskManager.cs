using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    //Task
    public int normalTask = 3;
    public int taskPoint = 0;

    public bool taskCom = false;
    public bool win = false;

    public TMP_Text[] task = new TMP_Text[3];
    public TMP_Text winTask;

    private string[] taskList = new string[4];
    private string[] winTaskList = new string[1];

    private System.Random rand = new System.Random();
    private bool randomTaskStatus = true;
    public bool[] taskStatus = new bool[4];
    public int[] taskNum = new int[3];
    //TempInventory

    public int wood = 0;
    public int stone = 0;
    public int matches = 0;
    public int water = 0;
    public int vine = 0;
    public int food = 0;

    void Start()
    {
        Debug.Log("Start");
        //Temp Task
        /*task[0].text = "1";
        task[1].text = "2";
        task[2].text = "3";*/
        //Task Normal
        taskList[0] = "Gather 5 water for your survival: " +  water.ToString() + "/5";
        taskList[1] = "Gather 2 vine for climbing: " + vine.ToString() + "/2";
        taskList[2] = "Gather 1 matches to start fire: " + matches.ToString() + "/1";
        taskList[3] = "Gather 2 wood and 2 stone for making tools: wood " + wood.ToString() + "/2 stone " + stone.ToString() + "/2";
        //Task Win
        winTaskList[0] = "Escape the island by boat Gather 5 wood and 5 vine: wood " + wood.ToString() + "/5 vine " + vine.ToString() + "/5";
    }

    void Update()
    {
        //Random Normal Task
        if (randomTaskStatus == true)
        {
            List<int> availableIndices = new List<int>();
            for (int j = 0; j < taskList.Length; j++)
            {
                availableIndices.Add(j);
            }
            for (int i = 0; i < normalTask; i++)
            {
                int randomIndex = rand.Next(0, availableIndices.Count);
                int randomTask = availableIndices[randomIndex];
                task[i].text = taskList[randomTask];
                taskStatus[randomTask] = true;
                taskNum[i] = randomTask;
                availableIndices.RemoveAt(randomIndex);

                Debug.Log("สุ่มtask" + i.ToString());
                if (i == 2)
                {
                    randomTaskStatus = false;
                }
            }
        }
        //Task Update Inventory
        if(taskStatus[0] == true)
        {
            taskList[0] = "Gather 5 water for your survival: " +  water.ToString() + "/5";
        }
        if(taskStatus[1] == true)
        {
            taskList[1] = "Gather 2 vine for climbing: " + vine.ToString() + "/2";
        }
        if(taskStatus[2] == true)
        {
            taskList[2] = "Gather 1 matches to start fire: " + matches.ToString() + "/1";
        }
        if(taskStatus[3] == true)
        {
            taskList[3] = "Gather 2 wood and 2 stone for making tools: wood " + wood.ToString() + "/2 stone " + stone.ToString() + "/2";
        }

        //Task Update to Text
        if(taskCom == false)
        {
            for (int i = 0; i < normalTask; i++)
                    {
                        switch (taskNum[i])
                        {
                            case 0:
                                task[i].text = taskList[0];
                                if(water > 5)
                                {
                                    task[i].fontStyle |= FontStyles.Strikethrough;
                                    taskStatus[0] = false;
                                    water -= 5;
                                    taskPoint += 1;
                                    Debug.Log("Task Water Complete :" + taskPoint);
                                }
                                break;
                            case 1:
                                task[i].text = taskList[1];
                                if(vine > 2)
                                {
                                    task[i].fontStyle |= FontStyles.Strikethrough;
                                    taskStatus[0] = false;
                                    vine -= 2;
                                    taskPoint += 1;
                                    Debug.Log("Task Vine Complete :" + taskPoint);
                                }
                                break;
                            case 2:
                                task[i].text = taskList[2];
                                if(matches > 1)
                                {
                                    task[i].fontStyle |= FontStyles.Strikethrough;
                                    taskStatus[0] = false;
                                    matches -= 1;
                                    taskPoint += 1;
                                    Debug.Log("Task Matches Complete :" + taskPoint);
                                }
                                break;
                            case 3:
                                task[i].text = taskList[3];
                                if(wood > 2 && stone > 2)
                                {
                                    task[i].fontStyle |= FontStyles.Strikethrough;
                                    taskStatus[0] = false;
                                    wood -= 2;
                                    stone -= 2;
                                    taskPoint += 1;
                                    Debug.Log("Task Tools Complete :" + taskPoint);
                                }
                                break;
                        }
                    }
        }

        if(taskPoint == 3)
            {   
                
                //Debug.Log("Normal task all completed");
                taskCom = true;
                winTask.text = winTaskList[0];
                winTaskList[0] = "Escape the island by boat Gather 5 wood and 5 vine: wood " + wood.ToString() + "/5 vine " + vine.ToString() + "/5";
                if(wood > 5 && vine > 5)
                {
                    winTask.fontStyle |= FontStyles.Strikethrough;
                    wood -= 5;
                    vine -= 5;
                    win = true;
                }
            }

            if(win == true)
            {
                Debug.Log("WIN!!!!");
                win = false;
            }


        /*if(randomTaskStatus == true)
            for (int i = 0; i < normalTask; i++)
            {
                int randomTask = rand.Next(0,3);
                task[i].text = taskList[randomTask];
                taskStatus[randomTask] = true;
                //Debug.Log("Random number: " + randomTask);
                Debug.Log(i.ToString());
                if(i == 2)
                {
                    randomTaskStatus = false;
                }
            }*/
    }
}
