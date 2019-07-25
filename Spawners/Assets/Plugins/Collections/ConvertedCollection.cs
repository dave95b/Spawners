using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;

public readonly struct ConvertedCollection<TInput, TOutput> : IReadOnlyList<TOutput>
{
    private readonly IList<TInput> collection;
    private readonly Func<TInput, TOutput> converter;

    public ConvertedCollection(IList<TInput> collection, Func<TInput, TOutput> converter)
    {
        this.collection = collection;
        this.converter = converter;
    }

    public TOutput this[int i]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => converter(collection[i]);
    }

    public int Count => collection.Count;


    public Enumerator GetEnumerator()
    {
        return new Enumerator(collection, converter);
    }

    IEnumerator<TOutput> IEnumerable<TOutput>.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public struct Enumerator : IEnumerator<TOutput>
    {
        private readonly IList<TInput> collection;
        private readonly Func<TInput, TOutput> converter;
        private int index;

        public Enumerator(IList<TInput> collection, Func<TInput, TOutput> converter) : this()
        {
            this.collection = collection;
            this.converter = converter;
            index = -1;
        }

        public TOutput Current => converter(collection[index]);
        object IEnumerator.Current => converter(collection[index]);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            return EnumeratorsUtility.MoveNext(ref index, collection.Count);
        }

        public void Reset()
        {
            index = -1;
        }

        public void Dispose() { }
    }
}