using Bb.Oracle.Parser;
using Antlr4.Runtime.Misc;
using Bb.Oracle.Structures.Models;
using System.Diagnostics;
using Bb.Oracle.Helpers;
using Bb.Oracle.Models;

namespace Bb.Oracle.Visitors
{

    public partial class ConvertScriptToModelVisitor
    {

        /// <summary>
        /// 	CREATE SEQUENCE sequence_name (sequence_start_clause | sequence_spec)* ';'
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitCreate_sequence([NotNull] PlSqlParser.Create_sequenceContext context)
        {

            SequenceModel sequence = null;

            if (context.exception != null)
            {
                AppendException(context.exception);
                return null;
            }

            var name = context.sequence_name().GetCleanedTexts();
            string key = $"{name[0]}.{name[1]}";

            sequence = Db.Sequences[key];
            if (sequence != null)
            {
                GetEventParser(new Models.Error() { Message = Models.Constants.Errors.AllreadyExists }, key, KindModelEnum.Sequence);
                return null;
            }

            sequence = new SequenceModel()
            {
                Key = key,
                Owner = name[0],
                Name = name[1],
            };
            AppendFile(sequence, context.Start);

            using (Enqueue(sequence))
            {

                //foreach (var item in context.sequence_start_clause())
                //    this.VisitSequence_start_clause(item);

                foreach (var item in context.sequence_spec())
                    this.VisitSequence_spec(item);

            }


            return sequence;

        }

        public override object VisitAlter_sequence([NotNull] PlSqlParser.Alter_sequenceContext context)
        {
            Stop();
            SequenceModel sequence = null;

            var name = context.sequence_name().GetCleanedTexts();
            string key = $"{name[0]}.{name[1]}";

            sequence = Db.Sequences[key];
            if (sequence != null)
            {
                GetEventParser(new Models.Error() { Message = Models.Constants.Errors.NotFoundObject }, key, KindModelEnum.Sequence);
                return null;
            }

            using (Enqueue(sequence))
            {

                foreach (var item in context.sequence_spec())
                    this.VisitSequence_spec(item);

            }

            return sequence;

        }

        public override object VisitDrop_sequence([NotNull] PlSqlParser.Drop_sequenceContext context)
        {
            Stop();
            var result = base.VisitDrop_sequence(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// sequence_spec :
        ///       (INCREMENT BY | START WITH ) UNSIGNED_INTEGER
        ///     | (MAXVALUE UNSIGNED_INTEGER | NOMAXVALUE)
        ///     | (MINVALUE UNSIGNED_INTEGER | NOMINVALUE)
        ///     | (CYCLE | NOCYCLE)
        ///     | (CACHE UNSIGNED_INTEGER | NOCACHE)
        ///     | (ORDER | NOORDER)
        ///     | (KEEP | NOKEEP)
        ///     | (SESSION | GLOBAL)
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitSequence_spec([NotNull] PlSqlParser.Sequence_specContext context)
        {

            int value;
            var sequence = this.Current<SequenceModel>();

            if (context.INCREMENT() != null)
            {
                value = context.integer().ToInteger();
                sequence.IncrementBy = value;
            }
            else if (context.START() != null)
                sequence.MinValue = context.integer().GetText();

            else if (context.MAXVALUE() != null)
                sequence.MaxValue = context.integer().GetText();

            else if (context.NOMAXVALUE() != null)
                sequence.MaxValue = Models.Constants.DefaultValues.MaxValueSequence;

            else if (context.MINVALUE() != null)
                sequence.MinValue = context.integer().GetText();

            else if (context.NOMINVALUE() != null)
                sequence.MinValue = "0";

            else if (context.CYCLE() != null)
                sequence.CycleFlag = true;

            else if (context.NOCYCLE() != null)
                sequence.CycleFlag = false;

            else if (context.CACHE() != null)
            {
                value = context.integer().ToInteger();
                sequence.CacheSize = value;
            }
            else if (context.NOCACHE() != null)
            {
                sequence.CacheSize = 20;
            }
            else if (context.ORDER() != null)
            {
                sequence.OrderFlag = true;
            }
            else if (context.NOORDER() != null)
            {
                sequence.OrderFlag = false;
            }
            else if (context.KEEP() != null)
            {
                Stop();
            }
            else if (context.NOKEEP() != null)
            {
                Stop();
            }
            else if (context.SESSION() != null)
            {
                Stop();
            }
            else if (context.GLOBAL() != null)
            {
                Stop();
            }

            return null;

        }

    }

}

