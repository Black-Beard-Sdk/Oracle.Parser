using Bb.Oracle.Contracts;

namespace Bb.Oracle.Models
{
    public interface IScriptChangeEvaluator
    {

        void Generate(IFileManager fileManager, IchangeVisitor visitor, IEvaluateManager evaluator);

    }
}