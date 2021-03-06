﻿//From: http://blogs.msdn.com/b/dotnetinterop/archive/2008/03/14/sudoku-generator-and-solver-in-c.aspx

// swiped from http://www.klepphelmer.com/Sudoku/DLXEngine.java
// converted to csharp, and modified extensively. 

/****************************************************************************
 * DancingLinksEngine.cs
 *
 * Created on den 30 december 2005, 01:04
 *
 * DLXEngine
 * Sudoku puzzle generator and solver based on the suexg and suexk by
 * Günter Stertenbrink. Suexg and suexk are C implementations of the
 * Dancing Links algorithm by Donald Knuth and optimized for performance
 * which means that certain cleanup work has been done. There is still
 * lots of these activities left to do, however, the code is nasty and
 * hard to read - but extremely efficient.
 *
 * The code is public domain so feel free to use it.
 *****************************************************************************/

using System;

namespace Sudoku
{
    /****************************************************************************
    * dlx_solver solve any Sudoku in a fraction of a second. Input is
    * a string of dots and digits representing the puzzle to solve and
    * output is the solved puzzle.
    * 
    * @author Rolf Sandberg
    ****************************************************************************/


    // A sample of the string representation: 
    //     ..3.69.5.
    //     9.1.2...3
    //     .........
    //     .7.9..4..
    //     2....3...
    //     3.6....8.
    //     8..6921..
    //     .6.7...4.
    //     ....5....
    //
    // There are (3x3+1)x3x3  characters = 90 chars in the string, plus a trailing newline.
    // Each "row" in the printed string is 9 chars followed by \n.
    // And then there is a \n at the end.
    //


    class dlx_solver
    {
        static readonly int M = 8; // change this for larger grids. Use symbols as in L[] below
        static readonly int M2 = M * M;
        static readonly int M4 = M2 * M2;
        long zr = 362436069, wr = 521288629;

        /** Pseudo-random number generator */
        public long MWC()
        {
            return ((zr = 36969 * (zr & 65535) + (zr >> 16)) ^ (wr = 18000 * (wr & 65535) + (wr >> 16)));
        }

        int[,] A0 = new int[M2 + 9, M2 + 9];
        int[,] A = new int[M2 + 9, M2 + 9];
        int[] Rows = new int[4 * M4 + 9];
        int[] Cols = new int[M2 * M4 + 9];
        int[,] Row = new int[4 * M4 + 9, M2 + 9];

        int[,] Col = new int[M2 * M4 + 9, 5];
        int[] Ur = new int[M2 * M4 + 9];
        int[] Uc = new int[4 * M4 + 9];
        int[] V = new int[M2 * M4 + 9];
        int[] C = new int[M4 + 9];
        int[] I = new int[M4 + 9];
        int[] T = new int[M2 * M4 + 9];
        int[] P = new int[M2 * M4 + 9];
        int[] Mr = { 0, 1, 63, 1023, 4095, 16383, 46655, 131071, 262143 };
        int[] Mc = { 0, 1, 63, 511, 1023, 4095, 8191, 16383, 16383 };
        int[] Mw = { 0, 1, 3, 15, 15, 31, 63, 63, 63 };

        int nocheck = 0;
        //int max;
        int _try_;
        int rnd = 0, min, clues, gu;
        //int tries;
        long[] Node = new long[M4 + 9];
        long nodes, tnodes, solutions, vmax, smax, time0, time1, t1, x1;
        double xx, yy;
        int q, a, i, j, k, l, r, r1,
          c, c1, c2, n, N = 0, N2, N4,
          m, m0, m1, x, y, s;
#if P
    int p;
#endif
        //char ch;
        char[] L = {'.',
		'1','2','3','4','5','6','7','8','9',
		'A','B','C','D','E','F','G','H','I',
		'J','K','L','M','N','O','P','Q','R',
		'S','T','U','V','W','X','Y','Z','a',
		'b','c','d','e','f','g','h','i','j',
		'k','l','m','n','o','p','q','r','s',
		't','u','v','w','x','y','z','#','*','~'
    };

        /** State machine states */
        public enum SolverState : short
        {
            M6 = 10,
            M7 = 11,
            RESTART = 12,
            M2 = 13,
            M3 = 14,
            M4 = 15,
            NEXT_TRY = 16,
            END = 30
        }

