using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine.Profiling;
using System.Text;
using UnityEngine.UI;
public class BSTTest : MonoBehaviour {
    [SerializeField] Text debugText;
    BinarySearchTree<int> bst;
    IList<int> normalList;
    CancellationTokenSource tokenSource;
    int randomValueToSearch = int.MinValue;
    bool isRandomValueGenerated = false;
    bool isJobTerminated = false;
    long memoryUsed;
    // Use this for initialization
    void Start () {
        bst = new BinarySearchTree<int>();
        normalList = new List<int>();
        tokenSource = new CancellationTokenSource();
        FillBSTRandomly(10000000);
	}

    private void OnDestroy()
    {
        tokenSource.Cancel();
        bst.Dispose();
        bst = null;
        normalList.Clear();
        bst = null;
        normalList = null;
        tokenSource.Dispose();
        tokenSource = null;
        System.GC.Collect();
        
    }

    private void Update()
    {
        StringBuilder sb = new StringBuilder();

        memoryUsed = Profiler.GetMonoHeapSizeLong();
        sb.Append("Total Heap Memory Used: " + (memoryUsed / 1024 / 1024) + "MB\n");
        memoryUsed = Profiler.GetMonoUsedSizeLong();
        sb.Append("Total Mono Memory Used: " + (memoryUsed / 1024 / 1024) + "MB\n");
        memoryUsed = Profiler.GetRuntimeMemorySizeLong(this);
        sb.Append("Runtime Memory Used: " + (memoryUsed / 1024 / 1024) + "MB\n");
        memoryUsed = Profiler.GetTotalAllocatedMemoryLong();
        sb.Append("Total Allocated Memory Used: " + (memoryUsed / 1024 / 1024) + "MB\n");
        memoryUsed = Profiler.GetTotalReservedMemoryLong();
        sb.Append("Reversed Memory : " + (memoryUsed / 1024 / 1024) + "MB\n");
        memoryUsed = Profiler.GetTotalUnusedReservedMemoryLong();
        sb.Append("Unused Reversed Memory " + (memoryUsed / 1024 / 1024) + "MB");
        debugText.text = sb.ToString();
        sb.Clear();
    }

