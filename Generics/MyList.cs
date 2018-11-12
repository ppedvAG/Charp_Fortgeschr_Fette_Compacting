using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    public class MyList<ArrayType> : IEnumerable
    {
        //ctor


        public MyList(int maxCount)
        {

        }

        private ArrayType[] _internesArray = new ArrayType[0];
        private int size = 0;

        public void Add(ArrayType item)
        {
            ArrayType[] newArray = new ArrayType[size + 1];
            if (size > 0)
            {
                Array.Copy(_internesArray, newArray, size);
            }
            newArray[size] = item;
            _internesArray = newArray;
            size++;
        }

        //Bonus
        public void Remove(ArrayType item)
        {
            if (_internesArray.Contains(item))
            {


                ArrayType[] newArray = new ArrayType[size - 1];
                int indexNewArray = 0;
                for (int i = 0; i < _internesArray.Length; i++)
                {
                    //Vorsicht
                    if (!_internesArray[i].Equals(item))
                    {
                        newArray[indexNewArray] = _internesArray[i];
                        indexNewArray++;
                    }
                }
                _internesArray = newArray;
            }
        }



        //Gemeinsam: Indexer (z.B. liste[0])
        public ArrayType this[int index]
        {
            get
            {
                return _internesArray[index];
            }
            set
            {
                _internesArray[index] = value;
            }
        }

        //Gemeinsam: ForEach-Zugriff ermöglichen
        public IEnumerator GetEnumerator()
        {
            //yield return size;
            //foreach (var item in _internesArray)
            //{
            //    yield return item;
            //}

            return _internesArray.GetEnumerator();
        }
    }
}