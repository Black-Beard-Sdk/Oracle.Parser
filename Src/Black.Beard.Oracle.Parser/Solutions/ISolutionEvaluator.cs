using Antlr4.Runtime.Tree;

namespace Bb.Oracle.Solutions
{

    public interface ISolutionEvaluator
    {
        void Visit<T>(IParseTreeVisitor<T> visitor);

    }

}