using System;
using System.Collections.Generic;

namespace Task3.ArraySort {
    /// <summary>Sorting the array</summary>
    public abstract class ArraySort {
        /// <summary>compare two elements</summary>
        /// <param name="firstElement">The first element for comparison</param>
        /// <param name="secondElement">The second element for comparison</param>
        /// <returns></returns>
        public delegate int Compare<T>(T firstElement, T secondElement);

        #region Public Methods 

        /// <summary>
        /// Sort array by the <see cref="IComparer{T}"/>
        /// <remarks>null elements go to the end of array</remarks> 
        /// </summary>
        /// <typeparam name="T">The type of array elements</typeparam>
        /// <param name="array">Array for sorting</param>
        /// <param name="comparator">Interface, which allow compare two elements</param>
        public abstract void Sort<T>(T[] array, IComparer<T> comparator);

        /// <summary>
        /// Sort array by the <see cref="Compare{T}"/> method
        /// <remarks>null elements go to the end of array</remarks>
        /// </summary>
        /// <param name="array">Array for sorting</param>
        /// <param name="compare">Method, which allow compare two elements</param>
        public abstract void Sort<T>(T[] array, Compare<T> compare);
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
