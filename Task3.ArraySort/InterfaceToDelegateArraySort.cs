using System;
using System.Collections.Generic;

namespace Task3.ArraySort {
    public sealed class InterfaceToDelegateArraySort : ArraySort {

        #region Public Methods 
        public override void Sort<T>(T[] array, IComparer<T> comparator) {
            Sort(array, comparator.Compare);
        }

        public override void Sort<T>(T[] array, Compare<T> compare) {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (compare == null)
                throw new ArgumentNullException(nameof(compare));
            for (int i = array.Length - 1; i > 0; i--) {
                for (int j = 0; j < i; j++) {
                    if (compare(array[j], array[j + 1]) > 0) {
                        Swap(ref array[j], ref array[j + 1]);
                    }
                }
            }
        }
        #endregion
    }
}
