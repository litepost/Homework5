/*
 * Author: Jeff Leupold
 * Homework 5
 * Due date: 2021-05-13
 * Source: My code was inspired by Kevin Lundeen from data structures class in fall 2020
 */
using System;
using System.Collections.Generic;

namespace HeapSort {

    public class MaxHeap {
        //insert is O(n); Add is O(1) if already have the capacity, otherwise O(n)
        //element retrieval is O(1)
        private List<int> data; 
        private const int ROOT = 0;     //for readability

        public MaxHeap(int[] input) {
            data = new List<int>(input);
            heapify();
        }

        public static List<int> Sort(int[] input, bool Ascending) {
            //performs a heap sort
            //better than insertion sort but not as good as a merge sort or quick sort
            //however, it uses less memory
            MaxHeap temp = new MaxHeap(input);
            List<int> output = new List<int>(temp.data);
            if (Ascending) {
                for (int i = output.Count-1; i >= ROOT; i--) {
                    output[i] = temp.dequeue();
                }
            }
            else {
                for (int i = 0; i < output.Count; i++) {
                    output[i] = temp.dequeue();
                }
            }
            return output;
        }

        private void heapify() {
            //transforms an array into a heap
            for (int i = (data.Count - 1) / 2; i >=ROOT; i--) {
                percolateDown(i);
            }
        }

        private int dequeue() {
            //swap the root with the last element
            //then remove the last element/old root
            //re-heapify
            //return old root
            if (data.Count == 0)
                throw new InvalidOperationException("The root cannot be removed because the array is empty");

            int oldRoot = data[ROOT];
            swap(ROOT, data.Count - 1);
            data.RemoveAt(data.Count - 1);
            percolateDown(ROOT);
            return oldRoot;
        }

        private void percolateDown(int index) {
            if (hasLeft(index)) {
                int childIndex = leftChild(index);
                if (hasRight(index)) {
                    int rightChildIndex = rightChild(index);
                    //find the larger payload of the children
                    if (data[childIndex] < data[rightChildIndex]) {
                        childIndex = rightChildIndex;
                    }
                }
                if (data[childIndex] > data[index]) {
                    swap(childIndex, index);
                    //recursively continue to place the value in its proper place
                    percolateDown(childIndex);
                }
            }
        }

        private bool hasLeft(int index) {
            //if the would-be index of the left child is within range, then true
            return leftChild(index) < this.data.Count;
        }

        private bool hasRight(int index) {
            //if the would-be index of the right child is within range, then true
            return rightChild(index) < this.data.Count;
        }

        private int leftChild(int parentIndex) {
            //property of binary tree
            return parentIndex * 2 + 1;
        }

        private int rightChild(int parentIndex) {
            //property of binary tree
            return leftChild(parentIndex) + 1;
        }

        private void swap(int indexA, int indexB) {
            if (indexA >= data.Count || indexB >= data.Count)
                throw new ArgumentOutOfRangeException($"Either indexA or indexB are out of range: [{indexA}, {indexB}]");

            int temp = data[indexA];
            data[indexA] = data[indexB];
            data[indexB] = temp;
        }

        public override string ToString() {
            if (data.Count == 0)
                return "[]";

            string output = "";
            for (int i = 0; i < data.Count; i++) {
                output += data[i] + ", ";
            }
            return "[" + output.Substring(0, output.Length - 2) + "]";
        }
    }

    class Program {
        static void Main(string[] args) {
            int[] input = new int[] { };    //empty array
            int[] input2 = new int[] { 0 }; //root only
            int[] input3 = new int[] { 0, 33 }; //need to heapify
            int[] input4 = new int[] { 33, 0 }; //no need to heapify
            //3 complete rows & contains negative numbers
            //each parent has 2 children so it's a clean test
            int[] input5 = new int[] { 9, 44, -1, 23, 99, 0, -12 }; 
            //one less than 3 complete rows
            //testing how it handles an incomplete row & heapify loop condition that calcs last parent
            int[] input6 = new int[] { 9, -1, 23, 99, 0, -12 }; 
            //one more than 3 complete rows & has a repeating number
            //Again, testing heapify loop condition
            int[] input7 = new int[] { 8, 7, 34, 123, -3, 33, 34, 9 };  
            //all of the same number
            int[] input8 = new int[] { 0, 0, 0, 0 };    

            List<int[]> testCases = new List<int[]>();
            testCases.Add(input);
            testCases.Add(input2);
            testCases.Add(input3);
            testCases.Add(input4);
            testCases.Add(input5);
            testCases.Add(input6);
            testCases.Add(input7);
            testCases.Add(input8);

            foreach (int[] arr in testCases) {
                Console.WriteLine("Pre-sorted: " + printArray(arr));

                List<int> sortedAscending = MaxHeap.Sort(arr, true);
                Console.WriteLine("Sorted Ascending: " + printList(sortedAscending));

                List<int> sortedDescending = MaxHeap.Sort(arr, false);
                Console.WriteLine("Sorted Descending: " + printList(sortedDescending));
                Console.WriteLine("-----------------------");
            }
        }

        public static string printArray(int[] arr) {
            if (arr.Length == 0)
                return "[]";

            string msg = "";
            for (int i = 0; i < arr.Length; i++) {
                msg += $"{arr[i]}, ";
            }
            return "[" + msg.Substring(0, msg.Length - 2) + "]";
        }

        public static string printList(List<int> input) {
            if (input.Count == 0)
                return "[]";

            string msg = "";
            foreach (int i in input) {
                msg += $"{i}, ";
            }
            return "[" + msg.Substring(0, msg.Length - 2) + "]";
        }

    }
}