        /**
         * Solver function. 
         * Input parameter: A puzzle to solve
         * Output: The solved puzzle
         **/
        public String Solve(String puzzle)
        {
            String result = "";
            SolverState STATE = SolverState.M6;

            vmax = 4000000;
            smax = 25;
#if P
      p = 1;
#endif
            q = 0;


            t1 = System.DateTime.Now.Ticks;
            zr ^= t1;
            wr += t1;

            if (rnd < 999)
            {
                zr ^= rnd;
                wr += rnd;
                for (i = 1; i < rnd; i++)
                    MWC();
            }

            if (q > 0)
            {
                vmax = 99999999;
                smax = 99999999;
            }

            N = 3;
            N2 = N * N;
            N4 = N2 * N2;
            m = 4 * N4;
            n = N2 * N4;

            if (puzzle.Length < N4)
            {
                return "Error, puzzle incomplete";
            }

            time0 = System.DateTime.Now.Ticks;
            while (STATE != SolverState.END)
            {
                switch (STATE)
                {
                    case SolverState.M6:
                        clues = 0;
                        i = 0;
                        for (x = 0; x < N2; x++) for (y = 0; y < N2; y++)
                            {
                                c = puzzle[x * N2 + y];
                                j = 0;

                                if (c == '-' || c == '.' || c == '0' || c == '*')
                                {
                                    A0[x, y] = j;
                                    i++;
                                }
                                else
                                {
                                    while (L[j] != c && j <= N2)
                                        j++;

                                    if (j <= N2)
                                    {
                                        A0[x, y] = j;
                                        if (j > 0)
                                            clues++;
                                        i++;
                                    }
                                }
                            }

                        if (clues == N4)
                        {
                            clues--;
                            A0[1, 1] = 0;
                        }


#if P
	  if(p < 8) {
	    for(i = 0; i <= N4; i++)
	      Node[i]=0;
	  }
#endif

                        tnodes = 0;

                        STATE = SolverState.RESTART;
                        break;


                    case SolverState.RESTART:
                        r = 0;
                        for (x = 1; x <= N2; x++) for (y = 1; y <= N2; y++) for (s = 1; s <= N2; s++)
                                {
                                    r++;
                                    Cols[r] = 4;
                                    Col[r, 1] = x * N2 - N2 + y;
                                    Col[r, 4] = (N * ((x - 1) / N) + (y - 1) / N) * N2 + s + N4;

                                    Col[r, 3] = x * N2 - N2 + s + N4 * 2;
                                    Col[r, 2] = y * N2 - N2 + s + N4 * 3;
                                }
                        for (c = 1; c <= m; c++) Rows[c] = 0;

                        for (r = 1; r <= n; r++) for (c = 1; c <= Cols[r]; c++)
                            {
                                x = Col[r, c];
                                Rows[x]++;
                                Row[x, Rows[x]] = r;
                            }

                        for (x = 0; x < N2; x++) for (y = 0; y < N2; y++)
                                A[x, y] = A0[x, y];

                        for (i = 0; i <= n; i++) Ur[i] = 0;
                        for (i = 0; i <= m; i++) Uc[i] = 0;

                        solutions = 0;

                        for (x = 1; x <= N2; x++) for (y = 1; y <= N2; y++) if (A[x - 1, y - 1] > 0)
                                {
                                    r = x * N4 - N4 + y * N2 - N2 + A[x - 1, y - 1];

                                    for (j = 1; j <= Cols[r]; j++)
                                    {
                                        c1 = Col[r, j];
                                        if (Uc[c1] > 0 && nocheck == 0)
                                        {
                                            STATE = SolverState.NEXT_TRY;
                                            break;
                                        }

                                        Uc[c1]++;

                                        for (k = 1; k <= Rows[c1]; k++)
                                        {
                                            r1 = Row[c1, k];
                                            Ur[r1]++;
                                        }
                                    }
                                    if (STATE == SolverState.NEXT_TRY)
                                        break;
                                }
                        if (STATE == SolverState.NEXT_TRY)
                            break;

                        if (rnd > 0 && rnd != 17 && rnd != 18)
                            shuffle();

                        for (c = 1; c <= m; c++)
                        {
                            V[c] = 0;
                            for (r = 1; r <= Rows[c]; r++) if (Ur[Row[c, r]] == 0)
                                    V[c]++;
                        }

                        i = clues;
                        nodes = 0;
                        m0 = 0;
                        m1 = 0;
                        gu = 0;
                        solutions = 0;
                        STATE = SolverState.M2;
                        break;


                    case SolverState.M2:
                        i++;
                        I[i] = 0;
                        min = n + 1;
                        if (i > N4 || m0 > 0)
                        {
                            STATE = SolverState.M4;
                            break;
                        }
                        if (m1 > 0)
                        {
                            C[i] = m1;
                            STATE = SolverState.M3;
                            break;
                        }
                        for (c = 1; c <= m; c++) if (Uc[c] == 0)
                            {
                                if (V[c] <= min) c1 = c;
                                if (V[c] < min)
                                {
                                    min = V[c];
                                    C[i] = c;
                                    if (min < 2)
                                    {
                                        STATE = SolverState.M3;
                                        break;
                                    }
                                }
                            }
                        if (STATE == SolverState.M3)
                            break;

                        gu++;
                        if (min > 2)
                        {
                            STATE = SolverState.M3;
                            break;
                        }

                        if ((rnd & 255) == 18) if ((nodes & 1) > 0)
                            {
                                c = m + 1;
                                c--;
                                while (Uc[c] > 0 || V[c] != 2)
                                    c--;
                                C[i] = c;
                            }

                        if ((rnd & 255) == 17)
                        {
                            c1 = (int)(MWC() & Mc[N]);
                            while (c1 >= m)
                                c1 = (int)(MWC() & Mc[N]);
                            c1++;

                            for (c = c1; c <= m; c++) if (Uc[c] == 0) if (V[c] == 2)
                                    {
                                        C[i] = c;
                                        STATE = SolverState.M3;
                                        break;
                                    }
                            for (c = 1; c < c1; c++) if (Uc[c] == 0) if (V[c] == 2)
                                    {
                                        C[i] = c;
                                        STATE = SolverState.M3;
                                        break;
                                    }
                        }

                        STATE = SolverState.M3;
                        break;


                    case SolverState.M3:
                        c = C[i];
                        I[i]++;
                        if (I[i] > Rows[c])
                        {
                            STATE = SolverState.M4;
                            break;
                        }

                        r = Row[c, I[i]];
                        if (Ur[r] > 0)
                        {
                            STATE = SolverState.M3;
                            break;
                        }
                        m0 = 0;
                        m1 = 0;

                        if (q > 0 && i > 32 && i < 65) if ((MWC() & 127) < q)
                            {
                                STATE = SolverState.M3;
                                break;
                            }

                        k = N4;
                        x = (r - 1) / k + 1;
                        y = ((r - 1) % k) / j + 1;
                        s = (r - 1) % j + 1;

#if P                    
	  if((p&1) > 0) {
#endif
                        j = N2;
                        k = N4;
                        x = (r - 1) / k + 1;
                        y = ((r - 1) % k) / j + 1;
                        s = (r - 1) % j + 1;
                        A[x - 1, y - 1] = s;
                        if (i == k)
                        {
                            for (x = 0; x < j; x++)
                            {
                                for (y = 0; y < j; y++)
                                    result += L[A[x, y]];
                                result += "\n";
                            }
                            result += " #\n";
                        }
#if P
	  }
#endif


                        for (j = 1; j <= Cols[r]; j++)
                        {
                            c1 = Col[r, j];
                            Uc[c1]++;
                        }

                        for (j = 1; j <= Cols[r]; j++)
                        {
                            c1 = Col[r, j];

                            for (k = 1; k <= Rows[c1]; k++)
                            {
                                r1 = Row[c1, k];
                                Ur[r1]++;
                                if (Ur[r1] == 1) for (l = 1; l <= Cols[r1]; l++)
                                    {
                                        c2 = Col[r1, l];
                                        V[c2]--;

                                        if (Uc[c2] + V[c2] < 1) m0 = c2;
                                        if (Uc[c2] == 0 && V[c2] < 2) m1 = c2;
                                    }
                            }
                        }
                        Node[i]++;
                        tnodes++;
                        nodes++;
                        if (rnd > 99 && nodes > rnd)
                        {
                            STATE = SolverState.RESTART;
                            break;
                        }
                        if (i == N4) solutions++;

                        if (solutions >= smax)
                        {
                            Console.WriteLine("smax xolutions found");
                            if (_try_ == 1) Console.Write("+");
                            STATE = SolverState.NEXT_TRY;
                            break;
                        }
                        if (tnodes > vmax)
                        {
                            if (_try_ == 1) Console.Write("-");
                            STATE = SolverState.NEXT_TRY;
                            break;
                        }
                        STATE = SolverState.M2;
                        break;

                    case SolverState.M4:
                        i--;
                        c = C[i];
                        r = Row[c, I[i]];
                        if (i == clues)
                        {
                            STATE = SolverState.NEXT_TRY;
                            break;
                        }

                        for (j = 1; j <= Cols[r]; j++)
                        {
                            c1 = Col[r, j];
                            Uc[c1]--;

                            for (k = 1; k <= Rows[c1]; k++)
                            {
                                r1 = Row[c1, k];
                                Ur[r1]--;

                                if (Ur[r1] == 0) for (l = 1; l <= Cols[r1]; l++)
                                    {
                                        c2 = Col[r1, l];
                                        V[c2]++;
                                    }
                            }
                        }
#if P
	  if(p > 0) {
#endif
                        j = N2;
                        k = N4;
                        x = (r - 1) / k + 1;
                        y = ((r - 1) % k) / j + 1;
                        s = (r - 1) % j + 1;
                        A[x - 1, y - 1] = 0;
#if P
	  }
#endif

                        if (i > clues)
                        {
                            STATE = SolverState.M3;
                            break;
                        }
                        STATE = SolverState.NEXT_TRY;
                        break;


                    case SolverState.NEXT_TRY:
                        _try_++;
                        time1 = System.DateTime.Now.Ticks;
                        x1 = time1 - time0;

                        time0 = time1;

                        if (q > 0)
                        {
                            xx = 128;
                            yy = 128 - q;
                            xx = xx / yy;
                            yy = solutions;
                            for (i = 1; i < 33; i++) yy = yy * xx;
                            Console.WriteLine("clues: " + clues + " estimated solutions:" + yy + " time " + x1 + "ms");

                            STATE = SolverState.END;
                            break;
                        }
#if P
	  if((p == 0 || p == 1) && tnodes <= 999999) 
#else
                        if (tnodes <= 999999)
#endif
                        {
                            if (solutions >= smax)
                                result += "More than " + solutions + " solutions ( bad sudoku!! ), rating " + (100 * tnodes / solutions) + ", time " + x1 + " ms";
                            else if (solutions == 1)
                                result += solutions + " solution, rating " + (100 * tnodes) + ", time " + x1 + " ms";
                            else if (solutions == 0)
                                result += "0 solutions, no rating possible, time " + x1 + " ms";
                            else
                                result += solutions + " solutions ( bad sudoku!! ), rating " + (100 * tnodes / solutions) + ", time " + x1 + " ms";

                            STATE = SolverState.END;
                            break;
                        }
#if P
	  if(p == 6) {
	    Console.WriteLine(solutions);
	    STATE = SolverState.END;
	    break;
	  }
#endif

#if P
	  if(p == 0 || p == 1) 
#endif

                        Console.WriteLine(solutions + " solution(s), rating " + (100 * tnodes) + ", time " + x1 + "ms");

#if P
	  if(p > 5) {
	    x = 0;
	    for(i = 1; i <= N4; i++) {
	      x += Node[i];
	      Console.Write(Node[i]);
	      if(i % 9 == 0)
		Console.WriteLine();
	    }
	    Console.WriteLine(x);
	  }
#endif

                        STATE = SolverState.END;
                        break;
                } // end of switch statement
            } // end of while loop
            return result;
        }

