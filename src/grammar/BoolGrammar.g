options
{
	language = "CSharp";
}

/** Gramatika za sintaksu Bool programskoh jezika koristenogu Bebop-u prema
definiciji
 danoj u http://research.microsoft.com/slam/papers/bebop.pdf , strana 5*/


class BoolParser extends Parser;
options
{
      k=2;
	buildAST=true;
}
tokens
{
	PROC;
	SSEQ;
	STMT;
	LSTMT;
	ASSIGNMENT;
	DECIDER;
	PROCCALL;
	EXPR;
}

prog 		:	(decl)* (proc)*
		;

decl 		:	"decl"^ (ID (COMMA! ID)*) SEMI! 
		;

proc 		:	ID^ LPAREN! (ID (COMMA! ID)*)? RPAREN! "begin"! (decl)* sseq "end"!
				{#proc = #([PROC,"PROC"],#proc);}
		;

sseq 		:	(lstmt)+
				{#sseq = #([SSEQ,"SSEQ"],#sseq);}
		;

lstmt 	:	stmt
				{#lstmt = #([STMT,"STMT"],#lstmt);}
		|	ID COLON! stmt
				{#lstmt = #([LSTMT,"LSTMT"],#lstmt);}
		;

stmt 		:	"skip" SEMI!
		|	"print"^ LPAREN! (expr (COMMA! expr)*) RPAREN! SEMI!
		|	"goto"^ ID SEMI!
		|	"return"^ SEMI!
		|	(ID (COMMA! ID)*) ASSIGN (expr (COMMA! expr)*) SEMI!
				{#stmt = #([ASSIGNMENT,"ASSIGNMENT"],#stmt);}
		|	"if"^ LPAREN! decider RPAREN! "then"! sseq "else"! sseq "fi"!
		|	"while"^ LPAREN! decider RPAREN! "do"! sseq "od"!
		|	"assert"^ LPAREN! decider RPAREN! SEMI!
		|	ID^ LPAREN! (expr (COMMA! expr)*)? RPAREN! SEMI!
				{#stmt = #([PROCCALL,"PROCCALL"],#stmt);}
		;

decider 	:	(QMARK	|	expr)
				{#decider = #([DECIDER,"DECIDER"],#decider);}
		;

expr 		:	(EMARK expr | LPAREN! expr RPAREN! | ID | CONST) (options{greedy=true;}:BINOP expr)?
				{#expr = #([EXPR,"EXPR"],#expr);}
		;


class BoolLexer extends Lexer;

options {
    k=2;	
}

ASSIGN	:	":=";
COMMA		:	',';
SEMI		:	';';
COLON		:	':';
LPAREN	:	'(';
RPAREN	:	')';
QMARK		:	'?';
EMARK		:	'!' ('=' {$setType(BINOP);})?;

ID 		:	('a'..'z' | 'A'..'Z' | '_')('a'..'z' | 'A'..'Z' | '0'..'9' | '_')* | ('{' ~('{' | '}') '}'); 
	
BINOP 	:	'|' | '&' | '^' | '=' | "=>";/** I "!=" je BINOP ali je faktoriziran u EMARK pravilo*/

CONST 	:	'0' | '1';

WS		:	(' '
		|	'\t'
		|	'\n'{ newline(); } 
		|	'\r')
		{ _ttype = Token.SKIP; }
		;
