﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmrGraphs
{
    public class MinHeap<T> where T : IComparable<T>
    {
        //min heap 
        public T[] Root;
        public int Size = 0;

        public MinHeap()
        {
            //set the array to have 30 slots so we don't have to constantly resize
            Root = new T[30];
        }

        public T Parent(int index)
        {
            return Root[(index - 1) / 2];
        }

        public void Add(T value)
        {
            Size++;
            if (Size >= Root.Length)
            {
                //double the slots in the array when we reach that amount
                Resize(Root.Length * 2);
            }
            //adding value to the next available slot
            Root[Size - 1] = value;

            HeapifyUp(Size - 1);
        }

        public void HeapifyUp(int index)
        {
            if (index == 0) return;

            if (Root[index].CompareTo(Parent(index)) < 0)
            {
                int p = (index - 1) / 2;
                var temp = Root[index];
                Root[index] = Root[p];
                Root[p] = temp;

                HeapifyUp(p);
            }
        }

        public T LChild(int index)
        {
            return Root[(index * 2) + 1];
        }

        public T RChild(int index)
        {
            return Root[(index * 2) + 2];
        }

        public T Pop()
        {
            var ret = Root[0];
            Root[0] = Root[Size - 1];
            Size--;
            HeapifyDown(0);
            return ret;
        }

        public void HeapifyDown(int index)
        {
            int smallerIndex = LChild(index).CompareTo(RChild(index)) < 0 ? (index * 2) + 1 : (index * 2) + 2;

            if (Root[index].CompareTo(Root[smallerIndex]) < 0)
            {
                var temp = Root[index];
                Root[index] = Root[smallerIndex];
                Root[smallerIndex] = temp;
                HeapifyDown(smallerIndex);
            }
        }


        private void Resize(int size)
        {
            T[] temp = new T[size];
            if (size > 1)
            {
                for (int i = 0; i < size; i++)
                {
                    temp[i] = Root[i];
                }
            }
            Root = temp;
        }

        public bool Contains(T value)
        {
            foreach (var item in Root)
            {
                if (item == null)
                {
                    return false;
                }
                else
                {
                    if (item.CompareTo(value) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

}
