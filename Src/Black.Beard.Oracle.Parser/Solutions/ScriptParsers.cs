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
        /// <param name="filter"> function return true or false when the script must to be parsed.</param>
        /// <param name="scripts">List of scripts</param>
        /// <param name="visitor">visitor that must parse the script's list</param>
        /// <returns>return count of parsed scripts</returns>
        protected int Process<T>(Func<ScriptFileInfo, bool> filter, List<ScriptFileInfo> scripts, Antlr4.Runtime.Tree.IParseTreeVisitor<T> visitor)
        {

            int count = 0;
            int cut = this._Current_context._cut;
            
            List<ScriptFileInfo> _scripts = new List<ScriptFileInfo>();
            foreach (ScriptFileInfo script in scripts)
                if (filter(script))
                {
                    count++;
                    try
                    {
                        script.Visit<T>(cut, visitor);
                        _scripts.Add(script);
                    }
                    catch (Exception e)
                    {
                        if (System.Diagnostics.Debugger.IsAttached)
                            System.Diagnostics.Debugger.Break();
                        throw e;
                    }
                }
            
            return count;

        }

        protected readonly ScriptParserContext _Current_context;

    }


}
