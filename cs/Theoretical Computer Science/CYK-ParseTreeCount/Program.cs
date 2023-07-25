using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CYK_ParseTreeCount
{
    public readonly struct Rule
    {
        public readonly string   Left;  // left side of rule
        public readonly string[] Right; // right side of rule

        public Rule(string left, string[] right)
        {
            Left  = left;
            Right = right;
        }

        public override string ToString()
        {
            return Left + " → " + string.Join(" | ", Right);
        }
    }

    public readonly struct CFG
    {
        public readonly string[] Nonterminals; // nonterminal symbols
        public readonly string[] Terminals;    // terminal symbols
        public readonly Rule[]   Rules;        // rewrite rules
        public readonly string   Start;        // start symbol

        public CFG(string[] nonterminals, string[] terminals, Rule[] rules, string start)
        {
            Nonterminals = nonterminals;
            Terminals    = terminals;
            Rules        = rules;
            Start        = start;
        }

        public override string ToString()
        {
            return
                "G = (N, T, R, S)\n" +
                "N = [" + string.Join(", ", Nonterminals) + "]\n" +
                "T = [" + string.Join(", ", Terminals) + "]\n" +
                "R : " + string.Join("\n    ", Rules) + "\n" +
                "S = \"" + Start + "\"";
        }
    }

    public class NonterminalWithCount
    {
        public readonly string Nonterminal;      // nonterminal symbol
        public          uint   NonterminalCount; // nonterminal count
        
        public NonterminalWithCount(string nonterminal, uint nonterminalCount)
        {
            Nonterminal = nonterminal;
            NonterminalCount = nonterminalCount;
        }

        public override string ToString()
        {
            return "(" + Nonterminal + "," + NonterminalCount + ")";
        }
    }

    public class Program
    {
        public static List<List<List<string>>> CYK_ParseTable(CFG grammar, string word)
        {
            List<List<List<string>>> table = new List<List<List<string>>>(word.Length);

            // Word is at least 1 character long
            if (word.Length > 0)
            {
                // Create first line
                table.Add(new List<List<string>>(word.Length));

                //List<List<List<NonterminalWithCount>>> table = new List<List<List<NonterminalWithCount>>>(word.Length);

                // Go right through the first line
                for (int i = 0; i < word.Length; i++)
                {
                    // Create table cell
                    table[0].Add(new List<string>(grammar.Rules.Length));

                    // Go through all rules
                    for (int j = 0; j < grammar.Rules.Length; j++)
                    {
                        // Check whether the rule contains the given character
                        if (grammar.Rules[j].Right.Contains(char.ToString(word[i])))
                        {
                            // Add nonterminal to the selected table cell
                            table[0][i].Add(grammar.Rules[j].Left);
                        }
                    }
                }

                // Go up through all lines
                for (int i = 1; i < word.Length; i++)
                {
                    // Create another line
                    table.Add(new List<List<string>>(word.Length - i));

                    // Go right through another line
                    for (int j = 0; j < word.Length - i; j++)
                    {
                        // Create table cell
                        table[i].Add(new List<string>(grammar.Rules.Length));

                        // Go through all rules
                        for (int k = 0; k < grammar.Rules.Length; k++)
                        {
                            // Go through all table cell pairs
                            for (int l = 0; l < i; l++)
                            {
                                // Go through all nonterminals of A
                                for (int m = 0; m < table[l][j].Count; m++)
                                {
                                    // Go through all nonterminals of B
                                    for (int o = 0; o < table[i - 1 - l][j + 1 + l].Count; o++)
                                    {
                                        // Concatenate nonterminal of A and B (A·B)
                                        string concatenatedNonterminal = table[l][j][m] + table[i - 1 - l][j + 1 + l][o];

                                        // Check whether the rule contains the concatenated nonterminal
                                        if (grammar.Rules[k].Right.Contains(concatenatedNonterminal))
                                        {
                                            // Find index of nonterminal in the selected table cell
                                            int tableCellNonterminalIndex = table[i][j].FindIndex(nonterminal => nonterminal == grammar.Rules[k].Left);

                                            // Nonterminal is already in the selected table cell
                                            if (tableCellNonterminalIndex == -1)
                                            {
                                                // Add nonterminal to the selected table cell
                                                table[i][j].Add(grammar.Rules[k].Left);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // Word is empty
            else
            {
                // Create first line
                table.Add(new List<List<string>>(1));

                // Create table cell
                table[0].Add(new List<string>(grammar.Rules.Length));

                // Go through all rules
                for (int i = 0; i < grammar.Rules.Length; i++)
                {
                    // Check whether the rule contains the given character
                    if (grammar.Rules[i].Right.Contains("ε"))
                    {
                        // Add nonterminal to the selected table cell
                        table[0][0].Add(grammar.Rules[i].Left);
                    }
                }
            }

            return table;
        }

        public static bool CYK(CFG grammar, string word)
        {
            List<List<List<string>>> table = CYK_ParseTable(grammar, word);

            // Go through all rules
            for (int i = 0; i < table[table.Count - 1][0].Count; i++)
            {
                if (table[table.Count - 1][0][i] == grammar.Start)
                {
                    return true;
                }
            }

            return false;
        }

        /*public static List<List<List<(string N, ((int ax, int ay, int az) a, (int bx, int by, int bz) b)? p)>>> CYK_ParseTableWithPointers(CFG G, string w)
        {
            int n = w.Length;
            int r = G.Rules.Length;

            List<List<List<(string N, ((int ax, int ay, int az) a, (int bx, int by, int bz) b)? p)>>> T = new List<List<List<(string N, ((int ax, int ay, int az) a, (int bx, int by, int bz) b)? p)>>>(n);

            // Word is not empty
            if (n > 0)
            {
                T.Add(new List<List<(string N, ((int ax, int ay, int az) a, (int bx, int by, int bz) b)? p)>>(n));

                // Go through first line
                for (int j = 0; j < n; j++)
                {
                    T[0].Add(new List<(string N, ((int ax, int ay, int az) a, (int bx, int by, int bz) b)? p)>(r));

                    // Check all rules for given character
                    for (int k = 0; k < r; k++)
                    {
                        // Check if given character is part of a rule
                        if (G.Rules[k].Right.Contains(char.ToString(w[j])))
                        {
                            T[0][j].Add((G.Rules[k].Left, null));
                        }
                    }
                }

                // Go through rest of the lines
                for (int i = 1; i < n; i++)
                {
                    T.Add(new List<List<(string N, ((int ax, int ay, int az) a, (int bx, int by, int bz) b)? p)>>(n - i));

                    // Go through given line (the more lines went the less wide they are, which makes triangle shaped table)
                    for (int j = 0; j < n - i; j++)
                    {
                        T[i].Add(new List<(string N, ((int ax, int ay, int az) a, (int bx, int by, int bz) b)? p)>(r));

                        // Check all rules for given cartesian product (AxB)
                        for (int k = 0; k < r; k++)
                        {
                            // Loops all cells for AxB (AxB - cartesian product)
                            for (int l = 0; l < i; l++)
                            {
                                // Loop all nonterminals of A (AxB - cartesian product)
                                for (int m = 0; m < T[l][j].Count; m++)
                                {
                                    // Loop all nonterminals of B (AxB - cartesian product)
                                    for (int o = 0; o < T[i - 1 - l][j + 1 + l].Count; o++)
                                    {
                                        // Brings both nonterminals into one
                                        string checkW = T[l][j][m].N + T[i - 1 - l][j + 1 + l][o].N;

                                        // Check if given nonterminal is part of a rule
                                        if (G.Rules[k].Right.Contains(checkW))
                                        {
                                            T[i][j].Add((G.Rules[k].Left, ((l, j, m), (i - 1 - l, j + 1 + l, o))));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return T;
        }*/

        /*public static int CYK_ParseTreeCountOriginal(CFG G, string w)
        {
            int n = w.Length;
            int r = G.Rules.Length;

            List<List<List<(string N, ((int ax, int ay, int az) a, (int bx, int by, int bz) b)? p)>>> T = new List<List<List<(string N, ((int ax, int ay, int az) a, (int bx, int by, int bz) b)? p)>>>(n);

            // Word is not empty
            if (n > 0)
            {
                T.Add(new List<List<(string N, ((int ax, int ay, int az) a, (int bx, int by, int bz) b)? p)>>(n));

                // Go through first line
                for (int j = 0; j < n; j++)
                {
                    T[0].Add(new List<(string N, ((int ax, int ay, int az) a, (int bx, int by, int bz) b)? p)>(r));

                    // Check all rules for given character
                    for (int k = 0; k < r; k++)
                    {
                        // Check if given character is part of a rule
                        if (G.Rules[k].Right.Contains(char.ToString(w[j])))
                        {
                            T[0][j].Add((G.Rules[k].Left, null));
                        }
                    }
                }

                // Go through rest of the lines
                for (int i = 1; i < n; i++)
                {
                    T.Add(new List<List<(string N, ((int ax, int ay, int az) a, (int bx, int by, int bz) b)? p)>>(n - i));

                    // Go through given line (the more lines went the less wide they are, which makes triangle shaped table)
                    for (int j = 0; j < n - i; j++)
                    {
                        T[i].Add(new List<(string N, ((int ax, int ay, int az) a, (int bx, int by, int bz) b)? p)>(r));

                        // Check all rules for given cartesian product (AxB)
                        for (int k = 0; k < r; k++)
                        {
                            // Loops all cells for AxB (AxB - cartesian product)
                            for (int l = 0; l < i; l++)
                            {
                                // Loop all nonterminals of A (AxB - cartesian product)
                                for (int m = 0; m < T[l][j].Count; m++)
                                {
                                    // Loop all nonterminals of B (AxB - cartesian product)
                                    for (int o = 0; o < T[i - 1 - l][j + 1 + l].Count; o++)
                                    {
                                        // Brings both nonterminals into one
                                        string checkW = T[l][j][m].N + T[i - 1 - l][j + 1 + l][o].N;

                                        // Check if given nonterminal is part of a rule
                                        if (G.Rules[k].Right.Contains(checkW))
                                        {
                                            T[i][j].Add((G.Rules[k].Left, ((l, j, m), (i - 1 - l, j + 1 + l, o))));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            int treeCount = 0;
            
            for(int i = 0; i < r; i++)
            {
                if(T[n - 1][0][i].N == "S")
                {
                    treeCount++;
                }
            }

            return treeCount;
        }*/

        public static List<List<List<NonterminalWithCount>>> CYK_ParseTableWithCounts(CFG grammar, string word)
        {
            List<List<List<NonterminalWithCount>>> table = new List<List<List<NonterminalWithCount>>>(word.Length);

            // Word is at least 1 character long
            if (word.Length > 0)
            {
                // Create first line
                table.Add(new List<List<NonterminalWithCount>>(word.Length));

                // Go right through the first line
                for (int i = 0; i < word.Length; i++)
                {
                    // Create table cell
                    table[0].Add(new List<NonterminalWithCount>(grammar.Rules.Length));

                    // Go through all rules
                    for (int j = 0; j < grammar.Rules.Length; j++)
                    {
                        // Check whether the rule contains the given character
                        if (grammar.Rules[j].Right.Contains(char.ToString(word[i])))
                        {
                            // Add nonterminal to the selected table cell
                            table[0][i].Add(new NonterminalWithCount(grammar.Rules[j].Left, 1));
                        }
                    }
                }

                // Go up through all lines
                for (int i = 1; i < word.Length; i++)
                {
                    // Create another line
                    table.Add(new List<List<NonterminalWithCount>>(word.Length - i));

                    // Go right through another line
                    for (int j = 0; j < word.Length - i; j++)
                    {
                        // Create table cell
                        table[i].Add(new List<NonterminalWithCount>(grammar.Rules.Length));

                        // Go through all rules
                        for (int k = 0; k < grammar.Rules.Length; k++)
                        {
                            // Go through all table cell pairs
                            for (int l = 0; l < i; l++)
                            {
                                // Go through all nonterminals of A
                                for (int m = 0; m < table[l][j].Count; m++)
                                {
                                    // Go through all nonterminals of B
                                    for (int o = 0; o < table[i - 1 - l][j + 1 + l].Count; o++)
                                    {
                                        // Concatenate nonterminal of A and B (A·B)
                                        string concatenatedNonterminal = table[l][j][m].Nonterminal + table[i - 1 - l][j + 1 + l][o].Nonterminal;

                                        // Check whether the rule contains the concatenated nonterminal
                                        if (grammar.Rules[k].Right.Contains(concatenatedNonterminal))
                                        {
                                            // Multiply counts of concatenated nonterminals
                                            uint concatenatedNonterminalCount = table[l][j][m].NonterminalCount * table[i - 1 - l][j + 1 + l][o].NonterminalCount;

                                            // Find index of nonterminal in the selected table cell
                                            int tableCellNonterminalIndex = table[i][j].FindIndex(nonterminal => nonterminal.Nonterminal == grammar.Rules[k].Left);

                                            // Nonterminal is already in the selected table cell
                                            if (tableCellNonterminalIndex > -1)
                                            {
                                                // Update nonterminal in the selected table cell
                                                table[i][j][tableCellNonterminalIndex].NonterminalCount += concatenatedNonterminalCount;
                                            }
                                            // Nonterminal is not yet in the selected table cell
                                            else
                                            {
                                                // Add nonterminal to the selected table cell
                                                table[i][j].Add(new NonterminalWithCount(grammar.Rules[k].Left, concatenatedNonterminalCount));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // Word is empty
            else
            {
                // Create first line
                table.Add(new List<List<NonterminalWithCount>>(1));

                // Create table cell
                table[0].Add(new List<NonterminalWithCount>(grammar.Rules.Length));

                // Go through all rules
                for (int i = 0; i < grammar.Rules.Length; i++)
                {
                    // Check whether the rule contains the given character
                    if (grammar.Rules[i].Right.Contains("ε"))
                    {
                        // Add nonterminal to the selected table cell
                        table[0][0].Add(new NonterminalWithCount(grammar.Rules[i].Left, 1));
                    }
                }
            }

            return table;
        }

        public static uint CYK_ParseTreeCount(CFG grammar, string word)
        {
            List<List<List<NonterminalWithCount>>> table = CYK_ParseTableWithCounts(grammar, word);

            uint treeCount = 0;

            // Go through all rules
            for (int i = 0; i < table[table.Count - 1][0].Count; i++)
            {
                if (table[table.Count - 1][0][i].Nonterminal == grammar.Start)
                {
                    treeCount = table[table.Count - 1][0][i].NonterminalCount;
                    break;
                }
            }

            return treeCount;
        }

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            ShowExample(new CFG(
                new string[] { "A", "B", "C", "AB", "BA", "BC", "CC", "S" },
                new string[] { "a", "b" },
                new Rule[] {
                    new Rule("S", new string[]{ "AB", "BC" }),
                    new Rule("A", new string[]{ "BA", "a" }),
                    new Rule("B", new string[]{ "CC", "b" }),
                    new Rule("C", new string[]{ "AB", "a" }),
                },
                "S"
            ), "baaba");

            ShowExample(new CFG(
                new string[] { "S", "AA", "A" },
                new string[] { "ε", "a" },
                new Rule[] {
                    new Rule("S", new string[]{ "AA", "ε" }),
                    new Rule("A", new string[]{ "AA", "a" }),
                },
                "S"
            ), "aaaa");

            ShowExample(new CFG(
                new string[] { "S", "AA", "A" },
                new string[] { "ε", "a" },
                new Rule[] {
                    new Rule("S", new string[]{ "AA", "ε" }),
                    new Rule("A", new string[]{ "AA", "a" }),
                },
                "S"
            ), "");

            ShowExample(new CFG(
                new string[] { "S", "AA", "A" },
                new string[] { "ε", "a" },
                new Rule[] {
                    new Rule("S", new string[]{ "AA", "ε" }),
                    new Rule("A", new string[]{ "AA", "a" }),
                },
                "S"
            ), "b");

            ShowExample(new CFG(
                new string[] { "S", "AA", "A" },
                new string[] { "ε", "a" },
                new Rule[] {
                    new Rule("S", new string[]{ "AA", "ε" }),
                    new Rule("A", new string[]{ "AA", "a" }),
                },
                "S"
            ), "aaaaa");

            Console.ReadKey();
        }

        public static void ShowExample(CFG G, string w)
        {
            ResetColors();
            WriteComment("/* INPUT(G, w) : context-free grammar in Chomsky normal form and word");
            WriteComment(" * G (context-free grammar), N (nonterminals), T (terminals), R (rules), S (start)");
            WriteComment(" * w (word) */");
            Console.WriteLine(G);
            Console.WriteLine("w = \"" + string.Join(", ", w) + "\"\n");

            WriteComment("/* OUTPUT(bool) : At least 1 derivation tree? (original CYK)");
            WriteComment(" * CYK (Cocke-Younger-Kasami algorithm) */");

            /* CYK Parse Table */
            WriteFunction("CYK_ParseTable(G, w) : ");
            List<List<List<string>>> parseTable = CYK_ParseTable(G, w);
            for (int i = parseTable.Count - 1; i >= 0; i--)
            {
                if (i < parseTable.Count - 1)
                {
                    Console.Write("                       ");
                }
                for (int j = 0; j < parseTable.Count - i; j++)
                {
                    if (parseTable[i][j].Count > 0)
                    {
                        Console.Write(string.Join(",", parseTable[i][j]).PadRight(16, ' '));
                    }
                    else
                    {
                        Console.Write("-".PadRight(16, ' '));
                    }
                    if (j < parseTable.Count - i - 1)
                    {
                        Console.Write("\t");
                    }
                    else
                    {
                        Console.Write("\n");
                    }
                }
            }

            /* Original CYK Algorithm */
            WriteFunction("CYK(G, w) : ");
            WriteBoolean(CYK(G, w));

            /* CYK Parse Table With Pointers */
            /*Console.Write("CYK_ParseTableWithPointers(G, w) : ");
            List<List<List<(string N, ((int ax, int ay, int az) a, (int bx, int by, int bz) b)? p)>>> parseTableWithPointers = CYK_ParseTableWithPointers(G, w);
            for (int i = w.Length - 1; i >= 0; i--)
            {
                if (i < w.Length - 1)
                {
                    Console.Write("                                   ");
                }
                for (int j = 0; j < w.Length - i; j++)
                {
                    if (parseTableWithPointers[i][j].Count > 0)
                    {
                        string a, b;
                        if(parseTableWithPointers[i][j][0].p != null)
                        {
                            a = parseTableWithPointers[i][j][0].p.Value.a.ax.ToString() + "," + parseTableWithPointers[i][j][0].p.Value.a.ay.ToString() + "," + parseTableWithPointers[i][j][0].p.Value.a.az.ToString();
                            b = parseTableWithPointers[i][j][0].p.Value.b.bx.ToString() + "," + parseTableWithPointers[i][j][0].p.Value.b.by.ToString() + "," + parseTableWithPointers[i][j][0].p.Value.b.bz.ToString();
                            Console.Write(parseTableWithPointers[i][j][0].N + "(" + a + ";" + b + ")");
                        }
                        else
                        {
                            Console.Write(parseTableWithPointers[i][j][0].N);
                        }
                        for (int k = 1; k < parseTableWithPointers[i][j].Count; k++)
                        {
                            if(parseTableWithPointers[i][j][k].p != null)
                            {
                                a = parseTableWithPointers[i][j][k].p.Value.a.ax.ToString() + "," + parseTableWithPointers[i][j][k].p.Value.a.ay.ToString() + "," + parseTableWithPointers[i][j][k].p.Value.a.az.ToString();
                                b = parseTableWithPointers[i][j][k].p.Value.b.bx.ToString() + "," + parseTableWithPointers[i][j][k].p.Value.b.by.ToString() + "," + parseTableWithPointers[i][j][k].p.Value.b.bz.ToString();
                                Console.Write("," + parseTableWithPointers[i][j][k].N + "(" + a + ";" + b + ")");
                            }
                            else
                            {
                                Console.Write("," + parseTableWithPointers[i][j][k].N);
                            }
                        }
                    }
                    else
                    {
                        Console.Write("-");
                    }
                    if(j < w.Length - i - 1)
                    {
                        Console.Write("\t\t");
                    }
                    else
                    {
                        Console.Write("\n");
                    }
                }
            }*/

            /* CYK Parse Tree Count (ver. 1.0) */
            /*Console.Write("CYK_ParseTreeCountOriginal(G, w) : ");
            Console.WriteLine(CYK_ParseTreeCountOriginal(G, w));*/

            WriteComment("\n/* OUTPUT(uint) : Count of different derivation trees. (updated CYK) */");

            /* CYK Parse Table With Counts */
            WriteFunction("CYK_ParseTableWithCounts(G, w) : ");
            List<List<List<NonterminalWithCount>>> parseTableWithCounts = CYK_ParseTableWithCounts(G, w);
            for (int i = parseTableWithCounts.Count - 1; i >= 0; i--)
            {
                if (i < parseTableWithCounts.Count - 1)
                {
                    Console.Write("                                 ");
                }
                for (int j = 0; j < parseTableWithCounts.Count - i; j++)
                {
                    if (parseTableWithCounts[i][j].Count > 0)
                    {
                        Console.Write(string.Join(",", parseTableWithCounts[i][j]).PadRight(16, ' '));
                    }
                    else
                    {
                        Console.Write("-".PadRight(16, ' '));
                    }
                    if (j < parseTableWithCounts.Count - i - 1)
                    {
                        Console.Write("\t");
                    }
                    else
                    {
                        Console.Write("\n");
                    }
                }
            }

            /* CYK Parse Tree Count (ver. 1.1) */
            WriteFunction("CYK_ParseTreeCount(G, w) : ");
            WriteNumber(CYK_ParseTreeCount(G, w));

            Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------------\n");
        }

        public static void WriteComment(string comment)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(comment);
            ResetColors();
        }

        public static void WriteFunction(string function)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(function);
            ResetColors();
        }

        public static void WriteBoolean(bool boolean)
        {
            Console.ForegroundColor = boolean ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed;
            Console.WriteLine(boolean);
            ResetColors();
        }

        public static void WriteNumber(uint number)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(number);
            ResetColors();
        }

        public static void ResetColors()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
        }
    }
}