// Bdd.h

#pragma once
#pragma managed

#include "bdduser.h"

namespace BDDlib {

	public enum class BddType
	{
		NonTerminal = 0,
		Zero = 1,
		One = 2,
		Variable = 3,
		NegationVariable = 4,
		Overflow = 5,
		Constant = 6,
	};
	

	public ref class Bdd
	{
	private:
		// Pointer to unmanaged struct that represents a BDD
		bdd m_BDD;

		// Pointer to unmanaged BDD Manager struct that holds m_BDD
		bdd_manager m_Manager;

	internal:
		property bdd UnmanagedBdd
		{
			bdd get() 
			{ 
				return m_BDD; 
			}
			void set(bdd setBdd) 
			{ 
				m_BDD = setBdd; 
			}
		}

		property bdd_manager UnmangedBddManager
		{
			bdd_manager get()
			{
				return m_Manager;
			}
			void set(bdd_manager setManager)
			{
				m_Manager = setManager;
			}
		}
	
	public:
		Bdd();

		//Put in a try-catch block before doing anything with BDD or BDDManager
		void CheckUnmanedPointer();

		// Decreases the reference count by one. When the reference count of a BDD or MTBDD node
		// reaches 0, the node and any of its children that are not otherwise referenced may eventually be
		// garbage collected and reused. Intermediate results and unused BDDs and MTBDDs should be
		// freed whenever possible.
		void FreeBdd();

		void UnfreeBdd();

		BddType ReturnBddType();

		// Returns the index of the variable which labels the root of the BDD.
		// The result is undefined if BDD is one of the constants TRUE or FALSE.
		// The variable at the start of variable ordering has index 0, the next has index 1, etc.
		// Note that creating new variables may change the index of existing variables. 
		// Dynamic reordering may also change the index of variables.
		long GetBddRootVariableID();

		// Returns a unique ID number for the variable which labels the root of the BDD. 
		// The result is undefined if BDD is one of the constants TRUE or FALSE or an
		// The ID for a variable is fixed at the time the variable is created and never changes after that.
		long GetBddRootVariableIndex();

		// Returns then(else) branch of BDD
		Bdd^ GetThenBranch();
		Bdd^ GetElseBranch();

		// Returns the BDD for the variable which labels the root of given BDD
		Bdd^ GetBddRootVariable();

		virtual System::String^ ToString() override;

		int UniqueKey();

		bool EmptyBdd();

		int RefCount();
		
		Bdd^ Exists(array<Bdd^> ^bddVariables);

		Bdd^ Replace(array<Bdd^> ^bddVariablesToBdd);

		static bool operator==(Bdd ^f, Bdd ^g)
		{		
			System::Object^ f1 = f;
			System::Object^ g1 = g;
			
			if(f1!=nullptr && g1==nullptr) return false;

			else if(f1==nullptr && g1!=nullptr) return false;

			else if(f1==nullptr && g1==nullptr) return true;

			else{

				if(f->m_Manager == g->m_Manager)
				{
					if(f->m_BDD == g->m_BDD) return true;
					else return false;
				}
				else return false;
			}			
		}

		static bool operator!=(Bdd ^f, Bdd ^g)
		{
			return !(f==g);
		}
	};

}