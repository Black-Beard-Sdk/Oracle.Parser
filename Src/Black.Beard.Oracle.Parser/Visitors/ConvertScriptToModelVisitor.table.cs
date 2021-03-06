﻿using Bb.Oracle.Parser;
using Antlr4.Runtime.Misc;
using Bb.Oracle.Structures.Models;
using System.Diagnostics;
using Bb.Oracle.Helpers;
using System.Linq;
using System.Collections.Generic;
using Antlr4.Runtime;
using System.Text;
using Bb.Oracle.Models.Codes;

namespace Bb.Oracle.Visitors
{

    public partial class ConvertScriptToModelVisitor
    {

        /// <summary>
        /// create_table :
	    ///     CREATE(GLOBAL TEMPORARY)? TABLE table_fullname
        ///        (
        ///            relational_table
        ///         /*| object_table 
        ///           | xmltype_table*/
        ///        )
        ///        ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitCreate_table([NotNull] PlSqlParser.Create_tableContext context)
        {
            var f = this.Filename;

            var names = context.table_fullname().GetCleanedTexts();
            var tableModel = new TableModel();

            if (names.Count == 1)
                tableModel.Name = names[0];

            else if (names.Count == 2)
            {
                tableModel.Owner = names[0];
                tableModel.Name = names[1];
            }

            AppendFile(tableModel, context.Start);

            using (Enqueue(tableModel))
            {
                var relational_table = context.relational_table();
                if (relational_table != null)
                    VisitRelational_table(relational_table);
                else
                {
                    Stop();
                }

            }

            return tableModel;

        }

        /// <summary>
        /// relational_table : 
        ///     (LEFT_PAREN relational_properties RIGHT_PAREN)? 
        ///     (ON COMMIT (DELETE | PRESERVE)? ROWS)? 
        ///     physical_properties? table_properties
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitRelational_table([NotNull] PlSqlParser.Relational_tableContext context)
        {

            var table = Current<TableModel>();

            var relational_properties = context.relational_properties();
            if (relational_properties != null)
                VisitRelational_properties(relational_properties);

            if (context.ON().Exist() && context.COMMIT().Exist() && context.ROWS().Exist())
            {
                Stop();
                if (context.DELETE().Exist())
                {
                    Stop();

                }
                else if (context.PRESERVE().Exist())
                {
                    Stop();

                }

            }

            var physical_properties = context.physical_properties();
            if (physical_properties != null)
            {

                VisitPhysical_properties(physical_properties);

            }

            VisitTable_properties(context.table_properties());

            return table;

        }

        /// <summary>
        /// physical_properties :
        ///       deferred_segment_creation? segment_attributes_clause table_compression? inmemory_table_clause? ilm_clause
        ///     | deferred_segment_creation? ORGANIZATION
        ///     (
        ///           HEAP segment_attributes_clause? table_compression? inmemory_table_clause? ilm_clause?
        ///         | INDEX segment_attributes_clause? index_org_table_clause
        ///         | EXTERNAL external_table_clause
        ///     )
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitPhysical_properties([NotNull] PlSqlParser.Physical_propertiesContext context)
        {

            var table = Current<TableModel>();

            var deferred_segment_creation = context.deferred_segment_creation();
            if (deferred_segment_creation != null)
            {

                VisitDeferred_segment_creation(deferred_segment_creation);
                Stop();
            }

            var segment_attributes_clause = context.segment_attributes_clause();
            if (segment_attributes_clause != null)
                using (Enqueue(table.PhysicalAttributes))
                    VisitSegment_attributes_clause(segment_attributes_clause);

            var table_compression = context.table_compression();
            if (table_compression != null)
            {
                VisitTable_compression(table_compression);
                Stop();
            }

            var inmemory_table_clause = context.inmemory_table_clause();
            if (inmemory_table_clause != null)
            {
                VisitInmemory_table_clause(inmemory_table_clause);
                Stop();
            }

            var ilm_clause = context.ilm_clause();
            if (ilm_clause != null)
            {
                VisitIlm_clause(ilm_clause);
                Stop();
            }

            var index_org_table_clause = context.index_org_table_clause();
            if (index_org_table_clause != null)
            {
                VisitIndex_org_table_clause(index_org_table_clause);
                Stop();
            }

            var external_table_clause = context.external_table_clause();
            if (external_table_clause != null)
            {
                VisitExternal_table_clause(external_table_clause);
                Stop();
            }

            if (context.HEAP().Exist())
            {
                Stop();

            }
            else if (context.INDEX().Exist())
            {
                Stop();

            }

            return table;

        }

