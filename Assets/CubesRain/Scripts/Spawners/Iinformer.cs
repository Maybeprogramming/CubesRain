using System;

public interface IInformer
{
    event Action<int, int, int> Informing;
}