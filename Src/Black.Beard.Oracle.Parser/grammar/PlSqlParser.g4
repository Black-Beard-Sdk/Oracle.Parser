/**
 * Oracle(c) PL/SQL 11g Parser
 *
 * Copyright (c) 2009-2011 Alexandre Porcelli <alexandre.porcelli@gmail.com>
 * Copyright (c) 2015-2017 Ivan Kochurkin (KvanTTT, kvanttt@gmail.com, Positive Technologies).
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

parser grammar PlSqlParser;

options { 
    // memoize=True;
    tokenVocab=PlSqlLexer; 
    }

sql_script :
	((unit_statement | sql_plus_command) SEMICOLON?)* EOF
    ;

unit_statement :
	transaction_control_statements 
    | alter_function
    | alter_package
    | alter_procedure
    | alter_sequence
    | alter_trigger
    | alter_type
    | alter_table
    | alter_index
    | alter_user

    | create_function_body
    | create_procedure_body
    | create_package
    | create_package_body

    | create_index
    | create_table
    | create_tablespace
    // | create_view //TODO
//  | create_directory //TODO
//  | create_materialized_view //TODO
    | create_user

    | create_sequence
    | create_trigger
    | create_type
    | create_synonym

    | drop_function
    | drop_package
    | drop_procedure
    | drop_sequence
    | drop_trigger
    | drop_type
    | data_manipulation_language_statements
    | drop_table
    | drop_index

    | comment_on_column
    | comment_on_table

    | anonymous_block

    | grant_statement
	| revoke_statment
    ;

// DDL -> SQL Statements for Stored PL/SQL Units

// Function DDLs

drop_function :
	DROP FUNCTION function_name ';'
    ;

alter_function :
	ALTER FUNCTION function_name COMPILE DEBUG? compiler_parameters_clause* (REUSE SETTINGS)? ';'
    ;

create_function_body :
	CREATE (OR REPLACE)? FUNCTION function_name ( LEFT_PAREN (COMMA? parameter)+ RIGHT_PAREN )?
      RETURN type_spec (invoker_rights_clause | parallel_enable_clause | result_cache_clause | DETERMINISTIC)*
      ((PIPELINED? (IS | AS) (DECLARE? seq_of_declare_specs? body | call_spec)) | (PIPELINED | AGGREGATE) USING implementation_type_name) ';'
    ;

// Creation Function - Specific Clauses

parallel_enable_clause :
	PARALLEL_ENABLE partition_by_clause?
    ;

partition_by_clause :
	 LEFT_PAREN PARTITION expression BY (ANY | (HASH | RANGE | LIST) paren_column_list) streaming_clause? RIGHT_PAREN 
    ;

result_cache_clause :
	RESULT_CACHE relies_on_part?
    ;

relies_on_part :
	RELIES_ON LEFT_PAREN tableview_name (COMMA tableview_name)* RIGHT_PAREN 
    ;

streaming_clause :
	(ORDER | CLUSTER) expression BY paren_column_list
    ;

// Package DDLs

drop_package :
	DROP PACKAGE BODY? (schema_object_name '.')? package_name ';'
    ;

alter_package :
	ALTER PACKAGE package_name COMPILE DEBUG? (PACKAGE | BODY | SPECIFICATION)? compiler_parameters_clause* (REUSE SETTINGS)? ';'
    ;

create_package :
	CREATE (OR REPLACE)? PACKAGE (schema_object_name '.')? package_name invoker_rights_clause? (IS | AS) package_obj_spec* END package_name? ';'
    ;

create_package_body :
	CREATE (OR REPLACE)? PACKAGE BODY (schema_object_name '.')? package_name (IS | AS) package_obj_body* (BEGIN seq_of_statements)? END package_name? ';'
    ;

// Create Package Specific Clauses

package_obj_spec :
	variable_declaration
    | subtype_declaration
    | cursor_declaration
    | exception_declaration
    | pragma_declaration
    | type_declaration
    | procedure_spec
    | function_spec
    ;

procedure_spec :
	PROCEDURE identifier ( LEFT_PAREN parameter ( COMMA parameter )* RIGHT_PAREN )? ';'
    ;

function_spec :
	FUNCTION identifier ( LEFT_PAREN parameter ( COMMA parameter)* RIGHT_PAREN )?
      RETURN type_spec (DETERMINISTIC)? (RESULT_CACHE)? ';'
    ;

package_obj_body :
	variable_declaration
    | subtype_declaration
    | cursor_declaration
    | exception_declaration
    | type_declaration
    | procedure_body
    | function_body
    | procedure_spec
    | function_spec
    ;

// Procedure DDLs

drop_procedure :
	DROP PROCEDURE procedure_name ';'
    ;

alter_procedure :
	ALTER PROCEDURE procedure_name COMPILE DEBUG? compiler_parameters_clause* (REUSE SETTINGS)? ';'
    ;

function_body :
	FUNCTION function_name ( LEFT_PAREN parameter (COMMA parameter)* RIGHT_PAREN )?
      RETURN type_spec (invoker_rights_clause | parallel_enable_clause | result_cache_clause | DETERMINISTIC)*
      ((PIPELINED? (IS | AS) (DECLARE? seq_of_declare_specs? body | call_spec)) | (PIPELINED | AGGREGATE) USING implementation_type_name) ';'
    ;

procedure_body :
	PROCEDURE procedure_name ( LEFT_PAREN parameter (COMMA parameter)* RIGHT_PAREN )? (IS | AS)
      (DECLARE? seq_of_declare_specs? body | call_spec | EXTERNAL) ';'
    ;

create_procedure_body :
	CREATE (OR REPLACE)? PROCEDURE procedure_name ( LEFT_PAREN parameter (COMMA parameter)* RIGHT_PAREN )? 
      invoker_rights_clause? (IS | AS)
      (DECLARE? seq_of_declare_specs? body | call_spec | EXTERNAL) ';'
    ;

// Trigger DDLs

drop_trigger :
	DROP TRIGGER trigger_name ';'
    ;

alter_trigger :
	ALTER TRIGGER alter_trigger_name=trigger_name
      ((ENABLE | DISABLE) | RENAME TO rename_trigger_name=trigger_name | COMPILE DEBUG? compiler_parameters_clause* (REUSE SETTINGS)?) ';'
    ;

create_trigger :
	CREATE ( OR REPLACE )? TRIGGER trigger_name
      (simple_dml_trigger | compound_dml_trigger | non_dml_trigger)
      trigger_follows_clause? (ENABLE | DISABLE)? trigger_when_clause? trigger_body ';'
    ;

trigger_follows_clause :
	FOLLOWS trigger_name (COMMA trigger_name)*
    ;

trigger_when_clause :
	WHEN LEFT_PAREN condition RIGHT_PAREN 
    ;

// Create Trigger Specific Clauses

simple_dml_trigger :
	(BEFORE | AFTER | INSTEAD OF) dml_event_clause referencing_clause? for_each_row?
    ;

for_each_row :
	FOR EACH ROW
    ;

compound_dml_trigger :
	FOR dml_event_clause referencing_clause?
    ;

non_dml_trigger :
	(BEFORE | AFTER) non_dml_event (OR non_dml_event)* ON (DATABASE | (schema_name '.')? SCHEMA)
    ;

trigger_body :
	COMPOUND TRIGGER
    | CALL identifier
    | trigger_block
    ;

routine_clause :
	function_arguments? keep_clause?
    ;

compound_trigger_block :
	COMPOUND TRIGGER seq_of_declare_specs? timing_point_section+ END trigger_name
    ;

timing_point_section :
	bk=BEFORE STATEMENT IS trigger_block BEFORE STATEMENT ';'
    | bk=BEFORE EACH ROW IS trigger_block BEFORE EACH ROW ';'
    | ak=AFTER STATEMENT IS trigger_block AFTER STATEMENT ';'
    | ak=AFTER EACH ROW IS trigger_block AFTER EACH ROW ';'
    ;

non_dml_event :
	ALTER
    | ANALYZE
    | ASSOCIATE STATISTICS
    | AUDIT
    | COMMENT
    | CREATE
    | DISASSOCIATE STATISTICS
    | DROP
    | GRANT
    | NOAUDIT
    | RENAME
    | REVOKE
    | TRUNCATE
    | DDL
    | STARTUP
    | SHUTDOWN
    | DB_ROLE_CHANGE
    | LOGON
    | LOGOFF
    | SERVERERROR
    | SUSPEND
    | DATABASE
    | SCHEMA
    | FOLLOWS
    ;

dml_event_clause :
	dml_event_element (OR dml_event_element)* ON dml_event_nested_clause? tableview_name
    ;

dml_event_element :
	(DELETE | INSERT | UPDATE) (OF column_list)?
    ;

dml_event_nested_clause :
	NESTED TABLE tableview_name OF
    ;

referencing_clause :
	REFERENCING referencing_element+
    ;

referencing_element :
	(NEW | OLD | PARENT) column_alias
    ;

// DDLs

drop_type :
	DROP TYPE BODY? type_name (FORCE | VALIDATE)? ';'
    ;

alter_type :
	ALTER TYPE type_name
    (compile_type_clause
    | replace_type_clause
    //TODO | {input.LT(2).getText().equalsIgnoreCase("attribute")}? alter_attribute_definition
    | alter_method_spec
    | alter_collection_clauses
    | modifier_clause
    ) dependent_handling_clause? ';'
    ;

// Alter Type Specific Clauses

compile_type_clause :
	COMPILE DEBUG? (SPECIFICATION | BODY)? compiler_parameters_clause* (REUSE SETTINGS)?
    ;

replace_type_clause :
	REPLACE invoker_rights_clause? AS OBJECT LEFT_PAREN object_member_spec (COMMA object_member_spec)* RIGHT_PAREN 
    ;

alter_method_spec :
	alter_method_element (COMMA alter_method_element)*
    ;

alter_method_element :
	(ADD | DROP) (map_order_function_spec | subprogram_spec)
    ;

alter_attribute_definition :
	(ADD | MODIFY | DROP) ATTRIBUTE (attribute_definition | LEFT_PAREN attribute_definition (COMMA attribute_definition)* RIGHT_PAREN )
    ;

attribute_definition :
	attribute_name type_spec?
    ;

alter_collection_clauses :
	MODIFY (LIMIT expression | ELEMENT TYPE type_spec)
    ;

dependent_handling_clause :
	INVALIDATE
    | CASCADE (CONVERT TO SUBSTITUTABLE | NOT? INCLUDING TABLE DATA)? dependent_exceptions_part?
    ;

dependent_exceptions_part :
	FORCE? EXCEPTIONS INTO tableview_name
    ;

create_type :
	CREATE (OR REPLACE)? TYPE (type_definition | type_body) ';'
    ;

// Create Type Specific Clauses

type_definition :
	type_name (OID CHAR_STRING)? object_type_def?
    ;

object_type_def :
	invoker_rights_clause? (object_as_part | object_under_part) sqlj_object_type?
      ( LEFT_PAREN object_member_spec (COMMA object_member_spec)* RIGHT_PAREN )? modifier_clause*
    ;

object_as_part :
	(IS | AS) (OBJECT | varray_type_def | nested_table_type_def)
    ;

object_under_part :
	UNDER type_spec
    ;

nested_table_type_def :
	TABLE OF type_spec (NOT NULL)?
    ;

sqlj_object_type :
	EXTERNAL NAME expression LANGUAGE JAVA USING (SQLDATA | CUSTOMDATUM | ORADATA)
    ;

type_body :
	BODY type_name (IS | AS) (type_body_elements)+ END
    ;

type_body_elements :
	map_order_func_declaration
    | subprog_decl_in_type
    ;

map_order_func_declaration :
	(MAP | ORDER) MEMBER func_decl_in_type
    ;

subprog_decl_in_type :
	(MEMBER | STATIC) (proc_decl_in_type | func_decl_in_type | constructor_declaration)
    ;

proc_decl_in_type :
	PROCEDURE procedure_name LEFT_PAREN type_elements_parameter (COMMA type_elements_parameter)* RIGHT_PAREN 
      (IS | AS) (call_spec | DECLARE? seq_of_declare_specs? body ';')
    ;

func_decl_in_type :
	FUNCTION function_name ( LEFT_PAREN type_elements_parameter (COMMA type_elements_parameter)* RIGHT_PAREN )?
      RETURN type_spec (IS | AS) (call_spec | DECLARE? seq_of_declare_specs? body ';')
    ;

constructor_declaration :
	FINAL? INSTANTIABLE? CONSTRUCTOR FUNCTION type_spec
      ( LEFT_PAREN (SELF IN OUT type_spec COMMA) type_elements_parameter (COMMA type_elements_parameter)*RIGHT_PAREN )?
      RETURN SELF AS RESULT (IS | AS) (call_spec | DECLARE? seq_of_declare_specs? body ';')
    ;

// Common Type Clauses

modifier_clause :
	NOT? (INSTANTIABLE | FINAL | OVERRIDING)
    ;

object_member_spec :
	identifier type_spec sqlj_object_type_attr?
    | element_spec
    ;

sqlj_object_type_attr :
	EXTERNAL NAME expression
    ;

element_spec :
	modifier_clause? element_spec_options+ (COMMA pragma_clause)?
    ;

element_spec_options :
	subprogram_spec
    | constructor_spec
    | map_order_function_spec
    ;

subprogram_spec :
	(MEMBER | STATIC) (type_procedure_spec | type_function_spec)
    ;

type_procedure_spec :
	PROCEDURE procedure_name LEFT_PAREN type_elements_parameter (COMMA type_elements_parameter)* RIGHT_PAREN ((IS | AS) call_spec)?
    ;

type_function_spec :
	FUNCTION function_name ( LEFT_PAREN type_elements_parameter (COMMA type_elements_parameter)* RIGHT_PAREN )?
      RETURN (type_spec | SELF AS RESULT) ((IS | AS) call_spec | EXTERNAL VARIABLE? NAME expression)?
    ;

constructor_spec :
	FINAL? INSTANTIABLE? CONSTRUCTOR FUNCTION
      type_spec ( LEFT_PAREN (SELF IN OUT type_spec COMMA) type_elements_parameter (COMMA type_elements_parameter)*RIGHT_PAREN )?
      RETURN SELF AS RESULT ((IS | AS) call_spec)?
    ;

map_order_function_spec :
	(MAP | ORDER) MEMBER type_function_spec
    ;

pragma_clause :
	PRAGMA RESTRICT_REFERENCES LEFT_PAREN pragma_elements (COMMA pragma_elements)* RIGHT_PAREN 
    ;

pragma_elements :
	identifier
    | DEFAULT
    ;

type_elements_parameter :
	parameter_name type_spec
    ;

// Sequence DDLs

drop_sequence :
	DROP SEQUENCE sequence_name ';'
    ;

alter_sequence :
	ALTER SEQUENCE sequence_name sequence_spec+ ';'
    ;

create_sequence :
	CREATE SEQUENCE sequence_name sequence_spec* ';'
    ;

// Common Sequence

sequence_spec :
	  (INCREMENT BY | START WITH ) integer
    | (MAXVALUE integer | NOMAXVALUE)
    | (MINVALUE integer | NOMINVALUE)
    | (CYCLE | NOCYCLE)
    | (CACHE integer | NOCACHE)
    | (ORDER | NOORDER)
    | (KEEP | NOKEEP)
    | (SESSION | GLOBAL)
    ;

create_index :
	CREATE UNIQUE? INDEX index_name ON tableview_name paren_column_list (TABLESPACE REGULAR_ID)? (COMPUTE STATISTICS)? ';'
    ;

alter_index :
	ALTER INDEX old_index_name=index_name RENAME TO new_index_name=index_name ';'
    ;

create_user :
	CREATE USER
      user_object_name
        ( identified_by
          | identified_other_clause
          | user_tablespace_clause
          | quota_clause
          | profile_clause
          | password_expire_clause
          | user_lock_clause
          | user_editions_clause
          | container_clause
        )+ ';'
    ;

// The standard clauses only permit one user per statement.
// The proxy clause allows multiple users for a proxy designation.
alter_user :
	ALTER USER
      user_object_name
        ( alter_identified_by
        | identified_other_clause
        | user_tablespace_clause
        | quota_clause
        | profile_clause
        | user_default_role_clause
        | password_expire_clause
        | user_lock_clause
        | alter_user_editions_clause
        | container_clause
        | container_data_clause
        )+
      ';'
      | (COMMA? user_object_name)+ proxy_clause ';'
    ;

alter_identified_by :
	identified_by (REPLACE id_expression)?
    ;

identified_by :
	IDENTIFIED BY id_expression
    ;

identified_other_clause :
	IDENTIFIED (EXTERNALLY | GLOBALLY) (AS string)?
    ;

user_tablespace_clause :
	(DEFAULT | TEMPORARY) TABLESPACE id_expression
    ;

quota_clause :
	QUOTA (size_clause | UNLIMITED) ON id_expression
    ;

profile_clause :
	PROFILE id_expression
    ;

role_clause :
	(COMMA? role_name)+
    | ALL (EXCEPT (COMMA? role_name)+)*
    ;

user_default_role_clause :
	DEFAULT ROLE (NONE | role_clause)
    ;

password_expire_clause :
	PASSWORD EXPIRE
    ;

user_lock_clause :
	ACCOUNT (LOCK | UNLOCK)
    ;

user_editions_clause :
	ENABLE EDITIONS
    ;

alter_user_editions_clause :
	user_editions_clause (FOR (COMMA? regular_id)+)? FORCE?
    ;

proxy_clause :
	REVOKE CONNECT THROUGH (ENTERPRISE USERS | user_object_name)
    | GRANT CONNECT THROUGH
        ( ENTERPRISE USERS
        | user_object_name
            (WITH (NO ROLES | ROLE role_clause))?
            (AUTHENTICATION REQUIRED)?
            (AUTHENTICATED USING (PASSWORD | CERTIFICATE | DISTINGUISHED NAME))?
       )
    ;

container_names :
	LEFT_PAREN (COMMA? id_expression)+ RIGHT_PAREN
    ;

set_container_data :
	SET CONTAINER_DATA EQUALS_OP (ALL | DEFAULT | container_names)
    ;

add_rem_container_data :
	(ADD | REMOVE) CONTAINER_DATA EQUALS_OP container_names
    ;

container_data_clause :
	set_container_data
    | add_rem_container_data (FOR container_tableview_name)?
    ;

drop_index :
	DROP INDEX index_name ';'
    ;

revoke_statment :
	REVOKE (revoke_system_privileges | revoke_object_privileges)';'
	;

revoke_system_privileges :
	( COMMA? (role_name | system_privilege)) 
	  FROM (COMMA? grantee_name | PUBLIC)+
	;

revoke_object_privileges :
	( COMMA? (role_name | object_privilege))+ 
	  (ON grant_object_name)
	  FROM (COMMA? grantee_name | PUBLIC | role_name)+
	  (CASCADE CONSTRAINTS | FORCE)?
	;

grant_statement :
	GRANT
        ( COMMA?
          (role_name
          | system_privilege
          | object_privilege paren_column_list?
          )
        )+
      (ON grant_object_name)?
      TO (COMMA? grantee_name | PUBLIC)+
      (WITH (ADMIN | DELEGATE) OPTION)?
      (WITH HIERARCHY OPTION)?
      (WITH GRANT OPTION)?
      container_clause? ';'
    ;

container_clause :
	CONTAINER EQUALS_OP (CURRENT | ALL)
    ;

create_view :
	CREATE (OR REPLACE)? (NO? FORCE)? (EDITIONING	| EDITIONABLE EDITIONING? | NOEDITIONABLE)? VIEW
      tableview_name view_options?
      AS subquery subquery_restriction_clause?
    ;
 
view_options :
	( view_alias_constraint 
      | object_view_clause
//      | xmltype_view_clause //TODO
      )
    ;

view_alias_constraint :
	 LEFT_PAREN ( COMMA? (table_alias (VISIBLE | INVISIBLE)? inline_constraint* | out_of_line_constraint) )+ RIGHT_PAREN 
    ;

object_view_clause :
	OF type_name 
       ( WITH OBJECT (IDENTIFIER|ID|OID) ( DEFAULT | LEFT_PAREN (COMMA? REGULAR_ID)+ RIGHT_PAREN )
       | UNDER tableview_name
       )
       ( LEFT_PAREN ( COMMA? (out_of_line_constraint | REGULAR_ID inline_constraint ) )+ RIGHT_PAREN )*
    ;

constraint :
    inline_constraint
    out_of_line_constraint
    inline_ref_constraint
    out_of_line_ref_constraint
    ;

inline_constraint :
	(CONSTRAINT constraint_name)?
        ( NOT? NULL
        | UNIQUE
        | PRIMARY KEY
        | references_clause
        | check_constraint
        )
      constraint_state?
    ;

out_of_line_constraint :
	( (CONSTRAINT constraint_name)?
          ( primary_key_clause
          | foreign_key_clause
          | unique_key_clause
          | check_constraint
          )
       )+
      constraint_state? 
    ;     

constraint_state :
	( NOT? DEFERRABLE
      | INITIALLY (IMMEDIATE|DEFERRED)
      | (RELY|NORELY)
      | using_index_clause
      | (ENABLE|DISABLE)
      | (VALIDATE|NOVALIDATE)
	  | exceptions_clause
      )+
    ;

using_index_clause :
	USING INDEX ( 
                  index_name 
                | LEFT_PAREN create_index RIGHT_PAREN 
                | index_properties?
                )
	;

index_properties :
    ( 
        (
              global_partitioned_index 
            | local_partitioned_index
        ) 
      | index_attributes+
    )+ 
    | INDEXTYPE IS ( domain_index_clause /*| xmltable_index_clause */| xmlindex_clause) 
    
	;

