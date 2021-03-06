﻿using Bb.Oracle.Parser;
using Antlr4.Runtime.Misc;
using Bb.Oracle.Models;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using System;
using Bb.Oracle.Structures.Models;
using Bb.Oracle.Models.Codes;

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
                //this._initialSource = new StringBuilder(context.Start.InputStream.ToString());
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
            PlSqlParser.User_object_nameContext[] user_object = grant_object_name.user_object_name();
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

            HashSet<string> _grantees = new HashSet<string>(grantee_names.Select(c => c.GetText()).ToList());
            var _public = context.PUBLIC();
            if (_public != null)
            {
                foreach (var item in _public)
                {
                    var ii = item.GetText();
                    _grantees.Add(ii.ToUpper());
                }
            }

            foreach (var _grantee_name in _grantees)
            {

                if (tableView != null)
                    ParseTableView(context, _schema, _object, _privileges, _grantee_name, withHierarchy, withGrant, withDelegate, withAdmin);

                else if (dir_object != null)
                {

                    Stop();

                    key = _schema + "." + _object + "_to_" + dir_object.GetText();
                    CreateGrant(dir_object.Start, key, string.Empty, dir_object.GetText(), _privileges, _grantee_name, string.Empty, withHierarchy, withGrant, withDelegate, withAdmin, true, false, false, false, false, false, false);

                }
                else if (user_object.Length > 0)
                {
                    Stop();
                    foreach (PlSqlParser.User_object_nameContext user_object_name in user_object)
                    {
                        key = user_object_name.GetText() + "." + "_to_" + dir_object.GetText();
                        CreateGrant(user_object_name.Start, key, user_object_name.GetText(), string.Empty, _privileges, _grantee_name, string.Empty, withHierarchy, withGrant, withDelegate, withAdmin, false, true, false, false, false, false, false);
                    }
                }
                else if (shema_object != null)
                {

                    key = shema_object.GetText() + "." + "_to_" + dir_object.GetText();

                    if (grant_object_name.EDITION() != null)
                    {
                        Stop();
                        CreateGrant(grant_object_name.Start, key, shema_object.GetText(), string.Empty, _privileges, _grantee_name, string.Empty, withHierarchy, withGrant, withDelegate, withAdmin, false, false, true, false, false, false, false);
                    }
                    else if (grant_object_name.MINING() != null)
                    {
                        Stop();
                        CreateGrant(grant_object_name.Start, key, shema_object.GetText(), string.Empty, _privileges, _grantee_name, string.Empty, withHierarchy, withGrant, withDelegate, withAdmin, false, false, false, true, false, false, false);
                    }
                    else if (grant_object_name.JAVA() != null)
                    {
                        Stop();
                        CreateGrant(grant_object_name.Start, key, shema_object.GetText(), string.Empty, _privileges, _grantee_name, string.Empty, withHierarchy, withGrant, withDelegate, withAdmin, false, false, false, false, grant_object_name.SOURCE() != null, grant_object_name.RESOURCE() != null, false);
                    }
                    else if (grant_object_name.SQL() != null && grant_object_name.TRANSLATION() != null && grant_object_name.PROFILE() != null)
                    {
                        Stop();
                        CreateGrant(grant_object_name.Start, key, shema_object.GetText(), string.Empty, _privileges, _grantee_name, string.Empty, withHierarchy, withGrant, withDelegate, withAdmin, false, false, false, false, false, false, true);
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

            PlSqlParser.Paren_column_listContext[] c = context.paren_column_list();
            if (c.Length > 0)
            {
                foreach (PlSqlParser.Paren_column_listContext item in c)
                {
                    string col = item.GetText();
                    key = _schema + "." + _object + "." + col + "_to_" + _grantee_name;
                    CreateGrant(item.Start, key, _schema, _object, privileges, _grantee_name, col, withHierarchy, withGrant, withDelegate, withAdmin, false, false, false, false, false, false, false);
                }
            }
            else
            {
                key = _schema + "." + _object + "_to_" + _grantee_name;
                CreateGrant(context.Start, key, _schema, _object, privileges, _grantee_name, string.Empty, withHierarchy, withGrant, withDelegate, withAdmin, false, false, false, false, false, false, false);

            }
        }

        private GrantModel CreateGrant(IToken token, string key, string objectSchema, string objectName, HashSet<string> privileges, string role, string columnObjectName, bool withHierarchy, bool withGrant, bool withDelegate, bool withAdmin, bool withDirectory, bool withUser, bool withEdition, bool withMiningModel, bool withJavaSource, bool JavaResource, bool withSqlTranslationProfile)
        {

            GrantModel grant;

            grant = new GrantModel()
            {

                Key = key,
                Role = role,

                ObjectSchema = objectSchema,
                ObjectName = objectName,
                ColumnObjectName = columnObjectName,

                Grantable = withGrant,
                Hierarchy = withHierarchy,

            };

            var fileElement = AppendFile(grant, token);
            Append(grant);

            foreach (var privilege in privileges)
            {

                PrivilegeModel _privilege;
                if (!grant.Privileges.TryGet(privilege, out _privilege))
                {

                    _privilege = new PrivilegeModel()
                    {
                        Name = privilege
                    };

                    AppendFile(_privilege, token);
                    grant.Privileges.Add(_privilege);

                }
                else
                {
                    var o = privilege + " " + grant.Key;
                    string message = $"Duplicated grant privilege '{privilege}' on object {objectSchema}.{objectName} TO {role}";
                    GetEventParser(message, o, KindModelEnum.UserObjectPrivilege, fileElement, _privilege.Files.FirstOrDefault());
                }
            }

            return grant;

        }

        private void EvaluateGrants(OCallMethodReference method)
        {

            //if (method.Name.Name == "GRANT_QUEUE_PRIVILEGE")
            //{
            //    if (method.Name.Schema == "DBMS_AQADM" && string.IsNullOrEmpty(method.Name.Package))
            //    {

            //    }
            //}

        }


    }

}

//public override object VisitDir_object_name([NotNull] PlSqlParser.Dir_object_nameContext context)
//{
//    Stop();
//    return this..VisitDir_object_name(context);
//}

//public override object VisitGrantee_name([NotNull] PlSqlParser.Grantee_nameContext context)
//{
//    Stop();
//    return this..VisitGrantee_name(context);
//}

//public override object VisitGrant_object_name([NotNull] PlSqlParser.Grant_object_nameContext context)
//{
//    Stop();
//    return this..VisitGrant_object_name(context);
//}

//public override object VisitObject_privilege([NotNull] PlSqlParser.Object_privilegeContext context)
//{
//    Stop();
//    return this..VisitObject_privilege(context);
//}

//public override object VisitSchema_object_name([NotNull] PlSqlParser.Schema_object_nameContext context)
//{
//    Stop();
//    return this..VisitSchema_object_name(context);
//}

//public override object VisitSystem_privilege([NotNull] PlSqlParser.System_privilegeContext context)
//{
//    Stop();
//    return this..VisitSystem_privilege(context);
//}

//public override object VisitTableview_name([NotNull] PlSqlParser.Tableview_nameContext context)
//{
//    Stop();
//    return this..VisitTableview_name(context);
//}

//public override object VisitUser_object_name([NotNull] PlSqlParser.User_object_nameContext context)
//{
//    Stop();
//    return this..VisitUser_object_name(context);
//}