        /// <summary>
        /// external_table_clause :
        ///     LEFT_PAREN(TYPE /*access_driver_type*/)? external_data_properties RIGHT_PAREN(REJECT LIMIT (integer | UNLIMITED))?
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitExternal_table_clause([NotNull] PlSqlParser.External_table_clauseContext context)
        {
            Stop();
            var result = base.VisitExternal_table_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// external_data_properties :
        ///     DEFAULT DIRECTORY directory_name(ACCESS PARAMETERS (LEFT_PAREN /*opaque_format_spec*/ RIGHT_PAREN | USING CLOB subquery)) 
        ///     LOCATION LEFT_PAREN external_data_properties_location(COMMA external_data_properties_location)+ RIGHT_PAREN 
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitExternal_data_properties([NotNull] PlSqlParser.External_data_propertiesContext context)
        {
            Stop();
            var result = base.VisitExternal_data_properties(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// external_data_properties_location :
        ///     (directory_name? ':')? CHAR_STRING
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitExternal_data_properties_location([NotNull] PlSqlParser.External_data_properties_locationContext context)
        {
            Stop();
            var result = base.VisitExternal_data_properties_location(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// index_org_table_clause : 
        ///    (
        ///      mapping_table_clause
        ///    | PCTTHRESHOLD integer
        ///    | prefix_compression
        ///
        ///)? index_org_overflow_clause
        ///;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitIndex_org_table_clause([NotNull] PlSqlParser.Index_org_table_clauseContext context)
        {
            Stop();
            var result = base.VisitIndex_org_table_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// index_org_overflow_clause :
        ///    (INCLUDING column_name)? OVERFLOW segment_attributes_clause? 
        /// ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitIndex_org_overflow_clause([NotNull] PlSqlParser.Index_org_overflow_clauseContext context)
        {
            Stop();
            var result = base.VisitIndex_org_overflow_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// prefix_compression :
        ///       COMPRESS integer? 
	    ///     | NOCOMPRESS
	    ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitPrefix_compression([NotNull] PlSqlParser.Prefix_compressionContext context)
        {
            Stop();
            var result = base.VisitPrefix_compression(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// mapping_table_clause : 
        ///    MAPPING TABLE | NOMAPPING
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitMapping_table_clause([NotNull] PlSqlParser.Mapping_table_clauseContext context)
        {
            Stop();
            var result = base.VisitMapping_table_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// table_compression :
        ///       COMPRESS 
        ///     | ROW STORE COMPRESS(BASIC | ADVANCED)? 
        ///     | COLUMN STORE COMPRESS(FOR (QUERY | ARCHIVE) (LOW | HIGH)?)? (NO? ROW LEVEL LOCKING)?
        ///     | NO COMPRESS
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitTable_compression([NotNull] PlSqlParser.Table_compressionContext context)
        {
            Stop();
            var result = base.VisitTable_compression(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// segment_attributes_clause :
        ///     (physical_attributes_clause+ | tablespace_clause | logging_clause )+ 
	    ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitSegment_attributes_clause([NotNull] PlSqlParser.Segment_attributes_clauseContext context)
        {

            var physical = Current<PhysicalAttributesModel>();
            Debug.Assert(physical != null);

            var physical_attributes_clauses = context.physical_attributes_clauses();
            if (physical_attributes_clauses != null && physical_attributes_clauses.Length > 0)
            {

                foreach (var physical_attributes_clause in physical_attributes_clauses)
                {
                    foreach (var item in physical_attributes_clause.physical_attributes_clause())
                    {
                        Stop();
                        VisitPhysical_attributes_clause(item);
                    }
                }

            }

            var tablespace_clauses = context.tablespace_clause();
            if (tablespace_clauses != null && tablespace_clauses.Length > 0)
            {
                foreach (var tablespace_clause in tablespace_clauses)
                {
                    var tablespaces = (List<string>)VisitTablespace_clause(tablespace_clause);
                    physical.Tablespace.Name = tablespaces.FirstOrDefault();
                }
            }

            var logging_clauses = context.logging_clause();
            if (logging_clauses != null && logging_clauses.Length > 0)
            {
                foreach (var logging_clause in logging_clauses)
                {
                    VisitLogging_clause(logging_clause);
                    Stop();
                }
            }

            return physical;

        }

        /// <summary>
        /// physical_attributes_clauses : physical_attributes_clause+
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitPhysical_attributes_clauses([NotNull] PlSqlParser.Physical_attributes_clausesContext context)
        {

            var result = new PhysicalAttributesModel();

            var physical_attributes_clauses = context.physical_attributes_clause();
            using (Enqueue(result))
                foreach (var physical_attributes_clause in physical_attributes_clauses)
                {

                }

            return result;

        }


        /// <summary>
        /// storage_clause : STORAGE LEFT_PAREN storage_clause+ RIGHT_PAREN
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitStorage_clauses([NotNull] PlSqlParser.Storage_clausesContext context)
        {
            return base.VisitStorage_clauses(context);
        }


        /// <summary>
        /// physical_attributes_clause :
        ///       PCTFREE integer 
        ///     | PCTUSED integer 
        ///     | INITRANS integer 
        ///     | MAXTRANS integer 
        ///     | storage_clauses 
	    ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitPhysical_attributes_clause([NotNull] PlSqlParser.Physical_attributes_clauseContext context)
        {
            Stop();

            var physical = Current<PhysicalAttributesModel>();

            if (context.PCTFREE().Exist())
                physical.PctFree = context.integer().ToInteger();

            else if (context.PCTUSED().Exist())
                physical.PctUsed = context.integer().ToInteger();

            else if (context.INITRANS().Exist())
                physical.IniTrans = context.integer().ToInteger();

            else if (context.MAXTRANS().Exist())
                physical.MaxTrans = context.integer().ToInteger();

            else
            {
                var storage_clauses = context.storage_clauses();
                if (storage_clauses != null)
                    VisitStorage_clauses(storage_clauses);
            }

            return physical;

        }

        /// <summary>
        /// supplemental_logging_props :
        ///     SUPPLEMENTAL LOG(supplemental_log_grp_clause | supplemental_id_key_clause)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitSupplemental_logging_props([NotNull] PlSqlParser.Supplemental_logging_propsContext context)
        {
            Stop();
            var result = base.VisitSupplemental_logging_props(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// supplemental_log_grp_clause :
        ///     GROUP log_group_name LEFT_PAREN column_logged(COMMA column_logged)* RIGHT_PAREN ALWAYS? 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitSupplemental_log_grp_clause([NotNull] PlSqlParser.Supplemental_log_grp_clauseContext context)
        {
            Stop();
            var result = base.VisitSupplemental_log_grp_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// supplemental_id_key_clause :
        ///     DATA LEFT_PAREN(ALL | PRIMARY KEY | UNIQUE |FOREIGN KEY)+ RIGHT_PAREN COLUMNS
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitSupplemental_id_key_clause([NotNull] PlSqlParser.Supplemental_id_key_clauseContext context)
        {
            Stop();
            var result = base.VisitSupplemental_id_key_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// inmemory_table_clause : 
        ///       INMEMORY(inmemory_parameters inmemory_column_clause)? 
        ///     | NO INMEMORY
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitInmemory_table_clause([NotNull] PlSqlParser.Inmemory_table_clauseContext context)
        {
            Stop();
            var result = base.VisitInmemory_table_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// inmemory_parameters : 
        ///       inmemory_memcompress?
        ///     | inmemory_priority?
        ///     | inmemory_distribute?
        ///     | inmemory_duplicate?
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitInmemory_parameters([NotNull] PlSqlParser.Inmemory_parametersContext context)
        {
            Stop();
            var result = base.VisitInmemory_parameters(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// inmemory_duplicate :
        ///     DUPLICATE ALL? | NO DUPLICATE
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitInmemory_duplicate([NotNull] PlSqlParser.Inmemory_duplicateContext context)
        {
            Stop();
            var result = base.VisitInmemory_duplicate(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// inmemory_distribute:
        ///     DISTRIBUTE(
        ///           AUTO
        ///         | (BY (ROWID RANGE | PARTITION | SUBPARTITION))
        ///     )?
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitInmemory_distribute([NotNull] PlSqlParser.Inmemory_distributeContext context)
        {
            Stop();
            var result = base.VisitInmemory_distribute(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// PRIORITY (NONE | LOW | MEDIUM | HIGH | CRITICAL)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitInmemory_priority([NotNull] PlSqlParser.Inmemory_priorityContext context)
        {
            Stop();
            var result = base.VisitInmemory_priority(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// inmemory_memcompress :
        ///    MEMCOMPRESS FOR(DML | (QUERY | CAPACITY) (LOW | HIGH)?)
        ///  | NO MEMCOMPRESS
        ///  ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitInmemory_memcompress([NotNull] PlSqlParser.Inmemory_memcompressContext context)
        {
            Stop();
            var result = base.VisitInmemory_memcompress(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// inmemory_column_clause : 
        ///       INMEMORY inmemory_memcompress? paren_column_list
        ///     | NO INMEMORY
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitInmemory_column_clause([NotNull] PlSqlParser.Inmemory_column_clauseContext context)
        {
            Stop();
            var result = base.VisitInmemory_column_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// ilm_clause :
        ///         ILM
        ///           ADD POLICY ilm_policy_clause
        ///     | (
        ///           DELETE
        ///         | ENABLE
        ///         | DISABLE
        ///       ) POLICY ilm_policy_name
        ///     |   (DELETE ALL
        ///         | ENABLE ALL
        ///         | DISABLE ALL)
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitIlm_clause([NotNull] PlSqlParser.Ilm_clauseContext context)
        {
            Stop();
            var result = base.VisitIlm_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// ilm_compression_policy :
        ///       table_compression(SEGMENT | GROUP) (AFTER ilm_time_period OF(NO ACCESS | NO MODIFICATION | CREATION) | ON function_name)
        ///     | ROW STORE COMPRESS ADVANCED ROW AFTER ilm_time_period OF NO MODIFICATION
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitIlm_compression_policy([NotNull] PlSqlParser.Ilm_compression_policyContext context)
        {
            Stop();
            var result = base.VisitIlm_compression_policy(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// ilm_time_period :
        ///     integer
        ///     (
        ///       DAY
        ///     | DAYS
        ///     | MONTH
        ///     | MONTHS
        ///     | YEAR
        ///     | YEARS
        ///     )
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitIlm_time_period([NotNull] PlSqlParser.Ilm_time_periodContext context)
        {
            Stop();
            var result = base.VisitIlm_time_period(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// ilm_tiering_policy :
        ///       TIER TO tablespace_name(SEGMENT | GROUP)? ON function_name
        ///     | TIER TO tablespace_name READ ONLY(SEGMENT | GROUP)? (AFTER ilm_time_period OF(NO ACCESS | NO MODIFICATION | CREATION) | ON function_name)
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitIlm_tiering_policy([NotNull] PlSqlParser.Ilm_tiering_policyContext context)
        {
            Stop();
            var result = base.VisitIlm_tiering_policy(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// ilm_policy_clause :
        ///    ilm_compression_policy | ilm_tiering_policy
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitIlm_policy_clause([NotNull] PlSqlParser.Ilm_policy_clauseContext context)
        {
            Stop();
            var result = base.VisitIlm_policy_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// deferred_segment_creation :
        ///     SEGMENT CREATION(IMMEDIATE | DEFERRED)
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitDeferred_segment_creation([NotNull] PlSqlParser.Deferred_segment_creationContext context)
        {
            Stop();
            var result = base.VisitDeferred_segment_creation(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// table_properties : 
        ///     column_properties? indexing_clause? table_partitioning_clauses? attribute_clustering_clause? 
        ///     (CACHE | NOCACHE)? 
        ///     (RESULT_CACHE LEFT_PAREN MODE (DEFAULT | FORCE) RIGHT_PAREN )? 
        ///     parallel_clause?
        ///     (ROWDEPENDENCIES | NOROWDEPENDENCIES)? 
        ///     enable_disable_clause* row_movement_clause? 
        ///     flashback_archive_clause? (ROW ARCHIVAL)? (AS subquery)?
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitTable_properties([NotNull] PlSqlParser.Table_propertiesContext context)
        {

            var column_properties = context.column_properties();
            if (column_properties != null)
            {
                Stop();
                VisitColumn_properties(column_properties);
            }

            var indexing_clause = context.indexing_clause();
            if (indexing_clause != null)
            {
                Stop();
                VisitIndexing_clause(indexing_clause);
            }

            var table_partitioning_clauses = context.table_partitioning_clauses();
            if (table_partitioning_clauses != null)
            {
                Stop();
                VisitTable_partitioning_clauses(table_partitioning_clauses);
            }

            var attribute_clustering_clause = context.attribute_clustering_clause();
            if (attribute_clustering_clause != null)
            {
                Stop();
                VisitAttribute_clustering_clause(attribute_clustering_clause);
            }

            if (context.CACHE().Exist())
            {
                Stop();

            }

            if (context.NOCACHE().Exist())
            {
                Stop();

            }

            if (context.RESULT_CACHE().Exist())
            {

                if (context.DEFAULT().Exist())
                {
                    Stop();
                }
                else if (context.FORCE().Exist())
                {
                    Stop();
                }
                else
                {
                    Stop();
                }

            }

            var parallel_clause = context.parallel_clause();
            if (parallel_clause != null)
            {
                Stop();
                VisitParallel_clause(parallel_clause);
            }

            if (context.ROWDEPENDENCIES().Exist())
            {
                Stop();

            }
            else if (context.NOROWDEPENDENCIES().Exist())
            {
                Stop();
            }

            var enable_disable_clauses = context.enable_disable_clause();
            if (enable_disable_clauses != null && enable_disable_clauses.Length > 0)
            {
                foreach (var enable_disable_clause in enable_disable_clauses)
                {
                    Stop();
                    VisitEnable_disable_clause(enable_disable_clause);
                }
            }

            var row_movement_clause = context.row_movement_clause();
            if (row_movement_clause != null)
            {

                Stop();
                VisitRow_movement_clause(row_movement_clause);

            }

            var flashback_archive_clause = context.flashback_archive_clause();
            if (flashback_archive_clause != null)
            {
                Stop();
                VisitFlashback_archive_clause(flashback_archive_clause);
            }

            if (context.ROW().Exist() && context.ARCHIVAL().Exist())
            {
                Stop();

            }

            if (context.AS().Exist())
            {
                Stop();
                var subQuery = context.subquery();
                if (subQuery != null)
                {

                }
            }

            return null;

        }

        /// <summary>
        /// relational_properties :
        ///     relational_property(COMMA relational_property)*
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitRelational_properties([NotNull] PlSqlParser.Relational_propertiesContext context)
        {
            var table = this.Current<TableModel>();
            foreach (PlSqlParser.Relational_propertyContext property in context.relational_property())
            {
                var i = VisitRelational_property(property);

                if (i is ColumnModel col)
                {
                    col.Key = (col.ColumnId = table.Columns.Count()).ToString();
                    table.Columns.Add(col);
                }
                else if (i is List<ConstraintModel> m)
                {
                    foreach (ConstraintModel constraint in m)
                    {

                        constraint.TableReference.Owner = table.Owner;
                        constraint.TableReference.Name = table.Name;

                        if (string.IsNullOrEmpty(constraint.Owner))
                            if (!string.IsNullOrEmpty(constraint.TableReference.Owner))
                                constraint.Owner = constraint.TableReference.Owner;

                    }
                }
                else
                {
                    Stop();
                }

            }
            return table;
        }

        /// <summary>
        /// period_definition :
        ///     PERIOD FOR valid_time_column=column_name(start_time_column= column_name COMMA end_time_column = column_name)?
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitPeriod_definition([NotNull] PlSqlParser.Period_definitionContext context)
        {
            Stop();
            var result = base.VisitPeriod_definition(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// relational_property :
        ///       column_definition
        ///     | virtual_column_definition
        ///     | period_definition
        ///     | (out_of_line_constraint | out_of_line_ref_constraint)
        ///     | supplemental_logging_props
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitRelational_property([NotNull] PlSqlParser.Relational_propertyContext context)
        {
            List<ConstraintModel> constraints = null;
            ColumnModel result = null;
            var column_definition = context.column_definition();
            if (column_definition != null)
                result = (ColumnModel)VisitColumn_definition(column_definition);

            else
            {

                var virtual_column_definition = context.virtual_column_definition();
                if (virtual_column_definition != null)
                    result = (ColumnModel)VisitVirtual_column_definition(virtual_column_definition);

                else
                {
                    var out_of_line_constraint = context.out_of_line_constraint();
                    if (out_of_line_constraint != null)
                    {
                        constraints = (List<ConstraintModel>)VisitOut_of_line_constraint(out_of_line_constraint);
                    }
                    else
                    {
                        var out_of_line_ref_constraint = context.out_of_line_ref_constraint();
                        if (out_of_line_ref_constraint != null)
                        {
                            constraints = (List<ConstraintModel>)VisitOut_of_line_ref_constraint(out_of_line_ref_constraint);
                        }

                        else
                        {
                            Stop();
                        }
                    }
                }
            }

            if (constraints != null && constraints.Count > 0)
            {
                foreach (ConstraintModel constraint in constraints)
                {
                    Append(constraint);
                    //constraint.Columns.Add(new ConstraintColumnModel() { ColumnName = result.Name, Position = constraint.Columns.Count });
                }

                return constraints;

            }

            Debug.Assert(result != null);
            return result;

        }

        /// <summary>
        /// column_definition :
        ///     column_name datatype? SORT? (VISIBLE | INVISIBLE)? 
        ///     (
        ///       DEFAULT (ON NULL)? expression 
        ///     | (GENERATED (ALWAYS | BY DEFAULT(ON NULL)?)? AS IDENTITY(LEFT_PAREN identity_options RIGHT_PAREN )?)
        ///     )?
        ///     (ENCRYPT encryption_spec)?
        ///     (inline_constraint+ | inline_ref_constraint)?
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitColumn_definition([NotNull] PlSqlParser.Column_definitionContext context)
        {

            var names = context.column_name().GetCleanedTexts();
            var datatype = context.datatype();
            var _datatype = (OracleType)VisitDatatype(datatype);
            var sorted = context.SORT().Exist();
            var invisible = context.INVISIBLE().Exist();

            var col = new ColumnModel()
            {
                Name = names.First(),
                Type = _datatype,
                CharactereSetName = "",
                CharUsed = "",
                Description = "",
                EncryptionAlg = "",
                IntegrityAlg = "",
                DataUpgrated = true,
                IsComputed = false,
                IsPrimaryKey = false,
                Nullable = false,
                Salt = false,
                Valid = true,
                IsSequence = false,
            };

            if (context.DEFAULT().Exist())
            {
                var defaultExpression = context.expression();
                _datatype.DataDefault = GetText(defaultExpression.Start.StartIndex, defaultExpression.Stop.StopIndex).ToString();
            }

            if (context.GENERATED().Exist())
            {
                Stop();
                context.ALWAYS().Exist();
                context.BY().Exist();
                //context.On().Exist() && context.NULL().Exist();
            }

            if (context.IDENTITY().Exist())
            {
                Stop();
                var identity_options = context.identity_options();
                if (identity_options != null)
                {
                    VisitIdentity_options(identity_options);
                }
            }

            if (context.ENCRYPT().Exist())
            {
                Stop();
                var encryption_spec = VisitEncryption_spec(context.encryption_spec());
            }

            ConstraintModel constraint;
            var inline_constraints = context.inline_constraint();
            if (inline_constraints != null)
            {
                foreach (PlSqlParser.Inline_constraintContext inline_constraint in inline_constraints)
                {

                    constraint = (ConstraintModel)VisitInline_constraint(inline_constraint);
                    constraint.Columns.Add(new ConstraintColumnModel()
                    {
                        Position = constraint.Columns.Count(),
                        ColumnName = names.First(),
                    });

                }
            }

            var inline_ref_constraint = context.inline_ref_constraint();
            if (inline_ref_constraint != null)
            {
                constraint = (ConstraintModel)VisitInline_ref_constraint(inline_ref_constraint);
                Stop();
                constraint.Columns.Add(new ConstraintColumnModel()
                {
                    Position = constraint.Columns.Count(),
                    ColumnName = names.First(),
                });

            }

            return col;

        }

        /// <summary>
        /// virtual_column_definition : 
        ///     column_name datatype? SORT? (VISIBLE | INVISIBLE)? (GENERATED ALWAYS)? AS LEFT_PAREN RIGHT_PAREN
        ///     VIRTUAL? evaluation_edition_clause? unusable_editions_clause? inline_constraint*
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitVirtual_column_definition([NotNull] PlSqlParser.Virtual_column_definitionContext context)
        {
            Stop();
            var result = base.VisitVirtual_column_definition(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// UNUSABLE BEFORE (CURRENT EDITION | EDITION edition_name)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitUnusable_editions_clause([NotNull] PlSqlParser.Unusable_editions_clauseContext context)
        {
            Stop();
            var result = base.VisitUnusable_editions_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// evaluation_edition_clause :
        ///         EVALUATION USING
        ///         (
        ///               CURRENT EDITION
        ///         | EDITION edition_name
        ///         | NULL EDITION
        ///         )
        ///         ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitEvaluation_edition_clause([NotNull] PlSqlParser.Evaluation_edition_clauseContext context)
        {
            Stop();
            var result = base.VisitEvaluation_edition_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// inline_ref_constraint :
        ///         SCOPE IS table_fullname
        ///     | WITH ROWID
        ///     | CONSTRAINT constraint_name? references_clause constraint_state?
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitInline_ref_constraint([NotNull] PlSqlParser.Inline_ref_constraintContext context)
        {
            Stop();
            var result = base.VisitInline_ref_constraint(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// inline_constraint :
        ///    (CONSTRAINT constraint_name)?
        ///    (NOT? NULL
        ///     | UNIQUE
        ///     | PRIMARY KEY
        ///     | references_clause
        ///     | check_constraint
        ///    )
        ///    constraint_state?
        ///;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitInline_constraint([NotNull] PlSqlParser.Inline_constraintContext context)
        {

            var table = this.Current<TableModel>();
            var constraint = new ConstraintModel()
            {
                Owner = table.Owner,
            };

            using (this.Enqueue(constraint))
            {

                constraint.TableReference.Owner = table.Owner;
                constraint.TableReference.Name = table.Name;
                constraint.TableReference.Set(table);

                var c = context.constraint_name();
                if (c != null)
                    constraint.Name = c.GetCleanedTexts().FirstOrDefault();

                if (context.NOT().Exist() && context.NULL().Exist())
                    constraint.Search_Condition = Utils.Serialize("NOT NULL", false);

                else if (context.UNIQUE().Exist())
                    constraint.Type = "U";

                else if (context.PRIMARY().Exist() && context.KEY().Exist())
                    constraint.Type = "P";

                else
                {

                    var references_clause = context.references_clause();
                    if (references_clause != null)
                    {
                        Stop();
                        VisitReferences_clause(references_clause);
                    }
                    else
                    {
                        var check_constraint = context.check_constraint();
                        if (check_constraint != null)
                            VisitCheck_constraint(check_constraint);
                    }

                }

                var constraint_state = context.constraint_state();
                if (constraint_state != null)
                    VisitConstraint_state(constraint_state);

                AppendFile(constraint, context.Start);
                Append(constraint);

            }

            return constraint;

        }

        /// <summary>
        /// check_constraint :
        ///    CHECK LEFT_PAREN condition RIGHT_PAREN DISABLE?
        ///;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitCheck_constraint([NotNull] PlSqlParser.Check_constraintContext context)
        {
            var constraint = this.Current<ConstraintModel>();
            //constraint.Type = "O";
            constraint.Type = "C";
            Debug.Assert(constraint != null);

            var c = context.condition();
            StringBuilder condition = GetText(c.Start.StartIndex, c.Stop.StopIndex);
            constraint.Search_Condition = Utils.Serialize(condition.ToString(), false);

            constraint.Status = context.DISABLE().Exist() ? "DISABLE" : "ENABLE";

            return constraint;
        }

        /// <summary>
        /// exceptions_clause :
        ///    EXCEPTIONS INTO table_fullname
        /// ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitExceptions_clause([NotNull] PlSqlParser.Exceptions_clauseContext context)
        {
            Stop();
            // TODO : comment je retrouve la table des exceptions
            var result = base.VisitExceptions_clause(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// constraint_state :
        ///     (     NOT? DEFERRABLE
        ///         | INITIALLY(IMMEDIATE|DEFERRED)
        ///         | (RELY|NORELY)
        ///         | using_index_clause
        ///         | (ENABLE|DISABLE)
        ///         | (VALIDATE|NOVALIDATE)
	    ///         | exceptions_clause
        ///     )+
        /// ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitConstraint_state([NotNull] PlSqlParser.Constraint_stateContext context)
        {

            var constraint = this.Current<ConstraintModel>();

            if (context.DEFERRABLE().FirstOrDefault().Exist())
            {
                Stop();
                // TODO : Difference entre deferrable et defered
                if (context.NOT().FirstOrDefault().Exist())
                    constraint.Deferrable = "NOT DEFERRABLE";
                else
                    constraint.Deferrable = "DEFERRABLE";
            }
            else if (context.INITIALLY().FirstOrDefault().Exist())
            {
                Stop();
                if (context.IMMEDIATE().FirstOrDefault().Exist())
                    constraint.Deferred = "IMMEDIATE";
                else
                    constraint.Deferred = "DEFFERED";
            }
            else if (context.RELY().FirstOrDefault().Exist())
                constraint.Rely = "RELY";

            else if (context.NORELY().FirstOrDefault().Exist())
            {
                Stop();
                constraint.Rely = "NORELY";
            }
            else if (context.ENABLE().FirstOrDefault().Exist())
                constraint.Rely = "ENABLE";

            else if (context.DISABLE().FirstOrDefault().Exist())
                constraint.Rely = "DISABLE";

            else if (context.VALIDATE().FirstOrDefault().Exist())
                constraint.Validated = "VALIDATE";

            else if (context.NOVALIDATE().FirstOrDefault().Exist())
                constraint.Validated = "NOVALIDATE";

            else if (context.ENABLE().FirstOrDefault().Exist())
                constraint.Validated = "ENABLE";

            else
            {
                var using_index_clause = context.using_index_clause().FirstOrDefault();
                if (using_index_clause != null)
                {
                    var index = (IndexModel)VisitUsing_index_clause(using_index_clause);
                    constraint.IndexOwner = index.IndexOwner;
                    constraint.IndexName = index.Name;
                }
                var exceptions_clause = context.exceptions_clause().FirstOrDefault();
                if (exceptions_clause != null)
                {
                    Stop();
                    VisitExceptions_clause(exceptions_clause);
                }
            }

            return constraint;

        }

        /// <summary>
        /// using_index_clause :
        ///         USING INDEX(
        ///                       index_name
        ///                     | LEFT_PAREN create_index RIGHT_PAREN
        ///                     | index_properties?
        ///                     )
        ///         ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitUsing_index_clause([NotNull] PlSqlParser.Using_index_clauseContext context)
        {

            IndexModel result = null;

            var index_name = context.index_name();
            if (index_name != null)
            {
                Stop();

                var names = context.index_name().GetCleanedTexts();

                result = new IndexModel()
                {

                    IndexOwner = names[0],
                    Name = names[1],

                };

            }

            else
            {
                var create_index = context.create_index();
                if (create_index != null)
                {
                    Stop();
                    result = (IndexModel)VisitCreate_index(create_index);
                }
                else
                {
                    var index_properties = context.index_properties();
                    if (index_properties != null)
                    {
                        result = new IndexModel();
                        using (Enqueue(result))
                            VisitIndex_properties(index_properties);

                        //result.IndexOwner = "";
                        //result.Name = result.BuildName();

                        AppendFile(result, context.Start);
                        Append(result);

                    }
                }
            }

            return result;

        }

        /// <summary>
        /// index_properties :
        ///    (
        ///        (global_partitioned_index | local_partitioned_index) 
        ///      | index_attributes+
        ///    )+ 
        /// | INDEXTYPE IS(domain_index_clause /*| xmltable_index_clause */| xmlindex_clause)
        ///;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitIndex_properties([NotNull] PlSqlParser.Index_propertiesContext context)
        {

            var index = Current<IndexModel>();

            var global_partitioned_indexes = context.global_partitioned_index();
            if (global_partitioned_indexes != null && global_partitioned_indexes.Length > 0)
            {
                foreach (var global_partitioned_index in global_partitioned_indexes)
                {
                    Stop();
                    VisitGlobal_partitioned_index(global_partitioned_index);
                }
            }
            else
            {
                var local_partitioned_indexes = context.local_partitioned_index();
                if (local_partitioned_indexes != null && local_partitioned_indexes.Length > 0)
                {
                    foreach (var local_partitioned_index in local_partitioned_indexes)
                    {
                        Stop();
                        VisitLocal_partitioned_index(local_partitioned_index);
                    }
                }
                else
                {
                    var index_attributes = context.index_attributes();
                    if (index_attributes != null && index_attributes.Length > 0)
                    {
                        foreach (var index_attribute in index_attributes)
                            VisitIndex_attributes(index_attribute);
                    }
                    else
                    {
                        var domain_index_clause = context.domain_index_clause();
                        if (domain_index_clause != null)
                        {
                            Stop();
                            VisitDomain_index_clause(domain_index_clause);
                        }
                        else
                        {
                            var xmlindex_clause = context.xmlindex_clause();
                            if (xmlindex_clause != null)
                            {
                                Stop();
                                VisitXmlindex_clause(xmlindex_clause);
                            }
                            else
                            {
                                Stop();
                            }
                        }
                    }
                }
            }

            return index;

        }

        /// <summary>
        /// global_partitioned_index :
        ///     GLOBAL PARTITION BY
        ///         ( 
        ///        'RANGE' paren_column_list LEFT_PAREN index_partitioning_clause RIGHT_PAREN 
	    /// 	 | 'HASH'  paren_column_list (individual_hash_partitions | hash_partitions_by_quantity )
        ///     )
	    /// ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitGlobal_partitioned_index([NotNull] PlSqlParser.Global_partitioned_indexContext context)
        {
            Stop();

            return base.VisitGlobal_partitioned_index(context);
        }

        /// <summary>
        /// local_partitioned_index :
	    ///     LOCAL(on_range_partitioned_table | on_list_partitioned_table | on_hash_partitioned_table | on_comp_partitioned_table )?
	    ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitLocal_partitioned_index([NotNull] PlSqlParser.Local_partitioned_indexContext context)
        {
            Stop();
            return base.VisitLocal_partitioned_index(context);
        }

        /// <summary>
        /// EMPTY
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitXmlindex_clause([NotNull] PlSqlParser.Xmlindex_clauseContext context)
        {
            Stop();
            return base.VisitXmlindex_clause(context);
        }

        /// <summary>
        /// references_clause :
        ///    REFERENCES tableview_name paren_column_list(ON DELETE (CASCADE | SET NULL))?
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitReferences_clause([NotNull] PlSqlParser.References_clauseContext context)
        {

            var constraint = new ConstraintModel()
            {
                Type = "R",
            };

            var names = context.tableview_name().table_fullname().GetCleanedTexts();
            if (names.Count == 2)
            {
                constraint.TableReference.Owner = names[0];
                constraint.TableReference.Name = names[1];
            }
            else if (names.Count == 1)
                constraint.TableReference.Name = names[0];

            var columns = (List<string>)VisitParen_column_list(context.paren_column_list());
            foreach (string item in columns)
                constraint.Columns.Add(new ConstraintColumnModel() { ColumnName = item, Position = constraint.Columns.Count, });

            if (context.ON().Exist() && context.DELETE().Exist())
            {
                if (context.CASCADE().Exist())
                    constraint.DeleteRule = "CASCADE";

                else if (context.SET().Exist() && context.NULL().Exist())
                    constraint.DeleteRule = "SET NULL";
            }
            else
                constraint.DeleteRule = "SET NULL";

            return constraint;

        }

        /// <summary>
        /// index_attributes :
        ///     physical_attributes_clause+
        ///     | logging_clause 
        ///     | ONLINE 
        ///     | TABLESPACE(tablespace_name | DEFAULT)
        ///     | advanced_index_compression 
        ///     | (SORT | NOSORT) 
        ///     | REVERSE 
        ///     | (VISIBLE | INVISIBLE) 
        ///     | partial_index_clause 
        ///     | parallel_clause    
	    /// ;
        /// </summary>é
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitIndex_attributes([NotNull] PlSqlParser.Index_attributesContext context)
        {

            var index = Current<IndexModel>();
            var physical_attributes_clauses = context.physical_attributes_clauses();
            if (physical_attributes_clauses != null)
            {
                foreach (var physical_attributes_clause in physical_attributes_clauses.physical_attributes_clause())
                {
                    Stop();
                    using (Enqueue(index.PhysicalAttributes))
                        VisitPhysical_attributes_clause(physical_attributes_clause);
                }
            }
            else
            {
                var logging_clause = context.logging_clause();
                if (logging_clause != null)
                {
                    Stop();
                    VisitLogging_clause(logging_clause);
                }
                else if (context.ONLINE().Exist())
                {
                    Stop();
                    index.ONLINE = true;
                }
                else if (context.TABLESPACE().Exist())
                {
                    if (context.DEFAULT().Exist())
                        index.PhysicalAttributes.Tablespace.Name = "DEFAULT";
                    else
                        index.PhysicalAttributes.Tablespace.Name = context.tablespace_name().id_expression().GetCleanedTexts().FirstOrDefault();
                }
                else
                {
                    var advanced_index_compression = context.advanced_index_compression();
                    if (advanced_index_compression != null)
                    {
                        Stop();
                        VisitAdvanced_index_compression(advanced_index_compression);
                    }
                    else if (context.SORT().Exist())
                    {
                        Stop();
                    }
                    else if (context.NOSORT().Exist())
                    {
                        Stop();
                    }
                    else if (context.REVERSE().Exist())
                    {
                        Stop();

                    }
                    else if (context.VISIBLE().Exist())
                    {
                        Stop();

                    }
                    else if (context.INVISIBLE().Exist())
                    {
                        Stop();
                    }
                    else
                    {
                        var partial_index_clause = context.partial_index_clause();
                        if (partial_index_clause != null)
                        {
                            Stop();
                            VisitPartial_index_clause(partial_index_clause);
                        }
                        else
                        {
                            var parallel_clause = context.parallel_clause();
                            if (parallel_clause != null)
                            {
                                Stop();
                                VisitParallel_clause(parallel_clause);
                            }

                        }
                    }
                }
            }

            return index;
        }

        /// <summary>
        /// out_of_line_ref_constraint :
        ///       SCOPE FOR LEFT_PAREN column_name /*| ref_attr*/ RIGHT_PAREN IS table_fullname
        ///     | REF LEFT_PAREN column_name /*| ref_attr*/ RIGHT_PAREN WITH ROWID 
        ///     | CONSTRAINT constraint_name? FOREIGN KEY paren_column_list references_clause constraint_state?
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitOut_of_line_ref_constraint([NotNull] PlSqlParser.Out_of_line_ref_constraintContext context)
        {
            Stop();
            List<ConstraintModel> result = new List<ConstraintModel>();

            //var result = base.VisitOut_of_line_ref_constraint(context);
            //Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// out_of_line_constraints+ constraint_state? 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitOut_of_line_constraint([NotNull] PlSqlParser.Out_of_line_constraintContext context)
        {

            List<ConstraintModel> result = new List<ConstraintModel>();
            var constraint_state = context.constraint_state();

            foreach (PlSqlParser.Out_of_line_constraintsContext out_of_line_constraints in context.out_of_line_constraints())
            {

                var constraint = (ConstraintModel)VisitOut_of_line_constraints(out_of_line_constraints);
                if (constraint_state != null)
                {
                    using (Enqueue(constraint))
                        VisitConstraint_state(constraint_state);
                }
                result.Add(constraint);

            }

            return result;

        }

        /// <summary>
        /// (CONSTRAINT constraint_name)?
        ///   ( primary_key_clause
        ///     | foreign_key_clause
        ///     | unique_key_clause
        ///     | check_constraint
        ///   )
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitOut_of_line_constraints([NotNull] PlSqlParser.Out_of_line_constraintsContext context)
        {


            var constraint = new ConstraintModel()
            {
                //Owner = table.Owner,
            };


            using (Enqueue(constraint))
                base.VisitOut_of_line_constraints(context);


            var constraint_name = context.constraint_name();
            if (constraint_name != null)
            {

                var names = constraint_name.GetCleanedTexts();
                if (names.Count == 1)
                {
                    constraint.Name = names[0];
                }
                else if (names.Count == 2)
                {
                    constraint.Owner = names[0];
                    constraint.Name = names[1];
                }

                if (string.IsNullOrEmpty(constraint.Owner))
                    if (!string.IsNullOrEmpty(constraint.TableReference.Owner))
                        constraint.Owner = constraint.TableReference.Owner;

            }

            AppendFile(constraint, context.Start);

            return constraint;

        }

        /// <summary>
        /// unique_key_clause :
        ///     UNIQUE paren_column_list
        ///     ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitUnique_key_clause([NotNull] PlSqlParser.Unique_key_clauseContext context)
        {

            var result = (List<string>)VisitParen_column_list(context.paren_column_list());

            var constraint = this.Current<ConstraintModel>();
            constraint.Type = "U";

            foreach (string item in result)
                constraint.Columns.Add(new ConstraintColumnModel() { ColumnName = item, Position = constraint.Columns.Count, });

            return constraint;
        }

        /// <summary>
        /// foreign_key_clause : FOREIGN KEY paren_column_list references_clause
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitForeign_key_clause([NotNull] PlSqlParser.Foreign_key_clauseContext context)
        {

            var constraint = this.Current<ConstraintModel>();
            constraint.Type = "R";

            var result = (List<string>)VisitParen_column_list(context.paren_column_list());
            foreach (string item in result)
                constraint.Columns.Add(new ConstraintColumnModel() { ColumnName = item, Position = constraint.Columns.Count, });

            var constraint2 = (ConstraintModel)VisitReferences_clause(context.references_clause());
            constraint.Reference.Set(constraint2);

            return constraint;

        }

        /// <summary>
        /// primary_key_clause :
        ///    PRIMARY KEY paren_column_list
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitPrimary_key_clause([NotNull] PlSqlParser.Primary_key_clauseContext context)
        {
            var result = (List<string>)VisitParen_column_list(context.paren_column_list());
            var constraint = Current<ConstraintModel>();
            constraint.Type = "P";

            foreach (string item in result)
                constraint.Columns.Add(new ConstraintColumnModel() { ColumnName = item, Position = constraint.Columns.Count, });

            return result;
        }

        /// <summary>
        /// LEFT_PAREN column_list RIGHT_PAREN
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitParen_column_list([NotNull] PlSqlParser.Paren_column_listContext context)
        {
            return VisitColumn_list(context.column_list());
        }

        /// <summary>
        /// column_list :
        ///     (COMMA? column_name)+
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitColumn_list([NotNull] PlSqlParser.Column_listContext context)
        {
            List<string> _columns = new List<string>();
            foreach (PlSqlParser.Column_nameContext column_list in context.column_name())
                _columns.Add(column_list.GetCleanedTexts().First());
            return _columns;
        }

        /// <summary>
        /// comment_on_column :
        ///    COMMENT ON COLUMN tableview_name PERIOD column_name IS string
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitComment_on_column([NotNull] PlSqlParser.Comment_on_columnContext context)
        {

            var result = new OTableColumnCommentStatement()
            {
                Comment = context.@string().GetText(),
            };

            var t = context.tableview_name();
            var names = t.table_fullname().GetCleanedTexts();

            if (names.Count == 1)
                result.Table = names[0];

            else if (names.Count == 2)
            {
                result.Owner = names[0];
                result.Table = names[1];
            }

            result.Column = context.column_name().GetCleanedTexts().First();


            AppendFile(result, context.Start);
            Append(result);

            return result;
        }

        /// <summary>
        /// COMMENT ON TABLE tableview_name IS string
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitComment_on_table([NotNull] PlSqlParser.Comment_on_tableContext context)
        {

            var result = new OTableCommentStatement()
            {
                Comment = context.@string().GetText(),
            };

            var t = context.tableview_name();
            var names = t.table_fullname().GetCleanedTexts();

            if (names.Count == 1)
                result.Table = names[0];

            else if (names.Count == 2)
            {
                result.Owner = names[0];
                result.Table = names[1];
            }

            AppendFile(result, context.Start);
            Append(result);

            return result;

        }

        /// <summary>
        /// alter_table :
        ///    ALTER TABLE tableview_name
        ///      (add_constraint
        ///      | drop_constraint
        ///      | enable_constraint
        ///      | disable_constraint
        ///
        ///      )
        ///    ;
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitAlter_table([NotNull] PlSqlParser.Alter_tableContext context)
        {
            Stop();
            var result = base.VisitAlter_table(context);
            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// DROP TABLE tableview_name SEMICOLON
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitDrop_table([NotNull] PlSqlParser.Drop_tableContext context)
        {
            Stop();
            var result = base.VisitDrop_table(context);
            Debug.Assert(result != null);
            return result;
        }



    }

}