global_partitioned_index :
	GLOBAL PARTITION BY
		( 
           'RANGE' paren_column_list LEFT_PAREN index_partitioning_clause RIGHT_PAREN 
		 | 'HASH'  paren_column_list ( individual_hash_partitions | hash_partitions_by_quantity )
        )
	;
	
individual_hash_partitions :
	 LEFT_PAREN ( COMMA? (partition_clause indexing_clause? partitioning_storage_clause) )+ RIGHT_PAREN 
	;

index_partitioning_clause :
	partition_clause_optional VALUES LESS THAN LEFT_PAREN literal ( COMMA literal )* RIGHT_PAREN segment_attributes_clause?
	;

segment_attributes_clause :
	( physical_attributes_clause+ | tablespace_clause | logging_clause )+ 
	;

index_attributes :
          physical_attributes_clause+
        | logging_clause 
        | ONLINE 
        | TABLESPACE (tablespace_name | DEFAULT) 
        | advanced_index_compression 
        | (SORT | NOSORT) 
        | REVERSE 
        | (VISIBLE | INVISIBLE) 
        | partial_index_clause 
        | parallel_clause
    
	;

physical_attributes_clause :
      PCTFREE integer 
    | PCTUSED integer 
    | INITRANS integer 
    | storage_clause 
	;

hash_partitions_by_quantity :
	PARTITIONS hash_partition_quantity ( STORE IN LEFT_PAREN tablespace_name ( COMMA tablespace_name )* RIGHT_PAREN )? ( table_compression | index_compression )? ( OVERFLOW STORE IN LEFT_PAREN tablespace_name ( COMMA tablespace_name )* RIGHT_PAREN )?
	;
 
local_partitioned_index :
	LOCAL ( on_range_partitioned_table | on_list_partitioned_table | on_hash_partitioned_table | on_comp_partitioned_table )?
	;

on_range_partitioned_table :
	 LEFT_PAREN partition_clause_optional ( (segment_attributes_clause | index_compression )+ )? ( UNUSABLE )? ( COMMA PARTITION ( partition_name )? ( ( segment_attributes_clause | index_compression )+ )? ( UNUSABLE )? )* RIGHT_PAREN 
	;
	
on_list_partitioned_table :
	 LEFT_PAREN partition_clause_optional 
	( ( segment_attributes_clause | index_compression )+ )? 
	usable_clause? 
	( COMMA partition_clause_optional ( ( segment_attributes_clause | index_compression )+ )? usable_clause? )* 
	 RIGHT_PAREN 
	;

on_hash_partitioned_table :
	( 
		  STORE IN LEFT_PAREN tablespace_name ( COMMA tablespace_name )* RIGHT_PAREN 
		| LEFT_PAREN 
				partition_clause_optional tablespace_clause? advanced_index_compression? usable_clause? 
		  ( COMMA partition_clause_optional tablespace_clause? advanced_index_compression? usable_clause? )* 
		 RIGHT_PAREN 
	  );

on_comp_partitioned_table :
	store_in? 
	 LEFT_PAREN 
		  partition_clause_optional ( ( segment_attributes_clause | advanced_index_compression )+ )? usable_clause? index_subpartition_clause? 
	( COMMA partition_clause_optional ( ( segment_attributes_clause | advanced_index_compression )+ )? usable_clause? index_subpartition_clause? )* 
	 RIGHT_PAREN 
	;


xmltable_index_clause :
	EMPTY
	;

xmlindex_clause :
	EMPTY
	;

partition_clause : PARTITION partition_name;
partition_clause_optional : PARTITION partition_name?;

indexing_clause :
	INDEXING (ON |OFF)
	;

partitioning_storage_clause :
    (
	  tablespace_clause 
	| OVERFLOW tablespace_clause? 
	| table_compression 
	| index_compression
    | inmemory_clause 
	| lob_partitioning_storage 
	| VARRAY varray_item STORE AS ( SECUREFILE | BASICFILE )? LOB lob_segname 
	)+ 
	  
	;

inmemory_table_clause : 
      INMEMORY (inmemory_parameters inmemory_column_clause)? 
    | NO INMEMORY
    ;

inmemory_column_clause : 
      INMEMORY inmemory_memcompress? paren_column_list
    | NO INMEMORY
    ;

inmemory_clause : 
    INMEMORY inmemory_parameters | NO INMEMORY
    ;

inmemory_parameters : 
      inmemory_memcompress?
    | inmemory_priority?
    | inmemory_distribute?
    | inmemory_duplicate?
    ;

inmemory_memcompress :
      MEMCOMPRESS FOR (DML | (QUERY | CAPACITY) (LOW | HIGH)?)
    | NO MEMCOMPRESS
    ;

inmemory_priority :
    PRIORITY (NONE | LOW | MEDIUM | HIGH | CRITICAL)
    ;

inmemory_distribute:
    DISTRIBUTE (
          AUTO
        | (BY (ROWID RANGE | PARTITION | SUBPARTITION))
    )?
    ;

inmemory_duplicate :
    DUPLICATE ALL? | NO DUPLICATE
    ;

table_compression :
      COMPRESS 
    | ROW STORE COMPRESS (BASIC | ADVANCED)? 
    | COLUMN STORE COMPRESS (FOR (QUERY | ARCHIVE) (LOW | HIGH)?)? (NO? ROW LEVEL LOCKING)?
    | NO COMPRESS
    ;

index_compression : prefix_compression | advanced_index_compression;

prefix_compression :
	  COMPRESS integer? 
	| NOCOMPRESS
	;

advanced_index_compression :
	  COMPRESS ADVANCED LOW
    | NOCOMPRESS
	;

lob_partitioning_storage :
	LOB LEFT_PAREN lob_item_name RIGHT_PAREN 
	STORE AS ( BASICFILE | SECUREFILE )? ( lob_segname ( LEFT_PAREN tablespace_clause RIGHT_PAREN )? | LEFT_PAREN tablespace_clause RIGHT_PAREN )?
	;


index_subpartition_clause :
	store_in
	| LEFT_PAREN 
			partition_clause_optional tablespace_clause? advanced_index_compression? usable_clause? 
	  ( COMMA partition_clause_optional tablespace_clause? advanced_index_compression? usable_clause? )* 
	 RIGHT_PAREN 
	;

usable_clause :
	USABLE | UNUSABLE
	;

hash_partition_quantity :
	UNSIGNED_INTEGER
	;

varray_item :
	REGULAR_ID
	;

partial_index_clause :
	INDEXING (PARTIAL | FULL)
	;

parallel_clause :
	NOPARALLEL | PARALLEL integer
	;

domain_index_clause :
    indextype ( local_domain_index_clause )? ( parallel_clause )? ( PARAMETERS LEFT_PAREN odci_parameters RIGHT_PAREN )?
	;

local_domain_index_clause : 
    LOCAL local_domain_index_parameters_clause? ( COMMA local_domain_index_parameters_clause )*
    ;

local_domain_index_parameters_clause : 
    LEFT_PAREN PARTITION partition_name 
    (
        PARAMETERS LEFT_PAREN odci_parameters RIGHT_PAREN 
    )?
  RIGHT_PAREN 
    ;

odci_parameters :
    CHAR_STRING;

exceptions_clause :
	EXCEPTIONS INTO table_fullname
    ;

create_tablespace :
	CREATE (BIGFILE | SMALLFILE)? 
        ( permanent_tablespace_clause
        | temporary_tablespace_clause
        | undo_tablespace_clause
        )
      ';'
    ;

