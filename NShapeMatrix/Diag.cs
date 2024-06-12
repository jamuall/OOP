using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Collections;


namespace NShapeMatrix
{
    public class Diag
    {
        #region Exceptions
        public class NegativeSizeException : Exception { };
        public class InvalidIndexException : Exception { };
        public class OutOfShapeException : Exception { };
        public class SizeMismatchException : Exception { };
        #endregion

        #region Attribute
        private List<int> list = new(); // values of matrix only nonzeros
        private readonly int n; // size
        #endregion

        #region Constructor
        public Diag(int size)
        {
            if (size <= 0) throw new NegativeSizeException();

            n = size;
            int length = 3 * n - 2;
            for (int i = 0; i < length; i++)
            {
                list.Add(0);
            }
        }
        #endregion

        #region Getters and Setters
        public int Size()
        {
            return n;
        }

        private int CalculateIndex(int row, int col)
        {
            if (row < 0 || row >= n || col < 0 || col >= n)
            {
                throw new InvalidIndexException();
            }

            if (col == 0)
            {
                return row;
            }
            else if (col == n - 1)
            {
                return 2 * n + row - 2;
            }
            else if (row == col)
            {
                return n + row - 1;
            }
            else
            {
                throw new OutOfShapeException();
            }
        }
        
        public bool InNmatrix(int row, int col)
        {
            return col == 0 || col == n - 1 || row == col;
        }

        public int GetElem(int row, int col)
        {
            if (InNmatrix(row, col))
            {
                int index = CalculateIndex(row, col);
                return list[index];
            }
            else if (row >= 0 && col >= 0 && row < n && col < n)
            {
                return 0;
            }
            else
            {
                throw new InvalidIndexException();
            }
        }

        public void SetElem(int row, int col, int val)
        {
            if (InNmatrix(row, col))
            {
                int index = CalculateIndex(row, col);
                list[index] = val;
                //Set(list);
            }
            else if (!InNmatrix(row, col))
            {
                throw new OutOfShapeException();
            }
            else
            {
                throw new InvalidIndexException();
            }
        }

        public override string ToString()
        {
            List<List<int>> matrix = ConvertToList(this);

            string str = "";
            foreach (var row in matrix)
            {
                foreach (var element in row)
                {
                    str += element.ToString() + "\t";
                }
                str += "\n";
            }

            return str;
        }

        public string ToStringAfterMul()
        {
            string str = "";
            int index = 0;
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (InNmatrix(i, j))
                    {
                        str += "\t" + list[index];
                        index++;
                    }
                    else
                    {
                        str += "\t" + 0;
                    }
                }
                str += "\n";
            }
            return str;
        }


        public void Set(List<int> x)
        {
            int length = 3 * n - 2;
            if (length != x.Count) throw new SizeMismatchException();
            list.Clear();
            foreach (int item in x)
            {
                list.Add(item);
            }
        }

        public static bool AreEqual(Diag a, Diag b)
        {
            return a.list.SequenceEqual(b.list);
        }
        #endregion

        #region Operators


        public static Diag Add(Diag a, Diag b)
        {
            if (a.n != b.n)
            {
                throw new SizeMismatchException();
            }

            Diag sum = new Diag(a.n);
            for (int i = 0; i < sum.list.Count; i++)
            {
                sum.list[i] = a.list[i] + b.list[i];
            }
            return sum;
        }

        public static Diag Mul(Diag a, Diag b)
        {
            if (a.Size() != b.Size())
            {
                throw new SizeMismatchException();
            }

            Diag mul = new Diag(a.Size());
            
            List<int> lst = new List<int>();

            List<List<int>> matrixA = ConvertToList(a);
            List<List<int>> matrixB = ConvertToList(b);

            for (int i = 0; i < a.Size(); i++)
            {
                for (int j = 0; j < a.Size(); j++)
                {
                    if (mul.InNmatrix(i, j))
                    {
                        int value = 0;
                        for (int k = 0; k < a.Size(); k++)
                        {
                            value += matrixA[i][k] * matrixB[k][j];
                        }
                        lst.Add(value);
                        //mul.SetElem(i, j, value);
                    }
                }
            }
            mul.Set(lst);
            return mul;
        }
        
        public static List<List<int>> ConvertToList(Diag d)
        {
            List<List<int>> matrix = new List<List<int>>();
            for (int i = 0; i < d.Size(); i++)
            {
                matrix.Add(new List<int>());
                for (int j = 0; j < d.Size(); j++)
                {
                    matrix[i].Add(0);
                }
            }

            int index = 0;
            for (int i = 0; i < d.Size(); i++)
            {
                for (int j = 0; j < d.Size(); j++)
                {
                    if (d.InNmatrix(j, i))
                    {
                        matrix[j][i] = d.list[index];
                        index++;
                    }
                }
            }
            return matrix;
        }
        #endregion
        
    }
}
