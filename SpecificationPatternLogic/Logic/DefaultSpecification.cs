﻿using SpecificationPatternLogic.Entities;
using System;
using System.Linq.Expressions;

namespace SpecificationPatternLogic.Logic
{
    public class DefaultSpecification : SpecificationBase<Movie>
    {
        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => true;
        }
    }
}
