using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSTTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        BinarySearchTree<char> bst = new BinarySearchTree<char>();
        bst.Insert('I');
        bst.Insert('L');
        bst.Insert('O');
        bst.Insert('V');
        bst.Insert('E');
        bst.Insert('Y');
        bst.Insert('H');
        bst.Insert('U');
        var testArray = bst.Traverse(TraverseType.InOrder);
        foreach (var item in testArray)
        {
            Debug.Log(item);
        }

        bst.Delete('O');
        testArray = bst.Traverse(TraverseType.InOrder);
        foreach (var item in testArray)
        {
            Debug.Log(item);
        }
	}
}