        /**
         * Helper function.
         **/
        int shuffle()
        {
            for (i = 1; i <= m; i++)
            {
                a = (int)((MWC() >> 8) & Mc[N]);
                while (a >= i)
                    a = (int)((MWC() >> 8) & Mc[N]);
                a++;
                P[i] = P[a];
                P[a] = i;
            }

            for (c = 1; c <= m; c++)
            {
                Rows[c] = 0;
                T[c] = Uc[c];
            }

            for (c = 1; c <= m; c++)
                Uc[P[c]] = T[c];

            for (r = 1; r <= n; r++) for (i = 1; i <= Cols[r]; i++)
                {
                    c = P[Col[r, i]];
                    Col[r, i] = c;
                    Rows[c]++;
                    Row[c, Rows[c]] = r;
                }

            for (i = 1; i <= n; i++)
            {
                a = (int)((MWC() >> 8) & Mr[N]);
                while (a >= i)
                    a = (int)((MWC() >> 8) & Mr[N]);
                a++;
                P[i] = P[a];
                P[a] = i;
            }

            for (r = 1; r <= n; r++)
            {
                Cols[r] = 0;
                T[r] = Ur[r];
            }

            for (r = 1; r <= n; r++)
                Ur[P[r]] = T[r];

            for (c = 1; c <= m; c++) for (i = 1; i <= Rows[c]; i++)
                {
                    r = P[Row[c, i]];
                    Row[c, i] = r;
                    Cols[r]++;
                    Col[r, Cols[r]] = c;
                }

            for (r = 1; r <= n; r++)
            {
                for (i = 1; i <= Cols[r]; i++)
                {
                    a = (int)((MWC() >> 8) & 7);
                    while (a >= i)
                        a = (int)((MWC() >> 8) & 7);
                    a++;
                    P[i] = P[a];
                    P[a] = i;
                }

                for (i = 1; i <= Cols[r]; i++)
                    T[i] = Col[r, P[i]];

                for (i = 1; i <= Cols[r]; i++)
                    Col[r, i] = T[i];
            }

            for (c = 1; c <= m; c++)
            {
                for (i = 1; i <= Rows[c]; i++)
                {
                    a = (int)((MWC() >> 8) & Mw[N]);
                    while (a >= i)
                        a = (int)((MWC() >> 8) & Mw[N]);
                    a++;
                    P[i] = P[a];
                    P[a] = i;
                }

                for (i = 1; i <= Rows[c]; i++)
                    T[i] = Row[c, P[i]];

                for (i = 1; i <= Rows[c]; i++)
                    Row[c, i] = T[i];
            }
            return 0;
        }

