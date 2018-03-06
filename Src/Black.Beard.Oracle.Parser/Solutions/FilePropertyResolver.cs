using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bb.Oracle.Models;

namespace Bb.Oracle.Solutions
{

    public class FilePropertyResolver : IFilePropertyResolver
    {

        public SqlKind ResolveKind(FileInfo file)
        {
            var type = file.Directory.Name;
            return GetKind(type);
        }

        public int ResolvePriority(SqlKind kind)
        {
            return ResolvePriority_Impl(kind);
        }

        public string ResolveSchema(FileInfo file)
        {
            return file.Directory.Parent.Name?.ToUpper() ?? string.Empty;
        }

        private SqlKind GetKind(string expectedType)
        {

            switch (expectedType.ToUpper())
            {
                case Constants.Functions:
                    return SqlKind.Function;

                case Constants.Indexes:
                    return SqlKind.Index;

                case Constants.PackageBodies:
                    return SqlKind.PackageBodies;

                case Constants.Packages:
                    return SqlKind.Package;

                case Constants.Procedures:
                    return SqlKind.Procedure;

                case Constants.Synonyms:
                case Constants.PublicSynonyms:
                    return SqlKind.Synonym;

                case Constants.Sequences:
                    return SqlKind.Sequence;

                case Constants.Tables:
                    return SqlKind.Table;

                case Constants.Triggers:
                    return SqlKind.Trigger;
                    
                case Constants.AdvancedQueue:
                case Constants.UserObjectPrivileges:
                    return SqlKind.UserObjectPrivilege;

                case Constants.Views:
                    return SqlKind.View;

                case Constants.Types:
                    return SqlKind.Type;

                case Constants.MaterializedViews:
                    return SqlKind.MaterializedView;

                case Constants.Jobs:
                    return SqlKind.Jobs;

                default:
                    if (System.Diagnostics.Debugger.IsAttached)
                        System.Diagnostics.Debugger.Break();
                    return SqlKind.NotImplemented;
            }

        }

        protected virtual int ResolvePriority_Impl(SqlKind expectedKind)
        {

            int priority = 1000;

            switch (expectedKind)
            {

                case SqlKind.Table:
                case SqlKind.Sequence:
                    priority = 0;
                    break;

                case SqlKind.Index:
                case SqlKind.Trigger:
                case SqlKind.Package:
                case SqlKind.Function:
                case SqlKind.Procedure:
                case SqlKind.View:
                    priority = 10;
                    break;

                case SqlKind.PackageBodies:
                    priority = 20;
                    break;

                case SqlKind.MaterializedView:
                    break;
                case SqlKind.MaterializedViewLog:
                    break;
                case SqlKind.UserObjectPrivilege:
                    break;
                case SqlKind.Synonym:
                    break;

            }

            return priority;

        }


    }

}



/*



 
     
        

 */
