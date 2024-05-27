using System;
using System.Collections.Generic;

namespace verell.Architecture
{
    public interface IContainer
    {
        string Name   { get; }
        bool IsActive { get; }

        T Get<T>() where T : ISharedInterface;
        object Get(Type type);
        IReadOnlyList<T> GetAll<T>();
        void InjectAt(object target);
    }
}