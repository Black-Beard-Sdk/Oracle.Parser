using System.IO;
using Bb.Oracle.Models;

namespace Bb.Oracle.Solutions
{
    public interface IFilePropertyResolver
    {
        string ResolveSchema(FileInfo c);
        SqlKind ResolveKind(FileInfo c);
        int ResolvePriority(SqlKind file);
    }

}
