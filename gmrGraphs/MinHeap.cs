using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmrGraphs
{
    public class MinHeap<T> where T : IComparable
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

            int valuesIndex = 0;
            while (Root[valuesIndex].CompareTo(value) != 0)
            {
                valuesIndex++;
            }

            if (valuesIndex > 0)
            {
                while (Parent(valuesIndex).CompareTo(value) > 0)
                {
                    HeapifyUp(value);

                    valuesIndex = 0;
                    while (Root[valuesIndex].CompareTo(value) != 0)
                    {
                        valuesIndex++;
                    }
                }
            }

        }

        public void HeapifyUp(T value)
        {
            int valuesIndex = 0;
            while (Root[valuesIndex].CompareTo(value) != 0)
            {
                valuesIndex++;
            }

            if (Parent(valuesIndex).CompareTo(value) > 0)
            {
                //switching value and its parent
                Root[valuesIndex] = Parent(valuesIndex);
                Root[(valuesIndex - 1) / 2] = value;
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
            if (Size > 1)
            {
                int index = 0;
                return Root[0];
                HeapifyDown(index);
            }
            Size--;
            return Root[0];
        }

        public void HeapifyDown(int index)
        {
            if (((index * 2) + 1) > Size && ((index * 2) + 1) > Size)
            {
                //there is no children = it is at the bottom of the heap
                return;
            }
            else if (((index * 2) + 2) >= Size)
            {
                //there is no RChild so do not swap with it = swap with LChild
                T temp = Root[index];
                Root[index] = LChild(index);
                Root[(index * 2) + 1] = temp;
                return;
            }
            else if (LChild(index).CompareTo(RChild(index)) > 0)
            {
                //LChild's value is greater = RChild's value is less
                //want to swap with the RChild
                T temp = Root[index];
                Root[index] = RChild(index);
                Root[(index * 2) + 2] = temp;
                index = (index * 2) + 2;
                HeapifyDown(index);
            }
            else if (LChild(index).CompareTo(RChild(index)) < 0)
            {
                //want to swap with the LChild
                //if it swaps with the LChild, there will then be no LChild and just a RChild
                Root[index] = LChild(index);
                Root[(index * 2) + 1] = Root[(index * 2) + 2];
                index = (index * 2) + 1;
                HeapifyDown(index);
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
