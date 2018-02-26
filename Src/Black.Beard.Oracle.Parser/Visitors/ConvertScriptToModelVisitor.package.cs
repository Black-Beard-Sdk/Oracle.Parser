using Bb.Oracle.Parser;
using Antlr4.Runtime.Misc;
using Bb.Oracle.Models;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using System;
using Bb.Oracle.Exceptions;

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

            var schema = CleanName(context.schema_object_name().GetText());
            var package_name = CleanName(context.package_name().Select(c => c.GetText()).Last());
            PackageModel package = this.db.Packages.FirstOrDefault(c => c.GetOwner() == schema && c.GetName() == package_name);
            if (package == null)
            {

                var txt = GetText(context).ToString().Trim();
                if (txt.StartsWith("CREATE"))
                    txt = txt.Substring(6).Trim();

                package = new PackageModel()
                {
                    Name = schema + "." + package_name,
                    PackageOwner = schema,
                    PackageName = package_name,
                    Code = txt,
                    CodeBody = string.Empty,
                };

                package.Files.Add(GetFileElement(context.Start));
                this.db.Packages.Add(package);

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



        private string CleanName(string name)
        {
            if (string.IsNullOrEmpty(name))
                name = string.Empty;
            else
                name = name.Trim().Trim('"').Trim();
            return name;
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


//public override object VisitPackage_name([NotNull] PlSqlParser.Package_nameContext context)
//{
//    Stop();
//    return base.VisitPackage_name(context);
//}
