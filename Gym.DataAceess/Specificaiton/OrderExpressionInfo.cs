using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Gym.DataAceess.Specificaiton;

public class OrderExpressionInfo<TEntity>
{
    public Expression <Func<TEntity, object>> ?OrderExpression { get; set; }
    public bool isDescending { get; set; }
}
