using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace лаба_5
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите 2 строки:");
                string s1 = Console.ReadLine();
                string s2 = Console.ReadLine();
                Reverse_strings(s1, s2);
                Console.WriteLine("Введите префиксное выражение:");
                string input_prefix = Console.ReadLine();
                Prefix(input_prefix);
                Max_Min();
                BackSpace();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void Reverse_strings(string s1, string s2)
        {
            Stack<char> stack = new Stack<char>();
            foreach (char ch in s1)
            {
                stack.Push(ch);
            }
            bool status = true;
            for (int i = 0; i < stack.Count; i++)
            {
                if (s2[i] != stack.Pop())
                {
                    status = false;
                    break;
                }
                i++;
            }
            if (status == true) { Console.WriteLine("1. Строки обратны друг другу"); }
            else { Console.WriteLine("1. Строки не обратны друг другу"); }
        }
        public static void Prefix(string input)
        {
            string[] mas = input.Split(' ');
            Stack<Double> Stack = new Stack<Double>();
            for (int i = mas.Length - 1; i >= 0; i--)
            {
                double number;
                if (double.TryParse(mas[i], out number))
                {
                    Stack.Push(number);
                }
                else
                {
                    double oper1 = Stack.Peek();
                    Stack.Pop();
                    double oper2 = Stack.Peek();
                    Stack.Pop();
                    switch (mas[i])
                    {
                        case "+":
                            Stack.Push(oper1 + oper2);
                            break;
                        case "-":
                            Stack.Push(oper1 - oper2);
                            break;
                        case "*":
                            Stack.Push(oper1 * oper2);
                            break;
                        case "/":
                            Stack.Push(oper1 / oper2);
                            break;
                    }
                }
            }
            Console.WriteLine("Значение выражения: {0}", Stack.Peek());
        }
        public static void Max_Min()
        {
            StreamReader reader = new StreamReader("text.txt");
            string text = reader.ReadToEnd().Trim();
            Stack<Double> Stack = new Stack<Double>();
            Stack<char> charStack = new Stack<char>();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == 'M' || text[i] == 'm')
                {
                    charStack.Push(text[i]);
                }
                if (text[i] >= '0' && text[i] <= '9')
                {
                    int num = text[i] - '0';
                    Stack.Push(num);
                }
                else if (text[i] == ')')
                {
                    double oper1 = Convert.ToDouble(Stack.Peek());
                    Stack.Pop();
                    double oper2 = Convert.ToDouble(Stack.Peek());
                    Stack.Pop();
                    char process = charStack.Peek();
                    charStack.Pop();
                    switch (process)
                    {
                        case 'M':
                            Stack.Push(Math.Max(oper1, oper2));
                            break;
                        case 'm':
                            Stack.Push(Math.Min(oper1, oper2));
                            break;
                        default:
                            throw new Exception("File is not correct");
                    }
                }
            }
            Console.WriteLine(Stack.Peek());
            reader.Close();
        }
        public static void BackSpace()
        {
            StreamReader reader = new StreamReader("text_backspace.txt");
            Stack<char> stack = new Stack<char>();
            string text = reader.ReadToEnd();
            foreach (char i in text)
            {
                if (i == '#' && stack.Count > 0)
                {
                    stack.Pop();
                }
                else if (i != '#')
                {
                    stack.Push(i);
                }
            }
            foreach(char i in stack.ToArray().Reverse())
                {
                Console.Write(i);
            }
            reader.Close();
        }
    }
}

