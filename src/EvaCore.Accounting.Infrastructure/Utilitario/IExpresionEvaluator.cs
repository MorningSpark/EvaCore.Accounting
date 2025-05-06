using System;

namespace EvaCore.Accounting.Infrastructure.Utilitario;

public interface IExpresionEvaluator
{
    Task<decimal> Evaluate(string expression, decimal value);
}