    async void FillBSTRandomly(long capacity)
    {
        UnityEngine.Debug.Log("Generating Random data...");
        await Task.Factory.StartNew(() => {
            System.Random rnd = new System.Random();
            for (long i = 0; i < capacity; ++i)
            {
                if(tokenSource.IsCancellationRequested)
                {
                    UnityEngine.Debug.Log("Thread terminated because user cancelled the task");
                    isJobTerminated = true;
                    break;
                }
                int randomVal = (int)((rnd.NextDouble() - 0.5f) * 2 * capacity);
                bst.Insert(randomVal);
                normalList.Add(randomVal);

                if (!isRandomValueGenerated)
                {
                    if (rnd.NextDouble() > 0.8f || rnd.NextDouble() < 0.2f)
                    {
                        randomValueToSearch = randomVal;
                        isRandomValueGenerated = true;
                    }
                }
            }
        }, tokenSource.Token);
        UnityEngine.Debug.Log("Total Memory: " + System.GC.GetTotalMemory(false));
        if (isJobTerminated)
        {
            UnityEngine.Debug.Log("Process terminated");
            return;
        }
        UnityEngine.Debug.Log("Random Data generated");

        UnityEngine.Debug.Log("OK. Let's check whether searching from Binary Search AVL tree is faster than Normal List");
        UnityEngine.Debug.Log("Let's start searching from BST-AVL Tree");
        UnityEngine.Debug.Log("Start searching value: " + randomValueToSearch);
        System.DateTime currentTime = System.DateTime.Now;
        bool containVal = bst.Contains(randomValueToSearch);
        System.TimeSpan consumedTime = System.DateTime.Now - currentTime;
        UnityEngine.Debug.LogFormat("Search Result: {0}, Consumed Time: {1}", containVal, consumedTime);

        UnityEngine.Debug.Log("OK, Let's try to search a value that doesn't exist in BST-AVL tree");
        int notExistValue = int.MaxValue;
        containVal = false;
        currentTime = System.DateTime.Now;
        containVal = bst.Contains(notExistValue);
        consumedTime = System.DateTime.Now - currentTime;
        UnityEngine.Debug.LogFormat("Search Result: {0}, Consumed Time: {1}", containVal, consumedTime);

        UnityEngine.Debug.Log("OK, Let's try to delete a value.");
        currentTime = System.DateTime.Now;
        bst.Delete(randomValueToSearch);
        consumedTime = System.DateTime.Now - currentTime;
        UnityEngine.Debug.LogFormat("Delete complete, Consumed Time: {0}", consumedTime);

        UnityEngine.Debug.Log("Let's start searching from normal list");
        UnityEngine.Debug.Log("First let's try searching by normal for loop");
        containVal = false;
        currentTime = System.DateTime.Now;
        for(int i = 0; i < normalList.Count; ++i)
        {
            if(normalList[i] == randomValueToSearch)
            {
                containVal = true;
                break;
            }
        }
        consumedTime = System.DateTime.Now - currentTime;
        UnityEngine.Debug.LogFormat("Search Result: {0}, Consumed Time: {1}", containVal, consumedTime);

        UnityEngine.Debug.Log("Let's try the performance of Linq");
        containVal = false;
        currentTime = System.DateTime.Now;
        containVal = normalList.Contains(randomValueToSearch);
        consumedTime = System.DateTime.Now - currentTime;
        UnityEngine.Debug.LogFormat("Search Result: {0}, Consumed Time: {1}", containVal, consumedTime);

        UnityEngine.Debug.Log("Let's try the performance of Linq -- Does-Not-Exist value");
        containVal = false;
        currentTime = System.DateTime.Now;
        containVal = normalList.Contains(notExistValue);
        consumedTime = System.DateTime.Now - currentTime;
        UnityEngine.Debug.LogFormat("Search Result: {0}, Consumed Time: {1}", containVal, consumedTime);

        UnityEngine.Debug.Log("Let's try the value that does not exist");
        containVal = false;
        currentTime = System.DateTime.Now;
        for (int i = 0; i < normalList.Count; ++i)
        {
            if (normalList[i] == notExistValue)
            {
                containVal = true;
                break;
            }
        }
        consumedTime = System.DateTime.Now - currentTime;
        UnityEngine.Debug.LogFormat("Search Result: {0}, Consumed Time: {1}", containVal, consumedTime);

        UnityEngine.Debug.Log("Let's sort the array and try again.");
        UnityEngine.Debug.Log("Sorting...");
        int[] normalArray = normalList.ToArray();
        System.Array.Sort(normalArray);
        UnityEngine.Debug.Log("Sorting complete");

        UnityEngine.Debug.Log("OK. Let's start. First using for loop");
        containVal = false;
        currentTime = System.DateTime.Now;
        for (int i = 0; i < normalArray.Length; ++i)
        {
            if (normalArray[i] == randomValueToSearch)
            {
                containVal = true;
                break;
            }
        }
        consumedTime = System.DateTime.Now - currentTime;
        UnityEngine.Debug.LogFormat("Search Result: {0}, Consumed Time: {1}", containVal, consumedTime);

        UnityEngine.Debug.Log("Let's try the not-exist value");
        containVal = false;
        currentTime = System.DateTime.Now;
        for (int i = 0; i < normalArray.Length; ++i)
        {
            if (normalArray[i] == notExistValue)
            {
                containVal = true;
                break;
            }
        }
        consumedTime = System.DateTime.Now - currentTime;
        UnityEngine.Debug.LogFormat("Search Result: {0}, Consumed Time: {1}", containVal, consumedTime);

        UnityEngine.Debug.Log("Let's try linq. Does-Exist value");
        containVal = false;
        currentTime = System.DateTime.Now;
        containVal = normalArray.Contains(randomValueToSearch);
        consumedTime = System.DateTime.Now - currentTime;
        UnityEngine.Debug.LogFormat("Search Result: {0}, Consumed Time: {1}", containVal, consumedTime);

        UnityEngine.Debug.Log("Last Does-Not-Exist value");
        containVal = false;
        currentTime = System.DateTime.Now;
        containVal = normalArray.Contains(notExistValue);
        consumedTime = System.DateTime.Now - currentTime;
        UnityEngine.Debug.LogFormat("Search Result: {0}, Consumed Time: {1}", containVal, consumedTime);
        
        normalArray = null;
        Debug.Log("Wating 2 seconds");
        await Task.Delay(System.TimeSpan.FromSeconds(2));
        bst.Dispose();
        System.GC.Collect();
        UnityEngine.Debug.Log("Thank you for testing");
    }
}
