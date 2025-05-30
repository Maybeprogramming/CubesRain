using System;

public interface Iinformer
{
    event Action<int, int, int> Informing;
}