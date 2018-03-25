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
using System.Diagnostics;
using System.Text;

namespace Bb.Oracle.Visitors
{

    public partial class ConvertScriptToModelVisitor
    {


        /// <summary>
        /// package_obj_spec :
        ///       variable_declaration
        ///     | subtype_declaration
        ///     | cursor_declaration
        ///     | exception_declaration
        ///     | pragma_declaration
        ///     | type_declaration
        ///     | procedure_spec
        ///     | function_spec
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitPackage_obj_spec([NotNull] PlSqlParser.Package_obj_specContext context)
        {
            var result = base.VisitPackage_obj_spec(context);
            Debug.Assert(result != null);
            return result;
        }


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

        /// <summary>
        /// create_package :
	    ///     CREATE(OR REPLACE)? PACKAGE(schema_object_name '.')? package_name invoker_rights_clause? (IS | AS) package_obj_spec* END package_name? ';'
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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

                var invoker_rights_clause = context.invoker_rights_clause();

                var package_obj_specs = context.package_obj_spec();
                foreach (var package_obj_spec in package_obj_specs)
                {

                    var r = (OracleObject)this.VisitPackage_obj_spec(package_obj_spec);

                    

                    StringBuilder text = GetText(package_obj_spec);



                    Stop();

                }

            }

            return package;

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

