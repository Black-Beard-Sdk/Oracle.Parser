using Bb.Oracle.Parser;
using Antlr4.Runtime.Misc;
using Bb.Oracle.Models;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using System;
using Bb.Oracle.Exceptions;
using Bb.Oracle.Helpers;
using Bb.Oracle.Structures.Models;

namespace Bb.Oracle.Visitors
{

    public partial class ConvertScriptToModelVisitor
    {

        public override object VisitPackage_obj_body([NotNull] PlSqlParser.Package_obj_bodyContext context)
        {
            Stop();
            return base.VisitPackage_obj_body(context);
        }

        public override object VisitInvoker_rights_clause([NotNull] PlSqlParser.Invoker_rights_clauseContext context)
        {
            Stop();
            return base.VisitInvoker_rights_clause(context);
        }

        public override object VisitDrop_package([NotNull] PlSqlParser.Drop_packageContext context)
        {
            Stop();
            return base.VisitDrop_package(context);
        }

        public override object VisitAlter_package([NotNull] PlSqlParser.Alter_packageContext context)
        {
            Stop();
            return base.VisitAlter_package(context);
        }

        public override object VisitCreate_package([NotNull] PlSqlParser.Create_packageContext context)
        {

            if (context.exception != null)
            {
                AppendException(context.exception);
                return null;
            }

            var schema = context.schema_object_name().GetCleanedName();
            var package_name = context.package_name()[0].GetCleanedName();
            PackageModel package = this.Db.Packages.FirstOrDefault(c => c.GetOwner() == schema && c.GetName() == package_name);
            if (package == null)
            {

                var txt = GetText(context).ToString().Trim();
                if (txt.StartsWith("CREATE"))
                    txt = txt.Substring(6).Trim();

                package = new PackageModel()
                {
                    Key = schema + "." + package_name,
                    Owner = schema,
                    Name = package_name,
                };

                package.Code.Code = txt;

                package.Files.Add(GetFileElement(context.Start));
                this.Db.Packages.Add(package);

            }
            else
            {
                AppendException(new AmbiguousObjectException($"{schema}.{package_name}"), context);
            }


            using (Enqueue(package))
            {

                var r = base.VisitCreate_package(context);

                return r;

            }
        }

        public override object VisitCreate_package_body([NotNull] PlSqlParser.Create_package_bodyContext context)
        {
            Stop();
            return base.VisitCreate_package_body(context);
        }

        public override object VisitCreate_function_body([NotNull] PlSqlParser.Create_function_bodyContext context)
        {
            Stop();
            return base.VisitCreate_function_body(context);
        }

    }

}


//public override object VisitPackage_obj_spec([NotNull] PlSqlParser.Package_obj_specContext context)
//{
//    Stop();
//    return base.VisitPackage_obj_spec(context);
//}

