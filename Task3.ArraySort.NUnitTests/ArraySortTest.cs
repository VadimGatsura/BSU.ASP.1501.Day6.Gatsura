using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Task3.ArraySort.NUnitTests {
    [TestFixture]
    public class ArraySortTest {
        //RowSum: 16, 0, null, null, 27, 26
        //MaxAbsElement: 10, 0, null, null, 8, 95
        readonly int[][] m_Array = { new[] { 4, 2, -1, 9, -8, 10 }, new[] { 0 }, null, null, new[] { 4, 3, 8, 5, 7 }, new[] { 45, 7, -95, 21, -1, 0, 37, 12 } };

        public IEnumerable<TestCaseData> TestInterfaceDatas {
            get {
                yield return new TestCaseData(new[] { new[] { 0 }, new[] { 4, 2, -1, 9, -8, 10 }, new[] { 45, 7, -95, 21, -1, 0, 37, 12 }, new[] { 4, 3, 8, 5, 7 }, null, null }, new SumRowComparator(), new InterfaceToDelegateArraySort());
                yield return new TestCaseData(new[] { new[] { 0 }, new[] { 4, 3, 8, 5, 7 }, new[] { 4, 2, -1, 9, -8, 10 }, new[] { 45, 7, -95, 21, -1, 0, 37, 12 }, null, null }, new MaxAbsElementComparator(), new InterfaceToDelegateArraySort());
            }
        }

        [Test, TestCaseSource(nameof(TestInterfaceDatas))]
        public void SortArray_InterfaceToDelegate_Test(int[][] sortedArray, IComparer<int[]> comparator, ArraySort sort) {
            sort.Sort(m_Array, comparator);

            CollectionAssert.AreEqual(m_Array, sortedArray);
        }

        public IEnumerable<TestCaseData> TestDelegateDatas {
            get {
                yield return new TestCaseData(new[] { new[] { 0 }, new[] { 4, 2, -1, 9, -8, 10 }, new[] { 45, 7, -95, 21, -1, 0, 37, 12 }, new[] { 4, 3, 8, 5, 7 }, null, null }, new SumRowComparator(), new DelegateToInterfaceArraySort());
                yield return new TestCaseData(new[] { new[] { 0 }, new[] { 4, 3, 8, 5, 7 }, new[] { 4, 2, -1, 9, -8, 10 }, new[] { 45, 7, -95, 21, -1, 0, 37, 12 }, null, null }, new MaxAbsElementComparator(), new DelegateToInterfaceArraySort());
                yield return new TestCaseData(new[] { new[] { 0 }, new[] { 4, 3, 8, 5, 7 }, new[] { 4, 2, -1, 9, -8, 10 }, new[] { 45, 7, -95, 21, -1, 0, 37, 12 }, null, null }, null, new DelegateToInterfaceArraySort()).Throws(typeof(ArgumentException));
            }
        }

        [Test, TestCaseSource(nameof(TestDelegateDatas))]
        public void SortArray_DelegateToInterface_Test(int[][] sortedArray, IComparer<int[]> comparator, ArraySort sort) {
            if(comparator == null) {
                sort.Sort(m_Array, new RowComparator().Compare);
            } else
                sort.Sort(m_Array, comparator.Compare);

            CollectionAssert.AreEqual(m_Array, sortedArray);
        }
    }
}