permanent_tablespace_clause :
	tablespace_clause datafile_specification? 
        ( MINIMUM EXTENT size_clause
        | BLOCKSIZE size_clause
        | logging_clause
        | FORCE LOGGING
        | (ONLINE | OFFLINE)
        | ENCRYPTION tablespace_encryption_spec
        | DEFAULT //TODO table_compression? storage_clause?
        | extent_management_clause
        | segment_management_clause
        | flashback_mode_clause
        )*
    ;      

tablespace_encryption_spec :
	USING encrypt_algorithm=CHAR_STRING
    ;

logging_clause :
	    LOGGING
      | NOLOGGING
      | FILESYSTEM_LIKE_LOGGING
      
    ;

extent_management_clause :
	EXTENT MANAGEMENT LOCAL 
        ( AUTOALLOCATE
        | UNIFORM (SIZE size_clause)?
        )?
    ;

segment_management_clause :
	SEGMENT SPACE_KEYWORD MANAGEMENT (AUTO | MANUAL)
    ;

flashback_mode_clause :
	FLASHBACK (ON | OFF)
    ;

temporary_tablespace_clause :
	TEMPORARY tablespace_clause
        tempfile_specification?
        tablespace_group_clause? extent_management_clause?
    ;

tablespace_group_clause :
	TABLESPACE GROUP (REGULAR_ID | CHAR_STRING)
    ;

undo_tablespace_clause :
	UNDO tablespace_clause
         datafile_specification? 
         extent_management_clause? tablespace_retention_clause?
    ;

tablespace_retention_clause :
	RETENTION (GUARANTEE | NOGUARANTEE)
    ;

datafile_specification :
	DATAFILE
	  (COMMA? datafile_tempfile_spec) 
    ;

tempfile_specification :
	TEMPFILE
	  (COMMA? datafile_tempfile_spec) 
    ;

datafile_tempfile_spec :
	CHAR_STRING? (SIZE size_clause)? REUSE? autoextend_clause?
    ;

redo_log_file_spec :
	DATAFILE ( CHAR_STRING
      | LEFT_PAREN ( COMMA? CHAR_STRING )+ RIGHT_PAREN 
      )?
        (SIZE size_clause)?
        (BLOCKSIZE size_clause)?
        REUSE?
    ;

autoextend_clause :
	AUTOEXTEND (OFF | ON (NEXT size_clause)? maxsize_clause? )
    ;

maxsize_clause :
	MAXSIZE (UNLIMITED | size_clause)
    ;

subquery :
	subquery_basic_elements subquery_operation_part*
    ;

create_table :
	CREATE (GLOBAL TEMPORARY)? TABLE tableview_name
        (
            relational_table 
        /*| object_table 
          | xmltype_table*/
        )
        ;

relational_table : 
    ( LEFT_PAREN relational_properties+ RIGHT_PAREN )? 
    (ON COMMIT (DELETE | PRESERVE)? ROWS)? 
    physical_properties? table_properties
    ;

relational_properties :
      column_definition
    | virtual_column_definition
    | period_definition
    | (
          out_of_line_constraint 
        | out_of_line_ref_constraint
      )
    | supplemental_logging_props
    ;

table_properties : 
    column_properties? indexing_clause? table_partitioning_clauses? attribute_clustering_clause? 
    (CACHE | NOCACHE)? (RESULT_CACHE LEFT_PAREN MODE (DEFAULT | FORCE) RIGHT_PAREN )? parallel_clause? 
    (ROWDEPENDENCIES | NOROWDEPENDENCIES)? enable_disable_clause* row_movement_clause? 
    flashback_archive_clause? (ROW ARCHIVAL)? (AS subquery)?
    ;

flashback_archive_clause : 
      FLASHBACK ARCHIVE flashback_archive_name?
    | NO FLASHBACK ARCHIVE
    ;

row_movement_clause : 
    (ENABLE | DISABLE) ROW MOVEMENT
    ;

attribute_clustering_clause :
    CLUSTERING clustering_join? cluster_clause clustering_when? zonemap_clause
    ;

clustering_join :
    table_fullname JOIN table_fullname // ON LEFT_PAREN equijoin_condition RIGHT_PAREN 
    ;

cluster_clause :
    BY (LINEAR | INTERVLEAVED)? ORDER clustering_columns
    ;

clustering_columns : 
      clustering_columns_group
    | LEFT_PAREN clustering_columns (COMMA clustering_columns)+ RIGHT_PAREN 
    ;

clustering_columns_group : 
    paren_column_list
    ;

clustering_when :
      ((YES | NO) ON LOAD)
    | ((YES | NO) ON DATA MOVEMENT)
    ;

zonemap_clause : 
      WITH MATERIALIZED ZONEMAP LEFT_PAREN zonemap_name RIGHT_PAREN 
    | WITHOUT MATERIALIZED ZONEMAP
    ;

enable_disable_clause :
    (ENABLE | DISABLE) (VALIDATE | NOVALIDATE)? 
    (
          UNIQUE paren_column_list
        | PRIMARY KEY
        | CONSTRAINT constraint_name
    )
    using_index_clause? exceptions_clause CASCADE? (KEEP INDEX | DROP)?
    ;

table_partitioning_clauses :
      range_partitions
    | list_partitions
    | hash_partitions
    | composite_range_partitions
    | composite_list_partitions 
    | composite_hash_partitions
    | reference_partitioning
    // | system_partitioning 
    ;

range_partitions :
    PARTITION BY RANGE paren_column_list 
    (INTERVAL LEFT_PAREN expression RIGHT_PAREN STORAGE IN LEFT_PAREN tablespace_name (COMMA tablespace_name)* RIGHT_PAREN )?
    LEFT_PAREN PARTITION partition_name? range_values_clause table_partition_description RIGHT_PAREN 
    ;

hash_partitions :
    PARTITION BY HASH paren_column_list (individual_hash_partitions | hash_partition_quantity)
    ;

column_properties :
      object_type_col_properties 
    | nested_table_col_properties
    | (varray_col_properties | lob_storage_clause) ( LEFT_PAREN lob_partitioning_storage RIGHT_PAREN )*
    | xmltype_column_properties
    ;

xmltype_column_properties : 
    XMLTYPE COLUMN? column_name xmltype_storage? xmlschema_spec
    ;

xmltype_storage : 
    STORE
    (
        AS (
              OBJECT RELATIONAL
            | (SECUREFILE | BASICFILE)? 
              (CLOB | BINARY XML)? 
              ( lob_segname paren_lob_parameters? | paren_lob_parameters )?
        )
      | 
        ALL VARRAYS AS (LOBS | TABLES)
    )
    ;

xmlschema_spec : 
        (XMLSCHEMA xmlschema_url=CHAR_STRING)? ELEMENT (element_name | xmlschema_url2=CHAR_STRING SHARP element_name)
        (STORE ALL VARRAYS AS (LOBS | TABLES))?
        ((ALLOW | DISALLOW) NONSCHEMA)? 
        ((ALLOW | DISALLOW) ANYSCHEMA)? 
    ;
    

list_partitions :
    PARTITION BY LIST paren_column_list LEFT_PAREN (PARTITION partition_name? list_values_clause table_partition_description )+ RIGHT_PAREN 
    ;

composite_range_partitions :
    PARTITION BY RANGE paren_column_list INTERVAL LEFT_PAREN expression RIGHT_PAREN (STORE IN LEFT_PAREN tablespace_name (COMMA tablespace_name)*RIGHT_PAREN )
    (subpartition_by_range | subpartition_by_list | subpartition_by_hash) LEFT_PAREN range_partition_desc RIGHT_PAREN 
    ;



composite_hash_partitions :
    PARTITION BY HASH paren_column_list 
    (subpartition_by_range | subpartition_by_list | subpartition_by_hash)
    (individual_hash_partitions | hash_partitions_by_quantity)
    ;

composite_list_partitions :
    PARTITION BY LIST paren_column_list 
    (subpartition_by_range | subpartition_by_list | subpartition_by_hash)
    list_partition_desc (COMMA list_partition_desc)*
    ;

range_partition_desc : 
    PARTITION partition_name range_values_clause table_partition_description
    (
        LEFT_PAREN 
              range_subpartition_desc (COMMA range_subpartition_desc)* 
            | list_subpartition_desc (COMMA list_subpartition_desc)* 
            | individual_hash_subparts (COMMA individual_hash_subparts)* 
        RIGHT_PAREN 
        | hash_subparts_by_quantity
    )?;

list_partition_desc : 
    PARTITION partition_name list_values_clause table_partition_description
    (
        LEFT_PAREN 
              range_subpartition_desc (COMMA range_subpartition_desc)* 
            | list_subpartition_desc (COMMA list_subpartition_desc)* 
            | individual_hash_subparts (COMMA individual_hash_subparts)* 
        RIGHT_PAREN 
        | hash_subparts_by_quantity
    )?;

range_subpartition_desc : 
    SUBPARTITION subpartition_name? range_values_clause indexing_clause? partitioning_storage_clause?
    ;

list_subpartition_desc :
    SUBPARTITION subpartition_name? list_values_clause indexing_clause? partitioning_storage_clause?
    ;

individual_hash_subparts :
    SUBPARTITION subpartition_name? indexing_clause? partitioning_storage_clause?
    ;

hash_subparts_by_quantity :
    SUBPARTITION integer (STORE IN ( LEFT_PAREN tablespace_name (COMMA tablespace_name)* RIGHT_PAREN ))?
    ;

table_partition_description : 
    deferred_segment_creation? indexing_clause? segment_attributes_clause? (table_compression | prefix_compression)?
    inmemory_clause? ilm_clause? (OVERFLOW segment_attributes_clause?)?
    (lob_storage_clause | varray_col_properties | nested_table_col_properties)*
    ;

range_values_clause : 
    VALUES LESS THAN LEFT_PAREN (literal (COMMA literal)* | MAXVALUE) RIGHT_PAREN 
    ;

list_values_clause : 
    VALUES LEFT_PAREN (literal (COMMA literal)* | NULL) | DEFAULT RIGHT_PAREN 
    ;

subpartition_by_range :
    SUBPARTITION BY RANGE paren_column_list subpartition_template
    ;

subpartition_by_list :
    SUBPARTITION BY LIST paren_column_list subpartition_template
    ;

subpartition_by_hash :
    SUBPARTITION BY HASH paren_column_list 
    (
        SUBPARTITIONS integer (STORE IN LEFT_PAREN tablespace_name (COMMA tablespace_name)* RIGHT_PAREN )
        subpartition_template
    );


subpartition_template : 
    SUBPARTITION TEMPLATE 
    (
        LEFT_PAREN 
              range_subpartition_desc (COMMA range_subpartition_desc)* 
            | list_subpartition_desc (COMMA list_subpartition_desc)* 
            | individual_hash_subparts (COMMA individual_hash_subparts)* 
        RIGHT_PAREN 
        | hash_partition_quantity

    );

reference_partitioning :
    PARTITION BY REFERENCE LEFT_PAREN constraint RIGHT_PAREN ( LEFT_PAREN reference_partition_desc (COMMA reference_partition_desc)? RIGHT_PAREN )?
    ;

reference_partition_desc :
    PARTITION BY SYSTEM (PARTITIONS integer | (reference_partition_desc (COMMA reference_partition_desc)+))?
    ;

object_type_col_properties : 
    COLUMN column_name substituable_column_clause
    ;

substituable_column_clause : 
      (ELEMENT? IS OF TYPE? LEFT_PAREN ONLY type_name RIGHT_PAREN )
    | NOT ? SUBSTITUTABLE AT ALL LEVELS
    ;

nested_table_col_properties :
    NESTED TABLE (nested_item=collection_name | COLUMN VALUE) substituable_column_clause? (LOCAL GLOBAL)?
    STORE AS storage_table=table_fullname (
    LEFT_PAREN (
          LEFT_PAREN object_properties RIGHT_PAREN 
        | physical_properties
        | column_properties

    )+ RIGHT_PAREN 
    )?
    RETURN AS (LOCATOR | VALUE)
    ;

object_properties :
      (column_name | attribute_name) (DEFAULT expression)? (inline_constraint+ | inline_ref_constraint)?
    | (   out_of_line_constraint
        | out_of_line_ref_constraint
        | supplemental_logging_props
    )
    ;

varray_col_properties : 
	VARRAY varray_item (substituable_column_clause? varray_storage_clause | substituable_column_clause)
    ;

varray_storage_clause :
    STORE AS ( SECUREFILE | BASICFILE )? LOB (lob_segname? LEFT_PAREN lob_storage_parameters RIGHT_PAREN | lob_storage_parameters )
    ;

lob_storage_clause :
    LOB 
    (
          LEFT_PAREN lob_item_name (COMMA lob_item_name)* RIGHT_PAREN STORE AS (( SECUREFILE | BASICFILE ) | LEFT_PAREN lob_storage_parameters RIGHT_PAREN )
        | LEFT_PAREN lob_item_name (COMMA lob_item_name)* RIGHT_PAREN STORE AS (( SECUREFILE | BASICFILE ) | lob_segname | LEFT_PAREN lob_storage_parameters RIGHT_PAREN )

    )
    ;

lob_storage_parameters : 
    (
          tablespace_clause
        | lob_parameters+ storage_clause?
    )
    | storage_clause
    ;

paren_lob_parameters :
    LEFT_PAREN lob_parameters RIGHT_PAREN 
    ;

lob_parameters :
      (ENABLE | DISABLE) STORAGE IN ROW
    | CHUNK ChunkInteger=integer
    | PCTVERSION PctVersionIntger=integer
    | FREEPOOLS FreePoolsInteger=integer
    | lob_retention_clause
    | lob_deduplicate_clause
    | lob_compresssion_clause
    | (ENCRYPT encryption_spec | DECRYPT)
    | (CACHE | NOCACHE | CACHE READS) logging_clause
    ;

lob_retention_clause :
    RETENTION (MAX | MIN MinIntger=integer | AUTO | NONE)?
    ;

lob_deduplicate_clause : 
    DEDUPLICATE | KEEP_DUPLICATES
    ;

lob_compresssion_clause :
    COMPRESS (HIGH | MEDIUM | LOW )? | NOCOMPRESS 
    ;

physical_properties :
      deferred_segment_creation? segment_attributes_clause table_compression? inmemory_table_clause? ilm_clause
    | deferred_segment_creation? ORGANIZATION 
    (
          HEAP segment_attributes_clause? table_compression? inmemory_table_clause? ilm_clause?
        | INDEX segment_attributes_clause? index_org_table_clause
        | EXTERNAL external_table_clause
    )
    ;

