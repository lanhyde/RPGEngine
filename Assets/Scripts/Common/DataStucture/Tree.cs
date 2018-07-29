public interface ITree<T> 
{
    T[] Traverse(TraverseType traverseType);
    void Insert(T data);
    void Delete(T data);
    T GetMax();
    T GetMin();
    bool Contains(T data);
}

public enum TraverseType
{
    PreOrder, InOrder, PostOrder
}