using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBTree<T> : BinarySearchTree<T> where T : IComparable<T>
{
    public override void Insert(T data)
    {
        RBNode<T> node = new RBNode<T>(data);
        root = InsertNode(node, root) as RBNode<T>;

        FixViolation(node);
    }

    private Node<T> InsertNode(Node<T> node, Node<T> root)
    {
        if(root == null)
        {
            return node;
        }

        if(node < root)
        {
            root.LeftChild = InsertNode(node, root.LeftChild) as RBNode<T>;
            ((RBNode<T>)root.LeftChild).Parent = root;
        }
        else if(node > root)
        {
            root.RightChild = InsertNode(node, root.RightChild) as RBNode<T>;
            ((RBNode<T>)root.RightChild).Parent = root;
        }

        return root;
    }

    protected void LeftRotation(Node<T> node)
    {
        Node<T> tempRightNode = node.RightChild;
        node.RightChild = tempRightNode.LeftChild;

        if (node.RightChild != null)
        {
            (node.RightChild as RBNode<T>).Parent = node;
        }

        (tempRightNode as RBNode<T>).Parent = (node as RBNode<T>).Parent;

        if ((tempRightNode as RBNode<T>).Parent == null)
        {
            root = tempRightNode as RBNode<T>;
        }
        else if (node == ((RBNode<T>)node).Parent.LeftChild)
        {
            ((RBNode<T>)node).Parent.LeftChild = tempRightNode;
        }
        else
        {
            ((RBNode<T>)node).Parent.RightChild = tempRightNode;
        }
        tempRightNode.LeftChild = node;
        ((RBNode<T>)node).Parent = tempRightNode;

    }

    protected void RightRotation(Node<T> node)
    {
        Node<T> tempLeftNode = node.LeftChild;
        node.LeftChild = tempLeftNode.RightChild;

        if(node.LeftChild != null)
        {
            (node.LeftChild as RBNode<T>).Parent = node;
        }

        (tempLeftNode as RBNode<T>).Parent = (node as RBNode<T>).Parent;

        if((tempLeftNode as RBNode<T>).Parent == null)
        {
            root = tempLeftNode as RBNode<T>;
        }
        else if(node == ((RBNode<T>)node).Parent.LeftChild)
        {
            ((RBNode<T>)node).Parent.LeftChild = tempLeftNode;
        }
        else
        {
            ((RBNode<T>)node).Parent.RightChild = tempLeftNode;
        }
        tempLeftNode.RightChild = node;
        ((RBNode<T>)node).Parent = tempLeftNode;
    }

    private void FixViolation(Node<T> node)
    {
        Node<T> parentNode = null;
        Node<T> grandParentNode = null;

        while(node != root && ((RBNode<T>)node).Color != ColorEnum.BLACK && 
              (((RBNode<T>)node).Parent as RBNode<T>).Color == ColorEnum.RED)
        {
            parentNode = (node as RBNode<T>).Parent;
            grandParentNode = (parentNode as RBNode<T>).Parent;

            if(parentNode == grandParentNode.LeftChild)
            {
                Node<T> uncle = grandParentNode.RightChild;

                if(uncle != null && (uncle as RBNode<T>).Color == ColorEnum.RED)
                {
                    (grandParentNode as RBNode<T>).Color = ColorEnum.RED;
                    (parentNode as RBNode<T>).Color = ColorEnum.BLACK;
                    (uncle as RBNode<T>).Color = ColorEnum.BLACK;
                    node = grandParentNode;
                }
                else
                {
                    if(node == parentNode.RightChild)
                    {
                        LeftRotation(parentNode);
                        node = parentNode;
                        parentNode = (node as RBNode<T>).Parent;
                    }
                    RightRotation(grandParentNode);
                    ColorEnum tempColor = (parentNode as RBNode<T>).Color;
                    (parentNode as RBNode<T>).Color = (grandParentNode as RBNode<T>).Color;
                    (grandParentNode as RBNode<T>).Color = tempColor;
                    node = parentNode;
                }
            }
            else
            {
                Node<T> uncle = grandParentNode.LeftChild;
                if(uncle != null && (uncle as RBNode<T>).Color == ColorEnum.RED)
                {
                    (grandParentNode as RBNode<T>).Color = ColorEnum.RED;
                    (parentNode as RBNode<T>).Color = ColorEnum.BLACK;
                    (uncle as RBNode<T>).Color = ColorEnum.BLACK;
                    node = grandParentNode;
                }
                else
                {
                    if(node == parentNode.LeftChild)
                    {
                        RightRotation(parentNode);
                        node = parentNode;
                        parentNode = (node as RBNode<T>).Parent;
                    }
                    LeftRotation(grandParentNode);
                    ColorEnum tempColor = (parentNode as RBNode<T>).Color;
                    (parentNode as RBNode<T>).Color = (grandParentNode as RBNode<T>).Color;
                    (grandParentNode as RBNode<T>).Color = tempColor;
                    node = parentNode;
                }
            }
        }

        if((root as RBNode<T>).Color == ColorEnum.RED)
        {
            (root as RBNode<T>).Color = ColorEnum.BLACK;
        }
    }
}