external_table_clause :
    LEFT_PAREN (TYPE /*access_driver_type*/)? external_data_properties RIGHT_PAREN (REJECT LIMIT (integer | UNLIMITED))?
    ;

external_data_properties :
    DEFAULT DIRECTORY directory_name (ACCESS PARAMETERS ( LEFT_PAREN /*opaque_format_spec*/ RIGHT_PAREN | USING CLOB subquery)) 
    LOCATION LEFT_PAREN external_data_properties_location (COMMA external_data_properties_location)+ RIGHT_PAREN 
    ;

external_data_properties_location :
    (directory_name? ':')? CHAR_STRING
    ;

index_org_table_clause : 
    (
          mapping_table_clause
        | PCTTHRESHOLD integer
        | prefix_compression

    )? index_org_overflow_clause
    ;

mapping_table_clause : 
    MAPPING TABLE | NOMAPPING
    ;

index_org_overflow_clause :
    (INCLUDING column_name)? OVERFLOW segment_attributes_clause? 
    ;

deferred_segment_creation :
    SEGMENT CREATION (IMMEDIATE | DEFERRED)
    ;

tablespace_clause : TABLESPACE tablespace_name;

store_in : 
    STORE IN LEFT_PAREN tablespace_name ( COMMA tablespace_name )* RIGHT_PAREN 
    ;

ilm_clause :
    ILM
      ADD POLICY ilm_policy_clause
    | (
          DELETE
        | ENABLE
        | DISABLE
      ) POLICY ilm_policy_name
    |   ( DELETE ALL
        | ENABLE ALL
        | DISABLE ALL)
    ;

ilm_policy_clause :
    ilm_compression_policy | ilm_tiering_policy
    ;

ilm_compression_policy :
      table_compression (SEGMENT | GROUP) (AFTER ilm_time_period OF (NO ACCESS | NO MODIFICATION | CREATION) | ON function_name)
    | ROW STORE COMPRESS ADVANCED ROW AFTER ilm_time_period OF NO MODIFICATION
    ;

ilm_tiering_policy :
      TIER TO tablespace_name (SEGMENT | GROUP)? ON function_name
    | TIER TO tablespace_name READ ONLY (SEGMENT | GROUP)? ( AFTER ilm_time_period OF (NO ACCESS | NO MODIFICATION | CREATION) | ON function_name)
    ;

ilm_time_period :
    integer 
    (
      DAY 
    | DAYS
    | MONTH
    | MONTHS
    | YEAR
    | YEARS
    )
    ;

storage_clause :
    STORAGE LEFT_PAREN 
        (INITIAL initial_size=size_clause
        | NEXT next_size=size_clause
        | MINEXTENTS minextents=(UNSIGNED_INTEGER | UNLIMITED)
        | PCTINCREASE pctincrease=UNSIGNED_INTEGER
        | FREELISTS freelists=UNSIGNED_INTEGER
        | FREELIST GROUPS freelist_groups=UNSIGNED_INTEGER
        | OPTIMAL (size_clause | NULL)
        | BUFFER_POOL (KEEP | RECYCLE | DEFAULT)
        | FLASH_CACHE (KEEP | NONE | DEFAULT)
        | ENCRYPT
        )+
    RIGHT_PAREN 
    ;

column_definition :
    column_name datatype? SORT? (VISIBLE | INVISIBLE)? 
    (
        DEFAULT (ON NULL)? expression 
    | ( GENERATED (ALWAYS | BY DEFAULT (ON NULL)?)? AS IDENTITY ( LEFT_PAREN identity_options RIGHT_PAREN )?)
    )?
    (ENCRYPT encryption_spec)?
    (inline_constraint+ | inline_ref_constraint)?
    ;

virtual_column_definition : 
    column_name datatype? SORT? (VISIBLE | INVISIBLE)? (GENERATED ALWAYS)? AS LEFT_PAREN RIGHT_PAREN 
    VIRTUAL? evaluation_edition_clause? unusable_editions_clause? inline_constraint*
    ;

period_definition :
        PERIOD FOR valid_time_column=column_name (start_time_column=column_name COMMA end_time_column=column_name)?
    ;

supplemental_logging_props :
    SUPPLEMENTAL LOG (supplemental_log_grp_clause | supplemental_id_key_clause)
    ;

supplemental_log_grp_clause :
    GROUP log_group_name LEFT_PAREN column_logged (COMMA column_logged)* RIGHT_PAREN ALWAYS? 
    ;

supplemental_id_key_clause :
    DATA LEFT_PAREN (ALL | PRIMARY KEY | UNIQUE |FOREIGN KEY)+ RIGHT_PAREN COLUMNS
    ;

column_logged : column_name (NO LOG)?;

evaluation_edition_clause :
    EVALUATION USING 
    (
          CURRENT EDITION
        | EDITION edition_name
        | NULL EDITION
    )
    ;

edition_name : identifier;  // A valider

unusable_editions_clause :
    UNUSABLE BEFORE (CURRENT EDITION | EDITION edition_name)
    ;

// Remonté dans le parent
// identify_clause :
//     GENERATED (ALWAYS | BY DEFAULT (ON NULL)?)? AS IDENTITY ( LEFT_PAREN identity_options RIGHT_PAREN )?
//     ;

identity_options : 
   	  (INCREMENT BY | START WITH ) UNSIGNED_INTEGER
    | (MAXVALUE UNSIGNED_INTEGER | NOMAXVALUE)
    | (MINVALUE UNSIGNED_INTEGER | NOMINVALUE)
    | (CYCLE | NOCYCLE)
    | (CACHE UNSIGNED_INTEGER | NOCACHE)
    | (ORDER | NOORDER)
    ;

encryption_spec :
    (USING Encrypt_Algoritm=CHAR_STRING)? (IDENTIFIED BY Password=CHAR_STRING)? ( Integrity_Algoritm=CHAR_STRING)? (NO? SALT)?
    ;

inline_ref_constraint :
      SCOPE IS table_fullname
    | WITH ROWID
    | CONSTRAINT constraint_name? references_clause constraint_state?
    ;

out_of_line_ref_constraint :
      SCOPE FOR LEFT_PAREN column_name /*| ref_attr*/ RIGHT_PAREN IS table_fullname
    | REF LEFT_PAREN column_name /*| ref_attr*/ RIGHT_PAREN WITH ROWID 
    | CONSTRAINT constraint_name? FOREIGN KEY paren_column_list references_clause constraint_state?
    ;
    
//Technically, this should only allow 'K' | 'M' | 'G' | 'T' | 'P' | 'E'
// but having issues with examples/numbers01.sql line 11 "sysdate -1m"
size_clause :
	UNSIGNED_INTEGER REGULAR_ID?
    ;

drop_table :
	DROP TABLE tableview_name SEMICOLON
    ;

comment_on_column :
	COMMENT ON COLUMN tableview_name PERIOD column_name IS string
    ;

// Synonym DDL Clauses

create_synonym : 
	  CREATE (OR REPLACE)? PUBLIC SYNONYM synonym_name FOR (objectSchema=schema_name PERIOD)? objectName=schema_object_name (AT_SIGN link_name)?
    | CREATE (OR REPLACE)? SYNONYM (schema=schema_name PERIOD)? synonym_name FOR (objectSchema=schema_name PERIOD)? objectName=schema_object_name (AT_SIGN link_name)?
    ;

comment_on_table :
	COMMENT ON TABLE tableview_name IS string
    ;

alter_table :
	ALTER TABLE tableview_name
      ( add_constraint
      | drop_constraint
      | enable_constraint
      | disable_constraint
      )
    ;

add_constraint :
	ADD (CONSTRAINT constraint_name)?
     ( primary_key_clause
     | foreign_key_clause
     | unique_key_clause
     | check_constraint
     )
    ;

check_constraint :
	CHECK LEFT_PAREN condition RIGHT_PAREN DISABLE?
    ;

drop_constraint :
	DROP CONSTRAINT constraint_name
    ;

enable_constraint :
	ENABLE CONSTRAINT constraint_name
    ;

disable_constraint :
	DISABLE CONSTRAINT constraint_name
    ;

foreign_key_clause :
	FOREIGN KEY paren_column_list references_clause on_delete_clause?
    ;

references_clause :
	REFERENCES tableview_name paren_column_list (ON DELETE (CASCADE | SET NULL))?
    ;

on_delete_clause :
	ON DELETE (CASCADE | SET NULL)
    ;

unique_key_clause :
	UNIQUE paren_column_list
      // TODO implement  USING INDEX clause
    ;

primary_key_clause :
	PRIMARY KEY paren_column_list
      // TODO implement  USING INDEX clause
    ;

// Anonymous PL/SQL code block

anonymous_block :
	(DECLARE seq_of_declare_specs)? BEGIN seq_of_statements (EXCEPTION exception_handler+)? END SEMICOLON
    ;

// Common DDL Clauses

invoker_rights_clause :
	AUTHID (CURRENT_USER | DEFINER)
    ;

compiler_parameters_clause :
	identifier '=' expression
    ;

call_spec :
	LANGUAGE (java_spec | c_spec)
    ;

// Call Spec Specific Clauses

java_spec :
	JAVA NAME CHAR_STRING
    ;

c_spec :
	C_LETTER (NAME CHAR_STRING)? LIBRARY identifier c_agent_in_clause? (WITH CONTEXT)? c_parameters_clause?
    ;

c_agent_in_clause :
	AGENT IN LEFT_PAREN expressions RIGHT_PAREN 
    ;

c_parameters_clause :
	PARAMETERS LEFT_PAREN (expressions | '.' '.' '.') RIGHT_PAREN 
    ;

parameter :
	parameter_name (IN | OUT | INOUT | NOCOPY)* type_spec? default_value_part?
    ;

default_value_part :
	(ASSIGN_OP | DEFAULT) expression
    ;

// Elements Declarations

seq_of_declare_specs :
	declare_spec+
    ;

declare_spec :
	variable_declaration
    | subtype_declaration
    | cursor_declaration
    | exception_declaration
    | pragma_declaration
    | type_declaration
    | procedure_spec
    | function_spec
    | procedure_body
    | function_body
    ;

// incorporates constant_declaration
variable_declaration :
	identifier CONSTANT? type_spec (NOT NULL)? default_value_part? ';'
    ;

subtype_declaration :
	SUBTYPE identifier IS type_spec (RANGE expression '..' expression)? (NOT NULL)? ';'
    ;

// cursor_declaration incorportates curscursor_body and cursor_spec

cursor_declaration :
	CURSOR identifier ( LEFT_PAREN (COMMA? parameter_spec)+ RIGHT_PAREN )? (RETURN type_spec)? (IS select_statement)? ';'
    ;

parameter_spec :
	parameter_name (IN? type_spec)? default_value_part?
    ;

exception_declaration  :
	identifier EXCEPTION ';'
    ;

pragma_declaration :
	PRAGMA (SERIALLY_REUSABLE 
    | AUTONOMOUS_TRANSACTION
    | EXCEPTION_INIT LEFT_PAREN exception_name COMMA numeric_negative RIGHT_PAREN 
    | INLINE LEFT_PAREN id1=identifier COMMA expression RIGHT_PAREN 
    | RESTRICT_REFERENCES LEFT_PAREN (identifier | DEFAULT) (COMMA identifier)+ RIGHT_PAREN ) ';'
    ;

// incorporates ref_cursor_type_definition
type_declaration :
	 TYPE identifier IS (table_type_def | varray_type_def | record_type_def | ref_cursor_type_def) ';'
    ;

ref_cursor_type_def :
	REF CURSOR (RETURN type_spec)?
    ;

table_type_def :
	TABLE OF type_spec table_indexed_by_part? (NOT NULL)?
    ;

table_indexed_by_part :
	(idx1=INDEXED | idx2=INDEX) BY type_spec
    ;

varray_type_def :
	(VARRAY | VARYING ARRAY) LEFT_PAREN expression RIGHT_PAREN OF type_spec (NOT NULL)?
    ;

record_type_def : // Record Declaration Specific Clauses
	RECORD LEFT_PAREN (COMMA? field_spec)+ RIGHT_PAREN 
    ;

field_spec :
	column_name type_spec? (NOT NULL)? default_value_part?
    ;

// Statements
seq_of_statements :
	(statement (';' | EOF) | label_declaration)+
    ;

label_declaration :
	ltp1= '<' '<' label_name '>' '>'
    ;

statement :
	CREATE swallow_to_semi
    | TRUNCATE swallow_to_semi
    | body
    | block
    | assignment_statement
    | continue_statement
    | exit_statement
    | goto_statement
    | if_statement
    | loop_statement
    | forall_statement
    | null_statement
    | raise_statement
    | return_statement
    | case_statement/*[true]*/
    | sql_statement
    | function_call
    | pipe_row_statement
    ;

swallow_to_semi :
	~';'+
    ;

assignment_statement :
	(general_element | bind_variable) ASSIGN_OP expression
    ;

continue_statement :
	CONTINUE label_name? (WHEN condition)?
    ;

exit_statement :
	EXIT label_name? (WHEN condition)?
    ;

goto_statement :
	GOTO label_name
    ;

if_statement :
	IF condition THEN seq_of_statements elsif_part* else_part? END IF
    ;

elsif_part :
	ELSIF condition THEN seq_of_statements
    ;

else_part :
	ELSE seq_of_statements
    ;

loop_statement :
	label_declaration? (WHILE condition | FOR cursor_loop_param)? LOOP seq_of_statements END LOOP label_name?
    ;

// Loop Specific Clause

cursor_loop_param :
	index_name IN REVERSE? lower_bound range_separator='..' upper_bound
    | record_name IN (cursor_name ( LEFT_PAREN expressions? RIGHT_PAREN )? | LEFT_PAREN select_statement RIGHT_PAREN )
    ;

forall_statement :
	FORALL index_name IN bounds_clause sql_statement (SAVE EXCEPTIONS)?
    ;

bounds_clause :
	lower_bound '..' upper_bound
    | INDICES OF collection_name between_bound?
    | VALUES OF index_name
    ;

between_bound :
	BETWEEN lower_bound AND upper_bound
    ;

lower_bound :
	concatenation
    ;

upper_bound :
	concatenation
    ;

null_statement :
	NULL
    ;

raise_statement :
	RAISE exception_name?
    ;

return_statement :
	RETURN expression?
    ;

function_call :
	CALL? routine_name function_arguments? keep_clause?
    ;

pipe_row_statement :
	PIPE ROW LEFT_PAREN expression RIGHT_PAREN ;