        public dlx_solver() { }
    }

    /******************************************************************************
     * dlx_generator generate single solution locally minimized Sudoku puzzles.
     * Locally minimized means that all keys that can be removed without creating
     * a degenerate Sudoku (multiple solutions) are removed.
     ******************************************************************************/

    class dlx_generator
    {

        private static readonly int DefaultPuzzleSetSize = 100;
        private static readonly int SolveCycles = 100;
        private static readonly bool WANT_DEBUG_MESSAGES = false;
        private System.Random rnd = new System.Random();
        // long zr = 362436069, wr = 521288629;
        //   long MWC() {
        //     return ((zr = 36969 *(zr & 65535) + (zr >> 16)) ^ ( wr = 18000 * (wr & 65535) + (wr >> 16)));
        //   }


        private void InitializeRandomNumberGenerator(int seed)
        {
            rnd = new System.Random(seed);
        }

        private void InitializeRandomNumberGenerator()
        {
            rnd = new System.Random();
        }

        private long MWC()
        {
            int i = rnd.Next();
            if (i < 0) return -1 * i;
            return i;
        }


        int[] Rows = new int[325];
        int[] Cols = new int[730];
        int[,] Row = new int[325, 10];
        int[,] Col = new int[730, 5];
        int[] Ur = new int[730];
        int[] Uc = new int[325];
        int[] V = new int[325];
        int[] W = new int[325];
        int[] P = new int[88];
        int[] A = new int[88];
        int[] C = new int[88];
        int[] I = new int[88];
        int[] Two = new int[888];

        char[] B = {'0',
	       '1','1','1','2','2','2','3','3','3',
	       '1','1','1','2','2','2','3','3','3',
	       '1','1','1','2','2','2','3','3','3',
	       '4','4','4','5','5','5','6','6','6',
	       '4','4','4','5','5','5','6','6','6',
	       '4','4','4','5','5','5','6','6','6',
	       '7','7','7','8','8','8','9','9','9',
	       '7','7','7','8','8','8','9','9','9',
	       '7','7','7','8','8','8','9','9','9'
    };
        char[][] H = new char[326][];
        long c2, w; //seed;
        int b, s1, m0, c1,
          r1, l, i1, m1, a,
          i, j, r, c, n = 729,
          m = 324, x, y, s, fi;
        char ch;
        int numSolutions;
        int hintIndex, q7, part, nodes,
          solutions, min, samples, clues;
        char[] L = { '.', '1', '2', '3', '4', '5', '6', '7', '8', '9' };


