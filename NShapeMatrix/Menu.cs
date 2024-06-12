using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NShapeMatrix.Diag;

namespace NShapeMatrix
{
    public class Menu
    {
        private List<Diag> vec = new List<Diag>();

        public Menu() { }

        public void Run()
        {
            int n;
            do
            {
                PrintMenu();
                try
                {
                    n = int.Parse(Console.ReadLine()!);
                    if (n < 0 || n > 6)
                    {
                        throw new InvalidIndexException();
                    }
                }
                catch (InvalidIndexException)
                {
                    n = -1;
                    Console.WriteLine("Integer is expected between 0 and 6 !");
                    //continue;
                }
                catch (FormatException)
                {
                    n = -1;
                    Console.WriteLine("Wrong Type !! Integer is expected between 0 and 6 !");
                    //continue;
                }
                switch (n)
                {
                    case 1:
                        GetElement();
                        break;
                    case 2:
                        SetElement();
                        break;
                    case 3:
                        PrintMatrix();
                        break;
                    case 4:
                        AddMatrix();
                        break;
                    case 5:
                        Sum();
                        break;
                    case 6:
                        Mul();
                        break;
                }

            } while (n != 0);

        }

        #region Menu operations
        static private void PrintMenu()
        {
            Console.WriteLine("\n\n 0. - Quit");
            Console.WriteLine(" 1. - Get an element");
            Console.WriteLine(" 2. - Set an element");
            Console.WriteLine(" 3. - Print a matrix");
            Console.WriteLine(" 4. - Set a matrix");
            Console.WriteLine(" 5. - Add matrices");
            Console.WriteLine(" 6. - Multiply matrices");
            Console.Write(" Choose: ");
        }

        private int GetIndex()
        {
            if (vec.Count == 0) return -1;
            int n = 0;
            bool ok;
            do
            {
                Console.Write("Give a matrix index: ");
                ok = false;
                try
                {
                    n = int.Parse(Console.ReadLine()!);
                    ok = true;
                }
                catch (InvalidIndexException)
                {
                    Console.WriteLine("Integer is expected!");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Integer is expected!");
                }
                if (n <= 0 || n > vec.Count)
                {
                    ok = false;
                    Console.WriteLine("No such matrix!");
                }
            } while (!ok);
            return n - 1;
        }

        private void GetElement()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            int ind = GetIndex();
            do
            {
                try
                {
                    Console.WriteLine("Give the index of the row: ");
                    int i = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Give the index of the column: ");
                    int j = int.Parse(Console.ReadLine()!);
                    Console.WriteLine($"a[{i},{j}]={vec[ind].GetElem(i, j)}");
                    break;
                }
                catch (InvalidIndexException)
                {
                    Console.WriteLine($"Index must be between 0 and {vec[ind].Size() - 1} ");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Integer is expected!");
                }

            } while (true);
        }

        private void SetElement()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            int ind = GetIndex();
            do
            {
                try
                {
                    Console.WriteLine("Give the index of the row: ");
                    int i = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Give the index of the column: ");
                    int j = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Give the value: ");
                    int e = int.Parse(Console.ReadLine()!);
                    //vec[ind].matrix[i - 1][j - 1] = e;
                    vec[ind].SetElem(i, j, e);
                    break;
                }
                catch (InvalidIndexException)
                {
                    Console.WriteLine($"Index must be between 0 and {vec[ind].Size() - 1} !");
                }
                catch (OutOfShapeException)
                {
                    Console.WriteLine("Index must be located in N shape !");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input should be integer !");
                }
            } while (true);
        }

        private void PrintMatrix()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            int ind = GetIndex();
            Console.Write(vec[ind].ToString());
        }
        private void AddMatrix()
        {
            int ind = vec.Count;
            bool ok = false;
            int n = -1;

            do
            {
                Console.Write("Size: ");
                try
                {
                    n = int.Parse(Console.ReadLine()!);
                    ok = n > 0;
                }
                catch (NegativeSizeException)
                {
                    Console.WriteLine("Size should be a positive integer !!");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Wrong format !! Enter positive integer !");
                }
            } while (!ok);
            Diag d = new Diag(n);

            ok = true;
            List<int> elements = new List<int>();
            int length = 3 * n - 2;
            for (int i = 0; i < length; i++)
            {
                Console.Write("Element: ");
                try
                {
                    int elem = int.Parse(Console.ReadLine()!);
                    elements.Add(elem);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Number is expected!");
                    ok = false;
                    break;
                }
            }

            if (ok)
            {
                d.Set(elements);
                vec.Add(d);
            }
        }
        private void Sum()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            Console.Write("1st matrix: ");
            int ind1 = GetIndex();
            Console.Write("2nd matrix: ");
            int ind2 = GetIndex();
            try
            {
                Console.Write((Diag.Add(vec[ind1], vec[ind2])).ToString());
            }
            catch (SizeMismatchException)
            {
                Console.WriteLine("Size doesn't match!");
            }
        }

        private void Mul()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            Console.Write("1st matrix: ");
            int ind1 = GetIndex();
            Console.Write("2nd matrix: ");
            int ind2 = GetIndex();
            try
            {
                Diag mul = (Diag.Mul(vec[ind1], vec[ind2]));
                Console.Write(mul.ToStringAfterMul());
            }
            catch (SizeMismatchException)
            {
                Console.WriteLine("Size doesn't match!");
            }
            catch (InvalidIndexException)
            {
                Console.WriteLine("Invalid Index!");
            }
        }
        #endregion
    }
}