body :
	BEGIN seq_of_statements (EXCEPTION exception_handler+)? END label_name?
    ;

// Body Specific Clause

exception_handler :
	WHEN exception_name (OR exception_name)* THEN seq_of_statements
    ;

trigger_block :
	(DECLARE? declare_spec+)? body
    ;

block :
	DECLARE? declare_spec+ body
    ;

// SQL Statements

sql_statement :
	execute_immediate
    | data_manipulation_language_statements
    | cursor_manipulation_statements
    | transaction_control_statements
    ;

execute_immediate :
	EXECUTE IMMEDIATE expression (into_clause using_clause? | using_clause dynamic_returning_clause? | dynamic_returning_clause)?
    ;

// Execute Immediate Specific Clause

dynamic_returning_clause :
	(RETURNING | RETURN) into_clause
    ;

// DML Statements

data_manipulation_language_statements :
	  merge_statement
    | lock_table_statement
    | select_statement
    | update_statement
    | delete_statement
    | insert_statement
    // | explain_statement
    // | merge_statement
//    |    case_statement[true]
    ;

// Cursor Manipulation Statements

cursor_manipulation_statements :
	close_statement
    | open_statement
    | fetch_statement
    | open_for_statement
    ;

close_statement :
	CLOSE cursor_name
    ;

open_statement :
	OPEN cursor_name ( LEFT_PAREN expressions? RIGHT_PAREN )?
    ;

fetch_statement :
	FETCH cursor_name (it1=INTO (COMMA? variable_name)+ | BULK COLLECT INTO (COMMA? variable_name)+)
    ;

open_for_statement :
	OPEN variable_name FOR (select_statement | expression) using_clause?
    ;

// Transaction Control SQL Statements

transaction_control_statements :
	set_transaction_command
    | set_constraint_command
    | commit_statement
    | rollback_statement
    | savepoint_statement
    ;

set_transaction_command :
	SET TRANSACTION
      (READ (ONLY | WRITE) | ISOLATION LEVEL (SERIALIZABLE | READ COMMITTED) | USE ROLLBACK SEGMENT rollback_segment_name)?
      (NAME string)?
    ;

set_constraint_command :
	SET (CONSTRAINT | CONSTRAINTS) (ALL | (COMMA? constraint_name)+) (IMMEDIATE | DEFERRED)
    ;

commit_statement :
	COMMIT WORK? 
      (COMMENT expression | FORCE (CORRUPT_XID expression | CORRUPT_XID_ALL | expression (COMMA expression)?))?
      write_clause?
    ;

write_clause :
	WRITE (WAIT | NOWAIT)? (IMMEDIATE | BATCH)?
    ;

rollback_statement :
	ROLLBACK WORK? (TO SAVEPOINT? savepoint_name | FORCE string)?
    ;

savepoint_statement :
	SAVEPOINT savepoint_name 
    ;

// Dml

/* TODO
//SHOULD BE OVERRIDEN!
seq_of_statements  :
	select_statement
    | update_statement
    | delete_statement
    | insert_statement
    | lock_table_statement
    | merge_statement
    | explain_statement
//    | case_statement[true]
    ;
*/

explain_statement :
	EXPLAIN PLAN (SET STATEMENT_ID '=' string)? (INTO tableview_name)?
      FOR (select_statement | update_statement | delete_statement | insert_statement | merge_statement)
    ;

select_statement :
	subquery_factoring_clause? subquery (for_update_clause | order_by_clause)*
    ;

// Select Specific Clauses

subquery_factoring_clause :
	WITH (COMMA? factoring_element)+
    ;

factoring_element :
	query_name paren_column_list? AS LEFT_PAREN subquery order_by_clause? RIGHT_PAREN 
      search_clause? cycle_clause?
    ;

search_clause :
	SEARCH (DEPTH | BREADTH) FIRST BY column_name ASC? DESC? (NULLS FIRST)? (NULLS LAST)?
      (COMMA column_name ASC? DESC? (NULLS FIRST)? (NULLS LAST)?)* SET column_name
    ;

cycle_clause :
	CYCLE column_list SET column_name TO expression DEFAULT expression
    ;

subquery_basic_elements :
	query_block
    | LEFT_PAREN subquery RIGHT_PAREN 
    ;

subquery_operation_part :
	(UNION ALL? | INTERSECT | MINUS) subquery_basic_elements
    ;

query_block :
	SELECT (DISTINCT | UNIQUE | ALL)? (ASTERISK | (COMMA? selected_element)+)
      into_clause? from_clause where_clause? hierarchical_query_clause? group_by_clause? model_clause?
    ;

selected_element :
	select_list_elements column_alias?
    ;

from_clause :
	FROM table_ref_list
    ;

select_list_elements :
	tableview_name '.' ASTERISK
    | (regular_id '.')? expression
    ;

table_ref_list :
	(COMMA? table_ref)+
    ;

// NOTE to PIVOT clause
// according the SQL reference this should not be possible
// according to he reality it is. Here we probably apply pivot/unpivot onto whole join clause
// eventhough it is not enclosed in parenthesis. See pivot examples 09,10,11

table_ref :
	table_ref_aux join_clause* (pivot_clause | unpivot_clause)?
    ;

table_ref_aux :
	table_ref_aux_internal flashback_query_clause* (/*{isTableAlias()}?*/ table_alias)?
    ;

table_ref_aux_internal :
	 dml_table_expression_clause (pivot_clause | unpivot_clause)?                                 # table_ref_aux_internal_one
    | LEFT_PAREN table_ref subquery_operation_part* RIGHT_PAREN (pivot_clause | unpivot_clause)?  # table_ref_aux_internal_two
    | ONLY LEFT_PAREN dml_table_expression_clause RIGHT_PAREN                                     # table_ref_aux_internal_three
    ;

join_clause :
	query_partition_clause? (CROSS | NATURAL)? (INNER | outer_join_type)? 
      JOIN table_ref_aux query_partition_clause? (join_on_part | join_using_part)*
    ;

join_on_part :
	ON condition
    ;

join_using_part :
	USING paren_column_list
    ;

outer_join_type :
	(FULL | LEFT | RIGHT) OUTER?
    ;

query_partition_clause :
	PARTITION BY (( LEFT_PAREN (subquery | expressions)? RIGHT_PAREN ) | expressions)
    ;

flashback_query_clause :
	VERSIONS BETWEEN (SCN | TIMESTAMP) expression
    | AS OF (SCN | TIMESTAMP | SNAPSHOT) expression
    ;

pivot_clause :
	PIVOT XML? LEFT_PAREN (COMMA? pivot_element)+ pivot_for_clause pivot_in_clause RIGHT_PAREN 
    ;

pivot_element :
	aggregate_function_name LEFT_PAREN expression RIGHT_PAREN column_alias?
    ;

pivot_for_clause :
	FOR (column_name | paren_column_list)
    ;

pivot_in_clause :
	IN LEFT_PAREN (subquery | (COMMA? ANY)+ | (COMMA? pivot_in_clause_element)+) RIGHT_PAREN 
    ;

pivot_in_clause_element :
	pivot_in_clause_elements column_alias?
    ;

pivot_in_clause_elements :
	expression
    | LEFT_PAREN expressions? RIGHT_PAREN 
    ;

unpivot_clause :
	UNPIVOT ((INCLUDE | EXCLUDE) NULLS)?
    LEFT_PAREN (column_name | paren_column_list) pivot_for_clause unpivot_in_clause RIGHT_PAREN 
    ;

unpivot_in_clause :
	IN LEFT_PAREN (COMMA? unpivot_in_elements)+ RIGHT_PAREN 
    ;

unpivot_in_elements :
	(column_name | paren_column_list)
      (AS (constant | LEFT_PAREN (COMMA? constant)+ RIGHT_PAREN ))?
    ;

hierarchical_query_clause :
	CONNECT BY NOCYCLE? condition start_part?
    | start_part CONNECT BY NOCYCLE? condition
    ;

start_part :
	START WITH condition
    ;

group_by_clause :
	GROUP BY (COMMA? group_by_elements)+ having_clause?
    | having_clause (GROUP BY (COMMA? group_by_elements)+)?
    ;

group_by_elements :
	grouping_sets_clause
    | rollup_cube_clause
    | expression
    ;

rollup_cube_clause :
	(ROLLUP | CUBE) LEFT_PAREN (COMMA? grouping_sets_elements)+ RIGHT_PAREN 
    ;

grouping_sets_clause :
	GROUPING SETS LEFT_PAREN (COMMA? grouping_sets_elements)+ RIGHT_PAREN 
    ;

grouping_sets_elements :
	rollup_cube_clause
    | LEFT_PAREN expressions? RIGHT_PAREN 
    | expression
    ;

having_clause :
	HAVING condition
    ;

model_clause :
	MODEL cell_reference_options* return_rows_clause? reference_model* main_model
    ;

cell_reference_options :
	(IGNORE | KEEP) NAV
    | UNIQUE (DIMENSION | SINGLE REFERENCE) 
    ;

return_rows_clause :
	RETURN (UPDATED | ALL) ROWS
    ;

reference_model :
	REFERENCE reference_model_name ON LEFT_PAREN subquery RIGHT_PAREN model_column_clauses cell_reference_options*
    ;

main_model :
	(MAIN main_model_name)? model_column_clauses cell_reference_options* model_rules_clause
    ;

model_column_clauses :
	model_column_partition_part? DIMENSION BY model_column_list MEASURES model_column_list
    ;

model_column_partition_part :
	PARTITION BY model_column_list
    ;

model_column_list :
	 LEFT_PAREN (COMMA? model_column)+RIGHT_PAREN 
    ;

model_column :
	(expression | query_block) column_alias?
    ;

model_rules_clause :
	model_rules_part? LEFT_PAREN (COMMA? model_rules_element)* RIGHT_PAREN 
    ;

model_rules_part :
	RULES (UPDATE | UPSERT ALL?)? ((AUTOMATIC | SEQUENTIAL) ORDER)? model_iterate_clause?
    ;

model_rules_element :
	(UPDATE | UPSERT ALL?)? cell_assignment order_by_clause? '=' expression
    ;

cell_assignment :
	model_expression
    ;

model_iterate_clause :
	ITERATE LEFT_PAREN expression RIGHT_PAREN until_part?
    ;

until_part :
	UNTIL LEFT_PAREN condition RIGHT_PAREN 
    ;

order_by_clause :
	ORDER SIBLINGS? BY (COMMA? order_by_elements)+
    ;

order_by_elements :
	expression (ASC | DESC)? (NULLS (FIRST | LAST))?
    ;

for_update_clause :
	FOR UPDATE for_update_of_part? for_update_options?
    ;

for_update_of_part :
	OF column_list
    ;

for_update_options :
	SKIP_ LOCKED
    | NOWAIT
    | WAIT expression
    ;

update_statement :
	UPDATE general_table_ref update_set_clause where_clause? static_returning_clause? error_logging_clause?
    ;

// Update Specific Clauses

update_set_clause :
	SET
      ((COMMA? column_based_update_set_clause)+ | VALUE LEFT_PAREN identifier RIGHT_PAREN '=' expression)
    ;

column_based_update_set_clause :
	column_name '=' expression
    | paren_column_list '=' subquery
    ;

delete_statement :
	DELETE FROM? general_table_ref where_clause? static_returning_clause? error_logging_clause?
    ;

insert_statement :
	INSERT (single_table_insert | multi_table_insert)
    ;

// Insert Specific Clauses

single_table_insert :
	insert_into_clause (values_clause static_returning_clause? | select_statement) error_logging_clause?
    ;

multi_table_insert :
	(ALL multi_table_element+ | conditional_insert_clause) select_statement
    ;

multi_table_element :
	insert_into_clause values_clause? error_logging_clause?
    ;

conditional_insert_clause :
	(ALL | FIRST)? conditional_insert_when_part+ conditional_insert_else_part?
    ;

conditional_insert_when_part :
	WHEN condition THEN multi_table_element+
    ;

conditional_insert_else_part :
	ELSE multi_table_element+
    ;

insert_into_clause :
	INTO general_table_ref paren_column_list?
    ;

values_clause :
	VALUES LEFT_PAREN expressions? RIGHT_PAREN 
    ;

merge_statement :
	MERGE INTO tableview_name table_alias? USING selected_tableview ON LEFT_PAREN condition RIGHT_PAREN 
      (merge_update_clause merge_insert_clause? | merge_insert_clause merge_update_clause?)?
      error_logging_clause?
    ;

// Merge Specific Clauses

merge_update_clause :
	WHEN MATCHED THEN UPDATE SET merge_element (COMMA merge_element)* where_clause? merge_update_delete_part?
    ;

merge_element :
	column_name '=' expression
    ;

merge_update_delete_part :
	DELETE where_clause
    ;

merge_insert_clause :
	WHEN NOT MATCHED THEN INSERT paren_column_list?
      VALUES LEFT_PAREN expressions? RIGHT_PAREN where_clause?
    ;

selected_tableview :
	(tableview_name | LEFT_PAREN select_statement RIGHT_PAREN ) table_alias?
    ;

lock_table_statement :
	LOCK TABLE lock_table_element (COMMA lock_table_element)* IN lock_mode MODE wait_nowait_part?
    ;

wait_nowait_part :
	WAIT expression
    | NOWAIT
    ;

// Lock Specific Clauses

lock_table_element :
	tableview_name partition_extension_clause?
    ;

lock_mode :
	ROW SHARE
    | ROW EXCLUSIVE
    | SHARE UPDATE?
    | SHARE ROW EXCLUSIVE
    | EXCLUSIVE
    ;

// Common DDL Clauses

general_table_ref :
	(dml_table_expression_clause | ONLY LEFT_PAREN dml_table_expression_clause RIGHT_PAREN ) table_alias?
    ;

static_returning_clause :
	(RETURNING | RETURN) expressions into_clause
    ;

error_logging_clause :
	LOG ERRORS error_logging_into_part? expression? error_logging_reject_part?
    ;

error_logging_into_part :
	INTO tableview_name
    ;

error_logging_reject_part :
	REJECT LIMIT (UNLIMITED | expression)
    ;

dml_table_expression_clause :
	table_collection_expression
    | LEFT_PAREN select_statement subquery_restriction_clause? RIGHT_PAREN 
    | tableview_name sample_clause?
    ;

table_collection_expression :
	(TABLE | THE) ( LEFT_PAREN subquery RIGHT_PAREN | LEFT_PAREN expression RIGHT_PAREN ( LEFT_PAREN PLUS_SIGN RIGHT_PAREN )?)
    ;

subquery_restriction_clause :
	WITH (READ ONLY | CHECK OPTION (CONSTRAINT constraint_name)?)
    ;

