using System;
using System.Collections;

public class Node<T> : IComparable, IComparable<T> where T: IComparable<T>
{
    public T Data { get; set; }
    public Node<T> LeftChild { get; set; }
    public Node<T> RightChild { get; set; }

    public Node(T data)
    {
        this.Data = data;
    }
    public override string ToString()
    {
        return Data.ToString();
    }

    public int CompareTo(object obj)
    {
        return Data.CompareTo((obj as Node<T>).Data);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
    public int CompareTo(T obj)
    {
        return Data.CompareTo(obj);
    }

    public override int GetHashCode()
    {
        return Data.GetHashCode();
    }
    public static bool operator >(Node<T> lhs, Node<T> rhs) 
    {
        return lhs.Data.CompareTo(rhs.Data) > 0;
    }
    public static bool operator >(T lhs, Node<T> rhs)
    {
        return lhs.CompareTo(rhs.Data) > 0;
    }
    public static bool operator >(Node<T> lhs, T rhs)
    {
        return lhs.Data.CompareTo(rhs) > 0;
    }
    public static bool operator <(Node<T> lhs, Node<T> rhs)
    {
        return lhs.Data.CompareTo(rhs.Data) < 0;
    }
    public static bool operator <(T lhs, Node<T> rhs)
    {
        return lhs.CompareTo(rhs.Data) < 0;
    }
    public static bool operator <(Node<T> lhs, T rhs)
    {
        return lhs.Data.CompareTo(rhs) < 0;
    }

    public static bool operator ==(T lhs, Node<T> rhs)
    {
        return lhs.CompareTo(rhs.Data) == 0;
    }
    public static bool operator ==(Node<T> lhs, T rhs)
    {
        return lhs.Data.CompareTo(rhs) == 0;
    }

    public static bool operator !=(T lhs, Node<T> rhs)
    {
        return lhs.CompareTo(rhs.Data) != 0;
    }
    public static bool operator !=(Node<T> lhs, T rhs)
    {
        return lhs.Data.CompareTo(rhs) != 0;
    }
}
