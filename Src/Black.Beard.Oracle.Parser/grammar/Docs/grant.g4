

### include 'common.bsn'
  
<grant> ::= 'GRANT' ( <grant_system_privileges> | <grant_object_privileges> )

<grant_system_privileges> ::= <system_privilege_role_all_privilege> ( ',' <system_privilege_role_all_privilege> )* 
                              'TO' 
                              <grantee_clause> 
                              ( 'WITH' 'ADMIN' 'OPTION' )?

<grant_object_privileges> ::= <grant_object_privilege> ( ',' <grant_object_privilege> )*
                              <on_object_clause> 
                              'TO' <grantee_clause> 
                              ( 'WITH' 'HIERARCHY' 'OPTION' )? 
                              ( 'WITH' 'GRANT' 'OPTION' )?

<grant_object_privilege> ::= <object_privilege> ( '(' <column_list> ')' )?

<column_list> ::= <column> ( ',' <column> )*

<on_object_clause> ::= 'ON' ( <object_type_fullname> 
                            | 'DIRECTORY' <directory> 
                            | 'EDITION' <edition> 
                            | 'MINING' 'MODEL' <mining_model_name_fullname> 
                            | 'JAVA' ( 'SOURCE' | 'RESOURCE' ) <object_type_fullname> 
                            ) 
  
<grantee_clause> ::= ( <grantee_item_clause> ) ( ',' <grantee_item_clause> )*

<grantee_item_clause> ::= ( <user> ( 'IDENTIFIED' 'BY' <password> )? | <role> | 'PUBLIC' )

<system_privilege_role_all_privilege> ::= 'ALL' 'PRIVILEGES' | 'GRANT' 'ANY' 'OBJECT' 'PRIVILEGE' | 'GRANT' 'ANY' 'PRIVILEGE' | 'RESUMABLE' | 'SELECT' 'ANY' 'DICTIONARY' | 'SELECT' 'ANY' 'TRANSACTION' | 'SYSDBA' | 'SYSOPER' | <identifier>
<object_privilege> ::= ( 'SELECT' | 'DEBUG' | 'MERGE' 'VIEW' | 'UNDER' | 'INDEX' | 'ALTER' | 'INSERT' | 'DELETE' | 'UPDATE' | 'REFERENCES' | 'EXECUTE' | 'ON' 'COMMIT' 'REFRESH' | 'QUERY' 'REWRITE' | 'USE' | 'FLASHBACK' 'ARCHIVE' ) | 'ALL' ( 'PRIVILEGES' )?