sample_clause :
	SAMPLE BLOCK? LEFT_PAREN expression (COMMA expression)? RIGHT_PAREN seed_part?
    ;

seed_part :
	SEED LEFT_PAREN expression RIGHT_PAREN 
    ;

// Expression & Condition

condition :
	expression
    ;

expressions :
	expression (COMMA expression)*
    ;

expression :
	  cursor_expression
    | logical_expression
	| VARIABLE_SESSION
    ;

cursor_expression :
	CURSOR LEFT_PAREN subquery RIGHT_PAREN 
    ;

logical_expression :
	  multiset_expression (IS NOT? (NULL | NAN | PRESENT | INFINITE | A_LETTER SET | EMPTY | OF TYPE? LEFT_PAREN ONLY? type_spec1=type_spec (COMMA type_spec)* RIGHT_PAREN ))*
    | NOT logical_expression
    | logical_expression AND logical_expression
    | logical_expression OR logical_expression
    ;

multiset_expression :
	relational_expression (multiset_type=(MEMBER | SUBMULTISET) OF? concatenation)?
    ;

relational_expression :
	  relational_expression op=relational_operator relational_expression
    | compound_expression
    ;

compound_expression :
	concatenation1=concatenation
       (
           NOT? 
           (  IN in_elements
            | BETWEEN between_elements
            | like_type=(LIKE | LIKEC | LIKE2 | LIKE4) concatenation2=concatenation (ESCAPE concatenation3=concatenation)?
           )
        )?
    ;

relational_operator :
	'='
    | (NOT_EQUAL_OP | '<' '>' | '!' '=' | '^' '=')
    | ('<' | '>') '='?
    ;

in_elements :
	 LEFT_PAREN subquery RIGHT_PAREN 
    | LEFT_PAREN concatenation (COMMA concatenation)* RIGHT_PAREN 
    | constant
    | bind_variable
    | general_element
    ;

between_elements :
	concatenation AND concatenation
    ;

concatenation :
	  model_expression (AT (LOCAL | TIME ZONE concatenation) | interval_expression)?
    | concatenation op=(ASTERISK | SOLIDUS) concatenation
    | concatenation op=(PLUS_SIGN | MINUS_SIGN) concatenation
    | concatenation BAR BAR concatenation
    ;

interval_expression :
	DAY ( LEFT_PAREN concatenation RIGHT_PAREN )? TO SECOND ( LEFT_PAREN concatenation RIGHT_PAREN )?
    | YEAR ( LEFT_PAREN concatenation RIGHT_PAREN )? TO MONTH
    ;

model_expression :
	unary_expression ('[' model_expression_element ']')?
    ;

model_expression_element :
	(ANY | expression) (COMMA (ANY | expression))*
    | single_column_for_loop (COMMA single_column_for_loop)*
    | multi_column_for_loop
    ;

single_column_for_loop :
	FOR column_name
       ( IN LEFT_PAREN expressions? RIGHT_PAREN 
       | (LIKE expression)? FROM fromExpr=expression TO toExpr=expression
         action_type=(INCREMENT | DECREMENT) action_expr=expression)
    ;

multi_column_for_loop :
	FOR paren_column_list
      IN  LEFT_PAREN (subquery | LEFT_PAREN expressions? RIGHT_PAREN ) RIGHT_PAREN 
    ;

unary_expression :
	(MINUS_SIGN | PLUS_SIGN) unary_expression
    | PRIOR unary_expression
    | CONNECT_BY_ROOT unary_expression
    | /*TODO {input.LT(1).getText().equalsIgnoreCase("new") && !input.LT(2).getText().equals(".")}?*/ NEW unary_expression
    |  DISTINCT unary_expression
    |  ALL unary_expression
    |  /*TODO{(input.LA(1) == CASE || input.LA(2) == CASE)}?*/ case_statement/*[false]*/
    |  quantified_expression
    |  standard_function
    |  atom
    ;

case_statement /*TODO [boolean isStatementParameter]
TODO scope    {
    boolean isStatement;
}
@init    {$case_statement:
	:
	isStatement = $isStatementParameter;}*/ :
	searched_case_statement
    | simple_case_statement
    ;

// CASE

simple_case_statement :
	label_name? ck1=CASE expression simple_case_when_part+  case_else_part? END CASE? label_name?
    ;

simple_case_when_part :
	WHEN expression THEN (/*TODO{$case_statement:
	:
	isStatement}?*/ seq_of_statements | expression)
    ;

searched_case_statement :
	label_name? ck1=CASE searched_case_when_part+ case_else_part? END CASE? label_name?
    ;

searched_case_when_part :
	WHEN expression THEN (/*TODO{$case_statement:
	:
	isStatement}?*/ seq_of_statements | expression)
    ;

case_else_part :
	ELSE (/*{$case_statement:
	:
	isStatement}?*/ seq_of_statements | expression)
    ;

atom :
	table_element outer_join_sign
    | bind_variable
    | constant
    | general_element
    | LEFT_PAREN subquery RIGHT_PAREN subquery_operation_part*
    | LEFT_PAREN expressions RIGHT_PAREN 
    ;

quantified_expression :
	(SOME | EXISTS | ALL | ANY) ( LEFT_PAREN subquery RIGHT_PAREN | LEFT_PAREN expression RIGHT_PAREN )
    ;

string_function :
	SUBSTR LEFT_PAREN expression COMMA expression (COMMA expression)? RIGHT_PAREN 
    | TO_CHAR LEFT_PAREN (table_element | standard_function | expression)
                  (COMMA string)? (COMMA string)? RIGHT_PAREN 
    | DECODE LEFT_PAREN expressions RIGHT_PAREN 
    | CHR LEFT_PAREN concatenation USING NCHAR_CS RIGHT_PAREN 
    | NVL LEFT_PAREN expression COMMA expression RIGHT_PAREN 
    | TRIM LEFT_PAREN ((LEADING | TRAILING | BOTH)? string? FROM)? concatenation RIGHT_PAREN 
    | TO_DATE LEFT_PAREN expression (COMMA string)? RIGHT_PAREN 
    ;

standard_function :
	string_function
    | numeric_function_wrapper
    | other_function
    ;
    
numeric_function_wrapper :
	numeric_function (single_column_for_loop | multi_column_for_loop)?
    ;

numeric_function :
	SUM LEFT_PAREN (DISTINCT | ALL)? expression RIGHT_PAREN 
   | COUNT LEFT_PAREN ( ASTERISK | ((DISTINCT | UNIQUE | ALL)? concatenation)? ) RIGHT_PAREN over_clause?
   | ROUND LEFT_PAREN expression (COMMA UNSIGNED_INTEGER)?RIGHT_PAREN 
   | AVG LEFT_PAREN (DISTINCT | ALL)? expression RIGHT_PAREN 
   | MAX LEFT_PAREN (DISTINCT | ALL)? expression RIGHT_PAREN 
   | LEAST LEFT_PAREN expressions RIGHT_PAREN 
   | GREATEST LEFT_PAREN expressions RIGHT_PAREN 
   ;

other_function :
	over_clause_keyword function_argument_analytic over_clause?
    | /*TODO stantard_function_enabling_using*/ regular_id function_argument_modeling using_clause?
    | COUNT LEFT_PAREN (ASTERISK | (DISTINCT | UNIQUE | ALL)? concatenation) RIGHT_PAREN over_clause?
    | (CAST | XMLCAST) LEFT_PAREN (MULTISET LEFT_PAREN subquery RIGHT_PAREN | concatenation) AS type_spec RIGHT_PAREN 
    | COALESCE LEFT_PAREN table_element (COMMA (numeric | string))? RIGHT_PAREN 
    | COLLECT LEFT_PAREN (DISTINCT | UNIQUE)? concatenation collect_order_by_part? RIGHT_PAREN 
    | within_or_over_clause_keyword (function_arguments keep_clause?) within_or_over_part+
    | cursor_name ( PERCENT_ISOPEN | PERCENT_FOUND | PERCENT_NOTFOUND | PERCENT_ROWCOUNT )
    | DECOMPOSE LEFT_PAREN concatenation (CANONICAL | COMPATIBILITY)? RIGHT_PAREN 
    | EXTRACT LEFT_PAREN regular_id FROM concatenation RIGHT_PAREN 
    | (FIRST_VALUE | LAST_VALUE) function_argument_analytic respect_or_ignore_nulls? over_clause
    | standard_prediction_function_keyword 
      LEFT_PAREN expressions cost_matrix_clause? using_clause? RIGHT_PAREN 
    | TRANSLATE LEFT_PAREN expression (USING (CHAR_CS | NCHAR_CS))? (COMMA expression)* RIGHT_PAREN 
    | TREAT LEFT_PAREN expression AS REF? type_spec RIGHT_PAREN 
    | TRIM LEFT_PAREN ((LEADING | TRAILING | BOTH)? string? FROM)? concatenation RIGHT_PAREN 
    | XMLAGG LEFT_PAREN expression order_by_clause? RIGHT_PAREN ('.' general_element_part)?
    | (XMLCOLATTVAL | XMLFOREST)
      LEFT_PAREN (COMMA? xml_multiuse_expression_element)+ RIGHT_PAREN ('.' general_element_part)?
    | XMLELEMENT 
      LEFT_PAREN (ENTITYESCAPING | NOENTITYESCAPING)? (NAME | EVALNAME)? expression
       (/*TODO{input.LT(2).getText().equalsIgnoreCase("xmlattributes")}?*/ COMMA xml_attributes_clause)?
       (COMMA expression column_alias?)* RIGHT_PAREN ('.' general_element_part)?
    | XMLEXISTS LEFT_PAREN expression xml_passing_clause? RIGHT_PAREN 
    | XMLPARSE LEFT_PAREN (DOCUMENT | CONTENT) concatenation WELLFORMED? RIGHT_PAREN ('.' general_element_part)?
    | XMLPI
      LEFT_PAREN (NAME identifier | EVALNAME concatenation) (COMMA concatenation)? RIGHT_PAREN ('.' general_element_part)?
    | XMLQUERY
      LEFT_PAREN concatenation xml_passing_clause? RETURNING CONTENT (NULL ON EMPTY)? RIGHT_PAREN ('.' general_element_part)?
    | XMLROOT
      LEFT_PAREN concatenation (COMMA xmlroot_param_version_part)? (COMMA xmlroot_param_standalone_part)? RIGHT_PAREN ('.' general_element_part)?
    | XMLSERIALIZE
      LEFT_PAREN (DOCUMENT | CONTENT) concatenation (AS type_spec)?
      xmlserialize_param_enconding_part? xmlserialize_param_version_part? xmlserialize_param_ident_part? ((HIDE | SHOW) DEFAULTS)? RIGHT_PAREN 
      ('.' general_element_part)?
    | XMLTABLE
      LEFT_PAREN xml_namespaces_clause? concatenation xml_passing_clause? (COLUMNS xml_table_column (COMMA xml_table_column))? RIGHT_PAREN ('.' general_element_part)?
    ;

over_clause_keyword :
	AVG
    | CORR
    | LAG
    | LEAD
    | MAX
    | MEDIAN
    | MIN
    | NTILE
    | RATIO_TO_REPORT
    | ROW_NUMBER
    | SUM
    | VARIANCE
    | REGR_
    | STDDEV
    | VAR_
    | COVAR_
    ;

within_or_over_clause_keyword :
	CUME_DIST
    | DENSE_RANK
    | LISTAGG
    | PERCENT_RANK
    | PERCENTILE_CONT
    | PERCENTILE_DISC
    | RANK
    ;
    
standard_prediction_function_keyword :
	PREDICTION
    | PREDICTION_BOUNDS
    | PREDICTION_COST
    | PREDICTION_DETAILS
    | PREDICTION_PROBABILITY
    | PREDICTION_SET
    ;
    
over_clause :
	OVER LEFT_PAREN query_partition_clause? (order_by_clause windowing_clause?)? RIGHT_PAREN 
    ;

windowing_clause :
	windowing_type
      (BETWEEN windowing_elements AND windowing_elements | windowing_elements)
    ;

windowing_type :
	ROWS
    | RANGE
    ;

windowing_elements :
	UNBOUNDED PRECEDING
    | CURRENT ROW
    | concatenation (PRECEDING | FOLLOWING)
    ;

using_clause :
	USING (ASTERISK | (COMMA? using_element)+)
    ;

using_element :
	(IN OUT? | OUT)? select_list_elements column_alias?
    ;

collect_order_by_part :
	ORDER BY concatenation
    ;

within_or_over_part :
	WITHIN GROUP LEFT_PAREN order_by_clause RIGHT_PAREN 
    | over_clause
    ;

cost_matrix_clause :
	COST (MODEL AUTO? | LEFT_PAREN (COMMA? cost_class_name)+ RIGHT_PAREN VALUES LEFT_PAREN expressions? RIGHT_PAREN )
    ;

xml_passing_clause :
	PASSING (BY VALUE)? expression column_alias? (COMMA expression column_alias?)
    ;

xml_attributes_clause :
	XMLATTRIBUTES
     LEFT_PAREN (ENTITYESCAPING | NOENTITYESCAPING)? (SCHEMACHECK | NOSCHEMACHECK)?
     (COMMA? xml_multiuse_expression_element)+ RIGHT_PAREN 
    ;

xml_namespaces_clause :
	XMLNAMESPACES
      LEFT_PAREN (concatenation column_alias)? (COMMA concatenation column_alias)*
      xml_general_default_part? RIGHT_PAREN 
    ;

xml_table_column :
	xml_column_name
      (FOR ORDINALITY | type_spec (PATH concatenation)? xml_general_default_part?)
    ;

xml_general_default_part :
	DEFAULT concatenation
    ;

xml_multiuse_expression_element :
	expression (AS (id_expression | EVALNAME concatenation))?
    ;

xmlroot_param_version_part :
	VERSION (NO VALUE | expression)
    ;

xmlroot_param_standalone_part :
	STANDALONE (YES | NO VALUE?)
    ;

xmlserialize_param_enconding_part :
	ENCODING concatenation
    ;

xmlserialize_param_version_part :
	VERSION concatenation
    ;

xmlserialize_param_ident_part :
	NO INDENT
    | INDENT (SIZE '=' concatenation)?
    ;

// SqlPlus

