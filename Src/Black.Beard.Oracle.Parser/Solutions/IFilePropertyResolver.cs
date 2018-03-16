﻿using System.IO;
using Bb.Oracle.Models;

namespace Bb.Oracle.Solutions
{
    public interface IFilePropertyResolver
    {
        string ResolveSchema(FileInfo c);
        KindModelEnum ResolveKind(FileInfo c);
        int ResolvePriority(KindModelEnum file);
    }

}
