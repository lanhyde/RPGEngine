using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinarySearchTree<T> : ITree<T> where T : IComparable<T>
{
    private Node<T> root;
    private List<T> cachedBuffer = new List<T>();
    public void Delete(T data)
    {
        if(root != null)
        {
            root = Delete(root, data);
        }
    }

    private Node<T> Delete(Node<T> root, T data)
    {
        if (root == null)
            return root;
        if(data < root)
        {
            root.LeftChild = Delete(root.LeftChild, data);
        }
        else if(data > root)
        {
            root.RightChild = Delete(root.RightChild, data);
        }
        else
        {
            if (root.LeftChild == null && root.RightChild == null)
                return null;
            if(root.LeftChild == null)
            {
                Node<T> tempNode = root.RightChild;
                root = null;
                return tempNode;
            }
            else if(root.RightChild == null)
            {
                Node<T> tempNode = root.LeftChild;
                root = null;
                return tempNode;
            }
            Node<T> node = GetPredecessor(root.LeftChild);
            root.Data = node.Data;
            root.LeftChild = Delete(root.LeftChild, node.Data);
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
        if(node.LeftChild != null)
        {
            return GetMinValue(node.LeftChild);
        }
        return node.Data;
    }

    public T GetMaxValue(Node<T> node)
    {
        if(node.RightChild != null)
        {
            return GetMaxValue(node.RightChild);
        }
        return node.Data;
    }

    public void Insert(T data)
    {
        if(root == null)
        {
            root = new Node<T>(data);
        }
        else
        {
            InsertNode(data, root);
        }
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
                PreOrderTraverse(root);
                break;
            case TraverseType.InOrder:
                InOrderTraverse(root);
                break;
            case TraverseType.PostOrder:
                PostOrderTraverse(root);
                break;
        }
        T[] result = cachedBuffer.ToArray();
        ClearCache();
        return result;
    }

    private void PostOrderTraverse(Node<T> root)
    {
        if (root == null)
            return;
        if (root.LeftChild != null)
        {
            InOrderTraverse(root.LeftChild);
        }
        if (root.RightChild != null)
        {
            InOrderTraverse(root.RightChild);
        }
        cachedBuffer.Add(root.Data);
    }
    private void InOrderTraverse(Node<T> root)
    {
        if (root == null)
            return;
        if(root.LeftChild != null)
        {
            InOrderTraverse(root.LeftChild);
        }
        cachedBuffer.Add(root.Data);
        if(root.RightChild != null)
        {
            InOrderTraverse(root.RightChild);
        }
    }

    private void PreOrderTraverse(Node<T> root)
    {
        if (root == null)
            return;
        cachedBuffer.Add(root.Data);
        if (root.LeftChild != null)
        {
            InOrderTraverse(root.LeftChild);
        }
        if (root.RightChild != null)
        {
            InOrderTraverse(root.RightChild);
        }
    }

    private void InsertNode(T data, Node<T> node)
    {
        if(data < node)
        {
            if(node.LeftChild != null)
            {
                InsertNode(data, node.LeftChild);
            }
            else
            {
                Node<T> newNode = new Node<T>(data);
                node.LeftChild = newNode;
            }
        }
        else
        {
            if(node.RightChild != null)
            {
                InsertNode(data, node.RightChild);
            }
            else
            {
                Node<T> newNode = new Node<T>(data);
                node.RightChild = newNode;
            }
        }
    }
}
