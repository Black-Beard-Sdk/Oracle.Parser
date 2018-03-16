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

        public KindModelEnum ResolveKind(FileInfo file)
        {
            var type = file.Directory.Name;
            return GetKind(type);
        }

        public int ResolvePriority(KindModelEnum kind)
        {
            return ResolvePriority_Impl(kind);
        }

        public string ResolveSchema(FileInfo file)
        {
            return file.Directory.Parent.Name?.ToUpper() ?? string.Empty;
        }

        private KindModelEnum GetKind(string expectedType)
        {

            switch (expectedType.ToUpper())
            {
                case Constants.Functions:
                    return KindModelEnum.Function;

                case Constants.Indexes:
                    return KindModelEnum.Index;

                case Constants.PackageBodies:
                    return KindModelEnum.PackageBodies;

                case Constants.Packages:
                    return KindModelEnum.Package;

                case Constants.Procedures:
                    return KindModelEnum.Procedure;

                case Constants.Synonyms:
                case Constants.PublicSynonyms:
                    return KindModelEnum.Synonym;

                case Constants.Sequences:
                    return KindModelEnum.Sequence;

                case Constants.Tables:
                    return KindModelEnum.Table;

                case Constants.Triggers:
                    return KindModelEnum.Trigger;
                    
                case Constants.AdvancedQueue:
                case Constants.UserObjectPrivileges:
                    return KindModelEnum.UserObjectPrivilege;

                case Constants.Views:
                    return KindModelEnum.View;

                case Constants.Types:
                    return KindModelEnum.Type;

                case Constants.MaterializedViews:
                    return KindModelEnum.MaterializedView;

                case Constants.Jobs:
                    return KindModelEnum.Jobs;

                default:
                    if (System.Diagnostics.Debugger.IsAttached)
                        System.Diagnostics.Debugger.Break();
                    return KindModelEnum.NotImplemented;
            }

        }

        protected virtual int ResolvePriority_Impl(KindModelEnum expectedKind)
        {

            int priority = 1000;

            switch (expectedKind)
            {

                case KindModelEnum.Table:
                case KindModelEnum.Sequence:
                    priority = 0;
                    break;

                case KindModelEnum.Index:
                case KindModelEnum.Trigger:
                case KindModelEnum.Package:
                case KindModelEnum.Function:
                case KindModelEnum.Procedure:
                case KindModelEnum.View:
                    priority = 10;
                    break;

                case KindModelEnum.PackageBodies:
                    priority = 20;
                    break;

                case KindModelEnum.MaterializedView:
                    break;
                case KindModelEnum.MaterializedViewLog:
                    break;
                case KindModelEnum.UserObjectPrivilege:
                    break;
                case KindModelEnum.Synonym:
                    break;

            }

            return priority;

        }


    }

}



/*



 
     
        

 */
