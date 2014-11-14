// $ANTLR 2.7.7 (20060930): "BoolGrammar.g" -> "BoolParser.cs"$

	// Generate the header common to all output files.
	using System;
	
	using TokenBuffer              = antlr.TokenBuffer;
	using TokenStreamException     = antlr.TokenStreamException;
	using TokenStreamIOException   = antlr.TokenStreamIOException;
	using ANTLRException           = antlr.ANTLRException;
	using LLkParser = antlr.LLkParser;
	using Token                    = antlr.Token;
	using IToken                   = antlr.IToken;
	using TokenStream              = antlr.TokenStream;
	using RecognitionException     = antlr.RecognitionException;
	using NoViableAltException     = antlr.NoViableAltException;
	using MismatchedTokenException = antlr.MismatchedTokenException;
	using SemanticException        = antlr.SemanticException;
	using ParserSharedInputState   = antlr.ParserSharedInputState;
	using BitSet                   = antlr.collections.impl.BitSet;
	using AST                      = antlr.collections.AST;
	using ASTPair                  = antlr.ASTPair;
	using ASTFactory               = antlr.ASTFactory;
	using ASTArray                 = antlr.collections.impl.ASTArray;
	
/** Gramatika za sintaksu Bool programskoh jezika koristenogu Bebop-u prema
definiciji
 danoj u http://research.microsoft.com/slam/papers/bebop.pdf , strana 5*/
	public 	class BoolParser : antlr.LLkParser
	{
		public const int EOF = 1;
		public const int NULL_TREE_LOOKAHEAD = 3;
		public const int PROC = 4;
		public const int SSEQ = 5;
		public const int STMT = 6;
		public const int LSTMT = 7;
		public const int ASSIGNMENT = 8;
		public const int DECIDER = 9;
		public const int PROCCALL = 10;
		public const int EXPR = 11;
		public const int LITERAL_decl = 12;
		public const int ID = 13;
		public const int COMMA = 14;
		public const int SEMI = 15;
		public const int LPAREN = 16;
		public const int RPAREN = 17;
		public const int LITERAL_begin = 18;
		public const int LITERAL_end = 19;
		public const int COLON = 20;
		public const int LITERAL_skip = 21;
		public const int LITERAL_print = 22;
		public const int LITERAL_goto = 23;
		public const int LITERAL_return = 24;
		public const int ASSIGN = 25;
		public const int LITERAL_if = 26;
		public const int LITERAL_then = 27;
		public const int LITERAL_else = 28;
		public const int LITERAL_fi = 29;
		public const int LITERAL_while = 30;
		public const int LITERAL_do = 31;
		public const int LITERAL_od = 32;
		public const int LITERAL_assert = 33;
		public const int QMARK = 34;
		public const int EMARK = 35;
		public const int CONST = 36;
		public const int BINOP = 37;
		public const int WS = 38;
		
		
		protected void initialize()
		{
			tokenNames = tokenNames_;
			initializeFactory();
		}
		
		
		protected BoolParser(TokenBuffer tokenBuf, int k) : base(tokenBuf, k)
		{
			initialize();
		}
		
		public BoolParser(TokenBuffer tokenBuf) : this(tokenBuf,2)
		{
		}
		
		protected BoolParser(TokenStream lexer, int k) : base(lexer,k)
		{
			initialize();
		}
		
		public BoolParser(TokenStream lexer) : this(lexer,2)
		{
		}
		
		public BoolParser(ParserSharedInputState state) : base(state,2)
		{
			initialize();
		}
		
	public void prog() //throws RecognitionException, TokenStreamException
{
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		AST prog_AST = null;
		
		try {      // for error handling
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==LITERAL_decl))
					{
						decl();
						astFactory.addASTChild(ref currentAST, returnAST);
					}
					else
					{
						goto _loop3_breakloop;
					}
					
				}
