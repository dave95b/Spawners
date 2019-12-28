﻿using System;
using System.Collections.Generic;

using Random = UnityEngine.Random;

public static class CollectionExtensions
{

    #region IList

    public static void Shuffle<T>(this IList<T> list)
    {
        int count = list.Count;
        var last = count - 1;
        for (int i = 0; i < last; ++i)
        {
            int randomIndex = Random.Range(i, count);
            T tmp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = tmp;
        }
    }

    public static T GetRandom<T>(this IList<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static bool IsEmpty<T>(this IList<T> list)
    {
        return list.Count == 0;
    }

    public static void Swap<T>(this IList<T> list, int sourceIndex, int destinationIndex)
    {
        if (sourceIndex == destinationIndex)
            return;

        T temp = list[sourceIndex];
        list[sourceIndex] = list[destinationIndex];
        list[destinationIndex] = temp;
    }

    public static void RemoveAtSwapback<T>(this IList<T> list, int index)
    {
        int lastIndex = list.Count - 1;
        list[index] = list[lastIndex];
        list.RemoveAt(lastIndex);
    }

    #endregion IList

    #region Arrays

    public static void Shuffle<T>(this T[] array)
    {
        int count = array.Length;
        var last = count - 1;
        for (int i = 0; i < last; ++i)
        {
            int randomIndex = Random.Range(i, count);
            T tmp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = tmp;
        }
    }

    public static T GetRandom<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public static bool IsEmpty<T>(this T[] array)
    {
        if (array.Length == 0) return true;

        for (int i = 0; i < array.Length; i++)
            if (array[i] != null)
                return false;

        return true;
    }

    public static void Clear<T>(this T[] array)
    {
        for (int i = 0; i < array.Length; i++)
            array[i] = default(T);
    }

    public static void Swap<T>(this T[] array, int sourceIndex, int destinationIndex)
    {
        if (sourceIndex == destinationIndex)
            return;

        T temp = array[sourceIndex];
        array[sourceIndex] = array[destinationIndex];
        array[destinationIndex] = temp;
    }

    public static bool Contains<T>(this T[] array, T item)
    {
        return Array.IndexOf(array, item) >= 0;
    }

    #endregion Arrays
}