        /** State machine states */
        public enum GeneratorState : short
        {
            M0S = 10,
            M0 = 11,
            MR1 = 12,
            MR3 = 13,
            MR4 = 14,
            M2 = 15,
            M3 = 16,
            M4 = 17,
            M9 = 18,
            MR = 19,
            END = 20,
            M6 = 21
        }


        /** Output trace messages */
        void dbg(String s)
        {
            if (WANT_DEBUG_MESSAGES)
                Console.WriteLine(s);
        }

        public dlx_generator()
        {
            dbg("In constructor");
            for (int i = 0; i < H.Length; i++)
                H[i] = new char[7];
        }



#if NOT
    public static void saveSudokuToFile(String puzzle, String filename) {
      FileOutputStream FO = null;
      byte[] buffer = new byte[puzzle.length()+1];
      int i = 0;
        
      while (i < puzzle.length()){
	buffer[i] = (byte) puzzle.charAt(i);
	i++;
      }
        
      try {
	FO = new FileOutputStream(filename);
	FO.write(buffer);
	FO.close();
      } catch (IOException IOE) {
	// Well, well, well....
	return;
      }
    }
#endif

        public static String loadSudokuFromFile(String filename)
        {
            throw (new NotImplementedException());
        }


        /**
         * Initialization code for both generate() and rate()
         */
        void initialize()
        {
            for (i = 0; i < 888; i++)
            {
                j = 1;
                while (j <= i)
                    j += j;
                Two[i] = j - 1;
            }

            r = 0;
            for (x = 1; x <= 9; x++) for (y = 1; y <= 9; y++) for (s = 1; s <= 9; s++)
                    {
                        r++;
                        Cols[r] = 4;
                        Col[r, 1] = x * 9 - 9 + y;
                        Col[r, 2] = (B[x * 9 - 9 + y] - 48) * 9 - 9 + s + 81;
                        Col[r, 3] = x * 9 - 9 + s + 81 * 2;
                        Col[r, 4] = y * 9 - 9 + s + 81 * 3;
                    }

            for (c = 1; c <= m; c++) Rows[c] = 0;

            for (r = 1; r <= n; r++) for (c = 1; c <= Cols[r]; c++)
                {
                    a = Col[r, c];
                    Rows[a]++;
                    Row[a, Rows[a]] = r;
                }

            c = 0;
            for (x = 1; x <= 9; x++) for (y = 1; y <= 9; y++)
                {
                    c++;
                    H[c][0] = 'r';
                    H[c][1] = L[x];
                    H[c][2] = 'c';
                    H[c][3] = L[y];
                    H[c][4] = (char)0;
                }

            c = 81;
            for (b = 1; b <= 9; b++) for (s = 1; s <= 9; s++)
                {
                    c++;
                    H[c][0] = 'b';
                    H[c][1] = L[b];
                    H[c][2] = 's';
                    H[c][3] = L[s];
                    H[c][4] = (char)0;
                }

            c = 81 * 2;
            for (x = 1; x <= 9; x++) for (s = 1; s <= 9; s++)
                {
                    c++;
                    H[c][0] = 'r';
                    H[c][1] = L[x];
                    H[c][2] = 's';
                    H[c][3] = L[s];
                    H[c][4] = (char)0;
                }

            c = 81 * 3;
            for (y = 1; y <= 9; y++) for (s = 1; s <= 9; s++)
                {
                    c++;
                    H[c][0] = 'c';
                    H[c][1] = L[y];
                    H[c][2] = 's';
                    H[c][3] = L[s];
                    H[c][4] = (char)0;
                }
        }

        /// <summary>
        /// Solve the puzzle many times, generating an "average" rating
        /// of all those solutions.  
        /// </summary>
        /// <param name="puzzle">string representation of the puzzle.  No newline chars, please.</param>
        /// <returns>rating, an integer, between 5600 and 12000</returns>
        public long Rate(String puzzle)
        {
            GeneratorState engineState = GeneratorState.M6;

            fi = 0;
            int rateFlag = 1;
            int puzzleRating = 0;

            for (i = 0; i < 88; i++)
                A[i] = 0;

            initialize();

            while (engineState != GeneratorState.END)
            {
                switch (engineState)
                {
                    case GeneratorState.M6:
                        clues = 0;
                        for (i = 1; i <= 81; i++)
                        {
                            ch = (char)puzzle[i - 1];
                            j = 0;

                            if (ch == '-' || ch == '.' || ch == '0' || ch == '*')
                            {
                                A[i] = j;
                            }
                            else
                            {
                                while ((j <= 9) && (L[j] != ch))
                                    j++;

                                if (j <= 9)
                                {
                                    A[i] = j;
                                }
                            }
                        }

                        if (clues == 81)
                        {
                            clues--;
                            A[1] = 0;
                        }


                        puzzleRating = RatePuzzle();

                        if (puzzleRating < 0)
                        {
                            engineState = GeneratorState.END;
                            break;
                        }

                        if (fi > 0) if ((puzzleRating / SolveCycles) > fi)
                            {
                                for (i = 1; i <= 81; i++)
                                    Console.WriteLine(L[A[i]]);
                                Console.WriteLine();
                                engineState = GeneratorState.M6;
                                break;
                            }

                        if (fi > 0)
                        {
                            engineState = GeneratorState.M6;
                            break;
                        }

                        if ((SolveCycles & 1) > 0)
                        {  //????
                            Console.WriteLine(puzzleRating / SolveCycles);
                            engineState = GeneratorState.M6;
                            break;
                        }

                        if (rateFlag > 1)
                            Console.WriteLine("hint: " + new String(H[hintIndex]));

                        engineState = GeneratorState.END;
                        break;
                } // End of switch statement
            } // End of while loop
            return (puzzleRating);
        }


