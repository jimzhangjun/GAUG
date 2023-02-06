//=================================================================================================
//  Project:    SIPRO-library
//  Module:     MovingAverageClass.cs                                                                         
//  Author:     Andrew Powell
//  Date:       02/12/2010
//  
//  Details:    Provides a moving average FIFO facility
//
//=================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GAUGlib
{
    public class CircularList<T> : IEnumerable<T>, IEnumerator<T>
    {
        protected T[] items;
        protected int idx;
        protected bool loaded;
        protected int enumIdx;

        //-- Constructor that initializes the list with the required number of items
        public CircularList(int numItems)
        {
            if (numItems <= 0)
            {
                throw new ArgumentOutOfRangeException("numItems can't be negative or 0.");                   
            }

            items = new T[numItems];
            idx = 0;
            loaded = false;
            enumIdx = -1;
        }

        //-- Gets/sets the item value at the current index.
        public T Value
        {
            get { return items[idx]; }
            set { items[idx] = value; }
        }

        //-- Returns the count of the number of loaded items
        public int Count
        {
            get { return loaded ? items.Length : idx; }
        }

        //-- Returns the length of the items array.
        public int Length
        {
            get { return items.Length; }
        }

        //-- Gets/sets the value at the specified index.
        public T this[int index]
        {
            get
            {
                RangeCheck(index);
                return items[index];
            }
            set
            {
                RangeCheck(index);
                items[index] = value;
            }
        }

        //-- Advances to the next item or wraps to the first item.
        public void Next()
        {
            if (++idx == items.Length)
            {
                idx = 0;
                loaded = true;
            }
        }

        //- Clears the list
        public void Clear()
        {
            idx = 0;
            items.Initialize();
            loaded = false;
        }

        //-- Sets all items in the list to the specified value
        public void SetAll(T val)
        {
            idx = 0;
            loaded = true;

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = val;
            }
        }

        //-- Internal indexer range check helper
        protected void RangeCheck(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("Indexer cannot be less than 0.");                   
            }

            if (index >= items.Length)
            {
                throw new ArgumentOutOfRangeException("Indexer cannot be greater than or equal to the number of items in the collection.");
            }
        }

        //-- Interface implementations

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public T Current
        {
            get { return this[enumIdx]; }
        }

        public void Dispose()
        {
        }

        object IEnumerator.Current
        {
            get { return this[enumIdx]; }
        }

        public bool MoveNext()
        {
            ++enumIdx;
            bool ret = enumIdx < Count;

            if (!ret)
            {
                Reset();
            }

            return ret;
        }

        public void Reset()
        {
            enumIdx = -1;
        }
    }


    public class MovingAverageClass: IMovingAverage
    {
        CircularList<float> samples;
        protected float total;

        //-- Get the average for the current number of samples.
        public float Average
        {
            get
            {
                if (samples.Count == 0)
                {
                    throw new ApplicationException("Number of samples is 0.");
                }

                return total / samples.Count;
            }
        }

        //-- Constructor initialize the sample size to the specified number.
        public MovingAverageClass(int numSamples)
        {
            if (numSamples <= 0)
            {
                throw new ArgumentOutOfRangeException(
                     "numSamples can't be negative or 0.");
            }

            samples = new CircularList<float>(numSamples);
            total = 0;
        }

        //-- Adds a sample to the sample collection.
        public void AddSample(float val)
        {
            if (samples.Count == samples.Length)
            {
                total -= samples.Value;
            }

            samples.Value = val;
            total += val;
            samples.Next();
        }

        //-- Clears all samples to 0
        public void ClearSamples()
        {
            total = 0;
            samples.Clear();
        }

        //-- Initializes all samples to the specified value
        public void InitializeSamples(float v)
        {
            samples.SetAll(v);
            total = v * samples.Length;
        }
    }

    public interface IMovingAverage
    {
        float Average { get;}

        void AddSample(float val);
        void ClearSamples();
        void InitializeSamples(float val);
    }
    //=============================================================================================
}
