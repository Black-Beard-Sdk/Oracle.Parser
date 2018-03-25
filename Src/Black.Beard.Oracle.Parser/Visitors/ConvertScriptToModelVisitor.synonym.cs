using Bb.Oracle.Parser;
using Antlr4.Runtime.Misc;
using Bb.Oracle.Structures.Models;
using System.Diagnostics;
using Bb.Oracle.Helpers;
using System.Linq;

namespace Bb.Oracle.Visitors
{

    public partial class ConvertScriptToModelVisitor
    {

        /// <summary>
        /// create_synonym : 
	    ///       CREATE(OR REPLACE)? PUBLIC SYNONYM synonym_name FOR(objectSchema= schema_name PERIOD)? objectName=schema_object_name(AT_SIGN link_name)?
        ///     | CREATE(OR REPLACE)? SYNONYM(schema= schema_name PERIOD)? synonym_name FOR(objectSchema= schema_name PERIOD)? objectName=schema_object_name(AT_SIGN link_name)?
        ///     ;     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitCreate_synonym([NotNull] PlSqlParser.Create_synonymContext context)
        {
            var ispublic = context.PUBLIC() != null;
            string synonym_schema = ispublic ? "PUBLIC" : context.schema.GetCleanedTexts().FirstOrDefault();
            var synonym_name = context.synonym_name().GetCleanedTexts().FirstOrDefault();
            var object_schema = context.objectSchema.GetCleanedTexts().FirstOrDefault();
            var object_name = context.objectName.GetCleanedTexts().FirstOrDefault();
            string link_name = context.link_name().GetCleanedTexts().FirstOrDefault() ?? string.Empty;

            if (string.IsNullOrEmpty(synonym_schema))
            {

            }

            if (string.IsNullOrEmpty(object_schema))
            {

            }

            SynonymModel synonym = AppendSynonym(context.REPLACE()!= null, ispublic, synonym_schema, synonym_name, object_schema, object_name, link_name);

            Debug.Assert(synonym != null);

            return synonym;

        }

        private SynonymModel AppendSynonym(bool withReplace, bool ispublic, string synonym_schema, string synonym_name, string object_schema, string object_name, string link_name)
        {
            string key = $"{synonym_schema}.{synonym_name}:{object_schema}.{object_name}:{link_name}";
            SynonymModel synonym = Db.Synonyms[key];

            if (synonym != null)
            {
                if (withReplace)
                {

                }
            }
            else
            {

                synonym = new SynonymModel()
                {
                    Key = key,
                    Owner = synonym_schema,
                    Name = synonym_name,
                    ObjectTargetOwner = object_schema,
                    ObjectTargetName = object_name,
                    DbLink = link_name,
                    IsPublic = ispublic,
                };
                Db.Synonyms.Add(synonym);

            }

            return synonym;
        }
    }

}

