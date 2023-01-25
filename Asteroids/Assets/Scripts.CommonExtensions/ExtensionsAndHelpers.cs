using System;
using System.Collections.Generic;
using Scripts.Reactive;
using UnityEngine;

namespace Scripts.CommonExtensions
{
    public static class ExtensionsAndHelpers
    {
        public static System.Random Random = new System.Random();

        public static T GetOneRandom<T>(T[] values)
        {
            if (values.Length == 0)
                return default;

            var index = Random.Next(0, values.Length);
            return values[index];
        }
        
        public static void Subscribe<T>(this Component gameObject, IReactiveValue<T> reactiveValue,
            Action<T> action, ref Action  onDestroy)
        {
            void OnChanged(T value)
            {
                action?.Invoke(value);
            }

            reactiveValue.OnChanged += OnChanged;
            onDestroy += () => { reactiveValue.OnChanged -= OnChanged; };
        }

        public static T FirstIfAny<T>(this IList<T> collection)
        {
            if (collection.Count == 0)
                return default;

            return collection[0];
        }

        public static T RemoveAtAndReturn<T>(this IList<T> collection, int index)
        {
            var element = collection[index];
            collection.RemoveAt(index);
            return element;
        }

        public static void SetActiveOptimized(this GameObject gm, bool status)
        {
            if (gm is null)
                return;
            var gameObject = gm.gameObject;
            if (gameObject.activeSelf != status)
                gameObject.SetActive(status);
        }
    }
}