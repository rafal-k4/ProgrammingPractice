using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace PracticeCase.StudyCases
{
    class Expressions : IStudy
    {
        public void Execute()
        {
            Expression<Func<int>> number = () => 2;

            Console.WriteLine(
                $"Body: {number.Body} {Environment.NewLine}" +
                $"Type: {number.Type} {Environment.NewLine}" +
                $"NodeType: {number.NodeType} {Environment.NewLine}" +
                $"Name: {number.Name} {Environment.NewLine}" +
                $"Parameters: {number.Parameters} {Environment.NewLine}" +
                $"ReturnType: {number.ReturnType} {Environment.NewLine}" +
                $"TailCall: {number.TailCall}" +
                $"{Environment.NewLine} {Environment.NewLine} {Environment.NewLine}" +
                $"Result: {number.Compile()()}"
                );
        }
    }
}
