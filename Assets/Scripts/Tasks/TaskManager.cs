using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class TaskManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public int normalTaskCount = 7;

    public TMP_Text[] subTasks;
    public TMP_Text mainTask;

    public List<string> taskList = new List<string>();
    public List<string> mainTaskList = new List<string>();
    
    public List<string> playerTaskList = new List<string>();
    public List<string> playermainTaskList = new List<string>();
    private bool subtaskprog1 = false;
    private bool subtaskprog2 = false;
    private bool subtaskprog3 = false;
    private bool maintaskprog = false;

    void Start()
    {
        InitializeTaskList();
        InitializeMainTaskList();
        RandomizeTasks();
    }

    void Update()
    {

    }

    private void InitializeTaskList()
    {
        taskList.Add("Craft 3 plank: "); // taskList[0]
        taskList.Add("Craft 3 metal sheet: "); // taskList[1]
        taskList.Add("Craft 3 canned food: ");
        taskList.Add("Trash any 2 action cards: ");
        taskList.Add("Craft 2 fishing rod: ");
        taskList.Add("Gather 6 wood: ");
        taskList.Add("Gather 6 food: ");
        taskList.Add("Gather 6 scrap: ");
        taskList.Add("Gather 6 junk: ");
        taskList.Add("Craft 3 dummy card: ");
    }

    private void InitializeMainTaskList()
    {
        mainTaskList.Add("Deal damage 3 times (any player): ");
        mainTaskList.Add("Trash any item cards: ");
        mainTaskList.Add("Heal 3 heart: ");
        mainTaskList.Add("Trash 2 bomb or weapon card: ");
    }

    private void RandomizeTasks()
    {
        // Randomly shuffle task list
        taskList = taskList.OrderBy(x => Random.value).ToList();

        // Assign tasks to subtasks
        for (int i = 0; i < normalTaskCount; i++)
        {
            if (i < taskList.Count)
            {
                subTasks[i].text = taskList[i] + GetTaskProgressString(taskList[i]);
                playerTaskList.Add(taskList[i]);
            }
            else
            {
                subTasks[i].text = "";
            }
        }

        // Randomly select main task
        int mainIndex = Random.Range(0, mainTaskList.Count);
        mainTask.text = mainTaskList[mainIndex] + GetTaskProgressString(mainTaskList[mainIndex]);
        playermainTaskList.Add(mainTaskList[mainIndex]);
    }

    private string GetTaskProgressString(string task)
    {
        if (task.Contains("Craft 3 plank"))
            return playerManager.Plank + "/3";
        else if (task.Contains("Craft 3 metal sheet"))
            return playerManager.Metal + "/3";
        else if (task.Contains("Craft 3 canned food"))
            return playerManager.CannedFood + "/3";
        else if (task.Contains("Trash any 2 action cards"))
            return playerManager.ActionTrashed + "/2";
        else if (task.Contains("Craft 2 fishing rod"))
            return playerManager.FishingRod + "/2";
        else if (task.Contains("Gather 6 wood"))
            return playerManager.Wood + "/6";
        else if (task.Contains("Gather 6 food"))
            return playerManager.Food + "/6";
        else if (task.Contains("Gather 6 scrap"))
            return playerManager.Scrap + "/6";
        else if (task.Contains("Gather 6 junk"))
            return playerManager.Junk + "/6";
        else if (task.Contains("Craft 3 dummy card"))
            return playerManager.DummyCard + "/3";
        else if (task.Contains("Deal damage 3 times (any player)"))
            return playerManager.dealDamage + "/3";
        else if (task.Contains("Trash any item cards"))
            return playerManager.ItemTrashed + "/1";
        else if (task.Contains("Heal 3 heart"))
            return playerManager.heal + "/3";
        else if (task.Contains("Trash 2 bomb or weapon card"))
            return playerManager.weaponOrBombTrashed + "/2";
        else
            return "";
    }

    private bool CheckTaskProgress(string task)
    {
        if (task.Contains("Craft 3 plank") && playerManager.Plank == 3)
        {
            Debug.Log("Task done!!");
            return true;
        }
        if (task.Contains("Craft 3 metal sheet") && playerManager.Metal == 3)
        {
            Debug.Log("Task done!!");
            return true;
        }
        if (task.Contains("Craft 3 canned food") && playerManager.CannedFood == 3)
        {
            Debug.Log("Task done!!");
            return true;
        }
        if (task.Contains("Trash any 2 action cards") && playerManager.ActionTrashed == 2)
        {
            Debug.Log("Task done!!");
            return true;
        }
        if (task.Contains("Craft 2 fishing rod") && playerManager.FishingRod == 2)
        {
            Debug.Log("Task done!!");
            return true;
        }
        if (task.Contains("Gather 6 wood") && playerManager.Wood == 6)
        {
            Debug.Log("Task done!!");
            return true;
        }
        if (task.Contains("Gather 6 food") && playerManager.Food == 6)
        {
            Debug.Log("Task done!!");
            return true;
        }
        if (task.Contains("Gather 6 scrap") && playerManager.Scrap == 6)
        {
            Debug.Log("Task done!!");
            return true;
        }
        if (task.Contains("Gather 6 junk") && playerManager.Junk == 6)
        {
            Debug.Log("Task done!!");
            return true;
        }
        if (task.Contains("Craft 3 dummy card") && playerManager.DummyCard == 6)
        {
            Debug.Log("Task done!!");
            return true;
        }
        if (task.Contains("Deal damage 3 times (any player)") && playerManager.dealDamage == 3)
        {
            Debug.Log("Task done!!");
            return true;
        } 
        if (task.Contains("Trash any item cards") && playerManager.ItemTrashed == 1)
        {
            Debug.Log("Task done!!");
            return true;
        }
        if (task.Contains("Heal 3 heart") && playerManager.heal == 3)
        {
            Debug.Log("Task done!!");
            return true;
        }
        if (task.Contains("Trash 2 bomb or weapon card") && playerManager.weaponOrBombTrashed == 2)
        {
            Debug.Log("Task done!!");
            return true;
        }
        return false;
    }

    public void UpdateTaskString()
    {
        // Update subtasks
        for (int i = 0; i < Mathf.Min(normalTaskCount, taskList.Count, subTasks.Length); i++)
        {
            subTasks[i].text = taskList[i] + GetTaskProgressString(taskList[i]);
        }

        // Update main task (assuming only one main task for simplicity)
        if (mainTaskList.Count > 0)
        {
            mainTask.text = mainTaskList[0] + GetTaskProgressString(mainTaskList[0]);
        }
    }

    public void UpdateTaskProgress()
    {
        UpdateTaskString();
        subtaskprog1 = CheckTaskProgress(playerTaskList[0]);
        TextColorUpdate(subtaskprog1, subTasks[0]);
        subtaskprog2 = CheckTaskProgress(playerTaskList[1]);
        TextColorUpdate(subtaskprog2, subTasks[1]);
        subtaskprog3 = CheckTaskProgress(playerTaskList[2]);
        TextColorUpdate(subtaskprog3, subTasks[2]);
        maintaskprog = CheckTaskProgress(playermainTaskList[0]);
        TextColorUpdate(maintaskprog, mainTask);
    }

    private void TextColorUpdate(bool status, TMP_Text text)
    {
        if (status == true)
        {
            text.color = Color.green;
        }
    }
}
