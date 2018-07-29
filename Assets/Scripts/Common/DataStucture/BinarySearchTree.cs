using System;
using System.Collections.Generic;

public class BinarySearchTree<T> : IDisposable, ITree<T> where T : IComparable<T>
{
    private Node<T> root;
    private List<T> cachedBuffer = new List<T>();

    public void Delete(T data)
    {
        if (root != null)
        {
            root = Delete(root, data);
        }
    }

    public void Clear()
    {
        if (root != null)
        {
            DeleteAll(ref root);
        }
        //root = null;
    }

    private void DeleteAll(ref Node<T> root)
    {
        if(root != null)
        {
            Node<T> leftChild = root.LeftChild;
            Node<T> rightChild = root.RightChild;
            DeleteAll(ref leftChild);
            DeleteAll(ref rightChild);
            root = null;
        }
    }

    public int GetHeight(Node<T> node)
    {
        if (node == null)
            return -1;
        return node.Height;
    }

    private Node<T> RightRotation(Node<T> node)
    {
        Node<T> tmpLeftNode = node.LeftChild;
        Node<T> t = tmpLeftNode.RightChild;

        tmpLeftNode.RightChild = node;
        node.LeftChild = t;

        node.Height = Math.Max(GetHeight(node.LeftChild), GetHeight(node.RightChild)) + 1;
        tmpLeftNode.Height = Math.Max(GetHeight(tmpLeftNode.LeftChild), GetHeight(tmpLeftNode.RightChild)) + 1;

        return tmpLeftNode;
    }

    private Node<T> LeftRotation(Node<T> node)
    {
        Node<T> tmpRightNode = node.RightChild;
        Node<T> t = tmpRightNode.LeftChild;

        tmpRightNode.LeftChild = node;
        node.RightChild = t;

        node.Height = Math.Max(GetHeight(node.LeftChild), GetHeight(node.RightChild)) + 1;
        tmpRightNode.Height = Math.Max(GetHeight(tmpRightNode.LeftChild), GetHeight(tmpRightNode.RightChild)) + 1;

        return tmpRightNode;
    }

    private int GetBalance(Node<T> node)
    {
        if (node == null)
        {
            return 0;
        }
        return GetHeight(node.LeftChild) - GetHeight(node.RightChild);
    }

    private Node<T> Delete(Node<T> root, T data)
    {
        if (root == null)
            return root;
        if (data < root)
        {
            root.LeftChild = Delete(root.LeftChild, data);
        }
        else if (data > root)
        {
            root.RightChild = Delete(root.RightChild, data);
        }
        else
        {
            if (root.LeftChild == null && root.RightChild == null)
                return null;
            if (root.LeftChild == null)
            {
                Node<T> tempNode = root.RightChild;
                root = null;
                return tempNode;
            }
            else if (root.RightChild == null)
            {
                Node<T> tempNode = root.LeftChild;
                root = null;
                return tempNode;
            }
            Node<T> node = GetPredecessor(root.LeftChild);
            root.Data = node.Data;
            root.LeftChild = Delete(root.LeftChild, node.Data);
        }
        root.Height = Math.Max(GetHeight(root.LeftChild), GetHeight(root.RightChild)) + 1;

        return SettleDeletion(root);
    }

    private Node<T> SettleDeletion(Node<T> root)
    {
        int balance = GetBalance(root);

        if (balance > 1)
        {
            if(GetBalance(root.LeftChild) < 0)
            {
                root.LeftChild = LeftRotation(root.LeftChild);
            }
            return RightRotation(root);
        }
        if(balance < -1)
        {
            if(GetBalance(root.RightChild) > 0)
            {
                root.RightChild = RightRotation(root.RightChild);
            }
            return LeftRotation(root);
        }
        return root;
    }

    private Node<T> GetPredecessor(Node<T> root)
    {
        if (root.RightChild != null)
            return GetPredecessor(root.RightChild);
        return root;
    }

    public T GetMax()
    {
        return GetMaxValue(root);
    }

    public T GetMin()
    {
        return GetMinValue(root);
    }

    public T GetMinValue(Node<T> node)
    {
        if (node.LeftChild != null)
        {
            return GetMinValue(node.LeftChild);
        }
        return node.Data;
    }

    public T GetMaxValue(Node<T> node)
    {
        if (node.RightChild != null)
        {
            return GetMaxValue(node.RightChild);
        }
        return node.Data;
    }

    public void Insert(T data)
    {
        root = InsertNode(data, root);
    }