sql_plus_command :
	'/'
    | EXIT
    | PROMPT
    | SHOW (ERR | ERRORS)
    | START_CMD
    | sqlplus_whenever_command
    | sqlplus_set_command
    | sqlplus_execute_command
    
	// | sqlplus_accept      
	// | sqlplus_append      
	// | sqlplus_archive_log 
	// | sqlplus_attribute   
	// | sqlplus_break       
	// | sqlplus_btitle      
	// | sqlplus_change      
	// | sqlplus_clear       
	// | sqlplus_column      
	// | sqlplus_compute     
	// | sqlplus_connect     
	// | sqlplus_copy        
	// | sqlplus_define      
	// | sqlplus_del         
	// | sqlplus_describe    
	// | sqlplus_disconnect  
	// | sqlplus_edit        
	// | sqlplus_exit        
	// | sqlplus_get         
	// | sqlplus_help        
	// | sqlplus_host        
	// | sqlplus_input       
	// | sqlplus_list        
	// | sqlplus_password    
	// | sqlplus_pause       
	// | sqlplus_print       
	// | sqlplus_prompt      
	// | sqlplus_quit        
	// | sqlplus_recover     
	// | sqlplus_remark      
	// | sqlplus_repfooter   
	// | sqlplus_repheader   
	// | sqlplus_run         
	// | sqlplus_save        
	// | sqlplus_show        
	// | sqlplus_shutdown    
	// | sqlplus_spool       
	// | sqlplus_start       
	// | sqlplus_startup     
	// | sqlplus_store       
	// | sqlplus_timing      
	// | sqlplus_ttitle      
	// | sqlplus_undefine    
	// | sqlplus_variable    
    ;

sqlplus_execute_command : EXECUTE 
    //   CREATE swallow_to_semi
    // | TRUNCATE swallow_to_semi
    // | body
    // | block
    // | assignment_statement
    // | continue_statement
    // | exit_statement
    // | goto_statement
    // | if_statement
    // | loop_statement
    // | forall_statement
    // | null_statement
    // | raise_statement
    // | return_statement
    // | case_statement/*[true]*/
    // | sql_statement
     function_call
    // | pipe_row_statement
    ;

sqlplus_whenever_command :
	WHENEVER (SQLERROR | OSERROR)
         ( EXIT (SUCCESS | FAILURE | WARNING) (COMMIT | ROLLBACK)
         | CONTINUE (COMMIT | ROLLBACK | NONE))
    ;

/*
https://docs.oracle.com/cd/B10501_01/server.920/a90842/ch13.htm
sqlplus_accept : EMPTY;
sqlplus_append : EMPTY;
sqlplus_archive_log : EMPTY;
sqlplus_attribute : EMPTY;
sqlplus_break : EMPTY;
sqlplus_btitle : EMPTY;
sqlplus_change : EMPTY;
sqlplus_clear : EMPTY;
sqlplus_column : EMPTY;
sqlplus_compute : EMPTY;
sqlplus_connect : EMPTY;
sqlplus_copy : EMPTY;
sqlplus_define : EMPTY;
sqlplus_del : EMPTY;
sqlplus_describe : EMPTY;
sqlplus_disconnect : EMPTY;
sqlplus_edit : EMPTY;
sqlplus_exit : EMPTY;
sqlplus_get : EMPTY;
sqlplus_help : EMPTY;
sqlplus_host : EMPTY;
sqlplus_input : EMPTY;
sqlplus_list : EMPTY;
sqlplus_password : EMPTY;
sqlplus_pause : EMPTY;
sqlplus_print : EMPTY;
sqlplus_prompt : EMPTY;
sqlplus_quit : EMPTY;
sqlplus_recover : EMPTY;
sqlplus_remark : EMPTY
sqlplus_repfooter : EMPTY;
sqlplus_repheader : EMPTY;
sqlplus_run : EMPTY;
sqlplus_save : EMPTY;
sqlplus_show : EMPTY;
sqlplus_shutdown : EMPTY;
sqlplus_spool : EMPTY;
sqlplus_start : EMPTY;
sqlplus_startup : EMPTY;
sqlplus_store : EMPTY;
sqlplus_timing : EMPTY;
sqlplus_ttitle : EMPTY;
sqlplus_undefine : EMPTY;
sqlplus_variable : EMPTY;
 */

function_arguments : LEFT_PAREN arguments RIGHT_PAREN;
    //|  /*TODO{(input.LA(1) == CASE || input.LA(2) == CASE)}?*/ case_statement/*[false]*/

arguments :
    argument? (COMMA argument)+ 
    ;

argument :
      regular_id BIND_VAR /*EQUALS_OP GREATER_THAN_OP*/ expression
    | expression
    ;

sqlplus_set_command :
	SET regular_id (CHAR_STRING | ON | OFF | /*| EXACT_NUM_LIT*/numeric | regular_id)
    ;

// Common
partition_extension_clause :
	(SUBPARTITION | PARTITION) FOR? LEFT_PAREN expressions? RIGHT_PAREN 
    ;

column_alias :
	AS? (identifier | string)
    | AS
    ;

table_alias :
	identifier
    | string
    ;

where_clause :
	WHERE (CURRENT OF cursor_name | expression)
    ;

into_clause :
	(BULK COLLECT)? INTO (COMMA? variable_name)+
    ;

// Common Named Elements

xml_column_name :
	identifier
    | string
    ;


routine_name :
	identifiers ('@' link_name)?
    ;

indextype : 
    identifier
    ;

grantee_name :	id_expression identified_by?;

role_name :
	id_expression
    | CONNECT
    ;

constraint_name :
	identifiers ('@' link_name)?
    ;



variable_name :
	(INTRODUCER char_set_name)? id_expression ('.' id_expression)?
    | bind_variable
    ;

cursor_name :
	general_element
    | bind_variable
    ;

record_name :
	identifier
    | bind_variable
    ;

link_name :	identifier;

tableview_name :
	table_fullname ('@' link_name | /*TODO{!(input.LA(2) == BY)}?*/ partition_extension_clause)?
    ;



element_name            : id_expression;
flashback_archive_name  : id_expression;
zonemap_name            : id_expression;
subpartition_name       : id_expression;
lob_item_name           : id_expression;
dir_object_name         : id_expression;
user_object_name        : id_expression;
tablespace_name         : id_expression;
label_name              : id_expression;
partition_name          : id_expression;
schema_object_name      : id_expression;
lob_segname             : id_expression;
ilm_policy_name         : id_expression;
directory_name          : id_expression;

full_identifier             : identifier ('.' id_expression)?;
implementation_type_name    : full_identifier;
container_tableview_name    : full_identifier;
function_name               : full_identifier;
procedure_name              : full_identifier;
trigger_name                : full_identifier;
collection_name             : full_identifier;
index_name                  : full_identifier;
table_fullname              : full_identifier;

// Represents a valid DB object name in DDL commands which are valid for several DB (or schema) objects.
// For instance, create synonym ... for <DB object name>, or rename <old DB object name> to <new DB object name>.
// Both are valid for sequences, tables, views, etc.


grant_object_name :
	tableview_name
    | USER (COMMA? user_object_name)+
    | DIRECTORY dir_object_name
    | EDITION schema_object_name
    | MINING MODEL schema_object_name
    | JAVA (SOURCE | RESOURCE) schema_object_name
    | SQL TRANSLATION PROFILE schema_object_name
    ;

column_list :
	(COMMA? column_name)+
    ;

paren_column_list :
	LEFT_PAREN column_list RIGHT_PAREN
    ;

// PL/SQL Specs

// NOTE: In reality this applies to aggregate functions only
keep_clause :
	KEEP LEFT_PAREN DENSE_RANK (FIRST | LAST) order_by_clause RIGHT_PAREN over_clause?
    ;

function_argument_analytic :
	 LEFT_PAREN (COMMA? argument respect_or_ignore_nulls?)* RIGHT_PAREN keep_clause?
    ;

function_argument_modeling :
	 LEFT_PAREN column_name (COMMA (numeric | NULL) (COMMA (numeric | NULL))?)?
      USING (tableview_name '.' ASTERISK | ASTERISK | (COMMA? expression column_alias?)+)
 RIGHT_PAREN keep_clause?
    ;

respect_or_ignore_nulls :
	(RESPECT | IGNORE) NULLS
    ;

type_spec :
	  datatype
    | REF? type_name (PERCENT_ROWTYPE | PERCENT_TYPE)?
    ;

datatype :
	native_datatype_element precision_part? (WITH LOCAL? TIME ZONE | CHARACTER SET char_set_name)?
    | INTERVAL (YEAR | DAY) ( LEFT_PAREN expression RIGHT_PAREN )? TO (MONTH | SECOND) ( LEFT_PAREN expression RIGHT_PAREN )?
    ;

precision_part :
	 LEFT_PAREN numeric (COMMA numeric)? (CHAR | BYTE)? RIGHT_PAREN 
    ;

native_datatype_element :
	BINARY_INTEGER
    | PLS_INTEGER
    | NATURAL
    | BINARY_FLOAT
    | BINARY_DOUBLE
    | NATURALN
    | POSITIVE
    | POSITIVEN
    | SIGNTYPE
    | SIMPLE_INTEGER
    | NVARCHAR2
    | DEC
    | INTEGER
    | INT
    | NUMERIC
    | SMALLINT
    | NUMBER
    | DECIMAL 
    | DOUBLE PRECISION?
    | FLOAT
    | REAL
    | NCHAR
    | LONG RAW?
    | CHAR  
    | CHARACTER 
    | VARCHAR2
    | VARCHAR
    | STRING
    | RAW
    | BOOLEAN
    | DATE
    | ROWID
    | UROWID
    | YEAR
    | MONTH
    | DAY
    | HOUR
    | MINUTE
    | SECOND
    | TIMEZONE_HOUR
    | TIMEZONE_MINUTE
    | TIMEZONE_REGION
    | TIMEZONE_ABBR
    | TIMESTAMP
    | TIMESTAMP_UNCONSTRAINED
    | TIMESTAMP_TZ_UNCONSTRAINED
    | TIMESTAMP_LTZ_UNCONSTRAINED
    | YMINTERVAL_UNCONSTRAINED
    | DSINTERVAL_UNCONSTRAINED
    | BFILE
    | BLOB
    | CLOB
    | NCLOB
    | MLSLABEL
    ;

bind_variable :
	(BINDVAR | ':' UNSIGNED_INTEGER)
      // Pro*C/C++ indicator variables
      (INDICATOR? (BINDVAR | ':' UNSIGNED_INTEGER))?
      ('.' general_element_part)*
    ;

general_element :
	general_element_part ('.' general_element_part)*
    ;

general_element_part :
	(INTRODUCER char_set_name)? id_expressions ('@' link_name)? function_arguments? keep_clause?
    ;

table_element :
	(INTRODUCER char_set_name)? id_expressions
    ;

object_privilege :
	ALL PRIVILEGES?
    | ALTER
    | DEBUG
    | DELETE
    | EXECUTE
    | FLASHBACK
    | FLASHBACK ARCHIVE
    | INDEX
    | INHERIT PRIVILEGES
    | INSERT
    | KEEP SEQUENCE
    | MERGE VIEW
    | ON COMMIT REFRESH
    | QUERY REWRITE
    | READ
    | REFERENCES
    | SELECT
    | TRANSLATE SQL
    | UNDER
    | UPDATE
    | USE
    | WRITE
    ;

//Ordered by type rather than alphabetically
system_privilege :
	ALL PRIVILEGES
    | ADVISOR
    | ADMINISTER ANY? SQL TUNING SET
    | (ALTER | CREATE | DROP) ANY SQL PROFILE
    | ADMINISTER SQL MANAGEMENT OBJECT
    | CREATE ANY? CLUSTER
    | (ALTER | DROP) ANY CLUSTER
    | (CREATE | DROP) ANY CONTEXT
    | EXEMPT REDACTION POLICY
    | ALTER DATABASE
    | (ALTER | CREATE) PUBLIC? DATABASE LINK
    | DROP PUBLIC DATABASE LINK
    | DEBUG CONNECT SESSION
    | DEBUG ANY PROCEDURE
    | ANALYZE ANY DICTIONARY
    | CREATE ANY? DIMENSION
    | (ALTER | DROP) ANY DIMENSION
    | (CREATE | DROP) ANY DIRECTORY
    | (CREATE | DROP) ANY EDITION
    | FLASHBACK (ARCHIVE ADMINISTER | ANY TABLE)
    | (ALTER | CREATE | DROP) ANY INDEX
    | CREATE ANY? INDEXTYPE
    | (ALTER | DROP | EXECUTE) ANY INDEXTYPE
    | CREATE (ANY | EXTERNAL)? JOB
    | EXECUTE ANY (CLASS | PROGRAM)
    | MANAGE SCHEDULER
    | ADMINISTER KEY MANAGEMENT
    | CREATE ANY? LIBRARY
    | (ALTER | DROP | EXECUTE) ANY LIBRARY
    | LOGMINING
    | CREATE ANY? MATERIALIZED VIEW
    | (ALTER | DROP) ANY MATERIALIZED VIEW
    | GLOBAL? QUERY REWRITE
    | ON COMMIT REFRESH
    | CREATE ANY? MINING MODEL
    | (ALTER | DROP | SELECT | COMMENT) ANY MINING MODEL
    | CREATE ANY? CUBE
    | (ALTER | DROP | SELECT | UPDATE) ANY CUBE
    | CREATE ANY? MEASURE FOLDER
    | (DELETE | DROP | INSERT) ANY MEASURE FOLDER
    | CREATE ANY? CUBE DIMENSION
    | (ALTER | DELETE | DROP | INSERT | SELECT | UPDATE) ANY CUBE DIMENSION
    | CREATE ANY? CUBE BUILD PROCESS
    | (DROP | UPDATE) ANY CUBE BUILD PROCESS
    | CREATE ANY? OPERATOR
    | (ALTER | DROP | EXECUTE) ANY OPERATOR
    | (CREATE | ALTER | DROP) ANY OUTLINE
    | CREATE PLUGGABLE DATABASE
    | SET CONTAINER
    | CREATE ANY? PROCEDURE
    | (ALTER | DROP | EXECUTE) ANY PROCEDURE
    | (CREATE | ALTER | DROP ) PROFILE
    | CREATE ROLE
    | (ALTER | DROP | GRANT) ANY ROLE
    | (CREATE | ALTER | DROP) ROLLBACK SEGMENT
    | CREATE ANY? SEQUENCE
    | (ALTER | DROP | SELECT) ANY SEQUENCE
    | (ALTER | CREATE | RESTRICTED) SESSION
    | ALTER RESOURCE COST
    | CREATE ANY? SQL TRANSLATION PROFILE
    | (ALTER | DROP | USE) ANY SQL TRANSLATION PROFILE
    | TRANSLATE ANY SQL
    | CREATE ANY? SYNONYM
    | DROP ANY SYNONYM
    | (CREATE | DROP) PUBLIC SYNONYM
    | CREATE ANY? TABLE
    | (ALTER | BACKUP | COMMENT | DELETE | DROP | INSERT | LOCK | READ | SELECT | UPDATE) ANY TABLE
    | (CREATE | ALTER | DROP | MANAGE | UNLIMITED) TABLESPACE
    | CREATE ANY? TRIGGER
    | (ALTER | DROP) ANY TRIGGER
    | ADMINISTER DATABASE TRIGGER
    | CREATE ANY? TYPE
    | (ALTER | DROP | EXECUTE | UNDER) ANY TYPE
    | (CREATE | ALTER | DROP) USER
    | CREATE ANY? VIEW
    | (DROP | UNDER | MERGE) ANY VIEW
    | (ANALYZE | AUDIT) ANY
    | BECOME USER
    | CHANGE NOTIFICATION
    | EXEMPT ACCESS POLICY
    | FORCE ANY? TRANSACTION
    | GRANT ANY OBJECT? PRIVILEGE
    | INHERIT ANY PRIVILEGES
    | KEEP DATE TIME
    | KEEP SYSGUID
    | PURGE DBA_RECYCLEBIN
    | RESUMABLE
    | SELECT ANY (DICTIONARY | TRANSACTION)
    | SYSBACKUP
    | SYSDBA
    | SYSDG
    | SYSKM
    | SYSOPER
    ;

