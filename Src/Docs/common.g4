

<digit> ::= ('0'..'9')

<simple_letter> ::= 'a'..'z' | 'A'..'Z'

<int> 
	::= <digit>+

<float>
    ::= ('0'..'9')+ '.' ('0'..'9')* EXPONENT?
    |   '.' ('0'..'9')+ EXPONENT?
    |   ('0'..'9')+ EXPONENT

<comment>
    ::= '//' ~('\n'|'\r')* '\r'? '\n'
    |   '/*' '*/'

<ws>  
	::= ( ' '
        | '\t'
        | '\r'
        | '\n'
        )

<string>
	::= '\'' ( ESC_SEQ | ~('\\'|'\'') )* '\''

fragment
<exponent> 
	::= ('e'|'E') ('+'|'-')? ('0'..'9')+ 

fragment
<hex_digit> 
	::= ('0'..'9'|'a'..'f'|'A'..'F') 

fragment
<esc_seq>
    ::= '\\' ('b'|'t'|'n'|'f'|'r'|'\"'|'\''|'\\')
    |   UNICODE_ESC
    |   OCTAL_ESC

fragment
<octal_esc>
    ::= '\\' ('0'..'3') ('0'..'7') ('0'..'7')
    |   '\\' ('0'..'7') ('0'..'7')
    |   '\\' ('0'..'7')

fragment
<unicode_esc>
    ::= '\\' 'u' HEX_DIGIT HEX_DIGIT HEX_DIGIT HEX_DIGIT

<identifier> 
	::= <simple_letter> (<simple_letter> | '$' | '_' | '&#136;' | '0'..'9')*

<column> ::= <identifier>
<user> ::= <identifier>
<edition> ::= <identifier>
<role> ::= <identifier>
<schema_name> ::= <identifier>
<directory> ::= <identifier>

<password> ::= <string>
  
<identifier_fullname>         ::= ( <schema_name> '.' )? <identifier>
<mining_model_name_fullname>  ::= <identifier_fullname>
<object_type_fullname>        ::= <identifier_fullname>