    public void ClearCache()
    {
        cachedBuffer.Clear();
    }

    public T[] Traverse(TraverseType traverseType)
    {
        switch (traverseType)
        {
            case TraverseType.PreOrder:
                PreOrderTraverse(root, d => cachedBuffer.Add(d.Data));
                break;
            case TraverseType.InOrder:
                InOrderTraverse(root, d => cachedBuffer.Add(d.Data));
                break;
            case TraverseType.PostOrder:
                PostOrderTraverse(root, d => cachedBuffer.Add(d.Data));
                break;
        }
        T[] result = cachedBuffer.ToArray();
        ClearCache();
        return result;
    }

    private void PostOrderTraverse(Node<T> root, Action<Node<T>> callback = null)
    {
        if (root == null)
            return;
        if (root.LeftChild != null)
        {
            PostOrderTraverse(root.LeftChild, callback);
        }
        if (root.RightChild != null)
        {
            PostOrderTraverse(root.RightChild, callback);
        }
        callback(root);
    }
    private void InOrderTraverse(Node<T> root, Action<Node<T>> callback = null)
    {
        if (root == null)
            return;
        if (root.LeftChild != null)
        {
            InOrderTraverse(root.LeftChild, callback);
        }
        callback(root);
        if (root.RightChild != null)
        {
            InOrderTraverse(root.RightChild);
        }
    }

    private void PreOrderTraverse(Node<T> root, Action<Node<T>> callback = null)
    {
        if (root == null)
            return;
        callback(root);
        if (root.LeftChild != null)
        {
            PreOrderTraverse(root.LeftChild, callback);
        }
        if (root.RightChild != null)
        {
            PreOrderTraverse(root.RightChild, callback);
        }
    }

    public bool Contains(T data)
    {
        return Contains(root, data);
    }

    private bool Contains(Node<T> root, T data)
    {
        if (root == null)
            return false;
        if (root == data)
            return true;
        if (root > data)
        {
            return Contains(root.LeftChild, data);
        }
        else
        {
            return Contains(root.RightChild, data);
        }
    }

    private Node<T> InsertNode(T data, Node<T> node)
    {
        if (node == null)
        {
            return new Node<T>(data);
        }

        if (data < node)
        {
            node.LeftChild = InsertNode(data, node.LeftChild);
        }
        else if (data > node)
        {
            node.RightChild = InsertNode(data, node.RightChild);
        }
        else    // Duplicate data now allowed
        {
            return node;
        }

        node.Height = Math.Max(GetHeight(node.LeftChild), GetHeight(node.RightChild)) + 1;
        node = SetViolation(node, data);

        return node;
    }

    private Node<T> SetViolation(Node<T> node, T data)
    {
        int balance = GetBalance(node);
        ////////////////////////////////////////////////////////////////////
        // Case I : left heavy situation, we need to right rotate the tree
        //     A                                  B
        //    /                                  / \
        //   B                   ->             A   C
        //  /
        // C
        ////////////////////////////////////////////////////////////////////
        if (balance > 1 && data < node.LeftChild)
        {
            return RightRotation(node);
        }
        ////////////////////////////////////////////////////////////////////
        // Case II : right heavy situation, we need to left rotate the tree
        // A                                      B
        //  \                                    / \
        //   B                   ->             A   C
        //    \
        //     C
        ////////////////////////////////////////////////////////////////////
        if (balance < -1 && data > node.RightChild)
        {
            return LeftRotation(node);
        }
        ////////////////////////////////////////////////////////////////////
        // Case III : left right heavy situation, we need left rotation, 
        //            and then right notation
        //    C                   C                    B
        //   /                   /                    / \
        //  A           ->      B          ->        A   C
        //   \                 /
        //    B               A
        ////////////////////////////////////////////////////////////////////
        if (balance > 1 && data > node.LeftChild)
        {
            node.LeftChild = LeftRotation(node.LeftChild);
            return RightRotation(node);
        }
        ////////////////////////////////////////////////////////////////////
        // Case IV : right left heavy situation, we need right rotation, 
        //           and then left notation
        //    A                 A                      B
        //     \                 \                    / \
        //      C      ->         B         ->       A   C
        //     /                   \
        //    B                     C
        ////////////////////////////////////////////////////////////////////
        if (balance < -1 && data < node.RightChild)
        {
            node.RightChild = RightRotation(node.RightChild);
            return LeftRotation(node);
        }
        return node;
    }

    public void Dispose()
    {
        Clear();
    }
}