_loop3_breakloop:				;
			}    // ( ... )*
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==ID))
					{
						proc();
						astFactory.addASTChild(ref currentAST, returnAST);
					}
					else
					{
						goto _loop5_breakloop;
					}
					
				}
_loop5_breakloop:				;
			}    // ( ... )*
			prog_AST = currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_0_);
		}
		returnAST = prog_AST;
	}
	
	public void decl() //throws RecognitionException, TokenStreamException
{
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		AST decl_AST = null;
		
		try {      // for error handling
			AST tmp1_AST = null;
			tmp1_AST = astFactory.create(LT(1));
			astFactory.makeASTRoot(ref currentAST, tmp1_AST);
			match(LITERAL_decl);
			{
				AST tmp2_AST = null;
				tmp2_AST = astFactory.create(LT(1));
				astFactory.addASTChild(ref currentAST, tmp2_AST);
				match(ID);
				{    // ( ... )*
					for (;;)
					{
						if ((LA(1)==COMMA))
						{
							match(COMMA);
							AST tmp4_AST = null;
							tmp4_AST = astFactory.create(LT(1));
							astFactory.addASTChild(ref currentAST, tmp4_AST);
							match(ID);
						}
						else
						{
							goto _loop9_breakloop;
						}
						
					}
_loop9_breakloop:					;
				}    // ( ... )*
			}
			match(SEMI);
			decl_AST = currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_1_);
		}
		returnAST = decl_AST;
	}
	
	public void proc() //throws RecognitionException, TokenStreamException
{
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		AST proc_AST = null;
		
		try {      // for error handling
			AST tmp6_AST = null;
			tmp6_AST = astFactory.create(LT(1));
			astFactory.makeASTRoot(ref currentAST, tmp6_AST);
			match(ID);
			match(LPAREN);
			{
				switch ( LA(1) )
				{
				case ID:
				{
					AST tmp8_AST = null;
					tmp8_AST = astFactory.create(LT(1));
					astFactory.addASTChild(ref currentAST, tmp8_AST);
					match(ID);
					{    // ( ... )*
						for (;;)
						{
							if ((LA(1)==COMMA))
							{
								match(COMMA);
								AST tmp10_AST = null;
								tmp10_AST = astFactory.create(LT(1));
								astFactory.addASTChild(ref currentAST, tmp10_AST);
								match(ID);
							}
							else
							{
								goto _loop13_breakloop;
							}
							
						}
_loop13_breakloop:						;
					}    // ( ... )*
					break;
				}
				case RPAREN:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(RPAREN);
			match(LITERAL_begin);
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==LITERAL_decl))
					{
						decl();
						astFactory.addASTChild(ref currentAST, returnAST);
					}
					else
					{
						goto _loop15_breakloop;
					}
					
				}
_loop15_breakloop:				;
			}    // ( ... )*
			sseq();
			astFactory.addASTChild(ref currentAST, returnAST);
			match(LITERAL_end);
			proc_AST = (AST)currentAST.root;
			proc_AST = (AST) astFactory.make(astFactory.create(PROC,"PROC"), proc_AST);
			currentAST.root = proc_AST;
			if ( (null != proc_AST) && (null != proc_AST.getFirstChild()) )
				currentAST.child = proc_AST.getFirstChild();
			else
				currentAST.child = proc_AST;
			currentAST.advanceChildToEnd();
			proc_AST = currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_2_);
		}
		returnAST = proc_AST;
	}
	
	public void sseq() //throws RecognitionException, TokenStreamException
{
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		AST sseq_AST = null;
		
		try {      // for error handling
			{ // ( ... )+
				int _cnt18=0;
				for (;;)
				{
					if ((tokenSet_3_.member(LA(1))))
					{
						lstmt();
						astFactory.addASTChild(ref currentAST, returnAST);
					}
					else
					{
						if (_cnt18 >= 1) { goto _loop18_breakloop; } else { throw new NoViableAltException(LT(1), getFilename());; }
					}
					
					_cnt18++;
				}
_loop18_breakloop:				;
			}    // ( ... )+
			sseq_AST = (AST)currentAST.root;
			sseq_AST = (AST) astFactory.make(astFactory.create(SSEQ,"SSEQ"), sseq_AST);
			currentAST.root = sseq_AST;
			if ( (null != sseq_AST) && (null != sseq_AST.getFirstChild()) )
				currentAST.child = sseq_AST.getFirstChild();
			else
				currentAST.child = sseq_AST;
			currentAST.advanceChildToEnd();
			sseq_AST = currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_4_);
		}
		returnAST = sseq_AST;
	}
	
	public void lstmt() //throws RecognitionException, TokenStreamException
{
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		AST lstmt_AST = null;
		
		try {      // for error handling
			if ((tokenSet_3_.member(LA(1))) && (tokenSet_5_.member(LA(2))))
			{
				stmt();
				astFactory.addASTChild(ref currentAST, returnAST);
				lstmt_AST = (AST)currentAST.root;
				lstmt_AST = (AST) astFactory.make(astFactory.create(STMT,"STMT"), lstmt_AST);
				currentAST.root = lstmt_AST;
				if ( (null != lstmt_AST) && (null != lstmt_AST.getFirstChild()) )
					currentAST.child = lstmt_AST.getFirstChild();
				else
					currentAST.child = lstmt_AST;
				currentAST.advanceChildToEnd();
				lstmt_AST = currentAST.root;
			}
			else if ((LA(1)==ID) && (LA(2)==COLON)) {
				AST tmp14_AST = null;
				tmp14_AST = astFactory.create(LT(1));
				astFactory.addASTChild(ref currentAST, tmp14_AST);
				match(ID);
				match(COLON);
				stmt();
				astFactory.addASTChild(ref currentAST, returnAST);
				lstmt_AST = (AST)currentAST.root;
				lstmt_AST = (AST) astFactory.make(astFactory.create(LSTMT,"LSTMT"), lstmt_AST);
				currentAST.root = lstmt_AST;
				if ( (null != lstmt_AST) && (null != lstmt_AST.getFirstChild()) )
					currentAST.child = lstmt_AST.getFirstChild();
				else
					currentAST.child = lstmt_AST;
				currentAST.advanceChildToEnd();
				lstmt_AST = currentAST.root;
			}
			else
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_6_);
		}
		returnAST = lstmt_AST;
	}
	
	public void stmt() //throws RecognitionException, TokenStreamException
{
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		AST stmt_AST = null;
		
		try {      // for error handling
			switch ( LA(1) )
			{
			case LITERAL_skip:
			{
				AST tmp16_AST = null;
				tmp16_AST = astFactory.create(LT(1));
				astFactory.addASTChild(ref currentAST, tmp16_AST);
				match(LITERAL_skip);
				match(SEMI);
				stmt_AST = currentAST.root;
				break;
			}
			case LITERAL_print:
			{
				AST tmp18_AST = null;
				tmp18_AST = astFactory.create(LT(1));
				astFactory.makeASTRoot(ref currentAST, tmp18_AST);
				match(LITERAL_print);
				match(LPAREN);
				{
					expr();
					astFactory.addASTChild(ref currentAST, returnAST);
					{    // ( ... )*
						for (;;)
						{
							if ((LA(1)==COMMA))
							{
								match(COMMA);
								expr();
								astFactory.addASTChild(ref currentAST, returnAST);
							}
							else
							{
								goto _loop23_breakloop;
							}
							
						}
_loop23_breakloop:						;
					}    // ( ... )*
				}
				match(RPAREN);
				match(SEMI);
				stmt_AST = currentAST.root;
				break;
			}
			case LITERAL_goto:
			{
				AST tmp23_AST = null;
				tmp23_AST = astFactory.create(LT(1));
				astFactory.makeASTRoot(ref currentAST, tmp23_AST);
				match(LITERAL_goto);
				AST tmp24_AST = null;
				tmp24_AST = astFactory.create(LT(1));
				astFactory.addASTChild(ref currentAST, tmp24_AST);
				match(ID);
				match(SEMI);
				stmt_AST = currentAST.root;
				break;
			}
			case LITERAL_return:
			{
				AST tmp26_AST = null;
				tmp26_AST = astFactory.create(LT(1));
				astFactory.makeASTRoot(ref currentAST, tmp26_AST);
				match(LITERAL_return);
				match(SEMI);
				stmt_AST = currentAST.root;
				break;
			}
			case LITERAL_if:
			{
				AST tmp28_AST = null;
				tmp28_AST = astFactory.create(LT(1));
				astFactory.makeASTRoot(ref currentAST, tmp28_AST);
				match(LITERAL_if);
				match(LPAREN);
				decider();
				astFactory.addASTChild(ref currentAST, returnAST);
				match(RPAREN);
				match(LITERAL_then);
				sseq();
				astFactory.addASTChild(ref currentAST, returnAST);
				match(LITERAL_else);
				sseq();
				astFactory.addASTChild(ref currentAST, returnAST);
				match(LITERAL_fi);
				stmt_AST = currentAST.root;
				break;
			}
			case LITERAL_while:
			{
				AST tmp34_AST = null;
				tmp34_AST = astFactory.create(LT(1));
				astFactory.makeASTRoot(ref currentAST, tmp34_AST);
				match(LITERAL_while);
				match(LPAREN);
				decider();
				astFactory.addASTChild(ref currentAST, returnAST);
				match(RPAREN);
				match(LITERAL_do);
				sseq();
				astFactory.addASTChild(ref currentAST, returnAST);
				match(LITERAL_od);
				stmt_AST = currentAST.root;
				break;
			}
			case LITERAL_assert:
			{
				AST tmp39_AST = null;
				tmp39_AST = astFactory.create(LT(1));
				astFactory.makeASTRoot(ref currentAST, tmp39_AST);
				match(LITERAL_assert);
				match(LPAREN);
				decider();
				astFactory.addASTChild(ref currentAST, returnAST);
				match(RPAREN);
				match(SEMI);
				stmt_AST = currentAST.root;
				break;
			}
			default:
				if ((LA(1)==ID) && (LA(2)==COMMA||LA(2)==ASSIGN))
				{
					{
						AST tmp43_AST = null;
						tmp43_AST = astFactory.create(LT(1));
						astFactory.addASTChild(ref currentAST, tmp43_AST);
						match(ID);
						{    // ( ... )*
							for (;;)
							{
								if ((LA(1)==COMMA))
								{
									match(COMMA);
									AST tmp45_AST = null;
									tmp45_AST = astFactory.create(LT(1));
									astFactory.addASTChild(ref currentAST, tmp45_AST);
									match(ID);
								}
								else
								{
									goto _loop26_breakloop;
								}
								
							}
_loop26_breakloop:							;
						}    // ( ... )*
					}
					AST tmp46_AST = null;
					tmp46_AST = astFactory.create(LT(1));
					astFactory.addASTChild(ref currentAST, tmp46_AST);
					match(ASSIGN);
					{
						expr();
						astFactory.addASTChild(ref currentAST, returnAST);
						{    // ( ... )*
							for (;;)
							{
								if ((LA(1)==COMMA))
								{
									match(COMMA);
									expr();
									astFactory.addASTChild(ref currentAST, returnAST);
								}
								else
								{
									goto _loop29_breakloop;
								}
								
							}
_loop29_breakloop:							;
						}    // ( ... )*
					}
					match(SEMI);
					stmt_AST = (AST)currentAST.root;
					stmt_AST = (AST) astFactory.make(astFactory.create(ASSIGNMENT,"ASSIGNMENT"), stmt_AST);
					currentAST.root = stmt_AST;
					if ( (null != stmt_AST) && (null != stmt_AST.getFirstChild()) )
						currentAST.child = stmt_AST.getFirstChild();
					else
						currentAST.child = stmt_AST;
					currentAST.advanceChildToEnd();
					stmt_AST = currentAST.root;
				}
				else if ((LA(1)==ID) && (LA(2)==LPAREN)) {
					AST tmp49_AST = null;
					tmp49_AST = astFactory.create(LT(1));
					astFactory.makeASTRoot(ref currentAST, tmp49_AST);
					match(ID);
					match(LPAREN);
					{
						switch ( LA(1) )
						{
						case ID:
						case LPAREN:
						case EMARK:
						case CONST:
						{
							expr();
							astFactory.addASTChild(ref currentAST, returnAST);
							{    // ( ... )*
								for (;;)
								{
									if ((LA(1)==COMMA))
									{
										match(COMMA);
										expr();
										astFactory.addASTChild(ref currentAST, returnAST);
									}
									else
									{
										goto _loop32_breakloop;
									}
									
								}
_loop32_breakloop:								;
							}    // ( ... )*
							break;
						}
						case RPAREN:
						{
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
					match(RPAREN);
					match(SEMI);
					stmt_AST = (AST)currentAST.root;
					stmt_AST = (AST) astFactory.make(astFactory.create(PROCCALL,"PROCCALL"), stmt_AST);
					currentAST.root = stmt_AST;
					if ( (null != stmt_AST) && (null != stmt_AST.getFirstChild()) )
						currentAST.child = stmt_AST.getFirstChild();
					else
						currentAST.child = stmt_AST;
					currentAST.advanceChildToEnd();
					stmt_AST = currentAST.root;
				}
			else
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			break; }
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_6_);
		}
		returnAST = stmt_AST;
	}
	
	public void expr() //throws RecognitionException, TokenStreamException
{
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		AST expr_AST = null;
		
		try {      // for error handling
			{
				switch ( LA(1) )
				{
				case EMARK:
				{
					AST tmp54_AST = null;
					tmp54_AST = astFactory.create(LT(1));
					astFactory.addASTChild(ref currentAST, tmp54_AST);
					match(EMARK);
					expr();
					astFactory.addASTChild(ref currentAST, returnAST);
					break;
				}
				case LPAREN:
				{
					match(LPAREN);
					expr();
					astFactory.addASTChild(ref currentAST, returnAST);
					match(RPAREN);
					break;
				}
				case ID:
				{
					AST tmp57_AST = null;
					tmp57_AST = astFactory.create(LT(1));
					astFactory.addASTChild(ref currentAST, tmp57_AST);
					match(ID);
					break;
				}
				case CONST:
				{
					AST tmp58_AST = null;
					tmp58_AST = astFactory.create(LT(1));
					astFactory.addASTChild(ref currentAST, tmp58_AST);
					match(CONST);
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			{
				if ((LA(1)==BINOP) && (tokenSet_7_.member(LA(2))))
				{
					AST tmp59_AST = null;
					tmp59_AST = astFactory.create(LT(1));
					astFactory.addASTChild(ref currentAST, tmp59_AST);
					match(BINOP);
					expr();
					astFactory.addASTChild(ref currentAST, returnAST);
				}
				else if ((tokenSet_8_.member(LA(1))) && (tokenSet_9_.member(LA(2)))) {
				}
				else
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				
			}
			expr_AST = (AST)currentAST.root;
			expr_AST = (AST) astFactory.make(astFactory.create(EXPR,"EXPR"), expr_AST);
			currentAST.root = expr_AST;
			if ( (null != expr_AST) && (null != expr_AST.getFirstChild()) )
				currentAST.child = expr_AST.getFirstChild();
			else
				currentAST.child = expr_AST;
			currentAST.advanceChildToEnd();
			expr_AST = currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_8_);
		}
		returnAST = expr_AST;
	}
	
	public void decider() //throws RecognitionException, TokenStreamException
{
		
		returnAST = null;
		ASTPair currentAST = new ASTPair();
		AST decider_AST = null;
		
		try {      // for error handling
			{
				switch ( LA(1) )
				{
				case QMARK:
				{
					AST tmp60_AST = null;
					tmp60_AST = astFactory.create(LT(1));
					astFactory.addASTChild(ref currentAST, tmp60_AST);
					match(QMARK);
					break;
				}
				case ID:
				case LPAREN:
				case EMARK:
				case CONST:
				{
					expr();
					astFactory.addASTChild(ref currentAST, returnAST);
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			decider_AST = (AST)currentAST.root;
			decider_AST = (AST) astFactory.make(astFactory.create(DECIDER,"DECIDER"), decider_AST);
			currentAST.root = decider_AST;
			if ( (null != decider_AST) && (null != decider_AST.getFirstChild()) )
				currentAST.child = decider_AST.getFirstChild();
			else
				currentAST.child = decider_AST;
			currentAST.advanceChildToEnd();
			decider_AST = currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_10_);
		}
		returnAST = decider_AST;
	}
	
	private void initializeFactory()
	{
		if (astFactory == null)
		{
			astFactory = new ASTFactory();
		}
		initializeASTFactory( astFactory );
	}
	static public void initializeASTFactory( ASTFactory factory )
	{
		factory.setMaxNodeType(38);
	}
	
	public static readonly string[] tokenNames_ = new string[] {
		@"""<0>""",
		@"""EOF""",
		@"""<2>""",
		@"""NULL_TREE_LOOKAHEAD""",
		@"""PROC""",
		@"""SSEQ""",
		@"""STMT""",
		@"""LSTMT""",
		@"""ASSIGNMENT""",
		@"""DECIDER""",
		@"""PROCCALL""",
		@"""EXPR""",
		@"""decl""",
		@"""ID""",
		@"""COMMA""",
		@"""SEMI""",
		@"""LPAREN""",
		@"""RPAREN""",
		@"""begin""",
		@"""end""",
		@"""COLON""",
		@"""skip""",
		@"""print""",
		@"""goto""",
		@"""return""",
		@"""ASSIGN""",
		@"""if""",
		@"""then""",
		@"""else""",
		@"""fi""",
		@"""while""",
		@"""do""",
		@"""od""",
		@"""assert""",
		@"""QMARK""",
		@"""EMARK""",
		@"""CONST""",
		@"""BINOP""",
		@"""WS"""
	};
	
	private static long[] mk_tokenSet_0_()
	{
		long[] data = { 2L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_0_ = new BitSet(mk_tokenSet_0_());
	private static long[] mk_tokenSet_1_()
	{
		long[] data = { 9762254850L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_1_ = new BitSet(mk_tokenSet_1_());
	private static long[] mk_tokenSet_2_()
	{
		long[] data = { 8194L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_2_ = new BitSet(mk_tokenSet_2_());
	private static long[] mk_tokenSet_3_()
	{
		long[] data = { 9762250752L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_3_ = new BitSet(mk_tokenSet_3_());
	private static long[] mk_tokenSet_4_()
	{
		long[] data = { 5100797952L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_4_ = new BitSet(mk_tokenSet_4_());
	private static long[] mk_tokenSet_5_()
	{
		long[] data = { 33677312L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_5_ = new BitSet(mk_tokenSet_5_());
	private static long[] mk_tokenSet_6_()
	{
		long[] data = { 14863048704L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_6_ = new BitSet(mk_tokenSet_6_());
	private static long[] mk_tokenSet_7_()
	{
		long[] data = { 103079288832L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_7_ = new BitSet(mk_tokenSet_7_());
	private static long[] mk_tokenSet_8_()
	{
		long[] data = { 137439133696L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_8_ = new BitSet(mk_tokenSet_8_());
	private static long[] mk_tokenSet_9_()
	{
		long[] data = { 257663164416L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_9_ = new BitSet(mk_tokenSet_9_());
	private static long[] mk_tokenSet_10_()
	{
		long[] data = { 131072L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_10_ = new BitSet(mk_tokenSet_10_());
	
}
