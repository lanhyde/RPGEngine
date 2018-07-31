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
    private void Start()
    {
        ITree<int> rbtree = new RBTree<int>();

        rbtree.Insert(10);
        rbtree.Insert(20);
        rbtree.Insert(30);

        rbtree.Insert(5);
        rbtree.Insert(1);

        int[] result = rbtree.Traverse(TraverseType.InOrder);

        foreach(int val in result)
        {
            Debug.Log(val);
        }
    }
}
