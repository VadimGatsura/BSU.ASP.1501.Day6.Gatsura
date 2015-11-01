using System;
using System.Collections.Generic;

namespace Task3.ArraySort {
    public sealed class DelegateToInterfaceArraySort : ArraySort {
        #region Public Methods 
        public override void Sort<T>(T[] array, IComparer<T> comparator) {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (comparator == null)
                throw new ArgumentNullException(nameof(comparator));

            for (int i = array.Length - 1; i > 0; i--) {
                for (int j = 0; j < i; j++) {
                    if (comparator.Compare(array[j], array[j + 1]) > 0) {
                        Swap(ref array[j], ref array[j + 1]);
                    }
                }
            }
        }

        public override void Sort<T>(T[] array, Compare<T> compare) {
            IComparer<T> comparer = compare.Target as IComparer<T>;
            if(comparer != null) 
                Sort(array, comparer);
            else
                throw new ArgumentException($"Target of {nameof(compare)} method don't implementing {nameof(IComparer<T>)} interface");
        }
        #endregion
    }
}
