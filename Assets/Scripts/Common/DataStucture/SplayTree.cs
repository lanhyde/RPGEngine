using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplayTree<T> : RBTree<T> where T : IComparable<T>
{
    private int size;

    public bool IsEmpty { get { return root == null; } }
    public int Size { get { return size; } }
    public override void Insert(T data)
    {
        Node<T> tempNode = root;
        Node<T> parentNode = null;

        while(tempNode != null)
        {
            parentNode = tempNode;
            if(tempNode < data)
            {
                tempNode = tempNode.RightChild;
            }
            else
            {
                tempNode = tempNode.LeftChild;
            }
        }

        tempNode = new RBNode<T>(data);
        ((RBNode<T>)tempNode).Parent = parentNode;

        if(parentNode == null)
        {
            this.root = tempNode;
        }
        else if(parentNode < tempNode)
        {
            parentNode.RightChild = tempNode;
        }
        else
        {
            parentNode.LeftChild = tempNode;
        }

        Splay(tempNode);
        size++;
    }

    public Node<T> Find(T data)
    {
        Node<T> tmpNode = root;

        while(tmpNode != null)
        {
            if(tmpNode < data)
            {
                tmpNode = tmpNode.RightChild;
            }
            else if(tmpNode > data)
            {
                tmpNode = tmpNode.LeftChild;
            }
            else
            {
                Splay(tmpNode);
                return tmpNode;
            }
        }
        Splay(tmpNode);
        return null;
    }

    private void Splay(Node<T> node)
    {
        while((node as RBNode<T>).Parent != null)
        {
            // ZIG situation
            if(((node as RBNode<T>).Parent as RBNode<T>).Parent == null)
            {
                if((node as RBNode<T>).LeftChild == node)
                {
                    RightRotation(((RBNode<T>)node).Parent);
                }
                else
                {
                    LeftRotation(((RBNode<T>)node).Parent);
                }
            }
            // ZIG-ZIG situation
            else if(((RBNode<T>)node).Parent.LeftChild == node &&
                    (((RBNode<T>)node).Parent as RBNode<T>).Parent.LeftChild == ((RBNode<T>)node).Parent)
            {
                RightRotation((((RBNode<T>)node).Parent as RBNode<T>).Parent);
                RightRotation(((RBNode<T>)node).Parent);
            }
            else if (((RBNode<T>)node).Parent.RightChild == node &&
                    (((RBNode<T>)node).Parent as RBNode<T>).Parent.RightChild== ((RBNode<T>)node).Parent)
            {
                LeftRotation((((RBNode<T>)node).Parent as RBNode<T>).Parent);
                LeftRotation(((RBNode<T>)node).Parent);
            }
            // ZIG-ZAG situation
            else if (((RBNode<T>)node).Parent.LeftChild == node &&
                    (((RBNode<T>)node).Parent as RBNode<T>).Parent.RightChild == ((RBNode<T>)node).Parent)
            {
                RightRotation(((RBNode<T>)node).Parent);
                LeftRotation(((RBNode<T>)node).Parent);
            }
            else
            {
                LeftRotation(((RBNode<T>)node).Parent);
                RightRotation(((RBNode<T>)node).Parent);
            }
        }
    }
}
