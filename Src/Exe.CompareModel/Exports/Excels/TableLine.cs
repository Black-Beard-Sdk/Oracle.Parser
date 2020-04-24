using Bb.Oracle.Models;
using Bb.Oracle.Structures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareModel.Exports.Excels
{

    public class TableLine : SheetBase
    {

        public const string Name = "Tables";

        public TableLine(params object[] args)
            : base(Name, args)
        {

        }

        public TableLine(ItemBase source, ItemBase target, string kind, string schemaName, string tableName, string propertyChanged)
            : base(source, target, Name, kind, schemaName, tableName, propertyChanged)
        {

        }

    }

    public class ViewLine : SheetBase
    {

        public const string Name = "Views";

        public ViewLine(params object[] args)
            : base(Name, args)
        {

        }

        public ViewLine(ItemBase source, ItemBase target, string kind, string schemaName, string tableName, string propertyChanged)
            : base(source, target, Name, kind, schemaName, tableName, propertyChanged)
        {

        }

    }

    public class ColumnLine : SheetBase
    {

        public const string Name = "Columns";

        public ColumnLine(params object[] args)
            : base(Name, args)
        {

        }
        public ColumnLine(ItemBase source, ItemBase target, string kind, string schemaName, string tableName, string column, string propertyChanged)
            : base(source, target, Name, kind, schemaName, tableName, column, propertyChanged)
        {

        }

    }

    public class ConstraintLine : SheetBase
    {

        public const string Name = "Constraints";

        public ConstraintLine(params object[] args)
            : base(Name, args)
        {

        }

        public ConstraintLine(ItemBase source, ItemBase target, string kind, string schemaName, string tableName, string column, string propertyChanged, string targetName, string Type, string RelOwner, string RelName, string sql1, string sql2)
            : base(source, target, Name, kind, schemaName, tableName, column, propertyChanged, targetName, Type, RelOwner, RelName, sql1, sql2)
        {

        }
    }

    public class IndexLine : SheetBase
    {

        public const string Name = "Indexes";

        public IndexLine(params object[] args)
            : base(Name, args)
        {

        }

        public IndexLine(ItemBase source, ItemBase target, string kind, string schemaName, string tableName, string indexName, string propertyChanged)
            : base(source, target, Name, kind, schemaName, tableName, indexName, propertyChanged)
        {

        }
    }

    public class TriggerLine : SheetBase
    {

        public const string Name = "Triggers";

        public TriggerLine(params object[] args)
            : base(Name, args)
        {

        }

        public TriggerLine(ItemBase source, ItemBase target, string kind, string schemaName, string tableName, string triggerName, string propertyChanged)
            : base(source, target, Name, kind, schemaName, tableName, triggerName, propertyChanged)
        {

        }
    }

    public class GrantLine : SheetBase
    {

        public const string Name = "Grants";

        public GrantLine(params object[] args)
            : base(Name, args)
        {

        }
        // // 1  - "kind"
        // 2  - "Full object name"
        // 3  - "Role / Grantee"
        // 4  - "Privilege"
        // 5  - "Grantable"
        // 6  - "sql"
        // 7  - Constants.Source
        // 8  - Constants.LastCommiter
        // 9  - Constants.Target
        // 10 - Constants.LastCommiter

        public GrantLine(ItemBase source, ItemBase target, string kind, string fullObjectName, string role, string[] privileges, string grantable, string sql, string _source, string lastCommiterSource, string fileSource, string _target, string lastCommiterTarget, string fileTarget)
            : base(source, target, Name, kind, fullObjectName, role, privileges, grantable, sql, _source, lastCommiterSource, fileSource, _target, lastCommiterTarget, fileTarget)
        {

        }
    }

    public class ProcedureLine : SheetBase
    {

        public const string Name = "Procedures";

        public ProcedureLine(params object[] args)
            : base(Name, args)
        {

        }

        public ProcedureLine(ItemBase source, ItemBase target, string kind, string SchemaName, string PackageName, string Name, string args)
            : base(source, target, Name, kind, SchemaName, PackageName, Name, args)
        {

        }
    }

    public class SequenceLine : SheetBase
    {

        public const string Name = "Sequences";

        public SequenceLine(params object[] args)
            : base(Name, args)
        {

        }

        public SequenceLine(ItemBase source, ItemBase target, string kind, string SchemaName, string PackageName, string Name, string changes)
            : base(source, target, Name, kind, SchemaName, PackageName, Name, changes)
        {

        }
    }

    public class SynonymLine : SheetBase
    {

        public const string Name = "Synonymes";

        public SynonymLine(params object[] args)
            : base(Name, args)
        {

        }

        public SynonymLine(ItemBase source, ItemBase target, string kind, string SchemaName, string Name, string ObjectTarget)
            : base(source, target, Name, kind, SchemaName, Name, ObjectTarget)
        {

        }
    }

    public class TypeLine : SheetBase
    {

        public const string Name = "Types";

        public TypeLine(params object[] args)
            : base(Name, args)
        {

        }

        public TypeLine(ItemBase source, ItemBase target, string kind, string SchemaName, string PackageName, string Name, string CollectionSchemaName, string CollectionTypeName)
            : base(source, target, Name, kind, SchemaName, PackageName, Name, CollectionSchemaName, CollectionTypeName)
        {

        }
    }

    public class PackageLine : SheetBase
    {

        public const string Name = "Packages";

        public PackageLine(params object[] args)
            : base(Name, args)
        {

        }

        public PackageLine(ItemBase source, ItemBase target, string kind, string SchemaName, string PackageName)
            : base(source, target, Name, kind, SchemaName, PackageName)
        {

        }
    }

    public class PackageBodyLine : SheetBase
    {

        public const string Name = "PackageBodies";

        public PackageBodyLine(params object[] args)
            : base(Name, args)
        {

        }

        public PackageBodyLine(ItemBase source, ItemBase target, string kind, string SchemaName, string PackageName)
            : base(source, target, Name, kind, SchemaName, PackageName)
        {

        }

    }

    public class DuplicateObjectsyLine : SheetBase
    {

        public const string Name = "duplicated objects";

        public DuplicateObjectsyLine(params object[] args)
            : base(Name, args)
        {

        }

        public DuplicateObjectsyLine(ItemBase source, ItemBase target, string name, string type, params string[] files)
            : base(source, target, Name, GetColumns(name, type, files))
        {

        }

        private static object[] GetColumns(string name, string type, string[] files)
        {

            List<object> _lst = new List<object>(files.Length + 2);

            _lst.Add(name);
            _lst.Add(name);
            _lst.AddRange(files);

            return _lst.ToArray();

        }

    }

}