        /// <summary>
        /// Rates the puzzle specified in A[]
        /// This routine solves the puzzle N times (specified by SolveCycles),
        /// tallying the number of nodes in each solution trial.  
        /// Side-effect: sets the hintIndex 
        /// </summary>
        /// 
        /// <returns>
        /// puzzle rating 
        /// (5600 <= r <= 9999999) if puzzle is valid
        /// r < 0 if puzzle is invalid (eg., multiple solutions or other problem).
        /// </returns>
        private int RatePuzzle()
        {
            int nodeTotal = 0;
            int minNodes = 999999;
            hintIndex = 0;
            for (int i = 0; i < SolveCycles && nodeTotal >= 0; i++)
            {
                int numSolutionsFound = solve();
                if (numSolutionsFound != 1)
                {
                    if (numSolutionsFound > 1)
                        nodeTotal = -1 * numSolutionsFound;
                }
                else
                {
                    nodeTotal += nodes;
                    if (nodes < minNodes)
                    {
                        minNodes = nodes;
                        hintIndex = C[clues];
                    }
                }
            }
            return nodeTotal;
        }





        /// <summary>
        /// generate a set of Sudoku puzzles, starting with a given seed 
        /// for the random-number generator.
        /// </summary>
        /// <param name="Seed">RNG Seed</param>
        /// <param name="Samples">how many puzzles to generate</param>
        /// <returns>puzzles, in string form. </returns>
        public String[] GeneratePuzzleSet(int Seed, int Samples)
        {
            dbg("Entering generate");

            InitializeRandomNumberGenerator(Seed);

            // samples = number of puzzles to generate
            samples = 1000;
            if (Samples <= 0) Samples = DefaultPuzzleSetSize;
            samples = Samples;

            String[] result = new String[Samples];
            for (i = 0; i < samples; i++)
                result[i] = "";

            initialize();

            dbg("Entering state machine");

            int currentSample = -1;

            GeneratorState engineState = GeneratorState.M0S;
            while (engineState != GeneratorState.END)
            {
                switch (engineState)
                {
                    case GeneratorState.M0S:
                        currentSample++;
                        if (currentSample >= samples)
                        {
                            engineState = GeneratorState.END;
                        }
                        else
                            engineState = GeneratorState.M0;
                        break;

                    case GeneratorState.M0:
                        for (i = 1; i <= 81; i++) A[i] = 0;
                        part = 0;
                        q7 = 0;
                        engineState = GeneratorState.MR1;
                        break;

                    case GeneratorState.MR1:
                        i1 = (int)((MWC() >> 8) & 127);
                        if (i1 > 80)
                        {
                            engineState = GeneratorState.MR1;
                            break;
                        }

                        i1++;
                        if (A[i1] > 0)
                        {
                            engineState = GeneratorState.MR1;
                            break;
                        }
                        engineState = GeneratorState.MR3;
                        break;

                    case GeneratorState.MR3:
                        s = (int)((MWC() >> 9) & 15);
                        if (s > 8)
                        {
                            engineState = GeneratorState.MR3;
                            break;
                        }

                        s++;
                        A[i1] = s;
                        numSolutions = solve();
                        q7++;

                        if (numSolutions < 1)
                            A[i1] = 0;

                        if (numSolutions != 1)
                        {
                            engineState = GeneratorState.MR1;
                            break;
                        }

                        part++;
                        if (solve() != 1)
                        {
                            engineState = GeneratorState.M0;
                            break;
                        }
                        engineState = GeneratorState.MR4;
                        break;

                    case GeneratorState.MR4:
                        for (i = 1; i <= 81; i++)
                        {
                            x = (int)((MWC() >> 8) & 127);
                            while (x >= i)
                            {
                                x = (int)((MWC() >> 8) & 127);
                            }
                            x++;
                            P[i] = P[x];
                            P[x] = i;
                        }

                        for (i1 = 1; i1 <= 81; i1++)
                        {
                            s1 = A[P[i1]];
                            A[P[i1]] = 0;
                            if (solve() > 1) A[P[i1]] = s1;
                        }


                        for (i = 1; i <= 81; i++)
                        {
                            result[currentSample] += L[A[i]];
                            if (i % 9 == 0)
                            {
                                result[currentSample] += "\n";
                            }
                        }
                        result[currentSample] += "\n";

                        engineState = GeneratorState.M0S;
                        break;

                    default:
                        dbg("Default case. New state M0S");
                        engineState = GeneratorState.M0S;
                        break;
                } // end of switch statement
            } // end of while loop
            return result;
        }


