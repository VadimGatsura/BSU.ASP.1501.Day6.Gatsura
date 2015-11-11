﻿using System;
using System.Collections.Generic;

namespace Task3.ArraySort {

    public sealed class ArraySortAdapter<T> : IComparer<T> {
        private readonly Comparison<T> m_Comparator; 

        public ArraySortAdapter(Comparison<T> comparator) {
            m_Comparator = comparator;
        } 

        public int Compare(T x, T y) {
            if(x != null && y != null)
                return m_Comparator(x, y);
            if (x == null && y == null)
                return 0;
            if (x == null)
                return 1;
            return -1;
        }
    }

    public sealed class DelegateToInterfaceArraySort : ArraySort {
        #region Public Methods 
        public override void Sort<T>(T[] array, IComparer<T> comparator) {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if(comparator == null) {
                if(typeof(T).IsSubclassOf(typeof(IComparable))) {
                    Sort<IComparable>(array);
                } else if(typeof(T).IsSubclassOf(typeof(IComparable<T>))) {
                    Sort<IComparable<T>>(array);
                }
                throw new ArgumentException($"Argument {nameof(comparator)} is null and Generic Type {typeof(T)} don't implement any of interfaces IComparable and IComparable<T>");
            }
                
            for (int i = array.Length - 1; i > 0; i--) {
                for (int j = 0; j < i; j++) {
                    if (comparator.Compare(array[j], array[j + 1]) > 0) {
                        Swap(ref array[j], ref array[j + 1]);
                    }
                }
            }
        }
        #endregion

        #region Private Methods

        private void Sort<T>(Array array) {
            for (int i = array.Length - 1; i > 0; i--) {
                for (int j = 0; j < i; j++) {
                    dynamic comparable = (T) array.GetValue(j);
                    if (comparable != null && comparable.CompareTo(array.GetValue(j + 1)) > 0) {
                        var bufferArray = array.GetValue(j);
                        array.SetValue(array.GetValue(j + 1), j);
                        array.SetValue(bufferArray, j + 1);
                    }
                }
            }
        }
        #endregion
    }
}
