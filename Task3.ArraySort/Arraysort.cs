using System;
using System.Collections.Generic;

namespace Task3.ArraySort {
    /// <summary>Sorting the array</summary>
    public abstract class ArraySort {
        #region Public Methods 

        /// <summary>
        /// Sort array by the <see cref="IComparer{T}"/>
        /// <remarks>null elements go to the end of array</remarks> 
        /// </summary>
        /// <typeparam name="T">The type of array elements</typeparam>
        /// <param name="array">Array for sorting</param>
        /// <param name="comparator">Interface, which allow compare two elements</param>
        public abstract void Sort<T>(T[] array, IComparer<T> comparator);
        #endregion

        #region Protected Methods
        protected void Swap<T>(ref T firstArray, ref T secondArray) {
            var bufferArray = firstArray;
            firstArray = secondArray;
            secondArray = bufferArray;
        }
        #endregion
    }
}