        private int solve()
        {

            //returns 0 (no solution), 1 (unique sol.), 2 (more than one sol.)

            GeneratorState engineState = GeneratorState.M2;
            int i = 0, j = 0, k = 0, d, r, c;
            for (i = 0; i <= n; i++) Ur[i] = 0;
            for (i = 0; i <= m; i++) Uc[i] = 0;
            clues = 0;

            for (i = 1; i <= 81; i++)
                if (A[i] > 0)
                {
                    clues++;
                    r = i * 9 - 9 + A[i];

                    for (j = 1; j <= Cols[r]; j++)
                    {
                        d = Col[r, j];
                        if (Uc[d] > 0) return 0;
                        Uc[d]++;

                        for (k = 1; k <= Rows[d]; k++)
                        {
                            Ur[Row[d, k]]++;
                        }
                    }
                }

            for (c = 1; c <= m; c++)
            {
                V[c] = 0;
                for (r = 1; r <= Rows[c]; r++) if (Ur[Row[c, r]] == 0)
                        V[c]++;
            }

            i = clues;
            m0 = 0;
            m1 = 0;
            solutions = 0;
            nodes = 0;

            dbg("Solve: Entering state machine");

            while (engineState != GeneratorState.END)
            {
                switch (engineState)
                {
                    case GeneratorState.M2:
                        i++;
                        I[i] = 0;
                        min = n + 1;
                        if ((i > 81) || (m0 > 0))
                        {
                            engineState = GeneratorState.M4;
                            break;
                        }

                        if (m1 > 0)
                        {
                            C[i] = m1;
                            engineState = GeneratorState.M3;
                            break;
                        }

                        w = 0;
                        for (c = 1; c <= m; c++) if (Uc[c] == 0)
                            {
                                if (V[c] < 2)
                                {
                                    C[i] = c;
                                    engineState = GeneratorState.M3;
                                    break;
                                }

                                if (V[c] <= min)
                                {
                                    w++;
                                    W[(int)w] = c;
                                }
                                ;

                                if (V[c] < min)
                                {
                                    w = 1;
                                    W[(int)w] = c;
                                    min = V[c];
                                }
                            }

                        if (engineState == GeneratorState.M3)
                        {
                            // break in for loop detected, continue breaking
                            break;
                        }
                        engineState = GeneratorState.MR;
                        break;

                    case GeneratorState.MR:
                        c2 = (MWC() & Two[(int)w]);
                        while (c2 >= w)
                        {
                            c2 = (MWC() & Two[(int)w]);
                        }
                        C[i] = W[(int)c2 + 1];
                        engineState = GeneratorState.M3;
                        break;

                    case GeneratorState.M3:
                        c = C[i];
                        I[i]++;
                        if (I[i] > Rows[c])
                        {
                            engineState = GeneratorState.M4;
                            break;
                        }

                        r = Row[c, I[i]];
                        if (Ur[r] > 0)
                        {
                            engineState = GeneratorState.M3;
                            break;
                        }
                        m0 = 0;
                        m1 = 0;
                        nodes++;
                        for (j = 1; j <= Cols[r]; j++)
                        {
                            c1 = Col[r, j];
                            Uc[c1]++;
                        }

                        for (j = 1; j <= Cols[r]; j++)
                        {
                            c1 = Col[r, j];
                            for (k = 1; k <= Rows[c1]; k++)
                            {
                                r1 = Row[c1, k];
                                Ur[r1]++;
                                if (Ur[r1] == 1) for (l = 1; l <= Cols[r1]; l++)
                                    {
                                        c2 = Col[r1, l];
                                        V[(int)c2]--;
                                        if (Uc[(int)c2] + V[(int)c2] < 1)
                                            m0 = (int)c2;
                                        if (Uc[(int)c2] == 0 && V[(int)c2] < 2)
                                            m1 = (int)c2;
                                    }
                            }
                        }

                        if (i == 81)
                            solutions++;

                        if (solutions > 1)
                        {
                            engineState = GeneratorState.M9;
                            break;
                        }
                        engineState = GeneratorState.M2;
                        break;

                    case GeneratorState.M4:
                        i--;
                        if (i == clues)
                        {
                            engineState = GeneratorState.M9;
                            break;
                        }
                        c = C[i];
                        r = Row[c, I[i]];

                        for (j = 1; j <= Cols[r]; j++)
                        {
                            c1 = Col[r, j];
                            Uc[c1]--;
                            for (k = 1; k <= Rows[c1]; k++)
                            {
                                r1 = Row[c1, k];
                                Ur[r1]--;
                                if (Ur[r1] == 0) for (l = 1; l <= Cols[r1]; l++)
                                    {
                                        c2 = Col[r1, l];
                                        V[(int)c2]++;
                                    }
                            }
                        }

                        if (i > clues)
                        {
                            engineState = GeneratorState.M3;
                            break;
                        }
                        engineState = GeneratorState.M9;
                        break;

                    case GeneratorState.M9:
                        engineState = GeneratorState.END;
                        break;
                    default:
                        engineState = GeneratorState.END;
                        break;
                } // end of switch statement
            } // end of while statement
            return solutions;
        }
    }



    class SudokuPuzzle
    {
        public String StringRep;
        public int DesiredRatingLevel;
        public long ActualRating;
        public long Seed;
        public int Delta;
        public int NumHints;
    }


    /**
     *
     * @author Rolf Sandberg
     */

    class DancingLinksEngine
    {
        dlx_generator _generator;
        dlx_solver _solver;

        private static readonly int MaxGenerateLoops = 99;
        private static readonly int SampleSetSize = 5;

