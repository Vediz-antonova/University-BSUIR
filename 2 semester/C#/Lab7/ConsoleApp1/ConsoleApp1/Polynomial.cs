using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	public class Polynomial
	{
		public int A { get; set; }
		public int B { get; set; }
		public int C { get; set; }

		public Polynomial(int a, int b, int c)
		{
			A = a;
			B = b;
			C = c;
		}

		public static Polynomial operator +(Polynomial p1, Polynomial p2)
		{
			return new Polynomial(p1.A + p2.A, p1.B + p2.B, p1.C + p2.C);
		}

		public static Polynomial operator -(Polynomial p1, Polynomial p2)
		{
			return new Polynomial(p1.A - p2.A, p1.B - p2.B, p1.C - p2.C);
		}

		public static Polynomial operator ++(Polynomial p)
		{
			return new Polynomial(p.A + 1, p.B + 1, p.C + 1);
		}

		public static Polynomial operator --(Polynomial p)
		{
			return new Polynomial(p.A - 1, p.B - 1, p.C - 1);
		}

		public static Polynomial operator *(Polynomial p, int num)
		{
			return new Polynomial(p.A * num, p.B * num, p.C * num);
		}

		public static Polynomial operator /(Polynomial p, int num)
		{
			return new Polynomial(p.A / num, p.B / num, p.C / num);
		}

		public static bool operator ==(Polynomial p1, Polynomial p2)
		{
			return p1.A == p2.A && p1.B == p2.B && p1.C == p2.C;
		}

		public static bool operator !=(Polynomial p1, Polynomial p2)
		{
			return !(p1 == p2);
		}

		public static explicit operator int(Polynomial p)
		{
			return p.A;
		}

		public static explicit operator Polynomial(int num)
		{
			return new Polynomial(num, 0, 0);
		}

		public static bool operator true(Polynomial p)
		{
			return p.A != 0 || p.B != 0 || p.C != 0;
		}

		public static bool operator false(Polynomial p)
		{
			return p.A == 0 && p.B == 0 && p.C == 0;
		}

		public override string ToString()
		{
			return $"{A}x + {B}y + {C}z";
		}

		public int this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return A;
					case 1: return B;
					case 2: return C;
					default: throw new IndexOutOfRangeException("Index must be between 0 and 2");
				}
			}
			set
			{
				switch (index)
				{
					case 0: A = value; break;
					case 1: B = value; break;
					case 2: C = value; break;
					default: throw new IndexOutOfRangeException("Index must be between 0 and 2");
				}
			}
		}
	}

}
