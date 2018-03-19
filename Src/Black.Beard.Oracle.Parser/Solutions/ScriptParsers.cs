using Bb.Oracle.Models;
using Bb.Oracle.Structures.Models;
using Bb.Oracle.Validators;
using Bb.Oracle.Visitors;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Oracle.Solutions
{

    public abstract class ScriptParsers : ISolutionEvaluator
    {

        public ScriptParsers(ScriptParserContext ctx)
        {

            this._Current_context = ctx;

        }

        /// <summary>
        /// Evaluate all script file in the visitor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="visitor"></param>
        public abstract void Visit<T>(Antlr4.Runtime.Tree.IParseTreeVisitor<T> visitor);

        /// <summary>
        /// Parse all scripts if the filter return true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="kind"></param>
        /// <param name="scripts"></param>
        /// <param name="visitor"></param>
        protected int Process<T>(Func<ScriptFileInfo, bool> filter, List<ScriptFileInfo> scripts, Antlr4.Runtime.Tree.IParseTreeVisitor<T> visitor)
        {

            int count = 0;
            int cut = this._Current_context._cut;

            NotifyCollectionChangedEventHandler action = null;
            OracleDatabase db = null;
            if (visitor is IDbModelVisitor _visitor)
            {
                db = _visitor.Db;
                action = (o, e) =>
                {
                    if (_visitor.Validators.Count > 0)
                    {
                        foreach (var item in e.NewItems)
                            if (item is ItemBase i)
                                foreach (ParserValidator validator in _visitor.Validators)
                                    if (validator.CanEvaluate(i))
                                        validator.Evaluate(i);
                    }
                };
                db.CollectionChanged += action;
            }


            foreach (ScriptFileInfo script in scripts)
                if (filter(script))
                {
                    count++;
                    try
                    {
                        script.Visit<T>(cut, visitor);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }

            foreach (ScriptFileInfo script in scripts)
                script.Finally();

            if (db != null)
            {
                db.CollectionChanged -= action;
                db.Initialize();
            }

            return count;

        }

        protected readonly ScriptParserContext _Current_context;

    }


}