        class RatingLimits
        {
            public int min;
            public int max;
            int _centrum = 0;
            int Centrum
            {
                get
                {
                    if (_centrum == 0)
                    {
                        _centrum = (max - min) / 2 + min;
                    }
                    return _centrum;
                }
            }
            public bool WithinRange(long rating)
            {
                return ((rating >= min) && (rating <= max));
            }
            public int Delta(long rating)
            {
                int intRating = (int)rating;
                return Math.Abs(intRating - Centrum);
            }
        }

        private RatingLimits[] RatingsByLevel = new RatingLimits[]  
        { 
	      new RatingLimits(){min=5600,max=5800},
	      new RatingLimits(){min=5800,max=6100},
	      new RatingLimits(){min=6100,max=6400},
	      new RatingLimits(){min=6400,max=6800},
	      new RatingLimits(){min=6800,max=7000},
	      new RatingLimits(){min=7000,max=7500},
	      new RatingLimits(){min=7500,max=8000},
	      new RatingLimits(){min=8000,max=9000},
	      new RatingLimits(){min=9000,max=10000},
	      new RatingLimits(){min=10000,max=12000},
	      new RatingLimits(){min=12000,max=999999},
        };

        public DancingLinksEngine()
        {
            _generator = new dlx_generator();
            _solver = new dlx_solver();
        }


        public SudokuPuzzle GenerateOne(int ratingLevel)
        {
            return internalGenerate(ratingLevel, 0);
        }

        public SudokuPuzzle GenerateOne(int ratingLevel, long seed)
        {
            return internalGenerate(ratingLevel, seed);
        }


        private SudokuPuzzle internalGenerate(int desiredRatingLevel, long initialSeed)
        {
            // validate the passed-in desired rating level:
            if ((desiredRatingLevel < 0) && (desiredRatingLevel >= RatingsByLevel.Length))
                desiredRatingLevel = 0;

            RatingLimits RL = RatingsByLevel[desiredRatingLevel];

            SudokuPuzzle p = new SudokuPuzzle();
            p.DesiredRatingLevel = desiredRatingLevel;
            p.Seed = initialSeed;
            p.NumHints = 0;

            // Find a Sudoku having a rating within a specified rating interval.
            // Do it by generating multiple samples and examining them.
            // Continue until an appropriate puzzle is found.

            // Sometimes we need to iterate over multiple sets of puzzles.
            // Iteration doesn't make sense though, if there is an initialSeed
            // provided. So we set the loopLimit appropriately. 
            int loopLimit = (initialSeed == 0) ? MaxGenerateLoops : 1;

            for (int tries = 0; tries < loopLimit; tries++, System.Threading.Thread.Sleep(20))
            {
                long actualSeed = (initialSeed == 0) ? System.DateTime.Now.Ticks : initialSeed;

                String[] puzzles = _generator.GeneratePuzzleSet((int)actualSeed, SampleSetSize);
                for (int i = 0; i < SampleSetSize; i++)
                {
                    // get the rating 
                    long rating = _generator.Rate(puzzles[i].Replace("\n", "").Trim());

                    if ((i == 0) || RL.Delta(rating) < p.Delta)
                    {
                        p.ActualRating = rating;
                        p.StringRep = puzzles[i];
                        p.Seed = actualSeed;
                        p.Delta = RL.Delta(rating);
                    }
                }
                if (RL.WithinRange(p.ActualRating)) return p;
            }

            return p;
        }


        public long Rate(String s)
        {
            String localCopy = s;
            if (localCopy.IndexOf("\n") > 0)
                localCopy = s.Replace("\n", "");
            long result = _generator.Rate(localCopy);
            return result;
        }

        System.Random _rnd = null;
        System.Random Rng
        {
            get
            {
                if (_rnd == null) _rnd = new System.Random();
                return _rnd;
            }
        }

        public int EmptyBlocks(String puzzleStringRep)
        {
            return System.Text.RegularExpressions.Regex.Matches(puzzleStringRep, "\\.").Count;
        }

        public String GimmeOneHint(String puzzleStringRep, ref int posn)
        {
            // count the number of unknown spaces in the matrix:
            int g = EmptyBlocks(puzzleStringRep);
            int unknownToFillIn = Rng.Next(g);

            // solve the puzzle completely
            String solution = Solve(puzzleStringRep);

            // now choose the correct character for the nth unknown
            int numFound = 0;
            int i = 0;
            for (i = 0; i < puzzleStringRep.Length; i++)
            {
                if (puzzleStringRep[i] == '.') numFound++;
                if (numFound == unknownToFillIn + 1)
                    break;
            }
            String answer = puzzleStringRep;
            if (numFound == unknownToFillIn + 1)
            {
                //puzzleStringRep[charToFillIn] = solution[charToFillIn];
                String t1 = puzzleStringRep.Substring(0, i);
                String t2 = puzzleStringRep.Substring(i + 1);
                answer = t1 + solution[i] + t2;
            }
            posn = i;
            return answer;
        }

        public String Solve(String s)
        {
            String localCopy = s;
            bool ReinsertNewlines = false;
            if (localCopy.IndexOf("\n") > 0)
            {
                localCopy = s.Replace("\n", "");
                ReinsertNewlines = true;
            }
            String result = _solver.Solve(localCopy);
            if (ReinsertNewlines)
            {
                // TODO: re-insert newlines into solved puzzle
                // After every 9 chars I guess.
            }
            return result;
        }


    }

}
