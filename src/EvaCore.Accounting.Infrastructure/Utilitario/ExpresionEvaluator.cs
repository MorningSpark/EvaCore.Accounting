using System;
using System.Text;

namespace EvaCore.Accounting.Infrastructure.Utilitario;

public class ExpresionEvaluator : IExpresionEvaluator
{
    public async Task<decimal> Evaluate(string expression, decimal value)
    {
        return await Task.Run(() =>
        {
            if (string.IsNullOrEmpty(expression))
                return 0;
            expression = expression.Replace("x", value.ToString());
            StringBuilder expressionBuilder = new StringBuilder(expression);
            string rpnExpression = ConvertInfixToRPN(expression);
            double result = EvaluateRPN(rpnExpression);
            return (decimal)result;
        });
    }

    private string ConvertInfixToRPN(string expression)
    {
        Dictionary<char, int> precedence = new Dictionary<char, int>
        {
            { '+', 1 }, { '-', 1 },
            { '*', 2 }, { '/', 2 }
        };

        Stack<char> operators = new Stack<char>();
        List<string> output = new List<string>();
        string numberBuffer = "";

        foreach (char token in expression.Replace(" ", ""))
        {
            if (char.IsDigit(token) || token == '.')
            {
                numberBuffer += token;
            }
            else
            {
                if (!string.IsNullOrEmpty(numberBuffer))
                {
                    output.Add(numberBuffer);
                    numberBuffer = "";
                }

                if (token == '(')
                {
                    operators.Push(token);
                }
                else if (token == ')')
                {
                    while (operators.Count > 0 && operators.Peek() != '(')
                        output.Add(operators.Pop().ToString());

                    operators.Pop(); // Quitar '('
                }
                else if (precedence.ContainsKey(token))
                {
                    while (operators.Count > 0 && precedence.ContainsKey(operators.Peek()) &&
                           precedence[token] <= precedence[operators.Peek()])
                    {
                        output.Add(operators.Pop().ToString());
                    }
                    operators.Push(token);
                }
            }
        }

        if (!string.IsNullOrEmpty(numberBuffer))
        {
            output.Add(numberBuffer);
        }

        while (operators.Count > 0)
        {
            output.Add(operators.Pop().ToString());
        }

        return string.Join(" ", output);
    }

    private double EvaluateRPN(string expression)
    {
        Stack<double> stack = new Stack<double>();
        string[] tokens = expression.Split(' ');

        foreach (string token in tokens)
        {
            if (double.TryParse(token, out double number))
            {
                stack.Push(number);
            }
            else
            {
                double b = stack.Pop();
                double a = stack.Pop();
                double tempResult = token switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => a / b,
                    _ => throw new InvalidOperationException($"Operador desconocido: {token}")
                };
                stack.Push(tempResult);
            }
        }

        return stack.Pop();
    }
}
