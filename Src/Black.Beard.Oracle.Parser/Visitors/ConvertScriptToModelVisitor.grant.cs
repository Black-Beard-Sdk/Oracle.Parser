using Bb.Oracle.Parser;
using Antlr4.Runtime.Misc;
using Bb.Oracle.Models;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using System;

namespace Bb.Oracle.Visitors
{

    public partial class ConvertScriptToModelVisitor
    {

        public override object VisitRevoke_object_privileges([NotNull] PlSqlParser.Revoke_object_privilegesContext context)
        {
            return base.VisitRevoke_object_privileges(context);
        }

        public override object VisitGrant_object_name([NotNull] PlSqlParser.Grant_object_nameContext context)
        {
            return base.VisitGrant_object_name(context);
        }

        public override object VisitRevoke_statment([NotNull] PlSqlParser.Revoke_statmentContext context)
        {
            
            return base.VisitRevoke_statment(context);
        }

        public override object VisitRevoke_system_privileges([NotNull] PlSqlParser.Revoke_system_privilegesContext context)
        {
            return base.VisitRevoke_system_privileges(context);
        }

        public override object VisitGrant_statement([NotNull] PlSqlParser.Grant_statementContext context)
        {

            if (context.exception != null)
            {
                AppendException(context.exception);
                return null;
            }

            string _schema = string.Empty;
            string _object = string.Empty;
            string key;

            var objectPrivileges = context.object_privilege();
            var system_privileges = context.system_privilege();
            var role_names = context.role_name();

            var grant_object_name = context.grant_object_name();

            var tableView = grant_object_name.tableview_name();
            var user_object = grant_object_name.user_object_name();
            var dir_object = grant_object_name.dir_object_name();
            var shema_object = grant_object_name.schema_object_name();

            if (tableView != null)
            {
                _schema = tableView.GetText();
                var i = _schema.IndexOf('.');
                if (i > -1)
                {
                    _object = _schema.Substring(i + 1, _schema.Length - i - 1);
                    _schema = _schema.Substring(0, i);
                }
                _schema = _schema.Replace(@"""", "");
                _object = _object.Replace(@"""", "");

            }

            var grantee_names = context.grantee_name();
            var container_clause = context.container_clause();

            bool withAdmin = context.ADMIN() != null;
            bool withDelegate = context.DELEGATE() != null;
            bool withHierarchy = context.HIERARCHY() != null;
            bool withGrant = context.GRANT().Length == 2 && context.OPTION().Length == 1;

            var current = container_clause?.CURRENT();
            var allt = container_clause?.ALL();


            HashSet<string> _privileges = new HashSet<string>();

            foreach (var system_privilege in system_privileges)
                _privileges.Add(system_privilege.GetText());

            foreach (var objectPrivilege in objectPrivileges)
                _privileges.Add(objectPrivilege.GetText());

            foreach (var role_name in role_names)
                _privileges.Add(role_name.GetText());

            _privileges = new HashSet<string>(_privileges.OrderBy(c => c));

            foreach (var grantee_name in grantee_names)
            {

                string _grantee_name = grantee_name.GetText();

                if (tableView != null)
                    ParseTableView(context, _schema, _object, _privileges, _grantee_name, withHierarchy, withGrant, withDelegate, withAdmin);

                else if (dir_object != null)
                {

                    Stop();

                    key = _schema + "." + _object + "_to_" + dir_object.GetText();
                    CreateGrant(key, string.Empty, dir_object.GetText(), _privileges, _grantee_name, string.Empty, withHierarchy, withGrant, withDelegate, withAdmin, true, false, false, false, false, false, false);

                }
                else if (user_object.Length > 0)
                {
                    Stop();
                    foreach (var user_object_name in user_object)
                    {
                        key = user_object_name.GetText() + "." + "_to_" + dir_object.GetText();
                        CreateGrant(key, user_object_name.GetText(), string.Empty, _privileges, _grantee_name, string.Empty, withHierarchy, withGrant, withDelegate, withAdmin, false, true, false, false, false, false, false);
                    }
                }
                else if (shema_object != null)
                {

                    key = shema_object.GetText() + "." + "_to_" + dir_object.GetText();

                    if (grant_object_name.EDITION() != null)
                    {
                        Stop();
                        CreateGrant(key, shema_object.GetText(), string.Empty, _privileges, _grantee_name, string.Empty, withHierarchy, withGrant, withDelegate, withAdmin, false, false, true, false, false, false, false);
                    }
                    else if (grant_object_name.MINING() != null)
                    {
                        Stop();
                        CreateGrant(key, shema_object.GetText(), string.Empty, _privileges, _grantee_name, string.Empty, withHierarchy, withGrant, withDelegate, withAdmin, false, false, false, true, false, false, false);
                    }
                    else if (grant_object_name.JAVA() != null)
                    {
                        Stop();
                        CreateGrant(key, shema_object.GetText(), string.Empty, _privileges, _grantee_name, string.Empty, withHierarchy, withGrant, withDelegate, withAdmin, false, false, false, false, grant_object_name.SOURCE() != null, grant_object_name.RESOURCE() != null, false);
                    }
                    else if (grant_object_name.SQL() != null && grant_object_name.TRANSLATION() != null && grant_object_name.PROFILE() != null)
                    {
                        Stop();
                        CreateGrant(key, shema_object.GetText(), string.Empty, _privileges, _grantee_name, string.Empty, withHierarchy, withGrant, withDelegate, withAdmin, false, false, false, false, false, false, true);
                    }
                    else
                    {
                        Stop();
                    }
                }


            }

            return null;

        }

        private void ParseTableView(PlSqlParser.Grant_statementContext context, string _schema, string _object, HashSet<string> privileges, string _grantee_name, bool withHierarchy, bool withGrant, bool withDelegate, bool withAdmin)
        {

            string key;

            var c = context.paren_column_list();
            if (c.Length > 0)
            {
                foreach (var item in c)
                {
                    string col = item.GetText();
                    key = _schema + "." + _object + "." + col + "_to_" + _grantee_name;
                    CreateGrant(key, _schema, _object, privileges, _grantee_name, col, withHierarchy, withGrant, withDelegate, withAdmin, false, false, false, false, false, false, false);
                }
            }
            else
            {

                key = _schema + "." + _object + "_to_" + _grantee_name;

                CreateGrant(key, _schema, _object, privileges, _grantee_name, string.Empty, withHierarchy, withGrant, withDelegate, withAdmin, false, false, false, false, false, false, false);

            }
        }

        private GrantModel CreateGrant(string key, string objectSchema, string objectName, HashSet<string> privileges, string role, string columnObjectName, bool withHierarchy, bool withGrant, bool withDelegate, bool withAdmin, bool withDirectory, bool withUser, bool withEdition, bool withMiningModel, bool withJavaSource, bool JavaResource, bool withSqlTranslationProfile)
        {

            GrantModel grant;
            if (!this.db.Grants.TryGet(key, out grant))
            {

                string fullObjectName = !string.IsNullOrEmpty(columnObjectName) ? @"""" + columnObjectName + @"""" : string.Empty;

                if (!string.IsNullOrEmpty(objectName))
                {
                    if (!string.IsNullOrEmpty(fullObjectName))
                        fullObjectName = "." + fullObjectName;
                    fullObjectName = @"""" + objectName + @"""" + fullObjectName;
                }

                if (!string.IsNullOrEmpty(objectSchema))
                {
                    if (!string.IsNullOrEmpty(fullObjectName))
                        fullObjectName = "." + fullObjectName;
                    fullObjectName = @"""" + objectSchema + @"""" + fullObjectName;
                }

                grant = new GrantModel()
                {

                    Key = key,

                    FullObjectName = fullObjectName,
                    Role = role,

                    ObjectSchema = objectSchema,
                    ObjectName = objectName,
                    ColumnObjectName = columnObjectName,

                    Grantable = withGrant,
                    Hierarchy = withHierarchy,

                    //Delegate = withDelegate,
                    //Admin = withAdmin,
                    //Directory = withDirectory,
                    //User = withUser,
                    //Edition = withEdition,
                    //JavaSource = withJavaSource
                    //JavaResource = withJavaResource,
                    //SqlTranslationProfile = withSqlTranslationProfile,

                };

                this.db.Grants.Add(grant);

            }

            //Delegate = withDelegate,
            //Admin = withAdmin,
            //Directory = withDirectory,
            //User = withUser,
            //Edition = withEdition,
            //JavaSource = withJavaSource
            //JavaResource = withJavaResource,
            //SqlTranslationProfile = withSqlTranslationProfile,

            if (!string.IsNullOrEmpty(this.File))
                grant.Files.AddIfNotExist(new FileElement() { Path = this.File });

            if (grant.Files.Count > 0)
                grant.File = grant.Files.FirstOrDefault();

            foreach (var privilege in privileges)
                grant.Privileges.Add(privilege);

            return grant;

        }
    }

}

//public override object VisitDir_object_name([NotNull] PlSqlParser.Dir_object_nameContext context)
//{
//    Stop();
//    return base.VisitDir_object_name(context);
//}

//public override object VisitGrantee_name([NotNull] PlSqlParser.Grantee_nameContext context)
//{
//    Stop();
//    return base.VisitGrantee_name(context);
//}

//public override object VisitGrant_object_name([NotNull] PlSqlParser.Grant_object_nameContext context)
//{
//    Stop();
//    return base.VisitGrant_object_name(context);
//}

//public override object VisitObject_privilege([NotNull] PlSqlParser.Object_privilegeContext context)
//{
//    Stop();
//    return base.VisitObject_privilege(context);
//}

//public override object VisitSchema_object_name([NotNull] PlSqlParser.Schema_object_nameContext context)
//{
//    Stop();
//    return base.VisitSchema_object_name(context);
//}

//public override object VisitSystem_privilege([NotNull] PlSqlParser.System_privilegeContext context)
//{
//    Stop();
//    return base.VisitSystem_privilege(context);
//}

//public override object VisitTableview_name([NotNull] PlSqlParser.Tableview_nameContext context)
//{
//    Stop();
//    return base.VisitTableview_name(context);
//}

//public override object VisitUser_object_name([NotNull] PlSqlParser.User_object_nameContext context)
//{
//    Stop();
//    return base.VisitUser_object_name(context);
//}