// $>

// $<Lexer Mappings

literal : 
	  constant
	| function_call
	;

literal_datetime : 
      DATE string
	| function_call
	;

constant :
	TIMESTAMP (string | bind_variable) (AT TIME ZONE string)?
    | INTERVAL (string | bind_variable | general_element_part) (YEAR | MONTH | DAY | HOUR | MINUTE | SECOND)
      ( LEFT_PAREN (UNSIGNED_INTEGER | bind_variable) (COMMA (UNSIGNED_INTEGER | bind_variable) )? RIGHT_PAREN )?
      (TO ( DAY | HOUR | MINUTE | SECOND ( LEFT_PAREN (UNSIGNED_INTEGER | bind_variable) RIGHT_PAREN )?))?
    | numeric
    | DATE string
    | string
    | NULL
    | TRUE
    | FALSE
    | DBTIMEZONE 
    | SESSIONTIMEZONE
    | MINVALUE
    | MAXVALUE
    | DEFAULT
    ;


identifier              : (INTRODUCER char_set_name)? id_expression;
synonym_name            : identifier;
package_name            : identifier;
parameter_name          : identifier;
reference_model_name    : identifier;
cost_class_name         : identifier;
attribute_name          : identifier;
savepoint_name          : identifier;
rollback_segment_name   : identifier;
table_var_name          : identifier;
schema_name             : identifier;
main_model_name         : identifier;
query_name              : identifier;
log_group_name          : identifier;

identifiers             : identifier ('.' id_expression)*;
aggregate_function_name : identifiers;
exception_name          : identifiers;
column_name             : identifiers;


id_expression : regular_id | DELIMITED_ID;

id_expressions  : id_expression ('.' id_expression)*;
type_name       : id_expressions;
sequence_name   : id_expressions;
char_set_name   : id_expressions;

outer_join_sign : LEFT_PAREN PLUS_SIGN RIGHT_PAREN ;
    
regular_id :
	  REGULAR_ID
//     | reserved_word;

// reserved_word :
    | A_LETTER
    | ADD
    | AFTER
    | AGENT
    | AGGREGATE
    //| ALL
    //| ALTER
    | ANALYZE
    //| AND
    //| ANY
    | ARRAY
    // | AS
    //| ASC
    | ASSOCIATE
    | AT
    | ATTRIBUTE
    | AUDIT
    | AUTHID
    | AUTO
    | AUTOMATIC
    | AUTONOMOUS_TRANSACTION
    | BATCH
    | BEFORE
    //| BEGIN
    // | BETWEEN
    | BFILE
    | BINARY_DOUBLE
    | BINARY_FLOAT
    | BINARY_INTEGER
    | BLOB
    | BLOCK
    | BODY
    | BOOLEAN
    | BOTH
    // | BREADTH
    | BUILD
    | BULK
    // | BY
    | BYTE
    | C_LETTER
    // | CACHE
    | CALL
    | CANONICAL
    | CASCADE
    //| CASE
    | CAST
    | CHAR
    | CHAR_CS
    | CHARACTER
    //| CHECK
    | CHR
    | CLOB
    | CLOSE
    | CLUSTER
    | COLLECT
    | COLUMNS
    | COMMENT
    | COMMIT
    | COMMITTED
    | COMPATIBILITY
    | COMPILE
    | COMPOUND
    //| CONNECT
    //| CONNECT_BY_ROOT
    | CONSTANT
    | CONSTRAINT
    | CONSTRAINTS
    | CONSTRUCTOR
    | CONTENT
    | CONTEXT
    | CONTINUE
    | CONVERT
    | CORRUPT_XID
    | CORRUPT_XID_ALL
    | COST
    | COUNT
    //| CREATE
    | CROSS
    | CUBE
    //| CURRENT
    | CURRENT_USER
    | CURSOR
    | CUSTOMDATUM
    | CYCLE
    | DATA
    | DATABASE
    //| DATE
    | DAY
    | DB_ROLE_CHANGE
    | DBTIMEZONE
    | DDL
    | DEBUG
    | DEC
    | DECIMAL
    //| DECLARE
    | DECOMPOSE
    | DECREMENT
    //| DEFAULT
    | DEFAULTS
    | DEFERRED
    | DEFINER
    | DELETE
    // | DEPTH
    //| DESC
    | DETERMINISTIC
    | DIMENSION
    | DISABLE
    | DISASSOCIATE
    //| DISTINCT
    | DOCUMENT
    | DOUBLE
    //| DROP
    | DSINTERVAL_UNCONSTRAINED
    | EACH
    | ELEMENT
    //| ELSE
    //| ELSIF
    | EMPTY
    | ENABLE
    | ENCODING
    //| END
    | ENTITYESCAPING
    | ERR
    | ERRORS
    | ESCAPE
    | EVALNAME
    | EXCEPTION
    | EXCEPTION_INIT
    | EXCEPTIONS
    | EXCLUDE
    //| EXCLUSIVE
    | EXECUTE
    //| EXISTS
    | EXIT
    | EXPLAIN
    | EXTERNAL
    | EXTRACT
    | FAILURE
    //| FALSE
    //| FETCH
    | FINAL
    | FIRST
    | FIRST_VALUE
    | FLOAT
    | FOLLOWING
    | FOLLOWS
    //| FOR
    | FORALL
    | FORCE
    // | FROM
    | FULL
    | FUNCTION
    //| GOTO
    //| GRANT
    //| GROUP
    | GROUPING
    | HASH
    //| HAVING
    | HIDE
    | HOUR
    | ID
    //| IF
    | IGNORE
    | IMMEDIATE
    // | IN
    | INCLUDE
    | INCLUDING
    | INCREMENT
    | INDENT
    //| INDEX
    | INDEXED
    | INDICATOR
    | INDICES
    | INFINITE
    | INLINE
    | INNER
    | INOUT
    //| INSERT
    | INSTANTIABLE
    | INSTEAD
    | INT
    | INTEGER
    //| INTERSECT
    | INTERVAL
    // | INTO
    | INVALIDATE
    //| IS
    | ISOLATION
    | ITERATE
    | JAVA
    | JOIN
    | KEEP
    | KEY
    | LANGUAGE
    | LAST
    | LAST_VALUE
    | LEADING
    | LEFT
    | LEVEL
    | LIBRARY
    // | LIKE
    | LIKE2
    | LIKE4
    | LIKEC
    | LIMIT
    | LINK
    | LOCAL
    //| LOCK
    | LOCKED
    | LOG
    | LOGOFF
    | LOGON
    | LONG
    | LOOP
    | MAIN
    | MAP
    | MATCHED
    | MAXVALUE
    | MEASURES
    | MEMBER
    | MERGE
    //| MINUS
    | MINIMUM
    | MINUTE
    | MINVALUE
    | MLSLABEL
    //| MODE
    | MODEL
    | MODIFY
    | MONTH
    | MULTISET
    | NAME
    | NAN
    | NATURAL
    | NATURALN
    | NAV
    | NCHAR
    | NCHAR_CS
    | NCLOB
    | NESTED
    | NEW
    | NO
    | NOAUDIT
    // | NOCACHE
    | NOCOPY
    | NOCYCLE
    | NOENTITYESCAPING
    //| NOMAXVALUE
    //| NOMINVALUE
    | NONE
    // | NOORDER
    | NOSCHEMACHECK
    //| NOT
    //| NOWAIT
    // | NULL
    | NULLS
    | NUMBER
    | NUMERIC
    | NVARCHAR2
    | OBJECT
    //| OF
    | OFF
    | OID
    | OLD
    //| ON
    | ONLY
    | OPEN
    | OPERATOR
    //| OPTION
    //| OR
    | ORADATA
    //| ORDER
    | ORDINALITY
    | OSERROR
    | OUT
    | OUTER
    | OVER
    | OVERRIDING
    | PACKAGE
    | PARALLEL_ENABLE
    | PARAMETERS
    | PARENT
    | PARTITION
    | PASSING
    | PATH
    //| PERCENT_ROWTYPE
    //| PERCENT_TYPE
    | PIPELINED
    //| PIVOT
    | PLAN
    | PLS_INTEGER
    | POSITIVE
    | POSITIVEN
    | PRAGMA
    | PRECEDING
    | PRECISION
    | PRESENT
    //| PRIOR
    | PRIVILEGE
    //| PROCEDURE
    | PROGRAM
    | RAISE
    | RANGE
    | RAW
    | READ
    | REAL
    | RECORD
    | REF
    | REFERENCE
    | REFERENCING
    | REJECT
    | RELIES_ON
    | RENAME
    | REPLACE
    | RESPECT
    | RESTRICT_REFERENCES
    | RESULT
    | RESULT_CACHE
    | RETURN
    | RETURNING
    | REUSE
    | REVERSE
    | REVOKE
    | RIGHT
    | ROLLBACK
    | ROLLUP
    | ROW
    | ROWID
    | ROWS
    | RULES
    | SAMPLE
    | SAVE
    | SAVEPOINT
    | SCHEMA
    | SCHEMACHECK
    | SCN
    // | SEARCH
    | SECOND
    | SEED
    | SEGMENT
    // | SELECT
    | SELF
    // | SEQUENCE
    | SEQUENTIAL
    | SERIALIZABLE
    | SERIALLY_REUSABLE
    | SERVERERROR
    | SESSIONTIMEZONE
    | SET
    | SETS
    | SETTINGS
    //| SHARE
    | SHOW
    | SHUTDOWN
    | SIBLINGS
    | SIGNTYPE
    | SIMPLE_INTEGER
    | SINGLE
    //| SIZE
    | SKIP_
    | SMALLINT
    | SNAPSHOT
    | SOME
    | SPECIFICATION
    | SQLDATA
    | SQLERROR
    | STANDALONE
    //| START
    | STARTUP
    | STATEMENT
    | STATEMENT_ID
    | STATIC
    | STATISTICS
    | STRING
    | STORE
    | SUBSTR
    | SUBMULTISET
    | SUBPARTITION
    | SUBSTITUTABLE
    | SUBTYPE
    | SUCCESS
    | SUSPEND
    | SYSDATE
    | TEMPORARY
    //| TABLE
    //| THE
    //| THEN
    | TIME
    | TIMESTAMP
    | TIMESTAMP_LTZ_UNCONSTRAINED
    | TIMESTAMP_TZ_UNCONSTRAINED
    | TIMESTAMP_UNCONSTRAINED
    | TIMEZONE_ABBR
    | TIMEZONE_HOUR
    | TIMEZONE_MINUTE
    | TIMEZONE_REGION
    //| TO
    | TRAILING
    | TRANSACTION
    | TRANSLATE
    | TREAT
    | TRIGGER
    | TRIM
    //| TRUE
    | TRUNCATE
    | TYPE
    | UNBOUNDED
    | UNDER
    //| UNION
    //| UNIQUE
    | UNLIMITED
    //| UNPIVOT
    | UNTIL
    //| UPDATE
    | UPDATED
    | UPSERT
    | UROWID
    | USE
    | USER
    | USERS
    //| USING
    | VALIDATE
    | VALUE
    //| VALUES
    | VARCHAR
    | VARCHAR2
    | VARIABLE
    | VARRAY
    | VARYING
    | VERSION
    | VERSIONS
    | WAIT
    | WARNING
    | WELLFORMED
    // | WHEN
    | WHENEVER
    // | WHERE
    | WHILE
    //| WITH
    | WITHIN
    | WORK
    | WRITE
    | XML
    | XMLAGG
    | XMLATTRIBUTES
    | XMLCAST
    | XMLCOLATTVAL
    | XMLELEMENT
    | XMLEXISTS
    | XMLFOREST
    | XMLNAMESPACES
    | XMLPARSE
    | XMLPI
    | XMLQUERY
    | XMLROOT
    | XMLSERIALIZE
    | XMLTABLE
    | YEAR
    | YES
    | YMINTERVAL_UNCONSTRAINED
    | ZONE
    | PREDICTION
    | PREDICTION_BOUNDS
    | PREDICTION_COST
    | PREDICTION_DETAILS
    | PREDICTION_PROBABILITY
    | PREDICTION_SET
    | CUME_DIST
    | DENSE_RANK
    | LISTAGG
    | PERCENT_RANK
    | PERCENTILE_CONT
    | PERCENTILE_DISC
    | RANK
    | AVG
    | CORR
    | LAG
    | LEAD
    | MAX
    | MEDIAN
    | MIN
    | NTILE
    | RATIO_TO_REPORT
    | ROW_NUMBER
    | SUM
    | VARIANCE
    | REGR_
    | STDDEV
    | VAR_
    | COVAR_    
    ;

string_function_name :
	  CHR
    | DECODE
    | SUBSTR
    | TO_CHAR
    | TRIM
    ;

numeric_function_name :
	  AVG
    | COUNT
    | NVL
    | ROUND
    | SUM
    ;

integer :
	  numeric
	| numeric_negative
	;

numeric :
	  PLUS_SIGN? UNSIGNED_INTEGER
    | APPROXIMATE_NUM_LIT
    ;

numeric_negative :
	MINUS_SIGN UNSIGNED_INTEGER
    ;

string :
	CHAR_STRING
    //| CHAR_STRING_PERL
    | NATIONAL_CHAR_STRING_LIT
    ;




