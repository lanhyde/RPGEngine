using System;
using System.Collections;
using System.Collections.Generic;

public enum ColorEnum { RED, BLACK }
public class RBNode<T> : Node<T> where T : IComparable<T>
{
    public ColorEnum Color { get; set; }
    public Node<T> Parent { get; set; }
    public RBNode(T data) : base(data)
    {
        Color = ColorEnum.RED;
    }

    public override string ToString()
    {
        return Data.ToString();
    }
}
