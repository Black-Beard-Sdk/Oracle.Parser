/**
 * Oracle(c) PL/SQL 11g Parser  
 *
 * Copyright (c) 2009-2011 Alexandre Porcelli <alexandre.porcelli@gmail.com>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
parser grammar PLSQLKeys;

options {
    output=AST;
	language=CSharp3;
}

tokens {
    DEBUG_VK;
    REUSE_VK;
    REPLACE_VK;
    DETERMINISTIC_VK;
    RESULT_CACHE_VK;
    PIPELINED_VK;
    AGGREGATE_VK;
    RELIES_ON_VK;
    AUTHID_VK;
    DEFINER_VK;
    CURRENT_USER_VK;
    CLUSTER_VK;
    PACKAGE_VK;
    BODY_VK;
    PARALLEL_ENABLE_VK;
    SPECIFICATION_VK;
    RANGE_VK;
    HASH_VK;
    EXTERNAL_VK;
    CALL_VK;
    DDL_VK;
    ENABLE_VK;
    DATABASE_VK;
    DISABLE_VK;
    BEFORE_VK;
    REFERENCING_VK;
    LOGON_VK;
    AFTER_VK;
    SCHEMA_VK;
    TRUNCATE_VK;
    STARTUP_VK;
    STATISTICS_VK;
    NOAUDIT_VK;
    SUSPEND_VK;
    AUDIT_VK;
    DISASSOCIATE_VK;
    SHUTDOWN_VK;
    COMPOUND_VK;
    SERVERERROR_VK;
    PARENT_VK;
    FOLLOWS_VK;
    NESTED_VK;
    OLD_VK;
    DB_ROLE_CHANGE_VK;
    LOGOFF_VK;
    ANALYZE_VK;
    INSTEAD_VK;
    ASSOCIATE_VK;
    NEW_VK;
    RENAME_VK;
    COMMENT_VK;
    FORCE_VK;
    VALIDATE_VK;
    TYPE_VK;
    COMPILE_VK;
    EXCEPTIONS_VK;
    LIMIT_VK;
    INSTANTIABLE_VK;
    FINAL_VK;
    ADD_VK;
    MODIFY_VK;
    INCLUDING_VK;
    CASCADE_VK;
    INVALIDATE_VK;
    CONVERT_VK;
    LANGUAGE_VK;
    JAVA_VK;
    OVERRIDING_VK;
    C_VK;
    LIBRARY_VK;
    CONTEXT_VK;
    OUT_VK;
    INOUT_VK;
    PARAMETERS_VK;
    AGENT_VK;
    NOCOPY_VK;
    PRAGMA_VK;
    CUSTOMDATUM_VK;
    ORADATA_VK;
    CONSTRUCTOR_VK;
    SQLDATA_VK;
    MEMBER_VK;
    SELF_VK;
    OBJECT_VK;
    STATIC_VK;
    UNDER_VK;
    MAP_VK;
    CONSTANT_VK;
    EXCEPTION_INIT_VK;
    PERCENT_NOTFOUND_VK;
    PERCENT_FOUND_VK;
    PERCENT_ISOPEN_VK;
    PERCENT_ROWCOUNT_VK;
    PERCENT_ROWTYPE_VK;
    PERCENT_TYPE_VK;
    SERIALLY_REUSABLE_VK;
    AUTONOMOUS_TRANSACTION_VK;
    INLINE_VK;
    RESTRICT_REFERENCES_VK;
    EXIT_VK;
    RETURN_VK;
    RAISE_VK;
    LOOP_VK;
    FORALL_VK;
    CONTINUE_VK;
    REVERSE_VK;
    OFF_VK;
    EXECUTE_VK;
    IMMEDIATE_VK;
    COMMIT_VK;
    WORK_VK;
    BULK_VK;
    COMMITTED_VK;
    ISOLATION_VK;
    SERIALIZABLE_VK;
    WRITE_VK;
    WAIT_VK;
    CORRUPT_XID_ALL_VK;
    CORRUPT_XID_VK;
    BATCH_VK;
    DEFERRED_VK;
    ROLLBACK_VK;
    OPEN_VK;
    SAVEPOINT_VK;
    CLOSE_VK;
    READ_VK;
    ONLY_VK;
    REF_VK;
    PLS_INTEGER_VK;
    SUBPARTITION_VK;
    PARTITION_VK;
    TIMESTAMP_TZ_UNCONSTRAINED_VK;
    UROWID_VK;
    POSITIVEN_VK;
    TIMEZONE_ABBR_VK;
    BINARY_DOUBLE_VK;
    BFILE_VK;
    TIMEZONE_REGION_VK;
    TIMESTAMP_LTZ_UNCONSTRAINED_VK;
    NATURALN_VK;
    SIMPLE_INTEGER_VK;
    BYTE_VK;
    BINARY_FLOAT_VK;
    NCLOB_VK;
    CLOB_VK;
    DSINTERVAL_UNCONSTRAINED_VK;
    YMINTERVAL_UNCONSTRAINED_VK;
    ROWID_VK;
    TIMESTAMP_UNCONSTRAINED_VK;
    SIGNTYPE_VK;
    BLOB_VK;
    NVARCHAR2_VK;
    STRING_VK;
    MAXVALUE_VK;
    MINVALUE_VK;
    DBTIMEZONE_VK;
    SESSIONTIMEZONE_VK;
    RAW_VK;
    NUMBER_VK;
    VARCHAR2_VK;
    BOOLEAN_VK;
    POSITIVE_VK;
    MLSLABEL_VK;
    BINARY_INTEGER_VK;
    LONG_VK;
    CHARACTER_VK;
    CHAR_VK;
    VARCHAR_VK;
    NCHAR_VK;
    BIT_VK;
    FLOAT_VK;
    REAL_VK;
    DOUBLE_VK;
    PRECISION_VK;
    TIME_VK;
    TIMESTAMP_VK;
    NUMERIC_VK;
    DECIMAL_VK;
    DEC_VK;
    INTEGER_VK;
    INT_VK;
    SMALLINT_VK;
    NATURAL_VK;
    SECOND_VK;
    TIMEZONE_HOUR_VK;
    TIMEZONE_MINUTE_VK;
    LOCAL_VK;
    YEAR_VK;
    MONTH_VK;
    DAY_VK;
    HOUR_VK;
    MINUTE_VK;
    MERGE_VK;
    REJECT_VK;
    LOG_VK;
    UNLIMITED_VK;
    FIRST_VK;
    NOCYCLE_VK;
    BLOCK_VK;
    XML_VK;
    PIVOT_VK;
    SEQUENTIAL_VK;
    SINGLE_VK;
    SKIP_VK;
    UPDATED_VK;
    EXCLUDE_VK;
    REFERENCE_VK;
    UNTIL_VK;
    SEED_VK;
    SIBLINGS_VK;
    CUBE_VK;
    NULLS_VK;
    DIMENSION_VK;
    SCN_VK;
    UNPIVOT_VK;
    KEEP_VK;
    MEASURES_VK;
    SAMPLE_VK;
    UPSERT_VK;
    VERSIONS_VK;
    RULES_VK;
    ITERATE_VK;
    ROLLUP_VK;
    NAV_VK;
    AUTOMATIC_VK;
    LAST_VK;
    GROUPING_VK;
    INCLUDE_VK;
    IGNORE_VK;
    RESPECT_VK;
    SUBMULTISET_VK;
    LIKEC_VK;
    LIKE2_VK;
    LIKE4_VK;
    ROW_VK;
    SET_VK;
    SOME_VK;
    FULL_VK;
    CROSS_VK;
    LEFT_VK;
    RIGHT_VK;
    INNER_VK;
    VALUE_VK;
    INCREMENT_VK;
    DECREMENT_VK;
    AT_VK;
    DENSE_RANK_VK;
    NAME_VK;
    COLLECT_VK;
    ROWS_VK;
    NCHAR_CS_VK;
    DECOMPOSE_VK;
    FOLLOWING_VK;
    FIRST_VALUE_VK;
    PRECEDING_VK;
    WITHIN_VK;
    CANONICAL_VK;
    COMPATIBILITY_VK;
    OVER_VK;
    LAST_VALUE_VK;
    CURRENT_VK;
    UNBOUNDED_VK;
    COST_VK;
    CHAR_CS_VK;
    AUTO_VK;
    TREAT_VK;
    CONTENT_VK;
    XMLPARSE_VK;
    XMLELEMENT_VK;
    ENTITYESCAPING_VK;
    STANDALONE_VK;
    WELLFORMED_VK;
    XMLEXISTS_VK;
    VERSION_VK;
    XMLCAST_VK;
    YES_VK;
    NO_VK;
    EVALNAME_VK;
    XMLPI_VK;
    XMLCOLATTVAL_VK;
    DOCUMENT_VK;
    XMLFOREST_VK;
    PASSING_VK;
    INDENT_VK;
    HIDE_VK;
    XMLAGG_VK;
    XMLNAMESPACES_VK;
    NOSCHEMACHECK_VK;
    NOENTITYESCAPING_VK;
    XMLQUERY_VK;
    XMLTABLE_VK;
    XMLROOT_VK;
    SCHEMACHECK_VK;
    XMLATTRIBUTES_VK;
    ENCODING_VK;
    SHOW_VK;
    XMLSERIALIZE_VK;
    ORDINALITY_VK;
    DEFAULTS_VK;
    CHR_VK;
    COUNT_VK;
    CAST_VK;
    TRANSLATE_VK;
    TRIM_VK;
    LEADING_VK;
    TRAILING_VK;
    BOTH_VK;
    EXTRACT_VK;
    SEQUENCE_VK;
    NOORDER_VK;
    CYCLE_VK;
    CACHE_VK;
    NOCACHE_VK;
    NOMAXVALUE_VK;
    NOMINVALUE_VK;
    SEARCH_VK;
    DEPTH_VK;
    BREADTH_VK;
}

create_key
    :    SQL92_RESERVED_CREATE
    ;
    
replace_key
    :    {input.LT(1).Text.Equals("replace", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> REPLACE_VK[$REGULAR_ID]
    ;

package_key
    :    {input.LT(1).Text.Equals("package", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> PACKAGE_VK[$REGULAR_ID]
    ;

body_key
    :    {input.LT(1).Text.Equals("body")}? REGULAR_ID -> BODY_VK[$REGULAR_ID]
    ;

begin_key
    :    SQL92_RESERVED_BEGIN
    ;

exit_key:    {input.LT(1).Text.Equals("exit")}? REGULAR_ID -> EXIT_VK[$REGULAR_ID]
    ;

declare_key
    :    SQL92_RESERVED_DECLARE
    ;

exception_key
    :    SQL92_RESERVED_EXCEPTION
    ;

serveroutput_key
    :    {input.LT(1).Text.Equals("serveroutput")}? REGULAR_ID
    ;

off_key
    :    {input.LT(1).Text.Equals("off")}? REGULAR_ID -> OFF_VK[$REGULAR_ID]
    ;

constant_key
    :    {input.LT(1).Text.Equals("constant")}? REGULAR_ID -> CONSTANT_VK[$REGULAR_ID]
    ;

subtype_key
    :    {input.LT(1).Text.Equals("subtype")}? REGULAR_ID
    ;

cursor_key//{input.LT(1).Text.Equals("cursor")}? REGULAR_ID
    :    SQL92_RESERVED_CURSOR
    ;

nextval_key
    :    {input.LT(1).Text.Equals("nextval")}?=> REGULAR_ID
    ;

goto_key
    :    SQL92_RESERVED_GOTO
    ;

execute_key
    :    {input.LT(1).Text.Equals("execute")}? REGULAR_ID -> EXECUTE_VK[$REGULAR_ID]
    ;

immediate_key
    :    {input.LT(1).Text.Equals("immediate", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> IMMEDIATE_VK[$REGULAR_ID]
    ;

return_key
    :    {input.LT(1).Text.Equals("return")}? REGULAR_ID -> RETURN_VK[$REGULAR_ID]
    ;

procedure_key
    :    SQL92_RESERVED_PROCEDURE
    ;

function_key
    :    {input.LT(1).Text.Equals("function")}?=> REGULAR_ID
    ;

pragma_key
    :    {input.LT(1).Text.Equals("pragma")}? REGULAR_ID -> PRAGMA_VK[$REGULAR_ID]
    ;

exception_init_key
    :    {input.LT(1).Text.Equals("exception_init")}? REGULAR_ID -> EXCEPTION_INIT_VK[$REGULAR_ID]
    ;

type_key
    :    {input.LT(1).Text.Equals("type", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> TYPE_VK[$REGULAR_ID]
    ;

record_key
    :    {input.LT(1).Text.Equals("record")}?=> REGULAR_ID
    ;

indexed_key
    :    {input.LT(1).Text.Equals("indexed")}? REGULAR_ID
    ;

index_key
    :    PLSQL_RESERVED_INDEX
    ;

percent_notfound_key
    :    {input.LT(2).Text.Equals("notfound")}?=> PERCENT REGULAR_ID -> PERCENT_NOTFOUND_VK[$REGULAR_ID, $REGULAR_ID.text]
    ;

percent_found_key
    :    {input.LT(2).Text.Equals("found")}?=> PERCENT REGULAR_ID -> PERCENT_FOUND_VK[$REGULAR_ID, $REGULAR_ID.text]
    ;

percent_isopen_key
    :    {input.LT(2).Text.Equals("isopen")}?=> PERCENT REGULAR_ID -> PERCENT_ISOPEN_VK[$REGULAR_ID, $REGULAR_ID.text]
    ;

percent_rowcount_key
    :    {input.LT(2).Text.Equals("rowcount")}?=> PERCENT REGULAR_ID -> PERCENT_ROWCOUNT_VK[$REGULAR_ID, $REGULAR_ID.text]
    ;

percent_rowtype_key
    :    {input.LT(2).Text.Equals("rowtype")}?=> PERCENT REGULAR_ID -> PERCENT_ROWTYPE_VK[$REGULAR_ID, $REGULAR_ID.text] 
    ;

percent_type_key
    :    {input.LT(2).Text.Equals("type")}?=> PERCENT REGULAR_ID -> PERCENT_TYPE_VK[$REGULAR_ID, $REGULAR_ID.text]
    ;

out_key
    :    {input.LT(1).Text.Equals("out", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> OUT_VK[$REGULAR_ID]
    ;

inout_key
    :    {input.LT(1).Text.Equals("inout")}? REGULAR_ID -> INOUT_VK[$REGULAR_ID]
    ;

extend_key
    :    {input.LT(1).Text.Equals("extend")}?=> REGULAR_ID
    ;

raise_key
    :    {input.LT(1).Text.Equals("raise")}? REGULAR_ID -> RAISE_VK[$REGULAR_ID]
    ;

while_key
    :    {input.LT(1).Text.Equals("while")}? REGULAR_ID
    ;

loop_key
    :    {input.LT(1).Text.Equals("loop")}? REGULAR_ID -> LOOP_VK[$REGULAR_ID]
    ;

commit_key
    :    {input.LT(1).Text.Equals("commit", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> COMMIT_VK[$REGULAR_ID]
    ;

work_key:    {input.LT(1).Text.Equals("work")}? REGULAR_ID -> WORK_VK[$REGULAR_ID]
    ;

if_key
    :    PLSQL_RESERVED_IF
    ;

elsif_key
    :    PLSQL_NON_RESERVED_ELSIF
    ;

authid_key
    :    {input.LT(1).Text.Equals("authid", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> AUTHID_VK[$REGULAR_ID]
    ;

definer_key
    :    {input.LT(1).Text.Equals("definer")}? REGULAR_ID -> DEFINER_VK[$REGULAR_ID]
    ;

external_key
    :    {input.LT(1).Text.Equals("external")}? REGULAR_ID -> EXTERNAL_VK[$REGULAR_ID]
    ;

language_key
    :    {input.LT(1).Text.Equals("language")}? REGULAR_ID -> LANGUAGE_VK[$REGULAR_ID]
    ;

java_key
    :    {input.LT(1).Text.Equals("java")}? REGULAR_ID -> JAVA_VK[$REGULAR_ID]
    ;

name_key
    :    {input.LT(1).Text.Equals("name", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NAME_VK[$REGULAR_ID]
    ;

deterministic_key
    :    {input.LT(1).Text.Equals("deterministic", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DETERMINISTIC_VK[$REGULAR_ID]
    ;

parallel_enable_key
    :    {input.LT(1).Text.Equals("parallel_enable", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> PARALLEL_ENABLE_VK[$REGULAR_ID]
    ;

result_cache_key
    :    {input.LT(1).Text.Equals("result_cache", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> RESULT_CACHE_VK[$REGULAR_ID]
    ;

pipelined_key
    :    {input.LT(1).Text.Equals("pipelined", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> PIPELINED_VK[$REGULAR_ID]
    ;

aggregate_key
    :    {input.LT(1).Text.Equals("aggregate")}? REGULAR_ID -> AGGREGATE_VK[$REGULAR_ID]
    ;

alter_key
    :    SQL92_RESERVED_ALTER
    ;

compile_key
    :    {input.LT(1).Text.Equals("compile")}? REGULAR_ID -> COMPILE_VK[$REGULAR_ID]
    ; 

debug_key
    :    {input.LT(1).Text.Equals("debug")}? REGULAR_ID -> DEBUG_VK[$REGULAR_ID]
    ;

reuse_key
    :    {input.LT(1).Text.Equals("reuse")}? REGULAR_ID -> REUSE_VK[$REGULAR_ID]
    ;

settings_key
    :    {input.LT(1).Text.Equals("settings")}? REGULAR_ID
    ;

specification_key
    :    {input.LT(1).Text.Equals("specification")}? REGULAR_ID -> SPECIFICATION_VK[$REGULAR_ID]
    ;

drop_key
    :    SQL92_RESERVED_DROP
    ;

trigger_key
    :    {input.LT(1).Text.Equals("trigger")}?=> REGULAR_ID
    ;

force_key
    :    {input.LT(1).Text.Equals("force", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> FORCE_VK[$REGULAR_ID]
    ;

validate_key
    :    {input.LT(1).Text.Equals("validate")}? REGULAR_ID -> VALIDATE_VK[$REGULAR_ID]
    ;

ref_key
    :    {input.LT(1).Text.Equals("ref", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> REF_VK[$REGULAR_ID]
    ;

array_key
    :    {input.LT(1).Text.Equals("array")}?=> REGULAR_ID
    ;

varray_key
    :    {input.LT(1).Text.Equals("varray")}?=> REGULAR_ID
    ;

pls_integer_key
    :    {input.LT(1).Text.Equals("pls_integer", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> PLS_INTEGER_VK[$REGULAR_ID]
    ;

serially_reusable_key
    :    {input.LT(1).Text.Equals("serially_reusable", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SERIALLY_REUSABLE_VK[$REGULAR_ID]
    ;

autonomous_transaction_key
    :    {input.LT(1).Text.Equals("autonomous_transaction", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> AUTONOMOUS_TRANSACTION_VK[$REGULAR_ID]
    ;

inline_key
    :    {input.LT(1).Text.Equals("inline", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> INLINE_VK[$REGULAR_ID]
    ;

restrict_references_key
    :    {input.LT(1).Text.Equals("restrict_references", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> RESTRICT_REFERENCES_VK[$REGULAR_ID]
    ;

exceptions_key
    :    {input.LT(1).Text.Equals("exceptions", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> EXCEPTIONS_VK[$REGULAR_ID] 
    ;

save_key
    :    {input.LT(1).Text.Equals("save")}?=> REGULAR_ID
    ;

forall_key
    :    {input.LT(1).Text.Equals("forall", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> FORALL_VK[$REGULAR_ID]
    ;

continue_key
    :    {input.LT(1).Text.Equals("continue", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CONTINUE_VK[$REGULAR_ID]
    ;

indices_key
    :    {input.LT(1).Text.Equals("indices")}?=> REGULAR_ID
    ;

values_key
    :    SQL92_RESERVED_VALUES
    ;

case_key
    :    SQL92_RESERVED_CASE
    ;

bulk_key
    :    {input.LT(1).Text.Equals("bulk", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> BULK_VK[$REGULAR_ID]
    ;

collect_key
    :    {input.LT(1).Text.Equals("collect", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> COLLECT_VK[$REGULAR_ID]
    ;

committed_key
    :    {input.LT(1).Text.Equals("committed")}? REGULAR_ID -> COMMITTED_VK[$REGULAR_ID]
    ;

use_key
    :    {input.LT(1).Text.Equals("use")}?=> REGULAR_ID
    ;

level_key
    :    {input.LT(1).Text.Equals("level")}? REGULAR_ID
    ;

isolation_key
    :    {input.LT(1).Text.Equals("isolation", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> ISOLATION_VK[$REGULAR_ID]
    ;

serializable_key
    :    {input.LT(1).Text.Equals("serializable")}? REGULAR_ID -> SERIALIZABLE_VK[$REGULAR_ID]
    ;

segment_key
    :    {input.LT(1).Text.Equals("segment")}? REGULAR_ID
    ;

write_key
    :    {input.LT(1).Text.Equals("write", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> WRITE_VK[$REGULAR_ID]
    ;

wait_key
    :    {input.LT(1).Text.Equals("wait", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> WAIT_VK[$REGULAR_ID]
    ;

corrupt_xid_all_key
    :    {input.LT(1).Text.Equals("corrupt_xid_all", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CORRUPT_XID_ALL_VK[$REGULAR_ID]
    ;

corrupt_xid_key
    :    {input.LT(1).Text.Equals("corrupt_xid", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CORRUPT_XID_VK[$REGULAR_ID]
    ;

batch_key
    :    {input.LT(1).Text.Equals("batch", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> BATCH_VK[$REGULAR_ID]
    ;

session_key
    :    {input.LT(1).Text.Equals("session")}?=> REGULAR_ID
    ;

role_key
    :    {input.LT(1).Text.Equals("role")}?=> REGULAR_ID
    ;

constraint_key
    :    {input.LT(1).Text.Equals("constraint")}?=> REGULAR_ID
    ;

constraints_key
    :    {input.LT(1).Text.Equals("constraints")}?=> REGULAR_ID
    ;

call_key
    :    {input.LT(1).Text.Equals("call", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CALL_VK[$REGULAR_ID]
    ;

explain_key
    :    {input.LT(1).Text.Equals("explain")}?=> REGULAR_ID
    ;

merge_key
    :    {input.LT(1).Text.Equals("merge", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> MERGE_VK[$REGULAR_ID]
    ;

plan_key
    :    {input.LT(1).Text.Equals("plan")}?=> REGULAR_ID
    ;

system_key
    :    {input.LT(1).Text.Equals("system")}?=> REGULAR_ID
    ;

subpartition_key
    :    {input.LT(1).Text.Equals("subpartition", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SUBPARTITION_VK[$REGULAR_ID]
    ;

partition_key
    :    {input.LT(1).Text.Equals("partition", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> PARTITION_VK[$REGULAR_ID]
    ;

matched_key
    :    {input.LT(1).Text.Equals("matched")}?=> REGULAR_ID
    ;

reject_key
    :    {input.LT(1).Text.Equals("reject", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> REJECT_VK[$REGULAR_ID]
    ;

log_key
    :    {input.LT(1).Text.Equals("log", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> LOG_VK[$REGULAR_ID]
    ;

unlimited_key
    :    {input.LT(1).Text.Equals("unlimited", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> UNLIMITED_VK[$REGULAR_ID]
    ;

limit_key
    :    {input.LT(1).Text.Equals("limit", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> LIMIT_VK[$REGULAR_ID]
    ;

errors_key
    :    {input.LT(1).Text.Equals("errors")}?=> REGULAR_ID
    ;

timestamp_tz_unconstrained_key
    :    {input.LT(1).Text.Equals("timestamp_tz_unconstrained", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> TIMESTAMP_TZ_UNCONSTRAINED_VK[$REGULAR_ID]
    ;

urowid_key
    :    {input.LT(1).Text.Equals("urowid", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> UROWID_VK[$REGULAR_ID]
    ;

binary_float_min_subnormal_key
    :    {input.LT(1).Text.Equals("binary_float_min_subnormal")}?=> REGULAR_ID
    ;

binary_double_min_normal_key
    :    {input.LT(1).Text.Equals("binary_double_min_normal")}?=> REGULAR_ID
    ;

binary_float_max_normal_key
    :    {input.LT(1).Text.Equals("binary_float_max_normal")}?=> REGULAR_ID
    ;

positiven_key
    :    {input.LT(1).Text.Equals("positiven", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> POSITIVEN_VK[$REGULAR_ID]
    ;

timezone_abbr_key
    :    {input.LT(1).Text.Equals("timezone_abbr", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> TIMEZONE_ABBR_VK[$REGULAR_ID]
    ;

binary_double_min_subnormal_key
    :    {input.LT(1).Text.Equals("binary_double_min_subnormal")}?=> REGULAR_ID
    ;

binary_float_max_subnormal_key
    :    {input.LT(1).Text.Equals("binary_float_max_subnormal")}?=> REGULAR_ID
    ;

binary_double_key
    :    {input.LT(1).Text.Equals("binary_double", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> BINARY_DOUBLE_VK[$REGULAR_ID]
    ;

bfile_key
    :    {input.LT(1).Text.Equals("bfile", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> BFILE_VK[$REGULAR_ID]
    ;

binary_double_infinity_key
    :    {input.LT(1).Text.Equals("binary_double_infinity")}?=> REGULAR_ID
    ;

timezone_region_key
    :    {input.LT(1).Text.Equals("timezone_region", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> TIMEZONE_REGION_VK[$REGULAR_ID]
    ;

timestamp_ltz_unconstrained_key
    :    {input.LT(1).Text.Equals("timestamp_ltz_unconstrained", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> TIMESTAMP_LTZ_UNCONSTRAINED_VK[$REGULAR_ID]
    ;

naturaln_key
    :    {input.LT(1).Text.Equals("naturaln", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NATURALN_VK[$REGULAR_ID]
    ;

simple_integer_key
    :    {input.LT(1).Text.Equals("simple_integer", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SIMPLE_INTEGER_VK[$REGULAR_ID]
    ;

binary_double_max_subnormal_key
    :    {input.LT(1).Text.Equals("binary_double_max_subnormal")}?=> REGULAR_ID
    ;

byte_key
    :    {input.LT(1).Text.Equals("byte", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> BYTE_VK[$REGULAR_ID]
    ;

binary_float_infinity_key
    :    {input.LT(1).Text.Equals("binary_float_infinity")}?=> REGULAR_ID
    ;

binary_float_key
    :    {input.LT(1).Text.Equals("binary_float", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> BINARY_FLOAT_VK[$REGULAR_ID]
    ;

range_key
    :    {input.LT(1).Text.Equals("range", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> RANGE_VK[$REGULAR_ID]
    ;

nclob_key
    :    {input.LT(1).Text.Equals("nclob", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NCLOB_VK[$REGULAR_ID]
    ;

clob_key
    :    {input.LT(1).Text.Equals("clob", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CLOB_VK[$REGULAR_ID]
    ;

dsinterval_unconstrained_key
    :    {input.LT(1).Text.Equals("dsinterval_unconstrained", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DSINTERVAL_UNCONSTRAINED_VK[$REGULAR_ID]
    ;

yminterval_unconstrained_key
    :    {input.LT(1).Text.Equals("yminterval_unconstrained", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> YMINTERVAL_UNCONSTRAINED_VK[$REGULAR_ID]
    ;

rowid_key
    :    {input.LT(1).Text.Equals("rowid", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> ROWID_VK[$REGULAR_ID]
    ;

binary_double_nan_key
    :    {input.LT(1).Text.Equals("binary_double_nan")}?=> REGULAR_ID
    ;

timestamp_unconstrained_key
    :    {input.LT(1).Text.Equals("timestamp_unconstrained", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> TIMESTAMP_UNCONSTRAINED_VK[$REGULAR_ID]
    ;

binary_float_min_normal_key
    :    {input.LT(1).Text.Equals("binary_float_min_normal")}?=> REGULAR_ID
    ;

signtype_key
    :    {input.LT(1).Text.Equals("signtype", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SIGNTYPE_VK[$REGULAR_ID]
    ;

blob_key
    :    {input.LT(1).Text.Equals("blob", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> BLOB_VK[$REGULAR_ID]
    ;

nvarchar2_key
    :    {input.LT(1).Text.Equals("nvarchar2", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NVARCHAR2_VK[$REGULAR_ID]
    ;

binary_double_max_normal_key
    :    {input.LT(1).Text.Equals("binary_double_max_normal")}?=> REGULAR_ID
    ;

binary_float_nan_key
    :    {input.LT(1).Text.Equals("binary_float_nan")}?=> REGULAR_ID
    ;

string_key
    :    {input.LT(1).Text.Equals("string", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> STRING_VK[$REGULAR_ID]
    ;

c_key
    :    {input.LT(1).Text.Equals("c", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> C_VK[$REGULAR_ID]
    ;

library_key
    :    {input.LT(1).Text.Equals("library", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> LIBRARY_VK[$REGULAR_ID]
    ;

context_key
    :    {input.LT(1).Text.Equals("context", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CONTEXT_VK[$REGULAR_ID]
    ;

parameters_key
    :    {input.LT(1).Text.Equals("parameters", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> PARAMETERS_VK[$REGULAR_ID]
    ;

agent_key
    :    {input.LT(1).Text.Equals("agent", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> AGENT_VK[$REGULAR_ID]
    ;

cluster_key
    :    {input.LT(1).Text.Equals("cluster", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CLUSTER_VK[$REGULAR_ID]
    ;

hash_key
    :    {input.LT(1).Text.Equals("hash", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> HASH_VK[$REGULAR_ID]
    ;

relies_on_key
    :    {input.LT(1).Text.Equals("relies_on", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> RELIES_ON_VK[$REGULAR_ID]
    ;

returning_key
    :    {input.LT(1).Text.Equals("returning")}?=> REGULAR_ID
    ;    

statement_id_key
    :    {input.LT(1).Text.Equals("statement_id")}?=> REGULAR_ID
    ;

deferred_key
    :    {input.LT(1).Text.Equals("deferred", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DEFERRED_VK[$REGULAR_ID]
    ;

advise_key
    :    {input.LT(1).Text.Equals("advise")}?=> REGULAR_ID
    ;

resumable_key
    :    {input.LT(1).Text.Equals("resumable")}?=> REGULAR_ID
    ;

timeout_key
    :    {input.LT(1).Text.Equals("timeout")}?=> REGULAR_ID
    ;

parallel_key
    :    {input.LT(1).Text.Equals("parallel")}?=> REGULAR_ID
    ;

ddl_key
    :    {input.LT(1).Text.Equals("ddl", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DDL_VK[$REGULAR_ID]
    ;

query_key
    :    {input.LT(1).Text.Equals("query")}?=> REGULAR_ID
    ;

dml_key
    :    {input.LT(1).Text.Equals("dml")}?=> REGULAR_ID
    ;

guard_key
    :    {input.LT(1).Text.Equals("guard")}?=> REGULAR_ID
    ;

nothing_key
    :    {input.LT(1).Text.Equals("nothing")}?=> REGULAR_ID
    ;

enable_key
    :    {input.LT(1).Text.Equals("enable", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> ENABLE_VK[$REGULAR_ID]
    ;

database_key
    :    {input.LT(1).Text.Equals("database", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DATABASE_VK[$REGULAR_ID]
    ;

disable_key
    :    {input.LT(1).Text.Equals("disable", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DISABLE_VK[$REGULAR_ID]
    ;

link_key
    :    {input.LT(1).Text.Equals("link")}?=> REGULAR_ID
    ;

identified_key
    :    PLSQL_RESERVED_IDENTIFIED
    ;

none_key
    :    {input.LT(1).Text.Equals("none")}?=> REGULAR_ID
    ;

before_key
    :    {input.LT(1).Text.Equals("before", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> BEFORE_VK[$REGULAR_ID] 
    ;

referencing_key
    :    {input.LT(1).Text.Equals("referencing", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> REFERENCING_VK[$REGULAR_ID]
    ;

logon_key
    :    {input.LT(1).Text.Equals("logon", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> LOGON_VK[$REGULAR_ID]
    ;

after_key
    :    {input.LT(1).Text.Equals("after", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> AFTER_VK[$REGULAR_ID]
    ;

schema_key
    :    {input.LT(1).Text.Equals("schema", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SCHEMA_VK[$REGULAR_ID]
    ;

grant_key
    :    SQL92_RESERVED_GRANT
    ;

truncate_key
    :    {input.LT(1).Text.Equals("truncate", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> TRUNCATE_VK[$REGULAR_ID]
    ;

startup_key
    :    {input.LT(1).Text.Equals("startup", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> STARTUP_VK[$REGULAR_ID]
    ;

statistics_key
    :    {input.LT(1).Text.Equals("statistics", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> STATISTICS_VK[$REGULAR_ID]
    ;

noaudit_key
    :    {input.LT(1).Text.Equals("noaudit", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NOAUDIT_VK[$REGULAR_ID]
    ;

suspend_key
    :    {input.LT(1).Text.Equals("suspend", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SUSPEND_VK[$REGULAR_ID]
    ;

audit_key
    :    {input.LT(1).Text.Equals("audit", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> AUDIT_VK[$REGULAR_ID]
    ;

disassociate_key
    :    {input.LT(1).Text.Equals("disassociate", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DISASSOCIATE_VK[$REGULAR_ID] 
    ;

shutdown_key
    :    {input.LT(1).Text.Equals("shutdown", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SHUTDOWN_VK[$REGULAR_ID]
    ;

compound_key
    :    {input.LT(1).Text.Equals("compound", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> COMPOUND_VK[$REGULAR_ID]
    ;

servererror_key
    :    {input.LT(1).Text.Equals("servererror", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SERVERERROR_VK[$REGULAR_ID]
    ;

parent_key
    :    {input.LT(1).Text.Equals("parent", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> PARENT_VK[$REGULAR_ID]
    ;

follows_key
    :    {input.LT(1).Text.Equals("follows", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> FOLLOWS_VK[$REGULAR_ID]
    ;

nested_key
    :    {input.LT(1).Text.Equals("nested", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NESTED_VK[$REGULAR_ID]
    ;

old_key
    :    {input.LT(1).Text.Equals("old", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> OLD_VK[$REGULAR_ID]
    ;

statement_key
    :    {input.LT(1).Text.Equals("statement")}?=> REGULAR_ID
    ;

db_role_change_key
    :    {input.LT(1).Text.Equals("db_role_change", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DB_ROLE_CHANGE_VK[$REGULAR_ID]
    ;

each_key
    :    {input.LT(1).Text.Equals("each")}?=> REGULAR_ID
    ;

logoff_key
    :    {input.LT(1).Text.Equals("logoff", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> LOGOFF_VK[$REGULAR_ID]
    ;

analyze_key
    :    {input.LT(1).Text.Equals("analyze", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> ANALYZE_VK[$REGULAR_ID]
    ;

instead_key
    :    {input.LT(1).Text.Equals("instead", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> INSTEAD_VK[$REGULAR_ID]
    ;

associate_key
    :    {input.LT(1).Text.Equals("associate", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> ASSOCIATE_VK[$REGULAR_ID]
    ;

new_key
    :    {input.LT(1).Text.Equals("new", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NEW_VK[$REGULAR_ID]
    ;

revoke_key
    :    SQL92_RESERVED_REVOKE
    ;

rename_key
    :    {input.LT(1).Text.Equals("rename", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> RENAME_VK[$REGULAR_ID] 
    ;

customdatum_key
    :    {input.LT(1).Text.Equals("customdatum", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CUSTOMDATUM_VK[$REGULAR_ID]
    ;

oradata_key
    :    {input.LT(1).Text.Equals("oradata", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> ORADATA_VK[$REGULAR_ID]
    ;

constructor_key
    :    {input.LT(1).Text.Equals("constructor", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CONSTRUCTOR_VK[$REGULAR_ID]
    ;

sqldata_key
    :    {input.LT(1).Text.Equals("sqldata", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SQLDATA_VK[$REGULAR_ID]
    ;

member_key
    :    {input.LT(1).Text.Equals("member", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> MEMBER_VK[$REGULAR_ID]
    ;

self_key
    :    {input.LT(1).Text.Equals("self", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SELF_VK[$REGULAR_ID]
    ;

object_key
    :    {input.LT(1).Text.Equals("object", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> OBJECT_VK[$REGULAR_ID]
    ;

variable_key
    :    {input.LT(1).Text.Equals("variable")}?=> REGULAR_ID
    ;

instantiable_key
    :    {input.LT(1).Text.Equals("instantiable", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> INSTANTIABLE_VK[$REGULAR_ID]
    ;

final_key
    :    {input.LT(1).Text.Equals("final", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> FINAL_VK[$REGULAR_ID]
    ;

static_key
    :    {input.LT(1).Text.Equals("static", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> STATIC_VK[$REGULAR_ID]
    ;

oid_key
    :    {input.LT(1).Text.Equals("oid")}?=> REGULAR_ID
    ;

result_key
    :    {input.LT(1).Text.Equals("result")}?=> REGULAR_ID
    ;

under_key
    :    {input.LT(1).Text.Equals("under", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> UNDER_VK[$REGULAR_ID]
    ;

map_key
    :    {input.LT(1).Text.Equals("map", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> MAP_VK[$REGULAR_ID]
    ;

overriding_key
    :    {input.LT(1).Text.Equals("overriding", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> OVERRIDING_VK[$REGULAR_ID]
    ;

add_key
    :    {input.LT(1).Text.Equals("add", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> ADD_VK[$REGULAR_ID]
    ;

modify_key
    :    {input.LT(1).Text.Equals("modify", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> MODIFY_VK[$REGULAR_ID]
    ;

including_key
    :    {input.LT(1).Text.Equals("including", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> INCLUDING_VK[$REGULAR_ID]
    ;

substitutable_key
    :    {input.LT(1).Text.Equals("substitutable")}?=> REGULAR_ID
    ;

attribute_key
    :    {input.LT(1).Text.Equals("attribute")}?=> REGULAR_ID
    ;

cascade_key
    :    {input.LT(1).Text.Equals("cascade", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CASCADE_VK[$REGULAR_ID] 
    ;

data_key
    :    {input.LT(1).Text.Equals("data")}?=> REGULAR_ID
    ;

invalidate_key
    :    {input.LT(1).Text.Equals("invalidate", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> INVALIDATE_VK[$REGULAR_ID]
    ;

element_key
    :    {input.LT(1).Text.Equals("element")}?=> REGULAR_ID
    ;

first_key
    :    {input.LT(1).Text.Equals("first", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> FIRST_VK[$REGULAR_ID]
    ;

check_key
    :    SQL92_RESERVED_CHECK
    ;

option_key
    :    SQL92_RESERVED_OPTION
    ;

nocycle_key
    :    {input.LT(1).Text.Equals("nocycle", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NOCYCLE_VK[$REGULAR_ID]
    ;

locked_key
    :    {input.LT(1).Text.Equals("locked")}?=> REGULAR_ID
    ;

block_key
    :    {input.LT(1).Text.Equals("block", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> BLOCK_VK[$REGULAR_ID]
    ;

xml_key
    :    {input.LT(1).Text.Equals("xml", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> XML_VK[$REGULAR_ID]
    ;

pivot_key
    :    {(input.LT(1).Text.Equals("pivot", System.StringComparison.InvariantCultureIgnoreCase))}?=> REGULAR_ID -> PIVOT_VK[$REGULAR_ID]
    ;

prior_key
    :    SQL92_RESERVED_PRIOR
    ;

sequential_key
    :    {input.LT(1).Text.Equals("sequential", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SEQUENTIAL_VK[$REGULAR_ID]
    ;

single_key
    :    {input.LT(1).Text.Equals("single", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SINGLE_VK[$REGULAR_ID]
    ;

skip_key
    :    {input.LT(1).Text.Equals("skip", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SKIP_VK[$REGULAR_ID]
    ;

model_key
    :    //{input.LT(1).Text.Equals("model")}?=> REGULAR_ID
        PLSQL_NON_RESERVED_MODEL
    ;

updated_key
    :    {input.LT(1).Text.Equals("updated", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> UPDATED_VK[$REGULAR_ID]
    ;

increment_key
    :    {input.LT(1).Text.Equals("increment", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> INCREMENT_VK[$REGULAR_ID]
    ;

exclude_key
    :    {input.LT(1).Text.Equals("exclude", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> EXCLUDE_VK[$REGULAR_ID]
    ;

reference_key
    :    {input.LT(1).Text.Equals("reference", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> REFERENCE_VK[$REGULAR_ID]
    ;

sets_key
    :    {input.LT(1).Text.Equals("sets")}?=> REGULAR_ID
    ;

until_key
    :    {input.LT(1).Text.Equals("until", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> UNTIL_VK[$REGULAR_ID]
    ;

seed_key
    :    {input.LT(1).Text.Equals("seed", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SEED_VK[$REGULAR_ID]
    ;

maxvalue_key
    :    {input.LT(1).Text.Equals("maxvalue", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> MAXVALUE_VK[$REGULAR_ID]
    ;

siblings_key
    :    {input.LT(1).Text.Equals("siblings", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SIBLINGS_VK[$REGULAR_ID]
    ;

cube_key
    :    {input.LT(1).Text.Equals("cube", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CUBE_VK[$REGULAR_ID]
    ;

nulls_key
    :    {input.LT(1).Text.Equals("nulls", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NULLS_VK[$REGULAR_ID]
    ;

dimension_key
    :    {input.LT(1).Text.Equals("dimension", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DIMENSION_VK[$REGULAR_ID]
    ;

scn_key
    :    {input.LT(1).Text.Equals("scn", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SCN_VK[$REGULAR_ID]
    ;

snapshot_key
    :    {input.LT(1).Text.Equals("snapshot")}?=> REGULAR_ID
    ;

decrement_key
    :    {input.LT(1).Text.Equals("decrement", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DECREMENT_VK[$REGULAR_ID]
    ;

unpivot_key
    :    {(input.LT(1).Text.Equals("unpivot", System.StringComparison.InvariantCultureIgnoreCase))}?=> REGULAR_ID -> UNPIVOT_VK[$REGULAR_ID]
    ;

keep_key
    :    {input.LT(1).Text.Equals("keep", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> KEEP_VK[$REGULAR_ID]
    ;

measures_key
    :    {input.LT(1).Text.Equals("measures", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> MEASURES_VK[$REGULAR_ID]
    ;

rows_key
    :    {input.LT(1).Text.Equals("rows", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> ROWS_VK[$REGULAR_ID]
    ;

sample_key
    :    {input.LT(1).Text.Equals("sample", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SAMPLE_VK[$REGULAR_ID]
    ;

upsert_key
    :    {input.LT(1).Text.Equals("upsert", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> UPSERT_VK[$REGULAR_ID]
    ;

versions_key
    :    {input.LT(1).Text.Equals("versions", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> VERSIONS_VK[$REGULAR_ID]
    ;

rules_key
    :    {input.LT(1).Text.Equals("rules", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> RULES_VK[$REGULAR_ID]
    ;

iterate_key
    :    {input.LT(1).Text.Equals("iterate", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> ITERATE_VK[$REGULAR_ID]
    ;

minvalue_key
    :    {input.LT(1).Text.Equals("minvalue", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> MINVALUE_VK[$REGULAR_ID]
    ;

rollup_key
    :    {input.LT(1).Text.Equals("rollup", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> ROLLUP_VK[$REGULAR_ID]
    ;

nav_key
    :    {input.LT(1).Text.Equals("nav", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NAV_VK[$REGULAR_ID]
    ;

automatic_key
    :    {input.LT(1).Text.Equals("automatic", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> AUTOMATIC_VK[$REGULAR_ID]
    ;

last_key
    :    {input.LT(1).Text.Equals("last", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> LAST_VK[$REGULAR_ID]
    ;

main_key
    :    {input.LT(1).Text.Equals("main")}?=> REGULAR_ID
    ;

grouping_key
    :    {input.LT(1).Text.Equals("grouping", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> GROUPING_VK[$REGULAR_ID]
    ;

include_key
    :    {input.LT(1).Text.Equals("include", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> INCLUDE_VK[$REGULAR_ID]
    ;

ignore_key
    :    {input.LT(1).Text.Equals("ignore", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> IGNORE_VK[$REGULAR_ID]
    ;

respect_key
    :    {input.LT(1).Text.Equals("respect")}?=> REGULAR_ID ->RESPECT_VK[$REGULAR_ID]
    ;

unique_key
    :    SQL92_RESERVED_UNIQUE
    ;

submultiset_key
    :    {input.LT(1).Text.Equals("submultiset", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SUBMULTISET_VK[$REGULAR_ID]
    ;

at_key
    :    {input.LT(1).Text.Equals("at", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> AT_VK[$REGULAR_ID]
    ;

a_key
    :    {input.LT(1).Text.Equals("a")}?=> REGULAR_ID
    ;

empty_key
    :    {input.LT(1).Text.Equals("empty")}?=> REGULAR_ID
    ;

likec_key
    :    {input.LT(1).Text.Equals("likec", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> LIKEC_VK[$REGULAR_ID]
    ;

nan_key
    :    {input.LT(1).Text.Equals("nan")}?=> REGULAR_ID
    ;

infinite_key
    :    {input.LT(1).Text.Equals("infinite")}?=> REGULAR_ID
    ;

like2_key
    :    {input.LT(1).Text.Equals("like2", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> LIKE2_VK[$REGULAR_ID]
    ;

like4_key
    :    {input.LT(1).Text.Equals("like4", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> LIKE4_VK[$REGULAR_ID]
    ;

present_key
    :    {input.LT(1).Text.Equals("present")}?=> REGULAR_ID
    ;

dbtimezone_key
    :    {input.LT(1).Text.Equals("dbtimezone", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DBTIMEZONE_VK[$REGULAR_ID]
    ;

sessiontimezone_key
    :    {input.LT(1).Text.Equals("sessiontimezone", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SESSIONTIMEZONE_VK[$REGULAR_ID]
    ;

nchar_cs_key
    :    {input.LT(1).Text.Equals("nchar_cs", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NCHAR_CS_VK[$REGULAR_ID]
    ;

decompose_key
    :    {input.LT(1).Text.Equals("decompose", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DECOMPOSE_VK[$REGULAR_ID]
    ;

following_key
    :    {input.LT(1).Text.Equals("following", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> FOLLOWING_VK[$REGULAR_ID]
    ;

first_value_key
    :    {input.LT(1).Text.Equals("first_value", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> FIRST_VALUE_VK[$REGULAR_ID]
    ;

preceding_key
    :    {input.LT(1).Text.Equals("preceding", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> PRECEDING_VK[$REGULAR_ID]
    ;

within_key
    :    {input.LT(1).Text.Equals("within", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> WITHIN_VK[$REGULAR_ID]
    ;

canonical_key
    :    {input.LT(1).Text.Equals("canonical", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CANONICAL_VK[$REGULAR_ID]
    ;

compatibility_key
    :    {input.LT(1).Text.Equals("compatibility", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> COMPATIBILITY_VK[$REGULAR_ID]
    ;

over_key
    :    {input.LT(1).Text.Equals("over", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> OVER_VK[$REGULAR_ID]
    ;

multiset_key
    :    {input.LT(1).Text.Equals("multiset")}?=> REGULAR_ID
    ;

connect_by_root_key
    :    PLSQL_NON_RESERVED_CONNECT_BY_ROOT
    ;

last_value_key
    :    {input.LT(1).Text.Equals("last_value", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> LAST_VALUE_VK[$REGULAR_ID]
    ;

current_key
    :    SQL92_RESERVED_CURRENT
    ;

unbounded_key
    :    {input.LT(1).Text.Equals("unbounded", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> UNBOUNDED_VK[$REGULAR_ID]
    ;

dense_rank_key
    :    {input.LT(1).Text.Equals("dense_rank", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DENSE_RANK_VK[$REGULAR_ID]
    ;

cost_key
    :    {input.LT(1).Text.Equals("cost", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> COST_VK[$REGULAR_ID]
    ;

char_cs_key
    :    {input.LT(1).Text.Equals("char_cs", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CHAR_CS_VK[$REGULAR_ID]
    ;

auto_key
    :    {input.LT(1).Text.Equals("auto", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> AUTO_VK[$REGULAR_ID]
    ;

treat_key
    :    {input.LT(1).Text.Equals("treat", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> TREAT_VK[$REGULAR_ID]
    ;

content_key
    :    {input.LT(1).Text.Equals("content", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CONTENT_VK[$REGULAR_ID]
    ;

xmlparse_key
    :    {input.LT(1).Text.Equals("xmlparse", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> XMLPARSE_VK[$REGULAR_ID]
    ;

xmlelement_key
    :    {input.LT(1).Text.Equals("xmlelement", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> XMLELEMENT_VK[$REGULAR_ID]
    ;

entityescaping_key
    :    {input.LT(1).Text.Equals("entityescaping", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> ENTITYESCAPING_VK[$REGULAR_ID]
    ;

standalone_key
    :    {input.LT(1).Text.Equals("standalone", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> STANDALONE_VK[$REGULAR_ID]
    ;

wellformed_key
    :    {input.LT(1).Text.Equals("wellformed", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> WELLFORMED_VK[$REGULAR_ID]
    ;

xmlexists_key
    :    {input.LT(1).Text.Equals("xmlexists", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> XMLEXISTS_VK[$REGULAR_ID]
    ;

version_key
    :    {input.LT(1).Text.Equals("version", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> VERSION_VK[$REGULAR_ID]
    ;

xmlcast_key
    :    {input.LT(1).Text.Equals("xmlcast", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> XMLCAST_VK[$REGULAR_ID]
    ;

yes_key
    :    {input.LT(1).Text.Equals("yes", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> YES_VK[$REGULAR_ID]
    ;

no_key
    :    {input.LT(1).Text.Equals("no", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NO_VK[$REGULAR_ID]
    ;

evalname_key
    :    {input.LT(1).Text.Equals("evalname", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> EVALNAME_VK[$REGULAR_ID]
    ;

xmlpi_key
    :    {input.LT(1).Text.Equals("xmlpi", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> XMLPI_VK[$REGULAR_ID]
    ;

xmlcolattval_key
    :    {input.LT(1).Text.Equals("xmlcolattval", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> XMLCOLATTVAL_VK[$REGULAR_ID]
    ;

document_key
    :    {input.LT(1).Text.Equals("document", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DOCUMENT_VK[$REGULAR_ID]
    ;

xmlforest_key
    :    {input.LT(1).Text.Equals("xmlforest", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> XMLFOREST_VK[$REGULAR_ID]
    ;

passing_key
    :    {input.LT(1).Text.Equals("passing", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> PASSING_VK[$REGULAR_ID]
    ;

columns_key//: PLSQL_RESERVED_COLUMNS
    :    {input.LT(1).Text.Equals("columns", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> PASSING_VK[$REGULAR_ID] 
    ;

indent_key
    :    {input.LT(1).Text.Equals("indent", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> INDENT_VK[$REGULAR_ID]
    ;

hide_key
    :    {input.LT(1).Text.Equals("hide", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> HIDE_VK[$REGULAR_ID]
    ;

xmlagg_key
    :    {input.LT(1).Text.Equals("xmlagg", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> XMLAGG_VK[$REGULAR_ID]
    ;

path_key
    :    {input.LT(1).Text.Equals("path")}?=> REGULAR_ID
    ;

xmlnamespaces_key
    :    {input.LT(1).Text.Equals("xmlnamespaces", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> XMLNAMESPACES_VK[$REGULAR_ID]
    ;

size_key
    :    SQL92_RESERVED_SIZE
    ;

noschemacheck_key
    :    {input.LT(1).Text.Equals("noschemacheck", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NOSCHEMACHECK_VK[$REGULAR_ID]
    ;

noentityescaping_key
    :    {input.LT(1).Text.Equals("noentityescaping", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NOENTITYESCAPING_VK[$REGULAR_ID]
    ;

xmlquery_key
    :    {input.LT(1).Text.Equals("xmlquery", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> XMLQUERY_VK[$REGULAR_ID]
    ;

xmltable_key
    :    {input.LT(1).Text.Equals("xmltable", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> XMLTABLE_VK[$REGULAR_ID]
    ;

xmlroot_key
    :    {input.LT(1).Text.Equals("xmlroot", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> XMLROOT_VK[$REGULAR_ID]
    ;

schemacheck_key
    :    {input.LT(1).Text.Equals("schemacheck", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SCHEMACHECK_VK[$REGULAR_ID]
    ;

xmlattributes_key
    :    {input.LT(1).Text.Equals("xmlattributes", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> XMLATTRIBUTES_VK[$REGULAR_ID]
    ;

encoding_key
    :    {input.LT(1).Text.Equals("encoding", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> ENCODING_VK[$REGULAR_ID]
    ;

show_key
    :    {input.LT(1).Text.Equals("show", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SHOW_VK[$REGULAR_ID]
    ;

xmlserialize_key
    :    {input.LT(1).Text.Equals("xmlserialize", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> XMLSERIALIZE_VK[$REGULAR_ID]
    ;

ordinality_key
    :    {input.LT(1).Text.Equals("ordinality", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> ORDINALITY_VK[$REGULAR_ID]
    ;

defaults_key
    :    {input.LT(1).Text.Equals("defaults", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DEFAULTS_VK[$REGULAR_ID]
    ;

sqlerror_key
    :    {input.LT(1).Text.Equals("sqlerror")}? REGULAR_ID 
    ;
	
oserror_key
    :    {input.LT(1).Text.Equals("oserror")}? REGULAR_ID 
    ;

success_key
    :    {input.LT(1).Text.Equals("success")}? REGULAR_ID 
    ;

warning_key
    :    {input.LT(1).Text.Equals("warning")}? REGULAR_ID 
    ;

failure_key
    :    {input.LT(1).Text.Equals("failure")}? REGULAR_ID 
    ;

insert_key
    :    SQL92_RESERVED_INSERT
    ;

order_key
    :    SQL92_RESERVED_ORDER
    ;

minus_key
    :    PLSQL_RESERVED_MINUS
    ;

row_key
    :    {input.LT(1).Text.Equals("row")}? REGULAR_ID -> ROW_VK[$REGULAR_ID]
    ;

mod_key
    :    {input.LT(1).Text.Equals("mod")}? REGULAR_ID
    ;

raw_key
    :    {input.LT(1).Text.Equals("raw", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> RAW_VK[$REGULAR_ID]
    ;

power_key
    :    {input.LT(1).Text.Equals("power")}? REGULAR_ID
    ;

lock_key
    :    PLSQL_RESERVED_LOCK
    ;

exists_key
    :    SQL92_RESERVED_EXISTS
    ;

having_key
    :    SQL92_RESERVED_HAVING
    ;

any_key
    :    SQL92_RESERVED_ANY
    ;

with_key
    :    SQL92_RESERVED_WITH
    ;

transaction_key
    :    {input.LT(1).Text.Equals("transaction")}?=> REGULAR_ID
    ;

rawtohex_key
    :    {input.LT(1).Text.Equals("rawtohex")}? REGULAR_ID
    ;

number_key
    :    {input.LT(1).Text.Equals("number", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NUMBER_VK[$REGULAR_ID]
    ;

nocopy_key
    :    {input.LT(1).Text.Equals("nocopy", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NOCOPY_VK[$REGULAR_ID]
    ;

to_key
    :    SQL92_RESERVED_TO
    ;

abs_key
    :    {input.LT(1).Text.Equals("abs")}? REGULAR_ID
    ;

rollback_key
    :    {input.LT(1).Text.Equals("rollback", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> ROLLBACK_VK[$REGULAR_ID]
    ;

share_key
    :    PLSQL_RESERVED_SHARE
    ;

greatest_key
    :    {input.LT(1).Text.Equals("greatest")}? REGULAR_ID
    ;

vsize_key
    :    {input.LT(1).Text.Equals("vsize")}? REGULAR_ID
    ;

exclusive_key
    :    PLSQL_RESERVED_EXCLUSIVE
    ;

varchar2_key
    :    {input.LT(1).Text.Equals("varchar2", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> VARCHAR2_VK[$REGULAR_ID]
    ;

rowidtochar_key
    :    {input.LT(1).Text.Equals("rowidtochar")}? REGULAR_ID
    ;

open_key
    :    {input.LT(1).Text.Equals("open", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> OPEN_VK[$REGULAR_ID]
    ;

comment_key
    :    {input.LT(1).Text.Equals("comment", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> COMMENT_VK[$REGULAR_ID]
    ;

sqrt_key
    :    {input.LT(1).Text.Equals("sqrt")}? REGULAR_ID
    ;

instr_key
    :    {input.LT(1).Text.Equals("instr")}? REGULAR_ID
    ;

nowait_key
    :    PLSQL_RESERVED_NOWAIT
    ;

lpad_key
    :    {input.LT(1).Text.Equals("lpad")}? REGULAR_ID
    ;

boolean_key
    :    {input.LT(1).Text.Equals("boolean", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> BOOLEAN_VK[$REGULAR_ID]
    ;

rpad_key
    :    {input.LT(1).Text.Equals("rpad")}? REGULAR_ID
    ;

savepoint_key
    :    {input.LT(1).Text.Equals("savepoint", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SAVEPOINT_VK[$REGULAR_ID]
    ;

decode_key
    :    {input.LT(1).Text.Equals("decode")}? REGULAR_ID
    ;

reverse_key
    :    {input.LT(1).Text.Equals("reverse")}? REGULAR_ID -> REVERSE_VK[$REGULAR_ID]
    ;

least_key
    :    {input.LT(1).Text.Equals("least")}? REGULAR_ID
    ;

nvl_key
    :    {input.LT(1).Text.Equals("nvl")}? REGULAR_ID
    ;

variance_key
    :    {input.LT(1).Text.Equals("variance")}? REGULAR_ID
    ;

start_key
    :    PLSQL_RESERVED_START
    ;

desc_key
    :    SQL92_RESERVED_DESC
    ;

concat_key
    :    {input.LT(1).Text.Equals("concat")}? REGULAR_ID
    ;

dump_key
    :    {input.LT(1).Text.Equals("dump")}? REGULAR_ID
    ;

soundex_key
    :    {input.LT(1).Text.Equals("soundex")}? REGULAR_ID
    ;

positive_key
    :    {input.LT(1).Text.Equals("positive", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> POSITIVE_VK[$REGULAR_ID]
    ;

union_key
    :    SQL92_RESERVED_UNION
    ;

ascii_key
    :    {input.LT(1).Text.Equals("ascii")}? REGULAR_ID
    ;

connect_key
    :    SQL92_RESERVED_CONNECT
    ;

asc_key
    :    SQL92_RESERVED_ASC
    ;

hextoraw_key
    :    {input.LT(1).Text.Equals("hextoraw")}? REGULAR_ID
    ;

to_date_key
    :    {input.LT(1).Text.Equals("to_date")}? REGULAR_ID
    ;

floor_key
    :    {input.LT(1).Text.Equals("floor")}? REGULAR_ID
    ;

sign_key
    :    {input.LT(1).Text.Equals("sign")}? REGULAR_ID
    ;

update_key
    :    SQL92_RESERVED_UPDATE
    ;

trunc_key
    :    {input.LT(1).Text.Equals("trunc")}? REGULAR_ID
    ;

rtrim_key
    :    {input.LT(1).Text.Equals("rtrim")}? REGULAR_ID
    ;

close_key
    :    {input.LT(1).Text.Equals("close", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CLOSE_VK[$REGULAR_ID]
    ;

to_char_key
    :    {input.LT(1).Text.Equals("to_char")}? REGULAR_ID
    ;

ltrim_key
    :    {input.LT(1).Text.Equals("ltrim")}? REGULAR_ID
    ;

mode_key
    :    PLSQL_RESERVED_MODE
    ;

uid_key
    :    {input.LT(1).Text.Equals("uid")}? REGULAR_ID
    ;

chr_key
    :    {input.LT(1).Text.Equals("chr")}? REGULAR_ID -> CHR_VK[$REGULAR_ID]
    ;

intersect_key
    :    SQL92_RESERVED_INTERSECT
    ;

chartorowid_key
    :    {input.LT(1).Text.Equals("chartorowid")}? REGULAR_ID
    ;

mlslabel_key
    :    {input.LT(1).Text.Equals("mlslabel", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> MLSLABEL_VK[$REGULAR_ID]
    ;

userenv_key
    :    {input.LT(1).Text.Equals("userenv")}? REGULAR_ID
    ;

stddev_key
    :    {input.LT(1).Text.Equals("stddev")}? REGULAR_ID
    ;

length_key
    :    {input.LT(1).Text.Equals("length")}? REGULAR_ID
    ;

fetch_key
    :    SQL92_RESERVED_FETCH
    ;

group_key
    :    SQL92_RESERVED_GROUP
    ;

sysdate_key
    :    {input.LT(1).Text.Equals("sysdate")}? REGULAR_ID
    ;

binary_integer_key
    :    {input.LT(1).Text.Equals("binary_integer", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> BINARY_INTEGER_VK[$REGULAR_ID]
    ;

to_number_key
    :    {input.LT(1).Text.Equals("to_number")}? REGULAR_ID
    ;

substr_key
    :    {input.LT(1).Text.Equals("substr")}? REGULAR_ID
    ;

ceil_key
    :    {input.LT(1).Text.Equals("ceil")}? REGULAR_ID
    ;

initcap_key
    :    {input.LT(1).Text.Equals("initcap")}? REGULAR_ID
    ;

round_key
    :    {input.LT(1).Text.Equals("round")}? REGULAR_ID
    ;

long_key
    :    {input.LT(1).Text.Equals("long", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> LONG_VK[$REGULAR_ID]
    ;

read_key
    :    {input.LT(1).Text.Equals("read", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> READ_VK[$REGULAR_ID]
    ;

only_key
    :    {input.LT(1).Text.Equals("only")}? REGULAR_ID -> ONLY_VK[$REGULAR_ID]
    ;

set_key
    :    {input.LT(1).Text.Equals("set", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SET_VK[$REGULAR_ID]
    ;

nullif_key
    :    {input.LT(1).Text.Equals("nullif")}? REGULAR_ID
    ;

coalesce_key
    :    {input.LT(1).Text.Equals("coalesce")}? REGULAR_ID
    ;

count_key
    :    {input.LT(1).Text.Equals("count")}? REGULAR_ID -> COUNT_VK[$REGULAR_ID]
    ;

avg_key    :    {input.LT(1).Text.Equals("avg")}? REGULAR_ID
    ;

max_key    :    {input.LT(1).Text.Equals("max")}? REGULAR_ID
    ;

min_key    :    {input.LT(1).Text.Equals("min")}? REGULAR_ID
    ;

sum_key    :    {input.LT(1).Text.Equals("sum")}? REGULAR_ID
    ;

unknown_key
    :    {input.LT(1).Text.Equals("unknown")}? REGULAR_ID
    ;

escape_key
    :    {input.LT(1).Text.Equals("escape")}? REGULAR_ID
    ;

some_key
    :    {input.LT(1).Text.Equals("some")}? REGULAR_ID -> SOME_VK[$REGULAR_ID]
    ;

match_key
    :    {input.LT(1).Text.Equals("match")}? REGULAR_ID
    ;

cast_key
    :    {input.LT(1).Text.Equals("cast")}? REGULAR_ID -> CAST_VK[$REGULAR_ID]
    ;

full_key:    {input.LT(1).Text.Equals("full", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> FULL_VK[$REGULAR_ID]
    ;

partial_key
    :    {input.LT(1).Text.Equals("partial")}? REGULAR_ID
    ;

character_key
    :    {input.LT(1).Text.Equals("character", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CHARACTER_VK[$REGULAR_ID]
    ;

except_key
    :    {input.LT(1).Text.Equals("except")}? REGULAR_ID
    ;

char_key:    {input.LT(1).Text.Equals("char", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CHAR_VK[$REGULAR_ID]
    ;

varying_key
    :    {input.LT(1).Text.Equals("varying")}?=> REGULAR_ID
    ;

varchar_key
    :    {input.LT(1).Text.Equals("varchar", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> VARCHAR_VK[$REGULAR_ID]
    ;

national_key
    :    {input.LT(1).Text.Equals("national")}? REGULAR_ID
    ;

nchar_key
    :    {input.LT(1).Text.Equals("nchar")}? REGULAR_ID -> NCHAR_VK[$REGULAR_ID]
    ;

bit_key    :    {input.LT(1).Text.Equals("bit")}? REGULAR_ID -> BIT_VK[$REGULAR_ID]
    ;

float_key
    :    {input.LT(1).Text.Equals("float")}? REGULAR_ID -> FLOAT_VK[$REGULAR_ID]
    ;
    
real_key:    {input.LT(1).Text.Equals("real", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> REAL_VK[$REGULAR_ID]
    ;

double_key
    :    {input.LT(1).Text.Equals("double", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DOUBLE_VK[$REGULAR_ID]
    ;

precision_key
    :    {input.LT(1).Text.Equals("precision")}? REGULAR_ID -> PRECISION_VK[$REGULAR_ID]
    ;

interval_key
    :    {input.LT(1).Text.Equals("interval")}?=> REGULAR_ID
    ;

time_key
    :    {input.LT(1).Text.Equals("time")}? REGULAR_ID -> TIME_VK[$REGULAR_ID]
    ;
 
zone_key:    {input.LT(1).Text.Equals("zone")}? REGULAR_ID
    ;

timestamp_key
    :    {input.LT(1).Text.Equals("timestamp")}? REGULAR_ID -> TIMESTAMP_VK[$REGULAR_ID]
    ;

date_key//:    {input.LT(1).Text.Equals("date", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DATE_VK[$REGULAR_ID]
    :    SQL92_RESERVED_DATE
    ;

numeric_key
    :    {input.LT(1).Text.Equals("numeric", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NUMERIC_VK[$REGULAR_ID]
    ;

decimal_key
    :    {input.LT(1).Text.Equals("decimal", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DECIMAL_VK[$REGULAR_ID]
    ;

dec_key    :    {input.LT(1).Text.Equals("dec", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DEC_VK[$REGULAR_ID]
    ;

integer_key
    :    {input.LT(1).Text.Equals("integer", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> INTEGER_VK[$REGULAR_ID]
    ;

int_key    :    {input.LT(1).Text.Equals("int", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> INT_VK[$REGULAR_ID]
    ;

smallint_key
    :    {input.LT(1).Text.Equals("smallint", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> SMALLINT_VK[$REGULAR_ID]
    ;

corresponding_key
    :    {input.LT(1).Text.Equals("corresponding")}? REGULAR_ID
    ;

cross_key
    :    {input.LT(1).Text.Equals("cross", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> CROSS_VK[$REGULAR_ID]
    ;

join_key
    :    {input.LT(1).Text.Equals("join")}?=> REGULAR_ID
    ;

left_key
    :    {input.LT(1).Text.Equals("left", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> LEFT_VK[$REGULAR_ID]
    ;

right_key
    :    {input.LT(1).Text.Equals("right", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> RIGHT_VK[$REGULAR_ID]
    ;

inner_key
    :    {input.LT(1).Text.Equals("inner", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> INNER_VK[$REGULAR_ID]
    ;

natural_key
    :    {input.LT(1).Text.Equals("natural", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> NATURAL_VK[$REGULAR_ID]
    ;

outer_key
    :    {input.LT(1).Text.Equals("outer")}?=> REGULAR_ID
    ;

using_key
    :    PLSQL_NON_RESERVED_USING
    ;

indicator_key
    :    {input.LT(1).Text.Equals("indicator")}? REGULAR_ID
    ;

user_key
    :    {input.LT(1).Text.Equals("user")}? REGULAR_ID
    ;

current_user_key
    :    {input.LT(1).Text.Equals("current_user")}? REGULAR_ID -> CURRENT_USER_VK[$REGULAR_ID]
    ;

session_user_key
    :    {input.LT(1).Text.Equals("session_user")}? REGULAR_ID
    ;

system_user_key
    :    {input.LT(1).Text.Equals("system_user")}? REGULAR_ID
    ;

value_key
    :    {input.LT(1).Text.Equals("value")}? REGULAR_ID -> VALUE_VK[$REGULAR_ID]
    ;

substring_key
    :    {input.LT(1).Text.Equals("substring")}?=> REGULAR_ID
    ;

upper_key
    :    {input.LT(1).Text.Equals("upper")}? REGULAR_ID
    ;

lower_key
    :    {input.LT(1).Text.Equals("lower")}? REGULAR_ID
    ;

convert_key
    :    {input.LT(1).Text.Equals("convert")}? REGULAR_ID -> CONVERT_VK[$REGULAR_ID]
    ;

translate_key
    :    {input.LT(1).Text.Equals("translate")}? REGULAR_ID -> TRANSLATE_VK[$REGULAR_ID]
    ;

trim_key
    :    {input.LT(1).Text.Equals("trim")}? REGULAR_ID -> TRIM_VK[$REGULAR_ID]
    ;

leading_key
    :    {input.LT(1).Text.Equals("leading")}? REGULAR_ID -> LEADING_VK[$REGULAR_ID]
    ;

trailing_key
    :    {input.LT(1).Text.Equals("trailing")}? REGULAR_ID -> TRAILING_VK[$REGULAR_ID]
    ;

both_key
    :    {input.LT(1).Text.Equals("both")}? REGULAR_ID -> BOTH_VK[$REGULAR_ID]
    ;

collate_key
    :    {input.LT(1).Text.Equals("collate")}? REGULAR_ID
    ;

position_key
    :    {input.LT(1).Text.Equals("position")}? REGULAR_ID
    ;

extract_key
    :    {input.LT(1).Text.Equals("extract")}? REGULAR_ID -> EXTRACT_VK[$REGULAR_ID]
    ;

second_key
    :    {input.LT(1).Text.Equals("second")}? REGULAR_ID -> SECOND_VK[$REGULAR_ID]
    ;

timezone_hour_key
    :    {input.LT(1).Text.Equals("timezone_hour")}? REGULAR_ID -> TIMEZONE_HOUR_VK[$REGULAR_ID]
    ;

timezone_minute_key
    :    {input.LT(1).Text.Equals("timezone_minute")}? REGULAR_ID -> TIMEZONE_MINUTE_VK[$REGULAR_ID]
    ;

char_length_key
    :    {input.LT(1).Text.Equals("char_length")}? REGULAR_ID
    ;

octet_length_key
    :    {input.LT(1).Text.Equals("octet_length")}? REGULAR_ID
    ;

character_length_key
    :    {input.LT(1).Text.Equals("character_length")}? REGULAR_ID
    ;

bit_length_key
    :    {input.LT(1).Text.Equals("bit_length")}? REGULAR_ID
    ;

local_key
    :    {input.LT(1).Text.Equals("local")}? REGULAR_ID -> LOCAL_VK[$REGULAR_ID]
    ;

current_timestamp_key
    :    {input.LT(1).Text.Equals("current_timestamp")}? REGULAR_ID
    ;

current_date_key
    :    {input.LT(1).Text.Equals("current_date")}? REGULAR_ID
    ;

current_time_key
    :    {input.LT(1).Text.Equals("current_time")}? REGULAR_ID
    ;

module_key
    :    {input.LT(1).Text.Equals("module")}? REGULAR_ID
    ;

global_key
    :    {input.LT(1).Text.Equals("global")}? REGULAR_ID
    ;

year_key
    :    {input.LT(1).Text.Equals("year", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> YEAR_VK[$REGULAR_ID]
    ;

month_key
    :    {input.LT(1).Text.Equals("month")}? REGULAR_ID -> MONTH_VK[$REGULAR_ID]
    ;

day_key
    :    {input.LT(1).Text.Equals("day", System.StringComparison.InvariantCultureIgnoreCase)}?=> REGULAR_ID -> DAY_VK[$REGULAR_ID]
    ;

hour_key:    {input.LT(1).Text.Equals("hour")}? REGULAR_ID -> HOUR_VK[$REGULAR_ID]
    ;

minute_key
    :    {input.LT(1).Text.Equals("minute")}? REGULAR_ID -> MINUTE_VK[$REGULAR_ID]
    ;

whenever_key
    :    {input.LT(1).Text.Equals("whenever")}? REGULAR_ID
    ;

is_key
    :    SQL92_RESERVED_IS
    ;

else_key
    :    SQL92_RESERVED_ELSE
    ;

table_key
    :    SQL92_RESERVED_TABLE
    ;

the_key
    :    SQL92_RESERVED_THE
    ;

then_key
    :    SQL92_RESERVED_THEN
    ;

end_key
    :    SQL92_RESERVED_END
    ;

all_key
    :    SQL92_RESERVED_ALL
    ;

on_key
    :    SQL92_RESERVED_ON
    ;

or_key
    :    SQL92_RESERVED_OR
    ;

and_key
    :    SQL92_RESERVED_AND
    ;

not_key
    :    SQL92_RESERVED_NOT
    ;

true_key
    :    SQL92_RESERVED_TRUE
    ;

false_key
    :    SQL92_RESERVED_FALSE
    ;

default_key
    :    SQL92_RESERVED_DEFAULT
    ;

distinct_key
    :    SQL92_RESERVED_DISTINCT
    ;

into_key
    :    SQL92_RESERVED_INTO
    ;

by_key
    :    SQL92_RESERVED_BY
    ;

as_key
    :    SQL92_RESERVED_AS
    ;

in_key
    :    SQL92_RESERVED_IN
    ;

of_key
    :    SQL92_RESERVED_OF
    ;

null_key
    :    SQL92_RESERVED_NULL
    ;

for_key
    :    SQL92_RESERVED_FOR
    ;

select_key
    :    SQL92_RESERVED_SELECT
    ;

when_key
    :    SQL92_RESERVED_WHEN
    ;

delete_key
    :    SQL92_RESERVED_DELETE
    ;

between_key
    :    SQL92_RESERVED_BETWEEN
    ;

like_key
    :    SQL92_RESERVED_LIKE
    ;

from_key
    :    SQL92_RESERVED_FROM
    ;

where_key
    :    SQL92_RESERVED_WHERE
    ;

sequence_key
    :   {input.LT(1).Text.Equals("sequence")}? REGULAR_ID -> SEQUENCE_VK[$REGULAR_ID]
    ;

noorder_key
    :   {input.LT(1).Text.Equals("noorder")}? REGULAR_ID -> NOORDER_VK[$REGULAR_ID]
    ;

cycle_key
    :   {input.LT(1).Text.Equals("cycle")}? REGULAR_ID -> CYCLE_VK[$REGULAR_ID]
    ;

cache_key
    :   {input.LT(1).Text.Equals("cache")}? REGULAR_ID -> CACHE_VK[$REGULAR_ID]
    ;

nocache_key
    :   {input.LT(1).Text.Equals("nocache")}? REGULAR_ID -> NOCACHE_VK[$REGULAR_ID]
    ;

nomaxvalue_key
    :   {input.LT(1).Text.Equals("nomaxvalue")}? REGULAR_ID -> NOMAXVALUE_VK[$REGULAR_ID]
    ;

nominvalue_key
    :   {input.LT(1).Text.Equals("nominvalue")}? REGULAR_ID -> NOMINVALUE_VK[$REGULAR_ID]
    ;

search_key
    :   {input.LT(1).Text.Equals("search")}? REGULAR_ID -> SEARCH_VK[$REGULAR_ID]
    ;

depth_key
    :   {input.LT(1).Text.Equals("depth")}? REGULAR_ID -> DEPTH_VK[$REGULAR_ID]
    ;

breadth_key
    :   {input.LT(1).Text.Equals("breadth")}? REGULAR_ID -> BREADTH_VK[$REGULAR_ID]
    ;
